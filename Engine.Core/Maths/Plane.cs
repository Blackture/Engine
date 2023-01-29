using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Core.Maths
{
    public class Plane
    {
        public enum PlaneSetupType
        {
            _3PositionVectors,
            _1PositionVector2SpanVectors
        }

        /// <summary>
        /// The normal vector of the plane
        /// </summary>
        Vector n;
        /// <summary>
        /// A position vector on the plane
        /// </summary>
        Vector p;
        /// <summary>
        /// Span Vector 1
        /// </summary>
        Vector s1;
        /// <summary>
        /// Span Vector 2
        /// </summary>
        Vector s2;
        /// <summary>
        /// The "b" value of the coordinate-form "n1x1+n2x2+n3a3-b"
        /// </summary>
        float b;

        public Vector N { get { return n; } set { n = value; p = GetPoint_00X(); } }
        public Vector P { get { return p; } }
        public float B { get { return b; } }

        public Plane(Vector n, float b)
        {
            this.n = n;
            this.b = b;
            p = GetPoint_00X();
        }

        public Plane(Vector p1, Vector s1, Vector s2, PlaneSetupType setupType)
        {
            switch (setupType)
            {
                case PlaneSetupType._3PositionVectors:
                    this.s1 = s1 - p1;
                    this.s2 = s2 - p1;
                    n = Vector.CrossProduct(s1, s2);
                    p = p1;
                    b = p * n;
                    break;
                case PlaneSetupType._1PositionVector2SpanVectors:
                    this.s1 = s1;
                    this.s2 = s2;
                    n = Vector.CrossProduct(s1, s2);
                    p = p1;
                    b = p * n;
                    break;
            }
        }

        public Plane(Vector p, Vector n)
        {
            this.n = n;
            this.p = p;
            b = p * n;
        }

        /// <summary>
        /// Gets a point on the plane
        /// </summary>
        /// <returns>Returns the point for X1 = 0, X2 = 0 and X3 = b/n3</returns>
        public Vector GetPoint_00X()
        {
            return new Vector(0, 0, b / n.X3);
        }

        public bool Contains(Vector v)
        {
            return Mathf.Approximately(n.X1 * v.X1 + n.X2 * v.X2 + n.X3 * v.X3, b);
        }

        public bool Contains(Straight s)
        {
            bool res = false;
            if (Contains(s.OA))
            {
                if (Mathf.Approximately(s.Dir * n, 0)) res = true;
            }
            return res;
        }

        public Vector GetPositionVectorAt(float s1, float s2)
        {
            Vector n2 = n.GetNormalVector(1);
            Vector n3 = n.GetNormalVector(2);

            return p + s1 * n2 + s2 * n3;
        }

        public bool Intersects(Plane plane, Straight line)
        {
            bool res = false;
            if (Mathf.Approximately(line.Dir * plane.n, 0))
            {
                if (plane.Contains(line.OA)) res = true;
            }
            else
            {
                float d = (plane.N * (plane.P - line.OA)) / (plane.N * line.Dir);
                if (d >= 0)
                {
                    res = true;
                }
            }
            return res;
        }


        /// <summary>
        /// Get plane ∩ plane and outputs its results
        /// </summary>
        /// <param name="plane"></param>
        /// <param name="line"></param>
        /// <param name="intersectionLine"></param>
        /// <param name="intersectionPoint"></param>
        /// <returns>True if plane ∩ plane ≠ ∅</returns>
        public static bool Intersection(Plane p1, Plane p2, out Straight s, out Plane p, out Vector v)
        {
            s = null;
            p = null;
            v = null;
            Vector direction = Vector.CrossProduct(p1.N, p2.N);
            if (direction.IsZero())
                return false;
            Gaussian.GaussianResult res = SolveSystem(p1, p2);
            if (res.straight == null && res.plane == null && res.results == null)
                return false;
            else
            {
                if (res.results != null)
                {
                    v = new Vector(res.results[0], res.results[1], res.results[2]);
                }
                if (res.straight != null)
                {
                    s = res.straight;
                }
                if (res.plane != null)
                {
                    p = res.plane;
                }
                return true;
            }
        }

        /// <summary>
        /// Get plane ∩ line and outputs its results
        /// </summary>
        /// <param name="plane"></param>
        /// <param name="line"></param>
        /// <param name="intersectionLine"></param>
        /// <param name="intersectionPoint"></param>
        /// <returns>True if plane ∩ line ≠ ∅</returns>
        public static bool Intersection(Plane plane, Straight line, out Straight intersectionLine, out Vector intersectionPoint)
        {
            intersectionLine = null;
            intersectionPoint = null;
            if (Mathf.Approximately(line.Dir * plane.n, 0))
            {
                if (plane.Contains(line.OA))
                {
                    intersectionLine = line;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                float d = (plane.N * (plane.P - line.OA)) / (plane.N * line.Dir);
                if (d >= 0)
                {
                    intersectionPoint = line.OA + d * line.Dir;
                    if (plane.Contains(intersectionPoint))
                    {
                        intersectionLine = new Straight(intersectionPoint, line.Dir, Straight.LineSetupType._1Point1Dir);
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
        }

        public float Distance(Plane p)
        {
            Vector direction = N.CrossProduct(p.N);
            float determinant = direction.LengthSquared;

            if (Mathf.Approximately(determinant, 0))
            {
                // Planes are parallel
                return float.PositiveInfinity;
            }

            Vector w = P - p.P;
            float s = (w.DotProduct(N.CrossProduct(p.N))) / determinant;

            return Mathf.Abs(s);
        }

        public float Distance(Straight s)
        {
            return Distance(s, out Vector _);
        }

        public float Distance(Straight s, out Vector l)
        {
            float t = -(s.OA * N) / (N * s.Dir);
            l = s.OA + t * s.Dir;
            return Mathf.Abs((N.X1 * l.X1 + N.X2 * l.X2 + N.X3 * l.X3 + B) / N.LengthSquared);
        }

        private static Gaussian.GaussianResult SolveSystem(Plane p1, Plane p2)
        {
            // create a matrix with the coefficients of the system
            Matrix m = new Matrix(3, 3);
            m[0, 0] = p1.N.X1;
            m[1, 0] = p1.N.X2;
            m[2, 0] = p1.N.X3;
            m[0, 1] = p2.N.X1;
            m[1, 1] = p2.N.X2;
            m[2, 1] = p2.N.X3;

            // create a augmentation with the constant terms of the system
            Matrix augmentation = new Matrix(3,1);
            augmentation[0, 0] = p1.B;
            augmentation[1, 0] = p2.B;
            augmentation[2, 0] = 0;

            // solve the system
            Gaussian.Elimination(m, augmentation, out Gaussian.GaussianResult res);
            return res;  
        }
    }
}
