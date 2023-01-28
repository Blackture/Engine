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

        public float X1 { get => x1; set { x1 = value; length = GetLength(this); } }
        public float X2 { get => x2; set { x2 = value; length = GetLength(this); } }
        public float X3 { get => x3; set { x3 = value; length = GetLength(this); } }
        public float Length { get => length; }

        public float this[int xindex]
        {
            get { return (xindex == 0) ? X1 : (xindex == 1) ? X2 : (xindex == 2) ? X3 : 0; }
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
            return new Vector(v.X1 * f, v.X2 * f, v.X3 * f);
        }

        public static Vector operator *(Vector v, float f)
        { 
            return new Vector(v.X1 * f, v.X2 * f, v.X3 * f);
        }

        public static float operator *(Vector v1, Vector v2)
        {
            return v1.x1 * v2.x1 + v1.x2 * v2.x2 + v1.x3 * v2.x3;
        }

        public static Vector operator +(Vector v1, Vector v2)
        {
            return new Vector(v1.X1 + v2.X1, v1.X2 + v2.X2, v1.X3 + v2.X3);
        }

        public static Vector operator -(Vector v1, Vector v2)
        {
            return new Vector(v1.X1 - v2.X1, v1.X2 - v2.X2, v1.X3 - v2.X3);
        }

        public static Vector CrossProduct(Vector u, Vector v)
        {
            return new Vector(u.X2 * v.X3 - u.X3 * v.X2, u.X3 * v.X1 - u.X1 * v.X3, u.X1 * v.X2 - u.X2 * v.X1);
        }

        public static Vector Normalize(Vector v)
        {
            if (v.Length == 0) return new Vector(0, 0, 0);
            return v * (1.0f / v.Length);
        }

        public static float GetLength(Vector v)
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
            if (v != null && this != Zero && v != Zero)
            {
                if (Mathf.Approximately(this * v, 0)) res = true;
            }
            return res;
        }

        public Vector GetNormalVector(int zeroAt)
        {
            Vector v;
            switch (zeroAt)
            {
                case 1:
                    v = new Vector(0, X2, X3);
                    return Vector.CrossProduct(this, v);
                case 2:
                    v = new Vector(X1, 0, X3);
                    return Vector.CrossProduct(this, v);
                case 3:
                    v = new Vector(X1, X2, 0);
                    return Vector.CrossProduct(this, v);
                default:
                    return new Vector(0, 0, 0);
            }
        }

        public bool IsZero()
        {
            return this == Zero;
        }
    }
}
