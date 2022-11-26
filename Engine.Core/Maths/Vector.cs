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
    public class Vector
    {
        public static readonly Vector Zero = new Vector(0, 0, 0);
        public static readonly Vector One = new Vector(1, 1, 1);

        private float x1;
        private float x2;
        private float x3;
        private float length;

        public float X1 { get => x1; set { x1 = value; GetLength(this); } }
        public float X2 { get => x2; set { x2 = value; GetLength(this); } }
        public float X3 { get => x3; set { x3 = value; GetLength(this); } }
        public float Length { get => length; }

        public Vector(float x1, float x2, float x3)
        {
            this.x1 = x1;
            this.x2 = x2;
            this.x3 = x3;
            length = GetLength(this);
        }
        public Vector(Vector vector)
        {
            x1 = vector.x1;
            x2 = vector.x2;
            x3 = vector.x3;
            length = GetLength(this);
        }

        public static Vector operator *(float f, Vector v)
        {
            return new Vector(f * v.x1, f * v.x2, f * v.x3);
        }

        public static Vector operator *(Vector v, float f)
        {
            return new Vector(f * v.x1, f * v.x2, f * v.x3);
        }

        public static float operator *(Vector v1, Vector v2)
        {
            return v1.x1 * v2.x1 + v1.x2 * v2.x2 + v1.x3 * v2.x3;
        }

        public static Vector operator +(Vector v1, Vector v2)
        {
            return new Vector(v1.x1 + v2.x1, v1.x2 + v2.x2, v1.x3 + v2.x3);
        }

        public static Vector operator -(Vector v1, Vector v2)
        {
            return new Vector(v1.x1 - v2.x1, v1.x2 - v2.x2, v1.x3 - v2.x3);
        }

        public static Vector CrossProduct(Vector v1, Vector v2)
        {
            return new Vector(v1.x2 * v2.x3 - v1.x3 * v2.x2, v1.x3 * v2.x1 - v1.x1 * v2.x3, v1.x1 * v2.x2 - v1.x2 * v2.x1);
        }

        public static Vector Normalize(Vector v)
        {
            if (v.Length == 0) return null;
            return v * (1.0f / v.Length);
        }

        private static float GetLength(Vector v)
        {
            float x12 = Mathf.Pow(v.X1, 2);
            float x22 = Mathf.Pow(v.X2, 2);
            float x32 = Mathf.Pow(v.X3, 2);

            return Mathf.Sqrt(x12 + x22 + x32);
        }

        public static float AngleBetween(Vector a, Vector b)
        {
            if (a == Zero && b == Zero) return 0.0f;
            return Mathf.Acos(a * b / (a.Length * b.Length)) * 180 / Mathf.pi;
        }

        public static bool OrthogonalityCheck(Vector a, Vector b)
        {
            bool res = false;
            if (a != Zero && b != Zero)
            {
                if (a * b == 0) res = true;
            }
            return res;
        }

        public bool IsPerpendicularTo(Vector v)
        {
            bool res = false;
            if (this != Zero && v != Zero)
            {
                if (this * v == 0) res = true;
            }
            return res;
        }
    }
}
