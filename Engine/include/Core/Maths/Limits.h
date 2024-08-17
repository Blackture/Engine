#ifndef LIMITS_H
#define LIMITS_H

#include <Set.h>
#include <Interval.h>
#include <Mathf.h>

namespace Engine::Core::Maths
{
    class Limits : public Interval
    {
    public:
        float lowerLimit;
        float upperLimit;
        /// @brief The increment is the internal step width
        float increment;

        Limits(float lowerLimit, float upperLimit, float increment);

        bool Contains(const float &f) const override;
    };
}

#endif // LIMITS_H
