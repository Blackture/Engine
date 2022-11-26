using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Core.Maths
{
    public class Matrix
    {
        public readonly int ColumnCount;
        public readonly int RowCount;

        private List<List<float>> content = new List<List<float>>();

        public float this[int row, int column]
        {
            get
            {
                return content[row][column];
            }
            set
            {
                content[row][column] = value;
            }
        }

        public Matrix(int rows, int columns)
        {
            ColumnCount = columns;
            RowCount = rows;

            for (int i = 0; i < rows; i++) 
            {
                content.Add(new List<float>());
                {
                    content[i].Add(0);
                }
            }
        }

        public Matrix(List<List<float>> content)
        {
            if (content == null || content.Count < 1) return;
            this.content = content;
            RowCount = content.Count;
            ColumnCount = content[0].Count;
        }

        public List<float> GetRow(int index)
        {
            if (index > RowCount - 1 || index < 0) return null;
            return content[index];
        }

        public List<float> GetColumn(int index)
        {
            if (index > ColumnCount - 1 || index < 0) return null;
            List<float> result = new List<float>();
            for (int i = 0; i <= RowCount - 1; i++)
            {
                result.Add(content[i][index]);
            }
            return result;
        }

        public static Matrix operator *(Matrix a, Matrix b)
        {
            if (a.ColumnCount == b.RowCount)
            {
                Matrix result = new Matrix(a.RowCount, b.ColumnCount);
                for (int i = 0; i < a.RowCount; i++)
                {
                    for (int j = 0; j < b.ColumnCount; j++)
                    {
                        result[i, j] = 0;
                        for (int k = 0; k < a.ColumnCount; k++)
                        {
                            result[i, j] += a[i, k] * b[k, j];
                        }
                    }
                }
                return result;
            }
            else return null;
        }

        public static Matrix operator *(float a, Matrix b)
        {
            Matrix res = new Matrix(b.RowCount, b.ColumnCount);
            for (int i = 0; i < b.RowCount; i++)
            {
                for (int k = 0; k < b.ColumnCount; k++)
                {
                    res[i, k] = a * b[i, k];
                }
            }
            return res;
        }

        public static Matrix operator *(Matrix b, float a)
        {
            Matrix res = new Matrix(b.RowCount, b.ColumnCount);
            for (int i = 0; i < b.RowCount; i++)
            {
                for (int k = 0; k < b.ColumnCount; k++)
                {
                    res[i, k] = a * b[i, k];
                }
            }
            return res;
        }

        public static Matrix operator +(Matrix a, Matrix b)
        {
            if (a.RowCount == b.RowCount && a.ColumnCount == b.ColumnCount)
            {
                Matrix res = new Matrix(a.RowCount, a.ColumnCount);
                for (int i = 0; i < a.RowCount; i++)
                {
                    for (int k = 0; k < a.ColumnCount; k++)
                    {
                        res[i, k] = a[i, k] + b[i, k];
                    }
                }
                return res;
            }
            else return null;
        }
    }
}
