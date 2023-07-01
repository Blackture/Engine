using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Core.Maths
{
    public class RotationMatrix3x3
    {
        public readonly Matrix3x3 R3x3;

        /// <summary>
        /// Creates a rotation matrix for the angle around the specific axis. Uses angles that are measured in radians.
        /// </summary>
        /// <param name="alpha"></param>
        /// <param name="beta"></param>
        /// <param name="gamma"></param>
        public RotationMatrix3x3(float phi, Axis axis)
        {
            switch (axis)
            {
                case Axis.X1:
                    R3x3 = new Matrix3x3(new List<Vector3>()
                    {
                        new Vector3(1, 0, 0),
                        new Vector3(0, Mathf.Cos(phi), -Mathf.Sin(phi)),
                        new Vector3(0, Mathf.Sin(phi), Mathf.Cos(phi))
                    });
                    break;
                case Axis.X2:
                    R3x3 = new Matrix3x3(new List<Vector3>()
                    {
                        new Vector3(Mathf.Cos(phi), 0, Mathf.Sin(phi)),
                        new Vector3(0, 1, 0),
                        new Vector3(-Mathf.Sin(phi), 0, Mathf.Cos(phi))
                    });
                    break;
                case Axis.X3:
                    R3x3 = new Matrix3x3(new List<Vector3>()
                    {
                        new Vector3(Mathf.Cos(phi), -Mathf.Sin(phi), 0),
                        new Vector3(Mathf.Sin(phi), Mathf.Cos(phi), 0),
                        new Vector3(0, 0, 1)
                    });
                    break;
            }
        }

        /// <summary>
        /// Creates a combined rotation matrix. Uses angles that are measured in radians.
        /// </summary>
        /// <param name="alpha"></param>
        /// <param name="beta"></param>
        /// <param name="gamma"></param>
        public RotationMatrix3x3(float alpha, float beta, float gamma, Convention convention = Convention.X1X2X3)
        {
            Matrix3x3 Rx1 = new RotationMatrix3x3(alpha, Axis.X1);
            Matrix3x3 Rx2 = new RotationMatrix3x3(beta, Axis.X2);
            Matrix3x3 Rx3 = new RotationMatrix3x3(gamma, Axis.X3);

            if (convention == Convention.X1X2X3)
            {
                R3x3 = Rx3 * Rx2 * Rx1;
            }
            else 
                R3x3 = Rx1 * Rx2 * Rx3;
        }

        public static implicit operator Matrix3x3(RotationMatrix3x3 r)
        {
            return r.R3x3;
        }
    }
}
