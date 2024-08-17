using Engine.Core.Maths;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Core.Maths
{
    public class SheerMatrix3D
    {
        public MatrixMxN S4x4;
        /// <summary>
        /// 3x3 Submatrix
        /// </summary>
        public Matrix3x3 S3x3;

        /// <summary>
        /// All angles θ are in radians.
        /// </summary>
        /// <param name="θ_xy"></param>
        /// <param name="θ_xz"></param>
        /// <param name="θ_yx"></param>
        /// <param name="θ_yz"></param>
        /// <param name="θ_zx"></param>
        /// <param name="θ_zy"></param>
        /// <param name="convention"></param>
        public SheerMatrix3D(ShearConvention convention, params float[] θ)
        {
            if (θ.Length != 6) throw new ArgumentException("Incorrect number of angles. You need 6 angles.");

            float s_xy = 0;
            float s_xz = 0;
            float s_yx = 0;
            float s_yz = 0;
            float s_zx = 0;
            float s_zy = 0;

            switch (convention)
            {
                case ShearConvention.SymmetricTan:
                    s_xy = Mathf.Tan((θ[0] + θ[2]) / 2);
                    s_xz = Mathf.Tan((θ[1] + θ[4]) / 2);
                    s_yx = Mathf.Tan((θ[2] + θ[0]) / 2);
                    s_yz = Mathf.Tan((θ[3] + θ[5]) / 2);
                    s_zx = Mathf.Tan((θ[4] + θ[1]) / 2);
                    s_zy = Mathf.Tan((θ[5] + θ[3]) / 2);
                    break;
                case ShearConvention.SymmetricCot:
                    s_xy = Mathf.Cot((θ[0] + θ[2]) / 2);
                    s_xz = Mathf.Cot((θ[1] + θ[4]) / 2);
                    s_yx = Mathf.Cot((θ[2] + θ[0]) / 2);
                    s_yz = Mathf.Cot((θ[3] + θ[5]) / 2);
                    s_zx = Mathf.Cot((θ[4] + θ[1]) / 2);
                    s_zy = Mathf.Cot((θ[5] + θ[3]) / 2);
                    break;
                case ShearConvention.AsymmetricTan:
                    s_xy = Mathf.Tan(θ[0]);
                    s_xz = Mathf.Tan(θ[1]);
                    s_yx = Mathf.Tan(θ[2]);
                    s_yz = Mathf.Tan(θ[3]);
                    s_zx = Mathf.Tan(θ[4]);
                    s_zy = Mathf.Tan(θ[5]);
                    break;
                case ShearConvention.AsymmetricCot:
                    s_xy = Mathf.Cot(θ[0]);
                    s_xz = Mathf.Cot(θ[1]);
                    s_yx = Mathf.Cot(θ[2]);
                    s_yz = Mathf.Cot(θ[3]);
                    s_zx = Mathf.Cot(θ[4]);
                    s_zy = Mathf.Cot(θ[5]);
                    break;
            }

            S4x4 = new MatrixMxN(new List<Vector>()
            {
                new Vector(1, s_xy, s_xz, 0),
                new Vector(s_yx, 1, s_yz, 0),
                new Vector(s_zx, s_zy, 1, 0),
                new Vector(0, 0, 0, 1)
            });

            S3x3 = new Matrix3x3(new List<Vector3>()
            {
                new Vector3(1, s_xy, s_xz),
                new Vector3(s_yx, 1, s_yz),
                new Vector3(s_zx, s_zy, 1)
            });
        }

        public static implicit operator MatrixMxN(SheerMatrix3D sm)
        {
            return sm.S4x4;
        }
        public static implicit operator Matrix3x3(SheerMatrix3D sm) { return sm.S3x3; }

        public static Vector operator *(SheerMatrix3D s, Vector3 v)
        {
            MatrixMxN _v = v.ToMatrix(true);
            _v.AddRow(new Vector(1));
            MatrixMxN res = s * _v;
            Vector vres = res.GetColumn(0);
            return vres;
        }

        public Vector3 Multiply(SheerMatrix3D s, Vector3 v)
        {
            Vector vres = s * v;
            return new Vector3(vres[0], vres[1], vres[2]);
        }
    }
}
