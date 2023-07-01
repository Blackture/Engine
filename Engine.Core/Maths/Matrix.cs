using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Core.Maths
{
    public abstract class Matrix : IMatrix
    {
        public abstract float this[int r, int c] { get; set; }
        public abstract int RowCount { get; }
        public abstract int ColumnCount { get; }

        public abstract IMatrix Operation(IMatrix m, MatrixOperation operation, float f = 0);
        public abstract void RowOperation(int target, int source, MatrixOperation operation, float f = 0);
        public abstract void SwapRows(int from, int to);

        public static bool IdentityMatrix<T>(int n, out T identityMatrix) where T : Matrix, IMatrix, new()
        {
            bool success = true;
            identityMatrix = new T();
            if (identityMatrix is Matrix3x3 && n == 3)
            {
                identityMatrix = new Matrix3x3(1, 0, 0, 0, 1, 0, 0, 0, 1) as T;
            }
            else if (identityMatrix is MatrixMxN)
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
                identityMatrix = new MatrixMxN(rows) as T;
            }
            else success = false;
            return success;
        }
        public static bool GetInverse<T>(T matrix, out T inverseMatrix) where T : Matrix, IMatrix
        {
            bool success = true;
            inverseMatrix = null;
            if (matrix.ColumnCount == matrix.RowCount)
            {
                if (matrix is Matrix3x3 m3) 
                {
                    MatrixMxN _adj = m3;
                    if (_adj.GetAdjointMatrix(out MatrixMxN adjMxN))
                    {
                        Matrix3x3 adj = adjMxN;
                        if (adj != null)
                        {
                            float d = m3.GetDeterminant();
                            inverseMatrix = (1.0f / d) * adj as T;
                        }
                        else success = false;
                    }
                    else success = false;
                }
                else if (matrix is MatrixMxN m)
                {
                    int n = m.ColumnCount;
                    if (n == 2)
                    {
                        m.GetDeterminant(out float d);
                        float fac = 1 / d;
                        MatrixMxN reordered = new MatrixMxN(2, 2);
                        reordered[0, 0] = m[1, 1];
                        reordered[0, 1] = -m[0, 1];
                        reordered[1, 0] = -m[1, 0];
                        reordered[1, 1] = m[0, 0];
                        inverseMatrix = reordered.ScalarMultiplication(fac) as T;
                    }
                    else
                    {
                        if (m.GetDeterminant(out float d))
                        {
                            if (d == 0) success = false;
                            else
                            {
                                if (m.GetAdjointMatrix(out MatrixMxN adj))
                                {
                                    inverseMatrix = (1.0f / d) * adj as T;
                                }
                                else
                                    success = false;
                            }
                        }
                        else success = false;
                    }
                }
                else success = false;
            }
            else success = false;
            return success;
        }
    }
}
