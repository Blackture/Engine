using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Runtime.InteropServices;
using System.CodeDom;

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
            if (a.ColumnCount != b.RowCount)
                throw new ArgumentException("Number of columns of first matrix must match number of rows of second matrix");
            List<List<float>> resultValues = new List<List<float>>();
            for (int i = 0; i < a.RowCount; i++)
            {
                resultValues.Add(new List<float>());
                for (int j = 0; j < b.ColumnCount; j++)
                {
                    float sum = 0;
                    for (int k = 0; k < a.ColumnCount; k++)
                    {
                        sum += a.Values[i][k] * b.Values[k][j];
                    }
                    resultValues[i].Add(sum);
                }
            }
            return new Matrix(resultValues);
        }

        public static Matrix operator *(float a, Matrix b)
        {
            var matrix = new Matrix(b.RowCount, b.ColumnCount);
            for (int i = 0; i < b.RowCount; i++)
            {
                for (int k = 0; k < b.ColumnCount; k++)
                {
                    matrix[i, k] = a * b[i, k];
                }
            }
            return matrix;
        }

        public static Matrix operator *(Matrix b, float a)
        {
            var matrix = new Matrix(b.RowCount, b.ColumnCount);
            for (int i = 0; i < b.RowCount; i++)
            {
                for (int k = 0; k < b.ColumnCount; k++)
                {
                    matrix[i, k] = a * b[i, k];
                }
            }
            return matrix;
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
            else
            {
                throw new Exception("Cannot add matrices of different dimensions.");
            }
        }

        public static Matrix operator -(Matrix a, Matrix b)
        {
            if (a.RowCount != b.RowCount || a.ColumnCount != b.ColumnCount)
            {
                throw new ArgumentException("Both matrices must have the same dimensions for subtraction.");
            }

            var result = new Matrix(a.RowCount, a.ColumnCount);

            for (int i = 0; i < a.RowCount; i++)
            {
                for (int j = 0; j < a.ColumnCount; j++)
                {
                    result[i, j] = a[i, j] - b[i, j];
                }
            }

            return result;
        }

        public static Matrix ConvertTo(List<List<float>> matrix)
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

        public void ScaleRow(int index, float scale)
        {
            for (int i = 0; i < ColumnCount; i++)
            {
                Values[index][i] *= scale;
            }
        }

        public void SwapRows(int from, int to)
        {
            List<float> storage = Values[from];
            Values.RemoveAt(from);
            Values.Insert(to, storage);
        }

        public void AddRow(List<float> row)
        {
            Values.Add(row);
        }

        public void AddRow(int targetRow, int sourceRow, float factor)
        {
            for (int i = 0; i < ColumnCount; i++)
            {
                Values[targetRow][i] += Values[sourceRow][i] * factor;
            }
        }
    }
}
