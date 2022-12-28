using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Runtime.InteropServices;

namespace Engine.Core.Maths
{
    //does not work lol
    public class Matrix
    {
        public List<List<float>> Values { get; set; }
        public int RowCount { get { return this.Values.Count; } }
        public int ColumnCount { get { return this.Values[0].Count; } }

        public float this[int row, int column]
        {
            get
            {
                return this.Values[row][column];
            }
            set
            {
                this.Values[row][column] = value;
            }
        }

        public Matrix(int rowCount, int columnCount)
        {
            this.Values = new List<List<float>>();
            for (int i = 0; i < rowCount; i++)
            {
                this.Values.Add(new List<float>());
                for (int j = 0; j < columnCount; j++)
                {
                    this.Values[i].Add(0);
                }
            }
        }

        public Matrix(List<List<float>> values)
        {
            this.Values = values;
        }

        public static Matrix operator *(Matrix a, Matrix b)
        {
            if (a.ColumnCount == b.RowCount)
            {
                List<List<float>> resultValues = new List<List<float>>();
                for (int i = 0; i < a.RowCount; i++)
                {
                    resultValues.Add(new List<float>());
                    for (int j = 0; j < b.ColumnCount; j++)
                    {
                        resultValues[i].Add(0);
                        for (int k = 0; k < a.ColumnCount; k++)
                        {
                            resultValues[i][j] += a.Values[i][k] * b.Values[k][j];
                        }
                    }
                }
                return new Matrix(resultValues);
            }
            else return null;
        }

        public static Matrix operator *(float a, Matrix b)
        {
            List<List<float>> matrix = new List<List<float>>();
            for (int i = 0; i < b.RowCount; i++)
            {
                matrix.Add(new List<float>());
                for (int k = 0; k < b.ColumnCount; k++)
                {
                    matrix[i].Add(0);
                    matrix[i][k] = a * b[i, k];
                }
            }
            return new Matrix(matrix);
        }

        public static Matrix operator *(Matrix b, float a)
        {
            List<List<float>> matrix = new List<List<float>>();
            for (int i = 0; i < b.RowCount; i++)
            {
                matrix.Add(new List<float>());
                for (int k = 0; k < b.ColumnCount; k++)
                {
                    matrix[i].Add(0);
                    matrix[i][k] = a * b[i, k];
                }
            }
            return new Matrix(matrix);
        }

        public static Matrix operator +(Matrix a, Matrix b)
        {
            if (a.RowCount == b.RowCount && a.ColumnCount == b.ColumnCount)
            {
                List<List<float>> matrix = new List<List<float>>();
                for (int i = 0; i < a.RowCount; i++)
                {
                    matrix.Add(new List<float>());
                    for (int k = 0; k < a.ColumnCount; k++)
                    {
                        matrix[i].Add(0);
                        matrix[i][k] = a[i, k] + b[i, k];
                    }
                }
                return new Matrix(matrix);
            }
            else return null;
        }

        public static Matrix operator -(Matrix a, Matrix b)
        {
            if (a.RowCount == b.RowCount && a.ColumnCount == b.ColumnCount)
            {
                List<List<float>> matrix = new List<List<float>>();
                for (int i = 0; i < a.RowCount; i++)
                {
                    matrix.Add(new List<float>());
                    for (int k = 0; k < a.ColumnCount; k++)
                    {
                        matrix[i].Add(0);
                        matrix[i][k] = a[i, k] - b[i, k];
                    }
                }
                return new Matrix(matrix);
            }
            else return null;
        }

        private static List<List<float>> MatrixAddition(List<List<float>> A, List<List<float>> B)
        {
            int rows = A.Count;
            int cols = A[0].Count;
            List<List<float>> C = new List<List<float>>();

            // Iterate through the rows and columns of the matrices
            for (int i = 0; i < rows; i++)
            {
                C.Add(new List<float>());
                for (int j = 0; j < cols; j++)
                {
                    // Add the corresponding elements of A and B, or 0 if the element does not exist
                    C[i].Add(i < A.Count && j < A[0].Count ? A[i][j] : 0 + i < B.Count && j < B[0].Count ? B[i][j] : 0);
                }
            }

            return C;
        }

