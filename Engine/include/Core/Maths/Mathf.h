#ifndef MATHF_H
#define MATHF_H
#include <cmath>
#include <algorithm>
#include <functional>
#include <initializer_list>

namespace Engine::Core::Maths
{
    enum class MidpointRounding {
        ToEven,
        AwayFromZero
    };

    class Mathf
    {
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
            return std::ceil(f);
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

        float IEEERemainder(float x, float y)
        {
            return x - y * std::round(x / y);
        }

        static float Log(float f)
        {
            return std::log(f);
        }

        static float Log(float f, float newBase)
        {
            return std::log(f) / std::log(newBase);
        }

        static float Log10(float f)
        {
            return std::log10(f);
        }

        static float Max(float val1, float val2)
        {
            return std::max(val1, val2);
        }

        static float Min(float val1, float val2)
        {
            return std::min(val1, val2);
        }

        static float Pow(float b, float p)
        {
            return std::pow(b, p);
        }

        static float Round(float f)
        {
            return std::round(f);
        }

        static float Round(float f, int digits) {
            float scale = std::pow(10.0f, digits);
            return std::round(f * scale) / scale;
        }

        static float Round(float f, int digits, MidpointRounding midpointRounding);

        static int RoundToInt(float f) {
            return static_cast<int>(Round(f, 0, MidpointRounding::AwayFromZero));
        }

        static int Sign(float f) {
            if (f > 0) return 1;
            if (f < 0) return -1;
            return 0;
        }

        static float Sinh(float f)
        {
            return std::sinh(f);
        }

        static float Sqrt(float f)
        {

            return std::sqrt(f);
        }

        static float NthSqrt(float f, int n)
        {
            return Pow(f, 1.0f / n);
        }

        static float Tan(float f)
        {
            return std::tan(f);
        }

        static float Tanh(float f)
        {
            return std::tanh(f);
        }

        static float Truncate(float f)
        {
            return std::truncf(f);
        }

        static float Pi(std::function<float(float)> innerFunction, int limit1, int limit2);

        static float Pi(std::function<float(float)> innerFunction, int limit1, int limit2, float increment);

        static float Sigma(std::function<float(float)> innerFunction, int limit1, int limit2);

        static float Sigma(std::function<float(float)> innerFunction, int limit1, int limit2, float increment);

        static float SigmaR(std::function<float(float)> innerFunction, int limit1, int limit2, float increment = std::numeric_limits<float>::epsilon());

        static float Sigma(std::initializer_list<float> floats);

        static float SigmaPow(float power, std::initializer_list<float> floats);

        static float SigmaPow(float power, const std::vector<float>& floats);

        private:

            static float SigmaHelper(std::function<float(float)> innerFunction, float currentI, int limit2, float current, float increment);
    };
}

#endif // MATHF_H