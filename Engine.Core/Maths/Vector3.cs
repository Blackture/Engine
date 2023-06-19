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
    public class Vector3
    {
        public static readonly Vector3 Zero = new Vector3(0, 0, 0);
        public static readonly Vector3 One = new Vector3(1, 1, 1);

        private float x1;
        private float x2;
        private float x3;
        private float length;
        private float lengthSquared;
        private float[] normalized;

        public float X1 { get => x1; set { x1 = value; length = GetLength(); CalculateNormalization(); } }
        public float X2 { get => x2; set { x2 = value; length = GetLength(); CalculateNormalization(); } }
        public float X3 { get => x3; set { x3 = value; length = GetLength(); CalculateNormalization(); } }
        public float Length { get => length; }
        public float LengthSquared { get => lengthSquared; }
        public Vector3 Normalized { get => new Vector3(normalized[0], normalized[1], normalized[2]); }

        public float this[int xindex]
        {
            get 
            {
                float res = 0;
                switch (xindex)
                {
                    case 0:
                        res = X1;
                        break;
                    case 1:
                        res = X2;
                        break;
                    case 2:
                        res = X3;
                        break;
                }
                return res;
            }
            set
            {
                switch (xindex) 
                {
                    case 0: 
                        X1 = value;                        
                        break;
                    case 1:
                        X2 = value;
                        break;
                    case 2: 
                        X3 = value;
                        break;
                    default:
                        break;
                }
            }
        }

        public Vector3(float x1, float x2, float x3)
        {
            X1 = x1;
            X2 = x2;
            X3 = x3;
        }
        public Vector3(Vector3 vector)
        {
            X1 = vector.X1;
            X2 = vector.X2;
            X3 = vector.X3;
        }

        public static Vector3 operator *(float f, Vector3 v)
        {
            return new Vector3(v.X1 * f, v.X2 * f, v.X3 * f);
        }

        public static Vector3 operator *(Vector3 v, float f)
        { 
            return new Vector3(v.X1 * f, v.X2 * f, v.X3 * f);
        }

        public static float operator *(Vector3 v1, Vector3 v2)
        {
            return v1.x1 * v2.x1 + v1.x2 * v2.x2 + v1.x3 * v2.x3;
        }

        public static Vector3 operator +(Vector3 v1, Vector3 v2)
        {
            return new Vector3(v1.X1 + v2.X1, v1.X2 + v2.X2, v1.X3 + v2.X3);
        }

        public static Vector3 operator -(Vector3 v1, Vector3 v2)
        {
            return new Vector3(v1.X1 - v2.X1, v1.X2 - v2.X2, v1.X3 - v2.X3);
        }

        public static Vector3 operator /(Vector3 v, float f)
        {
            return v * (1 / f);
        }

        public static Vector3 CrossProduct(Vector3 u, Vector3 v)
        {
            return new Vector3(u.X2 * v.X3 - u.X3 * v.X2, u.X3 * v.X1 - u.X1 * v.X3, u.X1 * v.X2 - u.X2 * v.X1);
        }

        public static float DotProduct(Vector3 u, Vector3 v)
        {
            return u * v;
        }

        public static Vector3 Normalize(Vector3 v)
        {
            v.Normalize();
            return v.Normalized;
        }

        public static float GetLength(Vector3 v)
        {
            return v.GetLength();
        }

        public static float AngleBetween(Vector3 a, Vector3 b)
        {
            if (a == Zero && b == Zero) return 0.0f;
            return Mathf.Acos(a * b / (a.Length * b.Length)) * 180 / Mathf.pi;
        }

        public static bool OrthogonalityCheck(Vector3 a, Vector3 b)
        {
            bool res = false;
            if (a != Zero && b != Zero)
            {
                if (a * b == 0) res = true;
            }
            return res;
        }

        public Vector3 Normalize()
        {
            CalculateNormalization();
            return Normalized;
        }

        public float GetLength()
        {
            float l = 0.0f;
            lengthSquared = 0.0f;
            float x12 = (X1 > 0) ? X1 * X1 : 0;
            float x22 = (X2 > 0) ? X2 * X2 : 0;
            float x32 = (X3 > 0) ? X3 * X3 : 0;

            if (x12 + x22 + x32 > 0) {
                l = Mathf.Sqrt(x12 + x22 + x32);
                lengthSquared = Mathf.Pow(l, 2);
            }
            return l;
        }

        public Vector3 CrossProduct(Vector3 v)
        {
            return CrossProduct(this, v);
        }

        public float DotProduct(Vector3 v)
        {
            return DotProduct(this, v);
        }

        public bool IsPerpendicularTo(Vector3 v)
        {
            bool res = false;
            if (v != null && this != Zero && v != Zero)
            {
                if (Mathf.Approximately(this * v, 0)) res = true;
            }
            return res;
        }

        public Vector3 GetNormalVector(int zeroAt)
        {
            Vector3 v;
            switch (zeroAt)
            {
                case 1:
                    v = new Vector3(0, X2, X3);
                    return Vector3.CrossProduct(this, v);
                case 2:
                    v = new Vector3(X1, 0, X3);
                    return Vector3.CrossProduct(this, v);
                case 3:
                    v = new Vector3(X1, X2, 0);
                    return Vector3.CrossProduct(this, v);
                default:
                    return new Vector3(0, 0, 0);
            }
        }

        public bool IsZero()
        {
            return this == Zero;
        }

        private void CalculateNormalization()
        {
            if (Length == 0) normalized = new float[3] { 0, 0, 0 };
            else normalized = new float[] { X1 / Length, X2 / Length, X3 / Length };
        }
    }
}
