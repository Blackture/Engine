using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Core.Maths
{
    public class Gauss
    {
        /// <summary>
        /// Takes only 3x3 Matrix and 3x1 Augmentation
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static bool GaussianElimination(AugmentedMatrix matrix, out EliminationResult result)
        {
            // Initialize the results list
            result = new EliminationResult();
            result.Floats = new List<float>();
            result.Plane = null;
            result.Straight = null;

            if (matrix.Matrix.RowCount == matrix.Augmentation.RowCount && matrix.Augmentation.ColumnCount == 1)
            {
                // Perform partial pivoting and scaling
                for (int i = 0; i < matrix.Matrix.RowCount; i++)
                {
                    // Find the maximum value in the current column
                    float maxValue = matrix.Matrix[i, i];
                    int maxIndex = i;
                    for (int j = i + 1; j < matrix.Matrix.RowCount; j++)
                    {
                        if (Math.Abs(matrix.Matrix[j, i]) > Math.Abs(maxValue))
                        {
                            maxValue = matrix.Matrix[j, i];
                            maxIndex = j;
                        }
                    }

                    // Swap rows if necessary
                    if (maxIndex != i)
                    {
                        matrix.SwapRows(i, maxIndex);
                    }

                    // Scale the row if necessary
                    if (Math.Abs(maxValue) > 0)
                    {
                        float scale = 1 / maxValue;
                        matrix.RowOperation(i, -1, MatrixOperation.ScalarMultiplication, scale);
                    }

                    // Elimination
                    for (int j = i + 1; j < matrix.Matrix.RowCount; j++)
                    {
                        float factor = matrix.Matrix[j, i] / matrix.Matrix[i, i];
                        matrix.RowOperation(i, -1, MatrixOperation.ScalarMultiplication, -factor);
                        matrix.RowOperation(j, i, MatrixOperation.Addition);
                    }

                    // Check for straight or plane
                    if (matrix.Matrix[i, i] == 0 && matrix.Augmentation[i, 0] == 0)
                    {
                        if (matrix.Matrix.ColumnCount == 3)
                        {
                            //Extract coeficients for the plane
                            float a = matrix.Matrix[i, 0];
                            float b = matrix.Matrix[i, 1];
                            float c = matrix.Matrix[i, 2];
                            float d = matrix.Augmentation[i, 0];
                            result.Plane = new Plane(new Vector3(a, b, c), d);
                        }
                        else if (matrix.Matrix.ColumnCount == 2)
                        {
                            //Extract coeficients for the straight
                            float a = matrix.Matrix[i, 0];
                            float b = matrix.Matrix[i, 1];
                            float c = matrix.Augmentation[i, 0];

                            // Compute supporting vector
                            Vector3 supportVector = new Vector3(-c / a, 0, 1);

                            // Compute directional vector
                            Vector3 directionVector = new Vector3(a, b, 0);

                            result.Straight = new Straight(supportVector, directionVector, Straight.LineSetupType._1Point1Dir);
                        }
                    }
                }

                // Back-substitution
                for (int i = matrix.Matrix.RowCount - 1; i >= 0; i--)
                {
                    float sum = 0;
                    for (int j = i + 1; j < matrix.Matrix.RowCount; j++)
                    {
                        sum += matrix.Matrix[i, j] * result.Floats[j];
                    }
                    result.Floats.Insert(0, (matrix.Augmentation[i, 0] - sum) / matrix.Matrix[i, i]);
                }

                // Check if the system is consistent
                for (int i = 0; i < matrix.Matrix.RowCount; i++)
                {
                    float sum = 0;
                    for (int j = 0; j < matrix.Matrix.RowCount; j++)
                    {
                        sum += matrix.Matrix[i, j] * result.Floats[j];
                    }
                    if (Math.Abs(sum - matrix.Augmentation[i, 0]) > 0.0001)
                    {
                        result.Floats = null;
                        return false;
                    }
                }

                return true;
            }
            else return false;
        }

        [Obsolete("Not fully implemented!")]
        public static bool GetInverse<T>(T matrix, out T inverseMatrix) where T : class, IMatrix
        {
            bool success = true;
            inverseMatrix = null;
            if (matrix.ColumnCount == matrix.RowCount)
            {
                if (matrix is Matrix3x3 m3)
                {
                    //Create augmented matrix
                    AugmentedMatrix a = new AugmentedMatrix(m3, Matrix3x3.IdentityMatrix);
                }
                else if (matrix is Matrix m)
                {
                    //Create identity matrix for given size.
                    int n = m.ColumnCount;
                    if (n == 2)
                    {
                        m.GetDeterminant(out float d);
                        float fac = 1 / d;
                        Matrix reordered = new Matrix(2, 2);
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
                                if (m.GetAdjointMatrix(out Matrix adj))
                                {
                                    inverseMatrix = (1.0f / d) * adj as T;
                                }
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

        public class EliminationResult
        {
            public List<float> Floats = null;
            public Straight Straight = null;
            public Plane Plane = null;
        }
    }
}
