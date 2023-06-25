using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Engine.Core.Maths.Matrix;

namespace Engine.Core.Maths
{
    public interface IMatrix
    {
        int RowCount { get; }
        int ColumnCount { get; }

        float GetValue(int r, int c);
        void SetValue(int r, int c, float f);
        Vector GetRow(int r);
        Vector GetColumn(int c);
        void AddRow(Vector row);
        void InsertRow(Vector row, int index);
        void RemoveRow(int index);
        void SwapRows(int from, int to);

        Matrix ScalarMultiplication(float f);
        Matrix Multiplication(Matrix m);
        Matrix Addition(Matrix m);
        Matrix Substraction(Matrix m);
        /// <summary>
        /// Calculates l+r. Doesn's change any row in the matrix
        /// <paramref name="l"/> left side of the equation
        /// <paramref name="r"/> right side of the equation
        /// </summary>
        /// <param name="l"></param>
        /// <param name="r"></param>
        /// <returns>Returns a row vector of the row l+r</returns>
        Vector RowAddition(int l, int r);
        Vector RowSubstraction(int l, int r);
        Vector RowOperation(int target, int source, MatrixOperation operation, float f = 0);
        Matrix Operation(Matrix m, MatrixOperation operation, float f = 0);
    }
}
