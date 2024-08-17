#include "Limits.h"
#include "Mathf.h"

namespace Engine::Core::Maths
{
    Limits::Limits(float lowerLimit, float upperLimit, float increment) : Interval(IntervalType::Closed, lowerLimit, upperLimit), lowerLimit(lowerLimit), upperLimit(upperLimit)
    {
        if (increment != 0.0f)
        {
            this->increment = increment;
        }

        if (lowerLimit > upperLimit)
        {
            throw std::invalid_argument("Lower limit cannot be greater than upper limit");
        }
    }

    bool Limits::Contains(const float &f) const
    {
        bool res = false;
        if (f >= Limits::lowerLimit && f <= Limits::upperLimit)
        {
            res = true;
            if (Limits::increment != 0)
            {
                float fbyi = f / Limits::increment;
                int x = Mathf::RoundToInt(fbyi);
                float y = fbyi - x;
                res = Mathf::Approximately(y, 0);
            }
        }
        return res;
    }
}