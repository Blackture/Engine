#ifndef MATHF_H
#define MATHF_H

#include <cmath>
#include <functional>
#include <limits>
#include <initializer_list>
#include <vector>
#include <Core.h>

namespace Engine::Core::Maths
{
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

        static float Round(float f, int digits)
        {
            float scale = std::pow(10.0f, digits);
            return std::round(f * scale) / scale;
        }

        static float Round(float f, int digits, MidpointRounding midpointRounding);

        static int RoundToInt(float f)
        {
            return static_cast<int>(Round(f, 0, MidpointRounding::AwayFromZero));
        }

        static int Sign(float f);

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

        static float Mathf::Sigma(std::vector<float> floats);

        static float SigmaPow(float power, std::initializer_list<float> floats);

        static float SigmaPow(float Didnpower, const std::vector<float> &floats);

        static float SigmaR(std::initializer_list<float> floats);

        static float SigmaR(std::vector<float> &floats);

        static float SigmaRPow(const std::vector<float> &floats, float power);

        static bool Approximately(float a, float b, float tolerance);

        static bool Approximately(float a, float b);

        static float Deg2Rad(float deg);

        static float Rad2Deg(float rad);

        static float Sinc(float f, bool Pi = false);

        static int Factorial(int i);

        static float Gamma(float f);

        static float Mathf::Integral(Limits limits, std::function<float(float)> innerFunction, IntegrationApproximation approximation, int numSteps = 100);

    private:
        static float SigmaHelper(std::function<float(float)> innerFunction, float currentI, int limit2, float current, float increment);

        static float SigmaHelper(int i, float current, const std::vector<float> &floats);

        static float SigmaHelperPow(int i, float current, const std::vector<float> &floats, float power);

        static int FactorialHelper(int i, int current);

        static float TrapezoidalRule(Limits limits, std::function<float(float)> innerFunction, int numSteps);

        static float MidpointRule(Limits limits, std::function<float(float)> innerFunction, int numSteps);

        static float SimpsonsRule(Limits limits, std::function<float(float)> innerFunction, int numSteps);
    };
}

#endif // MATHF_H