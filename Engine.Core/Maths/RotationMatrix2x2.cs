using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Core.Maths
{
    public class RotationMatrix2x2
    {
        public static readonly MatrixMxN PiQuarter;

        public readonly MatrixMxN R2x2;

        public RotationMatrix2x2(float phi)
        {
            R2x2 = new MatrixMxN(new List<Vector>()
            {
                new Vector(Mathf.Cos(phi), -Mathf.Sin(phi)),
                new Vector(Mathf.Sin(phi), Mathf.Cos(phi)),
            });
        }

        static RotationMatrix2x2()
        {
            PiQuarter = new MatrixMxN(new List<Vector>
            {
                new Vector(Constants.PiQuarter, -Constants.PiQuarter),
                new Vector(Constants.PiQuarter, Constants.PiQuarter)
            });
        }

        public static implicit operator MatrixMxN(RotationMatrix2x2 r)
        {
            return r.R2x2;
        }
    }
}
