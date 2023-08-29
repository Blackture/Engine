using Engine.Core.Events;
using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Core.Maths
{
    /// <summary>
    /// A vector with n dimensions
    /// </summary>
    public class Vector : IEnumerable<float>
    {
        private List<float> values = new List<float>();

        private int dimension;
        private float length;
        private float lengthSquared;
        private float[] normalized;
        private Event onValueChanged;

        public float this[int index]
        {
            get => GetValue(index);
            set => SetValue(value, index);
        }
        public float Length { get { return length; } }
        public int Dimension { get { return dimension; } }
        public float LengthSquared { get { return lengthSquared; } }
        public Vector Normalized { get { return new Vector(normalized); } }

        public Vector(List<float> values)
        {
            Instantiate(values.ToArray());
        }
        public Vector(params float[] values)
        {
            Instantiate(values);
        }

        public Vector(Vector v)
        {
            for (int i = 0; i < v.Dimension; i++)
            {
                values.Add(v[i]);
            }
        }

        private void Instantiate(float[] values)
        {
            this.values.Clear();
            this.values.AddRange(values);
            dimension = this.values.Count;
            if (dimension != 0)
            {
                length = GetLength();
                CalculateNormalization();
            }
            onValueChanged.AddListener(OnValueChanged);
        }

        public Vector Normalize()
        {
            CalculateNormalization();
            return Normalized as Vector;
        }

        private void CalculateNormalization()
        {
            float[] f = new float[values.Count];
            for (int i = 0; i < values.Count; i++)
            {
                f[i] = values[i] / Length;
            }
            normalized = f;
        }
        private void OnValueChanged()
        {
            dimension = values.Count;
            length = GetLength();
            CalculateNormalization();
        }

        public float GetLength()
        {
            float res = 0;
            foreach (float f in values)
            {
                res += Mathf.Pow(f, 2);
            }
            res = Mathf.Sqrt(res);
            lengthSquared = Mathf.Pow(res, 2);
            return res;
        }

        /// <summary>
        /// Checks if the vector contains the given index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public bool IsValidIndex(int index)
        {
            return (index >= 0 && index < values.Count);
        }
        public bool IsDimensionEqual(Vector a, Vector b) { return a.Dimension == b.Dimension; }
        public bool IsZeroVector()
        {
            return values.Sum() == 0;
        }


        public void SetValue(float value, int index)
        {
            if (IsValidIndex(index))
            {
                values[index] = value;
                onValueChanged.Invoke();
            }
        }
        public float GetValue(int index)
        {
            if (IsValidIndex(index))
            {
                return values[index];
            }
            throw new Exception($"Invalid index: {index}.");
        }
        /// <summary>
        /// Adds a dimension to the vector and adds the given value.
        /// </summary>
        /// <param name="value"></param>
        public void AddValue(float value)
        {
            values.Add(value);
            onValueChanged.Invoke();

        }

        public float DotProduct(Vector vector)
        {
            if (IsDimensionEqual(this, vector))
            {
                float res = 0;
                for (int i = 0; i < dimension; i++)
                {
                    res += values[i] * vector[i];
                }
                return res;
            }
            else throw new ArgumentException("The vectors have different dimensions.");
        }
        public Vector ScalarProduct(float scalar)
        {
            Vector res = new Vector();
            for (int i = 0; i < dimension; i++)
            {
                res.AddValue(scalar * values[i]);
            }
            return res;
        }
        public Vector CrossProduct(Vector b)
        {
            if (IsDimensionEqual(this, b))
            {
                Vector res;
                switch (Dimension)
                {
                    case 1:
                        res = new Vector(values[0] * b[0]);
                        break;
                    case 2:
                        Vector3 _a = new Vector3(values[0], values[1], 0);
                        Vector3 _b = new Vector3(b[0], b[1], 0);
                        Vector3 c = Vector3.CrossProduct(_a, _b);
                        res = new Vector(c.X3);
                        break;
                    case 3:
                        Vector3 __a = new Vector3(values[0], values[1], values[2]);
                        Vector3 __b = new Vector3(b[0], b[1], b[2]);
                        Vector3 _c = Vector3.CrossProduct(__a, __b);
                        res = new Vector(_c.X1, _c.X2, _c.X3);
                        break;
                    default: throw new ArgumentException("Cross Product's only defined for 1 to 3-dimensional vectors.");
                }
                return res;
            }
            else throw new ArgumentException("Vectors must have the same dimension.");
        }
        public Vector Addition(Vector vector)
        {
            if (IsDimensionEqual(this, vector))
            {
                Vector res = new Vector();
                for (int i = 0; i < Dimension; i++)
                {
                    res.AddValue(values[i] + vector[i]);
                }
                return res;
            }
            else throw new ArgumentException("Vectors must have the same dimension.");
        }
        public Vector Substraction(Vector vector)
        {
            if (IsDimensionEqual(this, vector))
            {
                Vector res = new Vector();
                for (int i = 0; i < Dimension; i++)
                {
                    res.AddValue(values[i] - vector[i]);
                }
                return res;
            }
            else throw new ArgumentException("Vectors must have the same dimension.");
        }
        public Vector Division(float scalar)
        {
            Vector res = new Vector();
            for (int i = 0; i < Dimension; i++)
            {
                res.AddValue(values[i] / scalar);
            }
            return res;
        }

        public static float Dot(Vector a, Vector b)
        {
            return a * b;
        }
        public static Vector Cross(Vector a, Vector b)
        {
            return a.CrossProduct(b);
        }
        public static Vector Scalar(Vector a, float f)
        {
            return a * f;
        }
        public static Vector Addition(Vector a, Vector b)
        {
            return a + b;
        }
        public static Vector Substraction(Vector a, Vector b)
        {
            return a - b;
        }
        public static Vector Division(Vector a, float f) 
        { 
            return a / f;
        }

        public static Vector operator +(Vector v1, Vector v2)
        {
            return v1.Addition(v2);
        }
        public static Vector operator -(Vector v1, Vector v2)
        {
            return v1.Substraction(v2);
        }
        public static float operator *(Vector v1, Vector v2)
        {
            return v1.DotProduct(v2);
        }
        public static Vector operator *(Vector v1, float f)
        {
            return v1.ScalarProduct(f);
        }
        public static Vector operator *(float f, Vector v1)
        {
            return v1.ScalarProduct(f);
        }
        public static Vector operator /(Vector v1, float f)
        {
            return v1.Division(f);
        }
        /// <summary>
        /// Turns the vector into single-row matrix, as long as <paramref name="isColumn"/> is false.
        /// Otherwise it turns the vector into a single-column matrix
        /// </summary>
        /// <param name="v"></param>
        public static MatrixMxN ToMatrix(Vector v, bool isColumn = false)
        {
            MatrixMxN m = null;
            switch (isColumn)
            {
                case false:
                    m = new MatrixMxN(new List<Vector>() { v });
                    break;
                case true:
                    List<Vector> rows = new List<Vector>();
                    foreach(float f in v)
                    {
                        rows.Add(new Vector(f));
                    }
                    m = new MatrixMxN(rows);
                    break;
            }
            return m;
        }
        

        public IEnumerator<float> GetEnumerator()
        {
            return new FloatEnumerator(values.ToArray());
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
