using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Core.Maths
{
    public class DilationMatrix2x2
    {
        public readonly MatrixMxN D2x2;

        public DilationMatrix2x2(float x, float y)
        {
            D2x2 = new MatrixMxN(new List<Vector>()
            {
                new Vector(x, 0),
                new Vector(0, y),
            });
        }

        public static implicit operator MatrixMxN(DilationMatrix2x2 d)
        {
            return d.D2x2;
        }
    }
}
