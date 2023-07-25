using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Lifetime;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Core.Maths
{
    public class Vector2
    {
        public static readonly Vector2 Zero = new Vector2(0, 0);
        public static readonly Vector2 One = new Vector2(1, 1);

        private float x;
        private float y;
        private float length;
        private float lengthSquared;
        private float[] normalized;

        public float X { get => x; set { x = value; length = GetLength(); CalculateNormalization(); } }
        public float Y { get => y; set { y = value; length = GetLength(); CalculateNormalization(); } }
        public float Length { get => length; }
        public float LengthSquared { get => lengthSquared; }
        public Vector2 Normalized { get => new Vector2(normalized[0], normalized[1]); }
        public int Dimension => 3;

        public float this[int xindex]
        {
            get 
            {
                float res = 0;
                switch (xindex)
                {
                    case 0:
                        res = X;
                        break;
                    case 1:
                        res = Y;
                        break;
                }
                return res;
            }
            set
            {
                switch (xindex) 
                {
                    case 0: 
                        X = value;                        
                        break;
                    case 1:
                        Y = value;
                        break;
                    default:
                        break;
                }
            }
        }

        public Vector2(float x, float y)
        {
            X = x;
            Y = y;
        }
        public Vector2(Vector2 vector)
        {
            X = vector.X;
            Y = vector.Y;
        }

        public static Vector2 operator *(float f, Vector2 v)
        {
            return new Vector2(v.X * f, v.Y * f);
        }
        public static Vector2 operator *(Vector2 v, float f)
        {
            return new Vector2(v.X * f, v.Y * f);
        }
        public static float operator *(Vector2 v1, Vector2 v2)
        {
            return v1.X * v2.X + v1.Y * v2.Y;
        }
        public static Vector2 operator +(Vector2 v1, Vector2 v2)
        {
            return new Vector2(v1.X + v2.X, v1.Y + v2.Y);
        }
        public static Vector2 operator -(Vector2 v1, Vector2 v2)
        {
            return new Vector2(v1.X - v2.X, v1.Y - v2.Y);
        }
        public static Vector2 operator /(Vector2 v, float f)
        {
            return v * (1 / f);
        }
        public static implicit operator Vector(Vector2 v)
        {
            return new Vector(v.X, v.Y);
        }

        /// <summary>
        /// Turns the vector into single-row matrix, as long as <paramref name="isColumn"/> is false.
        /// Otherwise it turns the vector into a single-column matrix
        /// </summary>
        /// <param name="v"></param>
        public static MatrixMxN ToMatrix(Vector2 v, bool isColumn = false)
        {
            return v.ToMatrix(isColumn);
        }
        public static float CrossProduct(Vector2 u, Vector2 v, out Vector3 resultAsVector3)
        {
            resultAsVector3 = new Vector3(0, 0, u.X * v.Y - u.Y * v.X);
            return u.X * v.Y - u.Y * v.X;
        }
        public static float DotProduct(Vector2 u, Vector2 v)
        {
            return u * v;
        }
        public static Vector2 Normalize(Vector2 v)
        {
            v.Normalize();
            return v.Normalized as Vector2;
        }
        public static float GetLength(Vector2 v)
        {
            return v.GetLength();
        }
        public static float AngleBetween(Vector2 a, Vector2 b)
        {
            if (a == Zero && b == Zero) return 0.0f;
            return Mathf.Acos(a * b / (a.Length * b.Length)) * 180 / Mathf.pi;
        }
        public static bool OrthogonalityCheck(Vector2 a, Vector2 b)
        {
            bool res = false;
            if (a != Zero && b != Zero)
            {
                if (a * b == 0) res = true;
            }
            return res;
        }

        public Vector2 Normalize()
        {
            CalculateNormalization();
            return Normalized;
        }
        public float GetLength()
        {
            float l = 0.0f;
            lengthSquared = 0.0f;
            float x2 = (X > 0) ? X * X : 0;
            float y2 = (X > 0) ? Y * Y : 0;

            if (x2 + y2 > 0) {
                l = Mathf.Sqrt(x2 + y2);
                lengthSquared = Mathf.Pow(l, 2);
            }
            return l;
        }
        public float CrossProduct(Vector2 v, out Vector3 resultAsVector3)
        {
            return CrossProduct(this, v, out resultAsVector3);
        }
        public float DotProduct(Vector2 v)
        {
            return DotProduct(this, v);
        }
        public Vector2 ScalarProduct(float scalar)
        {
            return this * scalar;
        }
        public Vector2 Addition(Vector2 vector)
        {
            return this + vector;
        }
        public Vector2 Substraction(Vector2 v)
        {
            return this - v;
        }
        public Vector2 Division(float scalar)
        {
            return this / scalar;
        }
        public bool IsPerpendicularTo(Vector2 v)
        {
            bool res = false;
            if (v != null && this != Zero && v != Zero)
            {
                if (Mathf.Approximately(this * v, 0)) res = true;
            }
            return res;
        }
        public Vector2 GetNormalVector(bool negateYEntryInstead)
        {
            Vector2 res = null;
            if (negateYEntryInstead)
            {
                res = new Vector2(Y, -X);

            }
            else
            {
                res = new Vector2(-Y, X);
            }
            return res;
        }
        public bool IsZero()
        {
            return this == Zero;
        }
        public MatrixMxN ToMatrix(bool isColumn = false)
        {
            MatrixMxN m = null;
            switch (isColumn)
            {
                case false:
                    m = new MatrixMxN(new List<Vector>() { this });
                    break;
                case true:
                    List<Vector> rows = new List<Vector>()
                    {
                        new Vector(X),
                        new Vector(Y),
                    };
                    m = new MatrixMxN(rows);
                    break;
            }
            return m;
        }

        private void CalculateNormalization()
        {
            if (Length == 0) normalized = new float[2] { 0, 0 };
            else normalized = new float[] { X / Length, Y / Length };
        }
    }
}
