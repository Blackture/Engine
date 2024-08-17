using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Core.Maths
{
    public interface IMatrix
    {
        int RowCount { get; }
        int ColumnCount { get; }

        float this[int r, int c] { get; set; }
        void RowOperation(int target, int source, MatrixOperation operation, float f = 0);
        void SwapRows(int from, int to);
        IMatrix Operation(IMatrix m, MatrixOperation operation, float f = 0);
    }
}
