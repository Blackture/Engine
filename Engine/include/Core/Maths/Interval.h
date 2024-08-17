#ifndef INTERVAL_H
#define INTERVAL_H

#include <vector>
#include <algorithm>
#include <iostream>
#include <cmath>
#include <Set.h>

namespace Engine::Core::Maths
{
    enum class IntervalType
    {
        Open,
        LowerHalfOpen,
        UpperHalfOpen,
        Closed
    };

    class Interval : public Set<float>
    {
    private:
        IntervalType intervalType;
        float lowerBoundary;
        float upperBoundary;

    public:
        float UpperBoundary() const { return upperBoundary; }
        float LowerBoundary() const { return lowerBoundary; }
        bool ContainsUpperBoundary() const { return Contains(upperBoundary); }
        bool ContainsLowerBoundary() const { return Contains(lowerBoundary); }
        bool ContainsBoundaries() const { return Type() == IntervalType::Closed; }

        IntervalType Type() const { return intervalType; }
        void SetType(IntervalType type) { intervalType = type; }

        Interval(IntervalType intervalType, float lowerBoundary, float upperBoundary);

    private:
        bool contains(float f) const;

    public:
        virtual bool Contains(const float &value) const override;
        
        int Cardinality() const override;

        Set<float> Union(const Interval &I2);

        Set<float> Intersect(const Interval &I2);
    };
}

#endif // INTERVAL_H