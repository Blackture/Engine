using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Engine.Core.Maths
{
    /// <summary>
    /// This code defines a class called Straight that represents a straight line in 3D space. It has properties such as OA, OB, and Dir, which represent the endpoints of the line, and the direction of the line, respectively. The class also contains methods such as GetPointAt, Intersection, Distance, and Distance with 2 output parameters, which return the point on the line at a given position, determine if the line intersects with another line, find the distance between the line and a point in 3D space, and find the distance between the line and another line in 3D space along with the closest points on both lines to the point of intersection, respectively. The class also has some static fields, such as x1_Axis, x2_Axis, and x3_Axis, which represent the x, y, and z axes in 3D space, respectively.
    /// </summary>
    public class Straight
    {
        public enum LineSetupType
        {
            _2Points,
            _1Point1Dir
        }

        public static readonly Straight x1_Axis = new Straight(Vector.Zero, new Vector(1, 0, 0), LineSetupType._1Point1Dir);
        public static readonly Straight x2_Axis = new Straight(Vector.Zero, new Vector(0, 1, 0), LineSetupType._1Point1Dir);
        public static readonly Straight x3_Axis = new Straight(Vector.Zero, new Vector(0, 0, 1), LineSetupType._1Point1Dir);

        private Vector a;
        private Vector b;
        private Vector dir;

        public Vector OA { get { return a; } set { a = value; dir = Vector.Normalize(b - a); } }
        public Vector OB { get { return b; } set { b = value; dir = Vector.Normalize(b - a); } }
        public Vector Dir { get => dir; }

        public Straight(Vector OA, Vector OB, LineSetupType setupType)
        {
            switch (setupType)
            {
                case LineSetupType._2Points:
                    a = OA;
                    b = OB;
                    dir = Vector.Normalize(b - a);
                    break;
                case LineSetupType._1Point1Dir:
                    a = OA;
                    dir = Vector.Normalize(OB);
                    b = GetPointAt(1);
                    break;
            }
        }

        public Vector GetPointAt(float f)
        {
            return a + f * dir;
        }

        public static bool Intersection(Straight a, Straight b, out Vector I)
        {

            I = null;
            float r = (b.a.X1 + a.a.X2 - b.a.X2) / (a.b.X1 * b.b.X2 - a.b.X2);
            float s = (a.a.X2 + r * a.b.X2 - b.a.X2) / (b.b.X2);
            if (a.a.X3 + r * a.b.X3 == b.a.X3 + s * a.b.X3)
            {
                I = new Vector(a.a + r * a.b);
                return true;
            }
            else return false;
        }

        public float Distance(Vector x, out Vector l)
        {
            Vector ax = x - a;
            float dp1 = ax * dir;
            float dp2 = dir * dir;
            float t = dp1 / dp2;
            l = a + t * dir;
            Vector lx = x - l;
            return Mathf.Sqrt(Mathf.Pow(lx.X1, 2) + Mathf.Pow(lx.X2, 2) + Mathf.Pow(lx.X3, 2));
        }

        public float Distance(Straight s)
        {
            float res = 0.0f;
            if (s.Dir * Dir == 1)
            {
                if ((s.Dir.Normalized - Dir.Normalized).Length != 0)
                {
                    res = (s.OA - OA).Length;
                }
            }
            else
            {
                if (!Intersection(this, s, out Vector I))
                {
                    Vector n = Vector.CrossProduct(Dir, s.Dir);
                    res = Mathf.Abs((OA - s.OA) * n / n.Length);
                }
            }
            return res;
        }

        public float Distance(Straight s, out Vector OG, out Vector OH)
        {
            float res = 0.0f;
            if (s.Dir * Dir == 1)
            {
                if ((s.Dir.Normalized - Dir.Normalized).Length != 0)
                {
                    OG = OA;
                    OH = s.OA;
                    res = (s.OA - OA).Length;
                }
                else OG = OH = OA;
            }
            else
            {
                if (!Intersection(this, s, out Vector I))
                {
                    Vector normal = Dir.Normalized.CrossProduct(s.Dir.Normalized);
                    float d1 = normal.DotProduct(OA - s.OA);
                    float d2 = normal.DotProduct(OA + Dir - s.OA);
                    Vector closestPointOnStraight1 = OA + Dir * d1 / (d1 - d2);
                    d1 = normal.DotProduct(s.OA - OA);
                    d2 = normal.DotProduct(s.OA + s.Dir - OA);
                    Vector closestPointOnStraight2 = s.OA + s.Dir * d1 / (d1 - d2);
                    res = (closestPointOnStraight1 - closestPointOnStraight2).Length;
                    OG = closestPointOnStraight1;
                    OH = closestPointOnStraight2;
                }
                else OG = OH = I;
            }
            return res;
        }

        public static float Distance(Vector a, Straight b, out Vector l)
        {
            return b.Distance(a, out l);
        }

        public static float Distance(Straight g, Straight h, out Vector OG, out Vector OH)
        {
            return g.Distance(h, out OG, out OH);
        }
    }
}
