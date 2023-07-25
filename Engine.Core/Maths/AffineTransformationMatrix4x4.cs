using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Core.Maths
{
    public class AffineTransformationMatrix4x4 : AffineTransformationMatrix
    {
        public Vector3 translationVector;
        private RotationConvention RConvention = RotationConvention.X1X2X3;
        public RotationMatrix3x3 rotationSub;
        public Quaternion rotationQ;
        public DilationMatrix3x3 dilationSub; //"globalScale"
        public ShearMatrix3D shearSub;

        public TransformationConvention TConvention = TransformationConvention.RDST;
        public MatrixMxN ATM4x4;

        public AffineTransformationMatrix4x4()
        {
            translationVector = Vector3.Zero;
            rotationSub = new RotationMatrix3x3(0, 0, 0, RConvention);
        }
        public AffineTransformationMatrix4x4(TransformationConvention TConvention, RotationConvention RConvention)
        {
            this.RConvention = RConvention;
            rotationSub = new RotationMatrix3x3(0, 0, 0, this.RConvention);


        }

        private void Initialize()
        {
            ATM4x4 = (MatrixMxN)Matrix.I4x4;
            ATM4x4[0, 3] = translationVector.X1;
            ATM4x4[1, 3] = translationVector.X2;
            ATM4x4[2, 3] = translationVector.X3;
            MatrixMxN rot = rotationSub.ToMatrix4x4();
            MatrixMxN dil = dilationSub.ToMatrix4x4();
            MatrixMxN she = shearSub.ToMatrix4x4();

            switch (TConvention)
            {
                case TransformationConvention.TRDS:
                    ATM4x4 = she * dil * rot * ATM4x4;
                    break; //áhhhh help!!! what am i doing...
            }
        }
    }
}
