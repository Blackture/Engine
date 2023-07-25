using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Core.Maths
{
    public class ShearMatrix2x2
    {
        public readonly MatrixMxN S2x2;

        /// <summary>
        /// <paramref name="X"/> and <paramref name="Y"/> in radians for (the) tan or cot convention.
        /// </summary>
        /// <param name="shearAngle"></param>
        /// <param name="bd2d"></param>
        public ShearMatrix2x2(float X, float Y, ShearConvention convention)
        {
            float sx = 0;
            float sy = 0;
            switch (convention) {
                case ShearConvention.SymmetricTan:
                    sx = Mathf.Tan((X + Y) / 2);
                    sy = Mathf.Tan((X + Y) / 2);
                    break;
                case ShearConvention.SymmetricCot:
                    sx = Mathf.Tan((X + Y) / 2);
                    sy = Mathf.Tan((X + Y) / 2);
                    break;
                case ShearConvention.AsymmetricTan:
                    sx = Mathf.Tan(X);
                    sy = Mathf.Tan(Y);
                    break;
                case ShearConvention.AsymmetricCot:
                    sx = Mathf.Cot(X);
                    sy = Mathf.Cot(Y);
                    break;
            }

            S2x2 = new MatrixMxN(new List<Vector>()
            {
                new Vector(1, sx),
                new Vector(sy, 1)
            });
        }
         
        public static implicit operator MatrixMxN(ShearMatrix2x2 sm)
        {
            return sm.S2x2;
        }
    }
}
