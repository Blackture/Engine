using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Core.Maths
{
    public class AugmentedMatrix
    {
        private IMatrix matrix;
        private IMatrix augmentation;

        public IMatrix Matrix
        {
            get => matrix;
            set => matrix = value != null ? value : throw new ArgumentNullException();
        }
        public IMatrix Augmentation
        {
            get => augmentation;
            set => augmentation = value != null ? value : throw new ArgumentNullException();
        }

        public AugmentedMatrix(IMatrix matrix, IMatrix augmentation)
        {
            if (matrix == null || augmentation == null) throw new ArgumentException("You cannot create an augmented matrix with a NULL-Matrix.");
            this.matrix = matrix;
            this.augmentation = augmentation;
        }

        /// <summary>
        /// result = target operation source (eg. target row + source row = new target row)
        /// If the operation is a scalar operation then use float f and input something random into source as it won't be used.
        /// </summary>
        /// <param name="target"></param>
        /// <param name="source"></param>
        /// <param name="operation"></param>
        /// <param name="f"></param>
        public void RowOperation(int target, int source, MatrixOperation operation, float f = 0)
        {
            matrix.RowOperation(target, source, operation, f);
            augmentation.RowOperation(target, source, operation, f);
        }
        public void SwapRows(int from, int to)
        {
            matrix.SwapRows(from, to);
            augmentation.SwapRows(from, to);
        }
    }
}
