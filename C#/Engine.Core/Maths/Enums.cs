using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Core.Maths
{
    public enum IntervalType
    {
        /// <summary>
        /// Open Inverval
        /// </summary>
        Open,
        /// <summary>
        /// Lower Boundary is open and upper closed
        /// </summary>
        LowerHalfOpen,
        /// <summary>
        /// Uppper Boundary is open and lower closed
        /// </summary>
        UpperHalfOpen,
        /// <summary>
        /// Closed Interval
        /// </summary>
        Closed
    }

    public enum Set
    {
        /// <summary>
        /// Natural Numbers
        /// </summary>
        N,
        /// <summary>
        /// Natural Number incl. 0
        /// </summary>
        N0,
        /// <summary>
        /// Integers
        /// </summary>
        Z,
        /// <summary>
        /// Rational Numbers
        /// </summary>
        Q,
        /// <summary>
        /// Real Numbers
        /// </summary>
        R,
        /// <summary>
        /// Complex Numbers
        /// </summary>
        C
    }

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
        DRS,
        RDS,
        DSR,
        RSD,
        SRD,
        SDR,
    }

    public enum RotationMeasure
    {
        RAD,
        DEG
    }

    public enum HomogeneneousType
    {
        Direction,
        Position
    }

    public enum IntegrationApproximation
    {
        TrapezoidalRule,
        SimpsonsRule,
        MidpointRile
    }
}
