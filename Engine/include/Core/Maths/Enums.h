#ifndef ENUMS_H
#define ENUMS_H

namespace Engine::Core::Maths
{
    enum class IntervalType
    {
        Open,
        LowerHalfOpen,
        UpperHalfOpen,
        Closed
    };

    enum class MidpointRounding
    {
        ToEven,
        AwayFromZero
    };

    enum IntegrationApproximation
    {
        TrapezoidalRule,
        SimpsonsRule,
        MidpointRule
    };
}

#endif // ENUMS_H