using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Core.Maths
{
    public class DilationMatrix3x3
    {
        public readonly Matrix3x3 D3x3;
        public DilationMatrix3x3(float x1, float x2, float x3)
        {
            new Matrix3x3(x1, 0, 0, 0, x2, 0, 0, 0, x3);
        }

        public MatrixMxN ToMatrix4x4()
        {
            MatrixMxN matrix = (MatrixMxN)Matrix.I4x4;

            // Copy the values from the submatrix to the upper left portion of the matrix
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    matrix[row, col] = D3x3[row, col];
                }
            }

            return matrix;
        }

        public static implicit operator Matrix3x3(DilationMatrix3x3 d) 
        {
            return d.D3x3;
        }
    }
}
