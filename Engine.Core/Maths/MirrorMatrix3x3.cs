using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Core.Maths
{
    public class MirrorMatrix3x3
    {
        public readonly Matrix3x3 M3x3;

        public static readonly Matrix3x3 Mirror_FreeSpace;
        public static readonly Matrix3x3 Mirror_X1;
        public static readonly Matrix3x3 Mirror_X2;
        public static readonly Matrix3x3 Mirror_X3;
        public static readonly Matrix3x3 Mirror_90Degree_X1_Roof;
        public static readonly Matrix3x3 Mirror_90Degree_X2_Roof;
        public static readonly Matrix3x3 Mirror_90Degree_X3_Roof;
        public static readonly Matrix3x3 Mirror_45Degree_X1_Roof;
        public static readonly Matrix3x3 Mirror_Cube_Corner;

        static MirrorMatrix3x3()
        {
            Mirror_FreeSpace = Matrix3x3.I3x3;
            Mirror_X1 = new Matrix3x3(-1, 0, 0, 0, 1, 0, 0, 0, 1);
            Mirror_X2 = new Matrix3x3(1, 0, 0, 0, -1, 0, 0, 0, 1);
            Mirror_X3 = new Matrix3x3(1, 0, 0, 0, 1, 0, 0, 0, -1);
            Mirror_90Degree_X1_Roof = new Matrix3x3(1, 0, 0, 0, -1, 0, 0, 0, -1);
            Mirror_90Degree_X2_Roof = new Matrix3x3(-1, 0, 0, 0, 1, 0, 0, 0, -1);
            Mirror_90Degree_X3_Roof = new Matrix3x3(-1, 0, 0, 0, -1, 0, 0, 0, 1);
            Mirror_45Degree_X1_Roof = new Matrix3x3(1, 0, 0, 0, 0, -1, 0, 1, 0);
            Mirror_Cube_Corner = -Matrix3x3.I3x3;
        }

        public MirrorMatrix3x3(Vector3 normal)
        {
            MatrixMxN vRow = normal.ToMatrix();
            MatrixMxN vCol = normal.ToMatrix(true);
            M3x3 = Matrix3x3.I3x3 - 2 * (Matrix3x3)(vCol * vRow);
        }

        public MatrixMxN ToMatrix4x4()
        {
            MatrixMxN matrix = (MatrixMxN)Matrix.I4x4;

            // Copy the values from the submatrix to the upper left portion of the matrix
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    matrix[row, col] = M3x3[row, col];
                }
            }

            return matrix;
        }
    }
}
