using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Core.Maths
{
    public class MatrixMxN : Matrix, IMatrix
    {
        private List<Vector> rows;
        private List<Vector> cols;

        private int rowCount;
        private int columnCount;

        public override int RowCount { get { return rowCount; } }
        public override int ColumnCount { get { return columnCount; } }
        public override float this[int r, int c]
        {
            get => GetValue(r, c);
            set => SetValue(r, c, value);
        }

        public MatrixMxN() { }
        public MatrixMxN(List<Vector> rows)
        {
            Instantiate(rows);
        }
        public MatrixMxN(int r, int c)
        {
            List<Vector> rows = new List<Vector>();
            for (int i = 0; i < r; i++)
            {
                Vector v = new Vector();
                for (int j = 0; j < c; j++)
                {
                    v.AddValue(0);
                }
                rows.Add(v);
            }
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
                Instantiate(rows);
            }
            else throw new ArgumentException($"Vector must have a dimension of {ColumnCount} but got {row.Dimension}.");
        }
        public void InsertRow(Vector row, int index)
        {
            if (row.Dimension == ColumnCount)
            {
                rows.Insert(index, row);
                Instantiate(rows);
            }
            else throw new ArgumentException($"Vector must have a dimension of {ColumnCount} but got {row.Dimension}.");
        }
        public void RemoveRow(int index)
        {
            rows.RemoveAt(index);
            Instantiate(rows);
        }
        public void RemoveColumn(int index)
        {
            if (ValidateIndex(index, true))
            {
                List<Vector> newRows = new List<Vector>();
                foreach (Vector v in rows)
                {
                    Vector r = new Vector();
                    for (int i = 0; i < ColumnCount; i++)
                    {
                        if (i != index) r.AddValue(v[i]);
                    }
                    newRows.Add(r);
                }
                Instantiate(newRows);
            }
        }
        public override void SwapRows(int from, int to)
        {
            Vector rowf = GetRow(from);
            RemoveRow(from);
            InsertRow(rowf, to);
        }

        public MatrixMxN ScalarMultiplication(float f)
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
            return new MatrixMxN(rows);
        }
        public MatrixMxN ScalarDivision(float f)
        {
            float reciprocal = 1 / f;
            return ScalarMultiplication(reciprocal);
        }
        public MatrixMxN Multiplication(MatrixMxN m)
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
                return new MatrixMxN(rows);
            }
            else throw new ArgumentException("Matrix m's column count must match this matrix' row count");
        }
        public MatrixMxN Addition(MatrixMxN m)
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
                return new MatrixMxN(rows);
            }
            else throw new ArgumentException("Matrices must have the same size.");
        }
        public MatrixMxN Substraction(MatrixMxN m)
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
                return new MatrixMxN(rows);
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
        public override void RowOperation(int target, int source, MatrixOperation operation, float f = 0)
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
                case MatrixOperation.ScalarDivision:
                    a = GetRow(target) * (1.0f / f);
                    break;
            }
            rows[target] = a;
        }
        /// <summary>
        /// Operates on the matrix (changes the target row) and outputs the row after the calculation as a vector.
        /// </summary>
        /// <param name="target"></param>
        /// <param name="source"></param>
        /// <param name="operation"></param>
        /// <param name="f"></param>
        /// <returns></returns>
        public MatrixMxN Operation(MatrixMxN m, MatrixOperation operation, float f = 0)
        {
            MatrixMxN result = new MatrixMxN();
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
                case MatrixOperation.ScalarDivision:
                    result = ScalarDivision(f);
                    break;
            }
            return result;
        }
        public override IMatrix Operation(IMatrix m, MatrixOperation operation, float f = 0)
        {
            return Operation(m as MatrixMxN, operation, f);
        }
        public void Transpose()
        {
            if (RowCount == ColumnCount)
            {
                for (int i = 0; i < RowCount; i++)
                {
                    for (int j = i + 1; j < ColumnCount; j++)
                    {
                        float temp = this[i, j];
                        this[i, j] = this[j, i];
                        this[j, i] = temp;
                    }
                }
            }
            else
            {
                // Handle the case where the matrix is not square
                throw new InvalidOperationException("Cannot transpose a non-square matrix.");
            }
        }
        public bool GetSubMatrix(int r, int c, out MatrixMxN subMatrix)
        {
            subMatrix = null;
            bool res = true;
            if (RowCount == ColumnCount && RowCount > 1)
            {
                if (ValidateIndexes(r, c))
                {
                    Vector[] _rows = new Vector[RowCount];
                    rows.CopyTo(_rows);
                    List<Vector> newRows = _rows.ToList();
                    subMatrix = new MatrixMxN(newRows);
                    subMatrix.RemoveRow(r);
                    subMatrix.RemoveColumn(c);
                }
                else res = false;
            }
            else res = false;
            return res;
        }
        public bool Get2x2SubMatrices(MatrixMxN m, out List<MatrixMxN> subs)
        {
            bool res = true;
            subs = null;
            try
            {
                List<MatrixMxN> matrices = new List<MatrixMxN>();
                int n = m.RowCount;
                while (n > 2)
                {
                    matrices = new List<MatrixMxN>();
                    for (int r = 0; r < RowCount; r++)
                    {
                        for (int c = 0; c < ColumnCount; c++)
                        {
                            if (m.GetSubMatrix(r, c, out MatrixMxN sub))
                            {
                                matrices.Add(sub);
                            }
                            else res = false;
                        }
                    }
                    n = matrices[0].RowCount;
                }
                subs = matrices;
            }
            catch
            {
                res = false;
            }
            return res;
        }
        public bool GetMinorMatrix(out MatrixMxN minor)
        {
            minor = new MatrixMxN(RowCount, ColumnCount);
            bool success = true;
            for (int r = 0; r < RowCount; r++)
            {
                for (int c = 0; c < ColumnCount; c++)
                {
                    float det = 0;
                    if (GetSubMatrix(r, c, out MatrixMxN src))
                    {
                        if (Get2x2SubMatrices(src, out List<MatrixMxN> subs))
                        {
                            if (subs.Count == 0)
                            {
                                if (src.GetDeterminant(out float _det))
                                {
                                    det += _det;
                                }
                                else success = false;
                            }
                            else
                            {
                                foreach (MatrixMxN m in subs)
                                {
                                    if (m.GetDeterminant(out float _det))
                                    {
                                        det += _det;
                                    }
                                    else success = false;
                                }
                            }
                        }
                        else success = false;
                    }
                    else success = false;
                    minor[r, c] = det;
                }
            }
            return success;
        }
        public bool GetCofactorMatrix(out MatrixMxN fac)
        {
            bool res = true;
            fac = new MatrixMxN(RowCount, ColumnCount);
            if (GetMinorMatrix(out MatrixMxN minor))
            {
                fac = new MatrixMxN(RowCount, ColumnCount);
                for (int i = 0; i < RowCount; i++)
                {
                    for (int j = 0; j < ColumnCount; j++)
                    {
                        fac[i, j] = minor[i, j] * Mathf.Pow(-1, (i + 1) + (j + 1));
                    }
                }
            }
            else res = false;
            return res;
        }
        public bool GetDeterminant(out float determinant)
        {
            bool success = true;
            determinant = 0;

            if (RowCount == ColumnCount)
            {
                if (RowCount == 1)
                {
                    determinant = rows[0][0];
                }
                else if (RowCount == 2)
                {
                    determinant = this[0, 0] * this[1, 1] - this[0, 1] * this[1, 0];
                }
                else if (RowCount == 3)
                {
                    float a = this[0, 0];
                    float b = this[0, 1];
                    float c = this[0, 2];
                    float p = this[1, 0];
                    float q = this[1, 1];
                    float r = this[1, 2];
                    float x = this[2, 0];
                    float y = this[2, 1];
                    float z = this[2, 2];

                    determinant = a * q * z + b * r * x + c * p * y - a * r * y - b * p * z - c * q * x;
                }
                else
                {
                    for (int i = 0; i < RowCount; i++)
                    {
                        if (GetMinorMatrix(out MatrixMxN minor))
                        {
                            determinant += Mathf.Pow(-1, i) * this[i, 0] * minor[i, 0];
                        }
                    }
                }
            }
            else success = false;
            return success;
        }
        public bool GetAdjointMatrix(out MatrixMxN adj)
        {
            adj = null;
            bool res = true;
            if (GetCofactorMatrix(out MatrixMxN fac))
            {
                fac.Transpose();
                adj = fac;
            } 
            else res = false;
            return res;
        }

        public static MatrixMxN ScalarMultiplication(MatrixMxN n, float f)
        {
            return n.ScalarMultiplication(f);
        }
        public static MatrixMxN ScalarDivision(MatrixMxN n, float f)
        {
            return n.ScalarDivision(f);
        }
        public static MatrixMxN Multiplication(MatrixMxN n, MatrixMxN m)
        {
            return n.Multiplication(m);
        }
        public static MatrixMxN Addition(MatrixMxN n, MatrixMxN m)
        {
            return n.Addition(m);
        }
        public static MatrixMxN Substraction(MatrixMxN n, MatrixMxN m)
        {
            return n.Substraction(m);
        }
        public static void RowOperation(MatrixMxN n, int target, int source, MatrixOperation operation, float f = 0)
        {
            n.RowOperation(target, source, operation, f);
        }
        public static MatrixMxN Operation(MatrixMxN n, MatrixMxN m, MatrixOperation operation, float f = 0)
        {
            return n.Operation(m, operation, f);
        }
        /// <summary>
        /// Creates a nxn identity and returns it.
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static MatrixMxN GetIdentityMatrix(int n)
        {
            List<Vector> rows = new List<Vector>();
            for (int r = 0; r < n; r++)
            {
                Vector v = new Vector();
                for (int c = 0; c < n; c++)
                {
                    v.AddValue(c == r ? 1 : 0);
                }
                rows.Add(v);
            }
            MatrixMxN identityMatrix = new MatrixMxN(rows);
            return identityMatrix;
        }
        public static bool GetInverse(MatrixMxN m, out MatrixMxN inverseMatrix)
        {
            bool success = true;
            inverseMatrix = null;
            if (Matrix.GetInverse(m, out MatrixMxN iM))
            {
                inverseMatrix = iM;
            }
            else success = false;
            return success;
        }

        public static MatrixMxN operator *(float f, MatrixMxN n)
        {
            return n.ScalarMultiplication(f);
        }
        public static MatrixMxN operator *(MatrixMxN n, float f)
        {
            return n.ScalarMultiplication(f);
        }
        public static MatrixMxN operator *(MatrixMxN n, MatrixMxN m)
        {
            return n.Multiplication(m);
        }
        public static MatrixMxN operator +(MatrixMxN n, MatrixMxN m)
        {
            return n.Addition(m);
        }
        public static MatrixMxN operator -(MatrixMxN n, MatrixMxN m)
        {
            return n.Substraction(m);
        }
        public static MatrixMxN operator /(MatrixMxN n, float f)
        {
            return n.ScalarDivision(f);
        }
        public static implicit operator Matrix3x3(MatrixMxN m)
        {
            if (m.RowCount == m.ColumnCount && m.RowCount == 3)
            {
                return new Matrix3x3(m[0, 0], m[0, 1], m[0, 2], m[1, 0], m[1, 1], m[1, 2], m[2, 0], m[2, 1], m[2, 2]);
            }
            else return null;
        }
    }
}
