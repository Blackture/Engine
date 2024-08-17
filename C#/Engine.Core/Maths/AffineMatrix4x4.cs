using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Core.Maths
{
    public class AffineMatrix4x4 : AffineMatrix3x3
    {
        public HomogeneousObject3D translation;
        private RotationConvention RConvention = RotationConvention.X1X2X3;
        public RotationMatrix3x3 rotationSub;
        public Quaternion rotationQ;
        public DilationMatrix3x3 dilationSub; //"globalScale"
        public SheerMatrix3D sheerSub;

        public TransformationConvention TConvention = TransformationConvention.RDS;
        public MatrixMxN ATM4x4;

        public AffineMatrix4x4()
        {
            translation = new HomogeneousObject3D(Vector3.Zero, HomogeneneousType.Position);
            rotationSub = new RotationMatrix3x3(0, 0, 0, RConvention);
        }
        public AffineMatrix4x4(TransformationConvention TConvention = TransformationConvention.RDS, RotationConvention RConvention = RotationConvention.X1X2X3)
        {
            this.TConvention = TConvention;
            this.RConvention = RConvention;
            translation = new HomogeneousObject3D(Vector3.Zero, HomogeneneousType.Position);
            rotationSub = new RotationMatrix3x3(0, 0, 0, this.RConvention);
            dilationSub = new DilationMatrix3x3(1, 1, 1);
            sheerSub = null;
        }

        private void Initialize()
        {
            ATM4x4 = new MatrixMxN(4, 4);
            Matrix3x3 m3x3 = null;

            switch (TConvention)
            {
                case TransformationConvention.RDS:
                    m3x3 = (Matrix3x3)sheerSub * dilationSub * rotationSub;
                    break;
            }

            ATM4x4 = (MatrixMxN)m3x3;
            ATM4x4.AddRow(new Vector(0, 0, 0));
            ATM4x4.AddColumn(translation);
        }
    }
}
