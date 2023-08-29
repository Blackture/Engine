using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Core.Maths.Obsolete
{
    [Obsolete]
    public class Gaussian
    {
        [Obsolete]
        public class GaussianResult
        {
            public List<float> results;
            public Straight3D straight;
            public Plane plane;
        }

        public static bool Elimination(Obsolete.Matrix matrix, Obsolete.Matrix augmentation, out GaussianResult result)
        {
            // Initialize the results list
            result = new GaussianResult();
            result.results = new List<float>();
            result.plane = null;
            result.straight = null;

            if (matrix.Values.Count == augmentation.Values.Count && augmentation.Values[0].Count == 1)
            {
                // Perform partial pivoting and scaling
                for (int i = 0; i < matrix.RowCount; i++)
                {
                    // Find the maximum value in the current column
                    float maxValue = matrix[i, i];
                    int maxIndex = i;
                    for (int j = i + 1; j < matrix.RowCount; j++)
                    {
                        if (Math.Abs(matrix[j, i]) > Math.Abs(maxValue))
                        {
                            maxValue = matrix[j, i];
                            maxIndex = j;
                        }
                    }

                    // Swap rows if necessary
                    if (maxIndex != i)
                    {
                        matrix.SwapRows(i, maxIndex);
                        augmentation.SwapRows(i, maxIndex);
                    }

                    // Scale the row if necessary
                    if (Math.Abs(maxValue) > 0)
                    {
                        float scale = 1 / maxValue;
                        matrix.ScaleRow(i, scale);
                        augmentation.ScaleRow(i, scale);
                    }

                    // Elimination
                    for (int j = i + 1; j < matrix.RowCount; j++)
                    {
                        float factor = matrix[j, i] / matrix[i, i];
                        matrix.AddRow(j, i, -factor);
                        augmentation.AddRow(j, i, -factor);
                    }

                    // Check for straight or _plane
                    if (matrix[i, i] == 0 && augmentation[i, 0] == 0)
                    {
                        if (matrix.ColumnCount == 3)
                        {
                            //Extract coeficients for the _plane
                            float a = matrix[i, 0];
                            float b = matrix[i, 1];
                            float c = matrix[i, 2];
                            float d = augmentation[i, 0];
                            result.plane = new Plane(new Vector3(a,b,c), d);
                        }
                        else if (matrix.ColumnCount == 2)
                        {
                            //Extract coeficients for the straight
                            float a = matrix[i, 0];
                            float b = matrix[i, 1];
                            float c = augmentation[i, 0];

                            // Compute supporting vector
                            Vector3 supportVector = new Vector3(-c / a, 0, 1);

                            // Compute directional vector
                            Vector3 directionVector = new Vector3(a, b, 0);

                            result.straight = new Straight3D(supportVector, directionVector, Straight3D.LineSetupType._1Point1Dir);
                        }
                    }
                }

                // Back-substitution
                for (int i = matrix.RowCount - 1; i >= 0; i--)
                {
                    float sum = 0;
                    for (int j = i + 1; j < matrix.RowCount; j++)
                    {
                        sum += matrix[i, j] * result.results[j];
                    }
                    result.results.Insert(0, (augmentation[i, 0] - sum) / matrix[i, i]);
                }

                // Check if the system is consistent
                for (int i = 0; i < matrix.RowCount; i++)
                {
                    float sum = 0;
                    for (int j = 0; j < matrix.RowCount; j++)
                    {
                        sum += matrix[i, j] * result.results[j];
                    }
                    if (Math.Abs(sum - augmentation[i, 0]) > 0.0001)
                    {
                        result.results = null;
                        return false;
                    }
                }

                return true;
            }
            else return false;
        }
    }
}
