using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Engine.Core.Maths.Obsolete;

namespace Engine.Core.Maths
{
    public class Plane
    {
        public enum PlaneSetupType
        {
            _3PositionVectors,
            _1PositionVector2SpanVectors
        }

        Vector3 n;
        Vector3 p;
        Vector3 s1;
        Vector3 s2;
        float b;

        /// <summary>
        /// The normal vector of the plane
        /// </summary>
        public Vector3 N { get { return n; } set { n = value; p = GetPoint_00X(); } }
        /// <summary>
        /// A position vector on the plane
        /// </summary>
        public Vector3 P { get { return p; } }
        /// <summary>
        /// The "b" value of the coordinate-form "n1x1+n2x2+n3a3-b"
        /// </summary>
        public float B { get { return b; } }
        /// <summary>
        /// Span Vector3 1
        /// </summary>
        public Vector3 S1 { get { return s1; } }
        /// <summary>
        /// Span Vector3 2
        /// </summary>
        public Vector3 S2 { get { return s2; } }

        public Plane(Vector3 n, float b)
        {
            this.n = n;
            this.b = b;
            p = GetPoint_00X();
        }

        public Plane(Vector3 p1, Vector3 s1, Vector3 s2, PlaneSetupType setupType)
        {
            switch (setupType)
            {
                case PlaneSetupType._3PositionVectors:
                    this.s1 = s1 - p1;
                    this.s2 = s2 - p1;
                    n = Vector3.CrossProduct(s1, s2);
                    p = p1;
                    b = p * n;
                    break;
                case PlaneSetupType._1PositionVector2SpanVectors:
                    this.s1 = s1;
                    this.s2 = s2;
                    n = Vector3.CrossProduct(s1, s2);
                    p = p1;
                    b = p * n;
                    break;
            }
        }

        public Plane(Vector3 p, Vector3 n)
        {
            this.n = n;
            s1 = n.GetNormalVector(1);
            s2 = n.GetNormalVector(3);
            this.p = p;
            b = p * n;
        }

        /// <summary>
        /// Gets a point on the plane
        /// </summary>
        /// <returns>Returns the point for X1 = 0, X2 = 0 and X3 = b/n3</returns>
        public Vector3 GetPoint_00X()
        {
            return new Vector3(0, 0, b / n.X3);
        }

        public bool Contains(Vector3 v)
        {
            return Mathf.Approximately(n.X1 * v.X1 + n.X2 * v.X2 + n.X3 * v.X3, b);
        }

        public bool Contains(Vector3 v, out bool? above)
        {
            float res = n.X1 * v.X1 + n.X2 * v.X2 + n.X3 * v.X3;
            above = res > b;
            above = Mathf.Approximately(n.X1 * v.X1 + n.X2 * v.X2 + n.X3 * v.X3, b) ? null : above;
            return above == null;
        }

        public bool Contains(Straight3D s)
        {
            bool res = false;
            if (Contains(s.OA))
            {
                if (Mathf.Approximately(s.Dir * n, 0)) res = true;
            }
            return res;
        }

        public Vector3 GetPositionVectorAt(float s1, float s2)
        {
            Vector3 n2 = n.GetNormalVector(1);
            Vector3 n3 = n.GetNormalVector(2);

            return p + s1 * n2 + s2 * n3;
        }

        public bool Intersects(Plane plane, Straight3D line)
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
        public static bool Intersection(Plane p1, Plane p2, out Straight3D s, out Plane p, out Vector3 v)
        {
            s = null;
            p = null;
            v = null;
            Vector3 direction = Vector3.CrossProduct(p1.N, p2.N);
            if (direction.IsZero())
                return false;
            Gauss.EliminationResult res = SolveSystem(p1, p2);
            if (res.Straight == null && res.Plane == null && res.Floats == null)
                return false;
            else
            {
                if (res.Floats != null)
                {
                    v = new Vector3(res.Floats[0], res.Floats[1], res.Floats[2]);
                }
                if (res.Straight != null)
                {
                    s = res.Straight;
                }
                if (res.Plane != null)
                {
                    p = res.Plane;
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
        public static bool Intersection(Plane plane, Straight3D line, out Straight3D intersectionLine, out Vector3 intersectionPoint)
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
                        intersectionLine = new Straight3D(intersectionPoint, line.Dir, Straight3D.LineSetupType._1Point1Dir);
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
            Vector3 direction = N.CrossProduct(p.N);
            float determinant = direction.LengthSquared;

            if (Mathf.Approximately(determinant, 0))
            {
                // Planes are parallel
                return float.PositiveInfinity;
            }

            Vector3 w = P - p.P;
            float s = (w.DotProduct(N.CrossProduct(p.N))) / determinant;

            return Mathf.Abs(s);
        }

        public float Distance(Straight3D s)
        {
            return Distance(s, out Vector3 _);
        }

        public float Distance(Straight3D s, out Vector3 l)
        {
            float t = -(s.OA * N) / (N * s.Dir);
            l = s.OA + t * s.Dir;
            return Mathf.Abs((N.X1 * l.X1 + N.X2 * l.X2 + N.X3 * l.X3 + B) / N.LengthSquared);
        }

        private static Gauss.EliminationResult SolveSystem(Plane p1, Plane p2)
        {
            // create a matrix with the coefficients of the system
            Matrix3x3 m = new Matrix3x3();
            m[0, 0] = p1.N.X1;
            m[1, 0] = p1.N.X2;
            m[2, 0] = p1.N.X3;
            m[0, 1] = p2.N.X1;
            m[1, 1] = p2.N.X2;
            m[2, 1] = p2.N.X3;



            // create a augmentation with the constant terms of the system
            MatrixMxN augmentation = new MatrixMxN(3,1);
            augmentation[0, 0] = p1.B;
            augmentation[1, 0] = p2.B;
            augmentation[2, 0] = 0;

            //create augmented matrix
            AugmentedMatrix a = new AugmentedMatrix(m, augmentation);

            // solve the system
            Gauss.GaussianElimination(a, out Gauss.EliminationResult res);
            return res;
        }
    }
}
