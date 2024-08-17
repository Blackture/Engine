#include <Interval.h>

namespace Engine::Core::Maths
{
    Interval::Interval(IntervalType intervalType, float lowerBoundary, float upperBoundary)
        : Set(nullptr), intervalType(intervalType), lowerBoundary(lowerBoundary), upperBoundary(upperBoundary)
    {
        AddInterval(*this);
    }

    bool Interval::contains(float f) const
    {
        switch (intervalType)
        {
        case IntervalType::Open:
            return f > lowerBoundary && f < upperBoundary;
        case IntervalType::LowerHalfOpen:
            return f > lowerBoundary && f <= upperBoundary;
        case IntervalType::UpperHalfOpen:
            return f >= lowerBoundary && f < upperBoundary;
        case IntervalType::Closed:
            return f >= lowerBoundary && f <= upperBoundary;
        default:
            return false;
        }
    }

    bool Interval::Contains(const float &value) const
    {
        for (const auto &I : intervals)
        {
            if (I.contains(value))
            {
                return true;
            }
        }
        return false;
    }

    int Interval::Cardinality() const
    {
        return -1;
    }

    Set<float> Interval::Union(const Interval &I2)
    {
        AddInterval(I2);
        return *this;
    }

    Set<float> Interval::Intersect(const Interval &I2)
    {
        bool uppers[2] = {ContainsUpperBoundary(), I2.ContainsUpperBoundary()};
        bool lowers[2] = {ContainsLowerBoundary(), I2.ContainsLowerBoundary()};

        bool upper2Lower1 = uppers[1] && lowers[0];
        bool upper2NotLower1 = uppers[1] && !lowers[0];
        bool notUpper2Lower1 = !uppers[1] && lowers[0];

        bool upper2LtLower1 = I2.UpperBoundary() < lowerBoundary;
        bool lower2GtUpper1 = I2.LowerBoundary() > upperBoundary;
        bool upper2LeqLower1 = I2.UpperBoundary() <= lowerBoundary;
        bool lower2GeqUpper1 = I2.LowerBoundary() >= upperBoundary;

        bool noIntersection = upper2Lower1 && (upper2LtLower1 || lower2GtUpper1) || (upper2NotLower1 && upper2LeqLower1) || (notUpper2Lower1 && lower2GeqUpper1);

        if (noIntersection)
        {
            intervals.clear();
            if (containingValues.empty())
            {
                containingValues.clear();
            }
            return Set<float>();
        }
        else
        {
            float lowerBoundIntersection = std::max(lowerBoundary, I2.LowerBoundary());
            int indexLow = (lowerBoundIntersection == lowerBoundary) ? 0 : 1;

            float upperBoundIntersection = std::min(upperBoundary, I2.UpperBoundary());
            int indexUp = (upperBoundIntersection == upperBoundary) ? 0 : 1;

            if (lowers[indexLow] && uppers[indexUp])
            {
                intervalType = IntervalType::Closed;
            }
            else if (lowers[indexLow] && !uppers[indexUp])
            {
                intervalType = IntervalType::UpperHalfOpen;
            }
            else if (!lowers[indexLow] && uppers[indexUp])
            {
                intervalType = IntervalType::LowerHalfOpen;
            }
            else
            {
                intervalType = IntervalType::Open;
            }

            lowerBoundary = lowerBoundIntersection;
            upperBoundary = upperBoundIntersection;

            if (lowerBoundIntersection == upperBoundIntersection)
            {
                if (containingValues.empty())
                {
                    containingValues.push_back(lowerBoundIntersection);
                }
            }

            return *this;
        }
    }
}