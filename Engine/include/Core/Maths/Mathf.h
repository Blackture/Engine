#ifndef MATHF_H
#define MATHF_H
#include <cmath>

namespace Engine::Core::Maths 
{
    class Mathf {
    public:
        static const float e;
        static const float pi;

        static const float PiQuarter;
        static const float PiHalf;

        static const float SinePiQuarter;
        static const float CosinePiQuarter;

        static float Sin(float f) 
        {
            return std::sin(f);
        }

        static float Acos(float f)
        {
            return std::acos(f);
        }

        static float Asin(float f)
        {
            return std::asin(f);
        }

        static float Atan(float f)
        {
            return std::atan(f);
        }

        static float Abs(float f)
        {
            return std::abs(f);
        }

        static float Atan2(float x, float y)
        {
            return std::atan2(x, y);
        }

        static float Ceiling(float f)
        {
            return std::ceiling(f);
        }

        static float Cos(float f)
        {
            return std::cos(f);
        }

        static float Cosh(float f)
        {
            return std::cosh(f);
        }

        static float Cot(float f)
        {
            return Cos(f) / Sin(f);
        }

        static float Sec(float f)
        {
            return 1.0f / Cos(f);
        }
        static float Csc(float f)
        {
            return 1.0f / Sin(f);
        }

        static float Exp(float f)
        {
            return std::exp(f);
        }

        static float Floor(float f)
        {
            return std::floor(f);
        }

        float IEEERemainder(float x, float y) {
            return x - y * std::round(x / y);
        }
    };
}

#endif // MATHF_H