        private static List<List<float>> MatrixSubtraction(List<List<float>> A, List<List<float>> B)
        {
            int rows = A.Count;
            int cols = A[0].Count;
            List<List<float>> C = new List<List<float>>();

            // Iterate through the rows and columns of the matrices
            for (int i = 0; i < rows; i++)
            {
                C.Add(new List<float>());
                for (int j = 0; j < cols; j++)
                {
                    // Add the corresponding elements of A and B, or 0 if the element does not exist
                    C[i].Add(i < A.Count && j < A[0].Count ? A[i][j] : 0 - i < B.Count && j < B[0].Count ? B[i][j] : 0);
                }
            }

            return C;
        }

        private static List<List<float>> ComputeM(List<List<float>> T1, List<List<float>> T2)
        {
            if (T1.Count != T2.Count || T1[0].Count != T2[0].Count)
            {
                return null;
            }


            int n = T1.Count;
            List<List<float>> M = new List<List<float>>();

            // Initialize M with the correct number of rows and columns:
            for (int i = 0; i < n; i++)
            {
                M.Add(new List<float>());
                for (int j = 0; j < n; j++)
                {
                    M[i].Add(0);
                }
            }

            // Compute intermediate matrix M:
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    for (int k = 0; k < n; k++)
                    {
                        M[i][j] += T1[i][k] * T2[k][j];
                    }
                }
            }

