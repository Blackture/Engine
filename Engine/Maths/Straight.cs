using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Engine.Core.Maths
{
    public class Straight
    {
        public static readonly Straight x1_Axis = new Straight(Vector.Zero, new Vector(1, 0, 0));
        public static readonly Straight x2_Axis = new Straight(Vector.Zero, new Vector(0, 1, 0));
        public static readonly Straight x3_Axis = new Straight(Vector.Zero, new Vector(0, 0, 1));

        private Vector a;
        private Vector b;
        private Vector dir;

        public Vector OA { get { return a; } set { a = value; dir = Vector.Normalize(b - a); } }
        public Vector OB { get { return b; } set { b = value; dir = Vector.Normalize(b - a); } }
        public Vector Dir { get => dir; }

        public Straight(Vector OA, Vector OB)
        {
            a = OA;
            b = OB;
            dir = Vector.Normalize(b - a);
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
    }
}
