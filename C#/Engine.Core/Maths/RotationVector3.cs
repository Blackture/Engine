using System;
using System.Collections.Generic;
using System.Configuration;
using System.Dynamic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Core.Maths
{
    /// <summary>
    /// Saves a globalRotation in RAD, allows input in degree or rad measure.
    /// </summary>
    public class RotationVector3 : Vector3
    {
        /// <summary>
        /// X1 in RAD
        /// </summary>
        public new float X1 
        {
            get { return base.X1; }
            set { SetRotation(value, Axis.X1, RotationMeasure.RAD); }
        }
        /// <summary>
        /// X2 in RAD
        /// </summary>
        public new float X2
        {
            get { return base.X2; }
            set { SetRotation(value, Axis.X2, RotationMeasure.RAD); }
        }
        /// <summary>
        /// X3 in RAD
        /// </summary>
        public new float X3
        {
            get { return base.X3; }
            set { SetRotation(value, Axis.X3, RotationMeasure.RAD); }
        }

        public RotationVector3(float x1, float x2, float x3, RotationMeasure measure) : base(x1, x2, x3)
        {
            SetRotation(x1, x2, x3, measure);
        }

        public Vector3 GetDegreeRotation()
        {
            return new Vector3(Mathf.Rad2Deg(X1), Mathf.Rad2Deg(X2), Mathf.Rad2Deg(X3));
        }

        public void SetRotation(float f, Axis axis, RotationMeasure measure)
        {
            float i = 0;
            switch (measure)
            {
                case RotationMeasure.RAD:
                    i = f;
                    break;
                case RotationMeasure.DEG:
                    i = Mathf.Deg2Rad(f);
                    break;
            }
            switch (axis)
            {
                case Axis.X1: 
                    SetRotation(i, X2, X3, RotationMeasure.RAD); 
                    break;
                case Axis.X2:
                    SetRotation(X1, i, X3, RotationMeasure.RAD);
                    break;
                case Axis.X3:
                    SetRotation(X1, X2, i, RotationMeasure.RAD);
                    break;
            }
        }

        public void SetRotation(float x1, float x2, float x3, RotationMeasure measure)
        {
            switch (measure)
            {
                case RotationMeasure.RAD:
                    base.X1 = x1;
                    base.X2 = x2;
                    base.X3 = x3;
                    break;
                case RotationMeasure.DEG:
                    base.X1 = Mathf.Deg2Rad(x1);
                    base.X2 = Mathf.Deg2Rad(x2);
                    base.X3 = Mathf.Deg2Rad(x3);
                    break;
            }
        }

        public static RotationVector3 operator +(RotationVector3 v1, RotationVector3 v2)
        {
            return v1 + v2;
        }
        /// <summary>
        /// Creates a <see cref="RotationMatrix3x3"/> if the given vector using the X1X2X3 convention.
        /// </summary>
        /// <param name="v"></param>
        public static implicit operator RotationMatrix3x3(RotationVector3 v)
        {
            return new RotationMatrix3x3(v.X1, v.X2, v.X3);
        }
        public static implicit operator MatrixMxN(RotationVector3 v)
        {
            return (MatrixMxN)v;
        }
    }
}