            return M;
        }

        private static bool IsPowerOfTwo(int x)
        {
            return (Math.Log(x) / Math.Log(2)) % 1 == 0;
        }

        private static bool AreMatricesValid(List<List<float>> A, List<List<float>> B)
        {
            if (A == null|| B == null) return false;

            int n = A.Count;
            if (n != B.Count || !IsPowerOfTwo(n))
            {
                return false;
            }

            // Check that the matrices are square (i.e., have the same number of rows and columns)
            for (int i = 0; i < n; i++)
            {
                if (A[i].Count != n || B[i].Count != n)
                {
                    return false;
                }
            }

            return true;
        }

        public static List<List<float>> Multiply(List<List<float>> A, List<List<float>> B)
        {
            if (!AreMatricesValid(A,B)) { return null; }

            int n = A.Count;
            List<List<float>> C = new List<List<float>>();

            for (int i = 0; i < n; i++)
            {
                C.Add(new List<float>());
                for (int j = 0; j < n; j++)
                {
                    C[i].Add(0);
                }
            }

            if (n == 1)
            {
                C[0][0] = A[0][0] * B[0][0];
            }
            else
            {
                List<List<float>> A11 = new List<List<float>>();
                List<List<float>> A12 = new List<List<float>>();
                List<List<float>> A21 = new List<List<float>>();
                List<List<float>> A22 = new List<List<float>>();
                List<List<float>> B11 = new List<List<float>>();
                List<List<float>> B12 = new List<List<float>>();
                List<List<float>> B21 = new List<List<float>>();
                List<List<float>> B22 = new List<List<float>>();
                List<List<float>> C11 = new List<List<float>>();
                List<List<float>> C12 = new List<List<float>>();
                List<List<float>> C21 = new List<List<float>>();
                List<List<float>> C22 = new List<List<float>>();
                List<List<float>> M1 = new List<List<float>>();
                List<List<float>> M2 = new List<List<float>>();
                List<List<float>> M3 = new List<List<float>>();
                List<List<float>> M4 = new List<List<float>>();
                List<List<float>> M5 = new List<List<float>>();
                List<List<float>> M6 = new List<List<float>>();
                List<List<float>> M7 = new List<List<float>>();
                List<List<float>> T1 = new List<List<float>>();
                List<List<float>> T2 = new List<List<float>>();

                // Divide A and B into sub-matrices:
                for (int i = 0; i < n / 2; i++)
                {
                    A11.Add(new List<float>());
                    A12.Add(new List<float>());
                    A21.Add(new List<float>());
                    A22.Add(new List<float>());
                    B11.Add(new List<float>());
                    B12.Add(new List<float>());
                    B21.Add(new List<float>());
                    B22.Add(new List<float>());

                    for (int j = 0; j < n / 2; j++)
                    {
                        A11[i].Add(A[i][j]);
                        A12[i].Add(A[i][j + n / 2]);
                        A21[i].Add(A[i + n / 2][j]);
                        A22[i].Add(A[i + n / 2][j + n / 2]);
                        B11[i].Add(B[i][j]);
                        B12[i].Add(B[i][j + n / 2]);
                        B21[i].Add(B[i + n / 2][j]);
                        B22[i].Add(B[i + n / 2][j + n / 2]);
                    }
                }

                // Compute intermediate matrices:
                for (int i = 0; i < n / 2; i++)
                {
                    T1.Add(new List<float>());
                    T2.Add(new List<float>());

                    for (int j = 0; j < n / 2; j++)
                    {
                        T1[i].Add(B12[i][j] - B22[i][j]);
                        T2[i].Add(A11[i][j] + A12[i][j]);
                    }

                    M1 = ComputeM(A11, T1);
                    M2 = ComputeM(T2, B22);
                    M3 = ComputeM(MatrixAddition(A11, A21), MatrixAddition(B11, B12));
                    M4 = ComputeM(MatrixAddition(A12, A22), B21);
                    M5 = ComputeM(A21, MatrixSubtraction(B12, B11));
                    M6 = ComputeM(A22, MatrixSubtraction(B21, B11));
                    M7 = ComputeM(MatrixAddition(A11, A12), B11);
                }


                // Initialize C11, C12, C21, and C22 with the correct number of rows and columns:
                for (int i = 0; i < n / 2; i++)
                {
                    C11.Add(new List<float>());
                    C12.Add(new List<float>());
                    C21.Add(new List<float>());
                    C22.Add(new List<float>());

                    for (int j = 0; j < n / 2; j++)
                    {
                        C11[i].Add(0);
                        C12[i].Add(0);
                        C21[i].Add(0);
                        C22[i].Add(0);
                    }
                }

                // Compute C11, C12, C21, and C22:
                for (int i = 0; i < n / 2; i++)
                {
                    for (int j = 0; j < n / 2; j++)
                    {
                        C11[i][j] = M1[i][j] + M4[i][j] - M5[i][j] + M7[i][j];
                        C12[i][j] = M1[i][j] + M2[i][j];
                        C21[i][j] = M3[i][j] + M4[i][j];
                        C22[i][j] = M5[i][j] + M6[i][j] + M7[i][j];
                    }
                }

                for (int i = 0; i < n / 2; i++)
                {
                    for (int j = 0; j < n / 2; j++)
                    {
                        C[i][j] = C11[i][j];
                        C[i][j + n / 2] = C12[i][j];
                        C[i + n / 2][j] = C21[i][j];
                        C[i + n / 2][j + n / 2] = C22[i][j];
                    }
                }
            }
            return C;
        }

        public static Matrix Multiply(Matrix a, Matrix b)
        {
            List<List<float>> A = a.Values;
            List<List<float>> B = b.Values;
            List<List<float>> C = Multiply(A, B);
            return new Matrix(C);
        }

        private static Matrix ConvertTo(List<List<float>> matrix)
        {
            return new Matrix(matrix);
        }

        public List<float> GetRow(int index)
        {
            if (index > RowCount - 1 || index < 0) return null;
            return this.Values[index];
        }

        public List<float> GetColumn(int index)
        {
            if (index > ColumnCount - 1 || index < 0) return null;
            List<float> result = new List<float>();
            for (int i = 0; i <= RowCount - 1; i++)
            {
                result.Add(this.Values[i][index]);
            }
            return result;
        }
    }
}
