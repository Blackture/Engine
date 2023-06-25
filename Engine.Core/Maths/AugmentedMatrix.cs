using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Core.Maths
{
    public class AugmentedMatrix<T> where T : IMatrix
    {
        public T m;
        public T n;

        public AugmentedMatrix(T m, T n)
        {
            if (m == null || n == null) throw new ArgumentException("You cannot create an augmented matrix with a NULL-Matrix.");
            this.m = m;
            this.n = n;
        }


    }
}
