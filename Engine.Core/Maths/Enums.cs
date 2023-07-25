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

    public enum RotationConvention
    {
        X1X2X3,
        X1X3X2,
        X2X1X3,
        X2X3X1,
        X3X1X2,
        X3X2X1
    }

    public enum ShearConvention
    {
        SymmetricTan,
        SymmetricCot,
        AsymmetricTan,
        AsymmetricCot,
    }

    public enum RotationType
    {
        RotationMatrix,
        Quaternion
    }

    public enum BasePlane
    {
        X1X2,
        X2X3,
        X1X3
    }
    
    public enum BaseDirections2D
    {
        Horizontal,
        Vertical
    }

    public enum TransformationConvention
    {
        TDRS,
        TRDS,
        TDSR,
        TRSD,
        TSRD,
        TSDR,

        DTRS,
        RTDS,
        DTSR,
        RTSD,
        STRD,
        STDR,

        DRTS,
        RDTS,
        DSTR,
        RSTD,
        SRTD,
        SDTR,

        DRST,
        RDST,
        DSRT,
        RSDT,
        SRDT,
        SDRT
    }

    public enum RotationMeasure
    {
        RAD,
        DEG
    }
}
