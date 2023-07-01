using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Core.Maths
{
    public class Matrix3x3 : Matrix, IMatrix
    {
        public new static readonly Matrix3x3 I3x3;

        private List<Vector3> rows = new List<Vector3>()
        {
            Vector3.Zero,
            Vector3.Zero,
            Vector3.Zero
        };
        private List<Vector3> cols = new List<Vector3>();

        public override int RowCount => 3;
        public override int ColumnCount => 3;
        public override float this[int r, int c]
        {
            get => GetValue(r, c);
            set => SetValue(r, c, value);
        }
        static Matrix3x3()
        {
            I3x3 = GetIdentityMatrix();
        }
        public Matrix3x3()
        {
            Instantiate(new List<Vector3>() { Vector3.Zero, Vector3.Zero, Vector3.Zero });
        }
        public Matrix3x3(List<Vector3> rows)
        {
            if (rows.Count > 3 || rows.Count <= 0) throw new ArgumentException("Too less or too many rows.");
            Instantiate(rows);
        }
        /// <summary>
        /// left to right and row by row
        /// </summary>
        /// <param name="values"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public Matrix3x3(params float[] values)
        {
            if (values.Length == 9)
            {
                Vector3 _0 = new Vector3(values[0], values[1], values[2]);
                Vector3 _1 = new Vector3(values[3], values[4], values[5]);
                Vector3 _2 = new Vector3(values[6], values[7], values[8]);
                Instantiate(new List<Vector3> { _0, _1, _2 });
            }
            else throw new ArgumentOutOfRangeException("Too less, or too many arguments.");
        }

        private void Instantiate(List<Vector3> rows)
        {
            this.rows = rows;
            cols = GetColumns();
        }
        private List<Vector3> GetColumns()
        {
            List<Vector3> cols = new List<Vector3>();
            for (int i = 0; i < ColumnCount; i++)
            {
                Vector3 c = Vector3.Zero;
                for (int j = 0; j < RowCount; j++)
                {
                    c[j] = rows[j][i];
                }
                cols.Add(c);
            }
            return cols;
        }

        private bool ValidateIndexes(int r, int c)
        {
            return (r >= 0 && r < 3 && c >= 0 && c < 3);
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

        public float GetValue(int r, int c)
        {
            if (ValidateIndexes(r, c))
            {
                return rows[r][c];
            }
            else throw new ArgumentOutOfRangeException("That's a non-existing index");
        }
        public void SetValue(int r, int c, float f)
        {
            if (ValidateIndexes(r, c))
            {
                rows[r][c] = f;
            }
            else throw new ArgumentOutOfRangeException("r∨c is/are a non-existing index/es");
        }
        public Vector3 GetRow(int r)
        {
            if (ValidateIndex(r))
            {
                return new Vector3(rows[r]);
            }
            else throw new ArgumentOutOfRangeException($"Index {r} is out of range.");
        }
        public Vector3 GetColumn(int c)
        {
            if (ValidateIndex(c, true))
            {
                return new Vector3(cols[c]);
            }
            else throw new ArgumentOutOfRangeException($"Index {c} is out of range.");
        }
        private void InsertRow(Vector3 row, int index)
        {
            rows.Insert(index, row);
        }
        private void RemoveRow(int index)
        {
            rows.RemoveAt(index);
        }
        public override void SwapRows(int from, int to)
        {
            Vector3 rowf = GetRow(from);
            RemoveRow(from);
            InsertRow(rowf, to);
        }

        public Matrix3x3 ScalarMultiplication(float f)
        {
            float _0 = rows[0][0] * f;
            float _1 = rows[0][1] * f;
            float _2 = rows[0][2] * f;
            float _3 = rows[1][0] * f;
            float _4 = rows[1][1] * f;
            float _5 = rows[1][2] * f;
            float _6 = rows[2][0] * f;
            float _7 = rows[2][1] * f;
            float _8 = rows[2][2] * f;
            return new Matrix3x3(_0, _1, _2, _3, _4, _5, _6, _7, _8);
        }
        public Matrix3x3 ScalarDivision(float f)
        {
            float reciprocal = 1 / f;
            return ScalarMultiplication(reciprocal);
        }
        public Matrix3x3 Multiplication(Matrix3x3 m)
        {
            float _0 = rows[0] * m.GetColumn(0);
            float _1 = rows[0] * m.GetColumn(1);
            float _2 = rows[0] * m.GetColumn(2);
            float _3 = rows[1] * m.GetColumn(0);
            float _4 = rows[1] * m.GetColumn(1);
            float _5 = rows[1] * m.GetColumn(2);
            float _6 = rows[2] * m.GetColumn(0);
            float _7 = rows[2] * m.GetColumn(1);
            float _8 = rows[2] * m.GetColumn(2);
            return new Matrix3x3(_0, _1, _2, _3, _4, _5, _6, _7, _8);
        }
        public Matrix3x3 Addition(Matrix3x3 m)
        {
            float _0 = rows[0][0] + m[0, 0];
            float _1 = rows[0][1] + m[0, 1];
            float _2 = rows[0][2] + m[0, 2];
            float _3 = rows[1][0] + m[1, 0];
            float _4 = rows[1][1] + m[1, 1];
            float _5 = rows[1][2] + m[1, 2];
            float _6 = rows[2][0] + m[2, 0];
            float _7 = rows[2][1] + m[2, 1];
            float _8 = rows[2][2] + m[2, 2];
            return new Matrix3x3(_0, _1, _2, _3, _4, _5, _6, _7, _8);
        }
        public Matrix3x3 Substraction(Matrix3x3 m)
        {
            float _0 = rows[0][0] - m[0, 0];
            float _1 = rows[0][1] - m[0, 1];
            float _2 = rows[0][2] - m[0, 2];
            float _3 = rows[1][0] - m[1, 0];
            float _4 = rows[1][1] - m[1, 1];
            float _5 = rows[1][2] - m[1, 2];
            float _6 = rows[2][0] - m[2, 0];
            float _7 = rows[2][1] - m[2, 1];
            float _8 = rows[2][2] - m[2, 2];
            return new Matrix3x3(_0, _1, _2, _3, _4, _5, _6, _7, _8);
        }
        /// <summary>
        /// Calculates l+r. Doesn's change any row in the matrix
        /// <paramref name="l"/> left side of the equation
        /// <paramref name="r"/> right side of the equation
        /// </summary>
        /// <param name="l"></param>
        /// <param name="r"></param>
        /// <returns>Returns a row vector of the row l+r</returns>
        public Vector3 RowAddition(int l, int r)
        {
            Vector3 a = rows[l] + rows[r];
            return new Vector3(a);
        }
        public Vector3 RowSubstraction(int l, int r)
        {
            Vector3 a = rows[l] - rows[r];
            return new Vector3(a);
        }
        public override void RowOperation(int target, int source, MatrixOperation operation, float f = 0)
        {
            Vector3 a = Vector3.Zero;
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
        public Matrix3x3 Operation(Matrix3x3 m, MatrixOperation operation, float f = 0)
        {
            Matrix3x3 result = new Matrix3x3();
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
            return Operation(m as Matrix3x3, operation, f);
        }
        public float GetDeterminant()
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

            return a * q * z + b * r * x + c * p * y - a * r * y - b * p * z - c * q * x;
        }

        public static Matrix3x3 ScalarMultiplication(Matrix3x3 n, float f)
        {
            return n.ScalarMultiplication(f);
        }
        public static Matrix3x3 ScalarDivision(Matrix3x3 n, float f)
        {
            return n.ScalarDivision(f);
        }
        public static Matrix3x3 Multiplication(Matrix3x3 n, Matrix3x3 m)
        {
            return n.Multiplication(m);
        }
        public static Matrix3x3 Addition(Matrix3x3 n, Matrix3x3 m)
        {
            return n.Addition(m);
        }
        public static Matrix3x3 Substraction(Matrix3x3 n, Matrix3x3 m)
        {
            return n.Substraction(m);
        }
        public static void RowOperation(Matrix3x3 n, int target, int source, MatrixOperation operation, float f = 0)
        {
            n.RowOperation(target, source, operation, f);
        }
        public static Matrix3x3 Operation(Matrix3x3 n, Matrix3x3 m, MatrixOperation operation, float f = 0)
        {
            return n.Operation(m, operation, f);
        }
        public static Matrix3x3 GetIdentityMatrix()
        {
            return new Matrix3x3(1, 0, 0, 0, 1, 0, 0, 0, 1);
        }
        public static bool GetInverse(Matrix3x3 m, out Matrix3x3 inverseMatrix)
        {
            bool success = true;
            inverseMatrix = null;
            if (Matrix.GetInverse(m, out Matrix3x3 iM))
            {
                inverseMatrix = iM;
            }
            else success = false;
            return success;
        }

        public static Matrix3x3 operator *(float f, Matrix3x3 n)
        {
            return n.ScalarMultiplication(f);
        }
        public static Matrix3x3 operator *(Matrix3x3 n, float f)
        {
            return n.ScalarMultiplication(f);
        }
        public static Matrix3x3 operator *(Matrix3x3 n, Matrix3x3 m)
        {
            return n.Multiplication(m);
        }
        public static Matrix3x3 operator +(Matrix3x3 n, Matrix3x3 m)
        {
            return n.Addition(m);
        }
        public static Matrix3x3 operator -(Matrix3x3 n, Matrix3x3 m)
        {
            return n.Substraction(m);
        }
        public static Matrix3x3 operator /(Matrix3x3 n, float f)
        {
            return n.ScalarDivision(f);
        }
        public static implicit operator MatrixMxN(Matrix3x3 m)
        {
            List<Vector> rows = new List<Vector>()
            {
                new Vector(m[0,0], m[0,1], m[0,2]),
                new Vector(m[1,0], m[1,1], m[1,2]),
                new Vector(m[2,0], m[2,1], m[2,2])
            };
            return new MatrixMxN(rows);
        }
    }
}
