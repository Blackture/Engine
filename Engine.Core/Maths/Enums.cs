using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Core.Maths
{
    public enum MatrixOperation
    {
        Addition,
        Substraction,
        Multiplication,
        ScalarMultiplication,
        ScalarDivision
    }

    public enum Axis
    {
        X1, //to the front
        X2, //to the right
        X3 //to the top
    }

    public enum Convention
    {
        X1X2X3,
        X3X2X1
    }

    public enum RotationType
    {
        RotationMatrix,
        Quaternion
    }
}
