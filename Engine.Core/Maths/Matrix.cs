using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Core.Maths
{
    public class Matrix : IMatrix
    {
        public enum MatrixOperation
        {
            Addition,
            Substraction,
            Multiplication,
            ScalarMultiplication
        }

        private List<Vector> rows;
        private List<Vector> cols;

        private int rowCount;
        private int columnCount;

        public int RowCount { get { return rowCount; } }
        public int ColumnCount { get { return columnCount; } }
        public float this[int r, int c]
        {
            get => GetValue(r, c);
            set => SetValue(r, c, value);
        }

        public Matrix() { }
        public Matrix(List<Vector> rows)
        {
            Instantiate(rows);
        }

        private void Instantiate(List<Vector> rows)
        {
            if (ValidateDimensions(rows, out int colCount))
            {
                rowCount = rows.Count;
                columnCount = colCount;
                this.rows = rows;
                cols = GetColumns();
            }
            else throw new ArgumentException("The rows must have the same dimension.");
        }
        private List<Vector> GetColumns()
        {
            List<Vector> cols = new List<Vector>();
            for (int i = 0; i < ColumnCount; i++)
            {
                Vector c = new Vector();
                for (int j = 0; j < RowCount; j++)
                {
                    c.AddValue(rows[j][i]);
                }
                cols.Add(c);
            }
            return cols;
        }

        private bool ValidateIndexes(int r, int c)
        {
            bool res = true;
            if (r >= RowCount || r < 0) res = false;
            if (c >= ColumnCount || c < 0) res = false;
            return res;
        }
        /// <summary>
        /// Checks if the inputted index is in range.
        /// <list type="bullet">
        ///     <item>
        ///         <paramref name="index"/>: The row's index
        ///     </item>
        ///     <item>
        ///         <paramref name="column"/>: Set to true if you want to check a column index instead.
        ///     </item>
        /// </list>
        /// </summary>
        /// <param name="index">Row index</param>
        /// <param name="column"></param>
        /// <returns>Returns wehter the index is in range or not.</returns>
        private bool ValidateIndex(int index, bool column = false)
        {
            bool res = true;
            switch (column)
            {
                case true:
                    if (index >= ColumnCount || index < 0) res = false;
                    break;
                case false:
                    if (index >= RowCount || index < 0) res = false;
                    break;
            }
            return res;
        }
        /// <summary>
        /// Checks if the row vectors have the same dimension.
        /// </summary>
        /// <param name="rows"></param>
        /// <param name="dim">Output parameter of the dimension</param>
        /// <returns>Returns a bool with the value indicating if the rows have the same dimension.</returns>
        private bool ValidateDimensions(List<Vector> rows, out int dim)
        {
            bool res = true;
            dim = rows[0].Dimension;
            foreach (Vector v in rows)
            {
                if (dim != v.Dimension) res = false;
            }
            if (!res) dim = -1;
            return res;
        }

        public float GetValue(int r, int c)
        {
            if (ValidateIndexes(r, c))
            {
                return rows[r][c];
            }
            else throw new ArgumentOutOfRangeException($"The index {r} or {c} was out of range.");
        }
        public void SetValue(int r, int c, float value)
        {
            if (ValidateIndexes(r, c))
            {
                rows[r][c] = value;
            }
            else throw new ArgumentOutOfRangeException($"{r} or {c} is out of range.");
        }
        public Vector GetRow(int r)
        {
            if (ValidateIndex(r))
            {
                Vector v = new Vector();
                foreach (float f in rows[r])
                {
                    v.AddValue(f);
                }
                return v;
            }
            else throw new ArgumentOutOfRangeException($"Index {r} is out of range.");
        }
        public Vector GetColumn(int c)
        {
            if (ValidateIndex(c, true))
            {
                Vector v = new Vector();
                foreach (float f in cols[c])
                {
                    v.AddValue(f);
                }
                return v;
            }
            else throw new ArgumentOutOfRangeException($"Index {c} is out of range.");
        }
        public void AddRow(Vector row)
        {
            if (row.Dimension == ColumnCount)
            {
                rows.Add(row);
            }
            else throw new ArgumentException($"Vector must have a dimension of {ColumnCount} but got {row.Dimension}.");
        }
        public void InsertRow(Vector row, int index)
        {
            if (row.Dimension == ColumnCount)
            {
                rows.Insert(index, row);
            }
            else throw new ArgumentException($"Vector must have a dimension of {ColumnCount} but got {row.Dimension}.");
        }
        public void RemoveRow(int index)
        {
            rows.RemoveAt(index);
        }
        public void SwapRows(int from, int to)
        {
            Vector rowf = GetRow(from);
            RemoveRow(from);
            InsertRow(rowf, to);
        }
        /// <summary>
        /// Operates on the matrix (changes the target row) and outputs the row after the calculation as a vector.
        /// </summary>
        /// <param name="target"></param>
        /// <param name="source"></param>
        /// <param name="operation"></param>
        /// <param name="f"></param>
        /// <returns></returns>

        public Matrix ScalarMultiplication(float f)
        {
            List<Vector> rows = new List<Vector>();
            for (int r = 0; r < RowCount; r++)
            {
                Vector row = new Vector();
                for (int c = 0; c < ColumnCount; c++)
                {
                    row.AddValue(this[r, c] * f);
                }
                rows.Add(row);
            }
            return new Matrix(rows);
        }
        public Matrix Multiplication(Matrix m)
        {
            if (RowCount == m.ColumnCount)
            {
                List<Vector> rows = new List<Vector>();
                for (int r = 0; r < RowCount; r++)
                {
                    Vector row = new Vector();
                    Vector vr = GetRow(r);
                    for (int c = 0; c < m.ColumnCount; c++)
                    {
                        Vector vc = m.GetColumn(c);
                        row.AddValue(vr * vc);
                    }
                    rows.Add(row);
                }
                return new Matrix(rows);
            }
            else throw new ArgumentException("Matrix m's column count must match this matrix' row count");
        }
        public Matrix Addition(Matrix m)
        {
            if (RowCount == m.RowCount && ColumnCount == m.ColumnCount)
            {
                List<Vector> rows = new List<Vector>();
                for (int r = 0; r < RowCount; r++)
                {
                    Vector row = new Vector();
                    for (int c = 0; c < ColumnCount; c++)
                    {
                        row.AddValue(this[r, c] + m[r, c]);
                    }
                    rows.Add(row);
                }
                return new Matrix(rows);
            }
            else throw new ArgumentException("Matrices must have the same size.");
        }
        public Matrix Substraction(Matrix m)
        {
            if (RowCount == m.RowCount && ColumnCount == m.ColumnCount)
            {
                List<Vector> rows = new List<Vector>();
                for (int r = 0; r < RowCount; r++)
                {
                    Vector row = new Vector();
                    for (int c = 0; c < ColumnCount; c++)
                    {
                        row.AddValue(this[r, c] - m[r, c]);
                    }
                    rows.Add(row);
                }
                return new Matrix(rows);
            }
            else throw new ArgumentException("Matrices must have the same size.");
        }
        /// <summary>
        /// Calculates l+r. Doesn's change any row in the matrix
        /// <paramref name="l"/> left side of the equation
        /// <paramref name="r"/> right side of the equation
        /// </summary>
        /// <param name="l"></param>
        /// <param name="r"></param>
        /// <returns>Returns a row vector of the row l+r</returns>
        public Vector RowAddition(int l, int r)
        {
            Vector a = GetRow(l);
            Vector b = GetRow(r);
            return a + b;
        }
        public Vector RowSubstraction(int l, int r)
        {
            Vector a = GetRow(l);
            Vector b = GetRow(r);
            return a - b;
        }
        public Matrix Operation(Matrix m, MatrixOperation operation, float f = 0)
        {
            Matrix result = new Matrix();
            switch (operation)
            {
                case MatrixOperation.Addition:
                    result = Addition(m);
                    break;
                case MatrixOperation.Substraction:
                    result = Substraction(m);
                    break;
                case MatrixOperation.Multiplication:
                    result = Multiplication(m);
                    break;
                case MatrixOperation.ScalarMultiplication:
                    result = ScalarMultiplication(f);
                    break;
            }
            return result;
        }
        public Vector RowOperation(int target, int source, MatrixOperation operation, float f = 0)
        {
            Vector a = new Vector();
            switch (operation)
            {
                case MatrixOperation.Addition:
                    a = RowAddition(target, source);
                    break;
                case MatrixOperation.Substraction:
                    a = RowSubstraction(target, source);
                    break;
                case MatrixOperation.ScalarMultiplication:
                    a = GetRow(target) * f;
                    break;
            }
            rows[target] = a;
            return a;
        }

        public static Matrix ScalarMultiplication(Matrix n, float f)
        {
            return n.ScalarMultiplication(f);
        }
        public static Matrix Multiplication(Matrix n, Matrix m)
        {
            return n.Multiplication(m);
        }
        public static Matrix Addition(Matrix n, Matrix m)
        {
            return n.Addition(m);
        }
        public static Matrix Substraction(Matrix n, Matrix m)
        {
            return n.Substraction(m);
        }
        public static Matrix Operation(Matrix n, Matrix m, MatrixOperation operation, float f = 0)
        {
            return n.Operation(m, operation, f);
        }
        public static Vector RowOperation(Matrix n, int target, int source, MatrixOperation operation, float f = 0)
        {
            return n.RowOperation(target, source, operation, f);
        }

        public static Matrix operator *(Matrix n, float f)
        {
            return n.ScalarMultiplication(f);
        }
        public static Matrix operator *(Matrix n, Matrix m)
        {
            return n.Multiplication(m);
        }
        public static Matrix operator +(Matrix n, Matrix m)
        {
            return n.Addition(m);
        }
        public static Matrix operator -(Matrix n, Matrix m)
        {
            return n.Substraction(m);
        }
    }
}
