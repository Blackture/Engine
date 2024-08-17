#include "Mathf.h"

namespace Engine::Core::Maths
{
	const float Mathf::e = static_cast<float>(M_E);
	const float Mathf::pi = static_cast<float>(M_PI);
	const float Mathf::PiQuarter = Mathf::pi / 4.0f;
	const float Mathf::PiHalf = Mathf::pi / 2.0f;
	const float Mathf::SinePiQuarter = sin(Mathf::PiQuarter);
	const float Mathf::CosinePiQuarter = cos(Mathf::PiQuarter);

	float Mathf::Round(float f, int digits, MidpointRounding midpointRounding)
	{
		float scale = std::pow(10.0f, digits);
		float scaledValue = f * scale;
		float roundedValue;

		if (midpointRounding == MidpointRounding::ToEven)
		{
			roundedValue = std::round(scaledValue);
		}
		else if (midpointRounding == MidpointRounding::AwayFromZero)
		{
			roundedValue = (scaledValue >= 0.0f) ? std::floor(scaledValue + 0.5f) : std::ceil(scaledValue - 0.5f);
		}
		else
		{
			roundedValue = std::round(scaledValue);
		}

		return roundedValue / scale;
	}

	int Mathf::Sign(float f)
	{
		if (f > 0)
			return 1;
		if (f < 0)
			return -1;
		return 0;
	}

	/// @brief Iterative Implementation
	/// @param innerFunction
	/// @param limit1
	/// @param limit2
	/// @return The resulting probably big number
	float Mathf::Pi(std::function<float(float)> innerFunction, int limit1, int limit2)
	{
		float res = 1.0f;
		for (float i = limit1; i < limit2; i++)
		{
			res *= innerFunction(i);
		}
		return res;
	}

	/// @brief Iterative Implementation
	/// @param innerFunction
	/// @param limit1
	/// @param limit2
	/// @param increment
	/// @return The resulting probably big number
	float Mathf::Pi(std::function<float(float)> innerFunction, int limit1, int limit2, float increment)
	{
		float res = 1.0f;
		for (float i = limit1; i < limit2; i += increment)
		{
			res *= innerFunction(i);
		}
		return res;
	}

	/// @brief Iterative Implementation
	/// @param innerFunction
	/// @param limit1
	/// @param limit2
	/// @return Sum
	float Mathf::Sigma(std::function<float(float)> innerFunction, int limit1, int limit2)
	{
		float res = 0.0f;
		for (float i = limit1; i < limit2; i++)
		{
			res += innerFunction(i);
		}
		return res;
	}

	/// @brief Iterative Implementation
	/// @param innerFunction
	/// @param limit1
	/// @param limit2
	/// @param increment
	/// @return Sum
	float Mathf::Sigma(std::function<float(float)> innerFunction, int limit1, int limit2, float increment)
	{
		float res = 0.0f;
		for (float i = limit1; i < limit2; i += increment)
		{
			res += innerFunction(i);
		}
		return res;
	}

	/// @brief Recursive Implementation
	/// @param innerFunction
	/// @param limit1
	/// @param limit2
	/// @param increment
	/// @return Sum
	float Mathf::SigmaR(std::function<float(float)> innerFunction, int limit1, int limit2, float increment = std::numeric_limits<float>::epsilon())
	{
		return Mathf::SigmaHelper(innerFunction, limit1, limit2, innerFunction(limit1), increment);
	}

	float Mathf::SigmaHelper(std::function<float(float)> innerFunction, float currentI, int limit2, float current, float increment)
	{

		if (currentI <= limit2)
		{
			return SigmaHelper(innerFunction, currentI + increment, limit2, current + innerFunction(currentI + increment), increment);
		}
		return current;
	}

	/// @brief Interative sum
	/// @param floats
	/// @return the sum
	float Mathf::Sigma(std::initializer_list<float> floats)
	{
		float res = 0.0f;
		for (float f : floats)
		{
			res += f;
		}
		return res;
	}

	/// @brief Interative sum
	/// @param floats
	/// @return the sum
	float Mathf::Sigma(std::vector<float> floats)
	{
		float res = 0.0f;
		for (float f : floats)
		{
			res += f;
		}
		return res;
	}

	/// @brief Sigma where each element gets raised by a power.
	/// @param power
	/// @param floats
	/// @return Sum
	float Mathf::SigmaPow(float power, std::initializer_list<float> floats)
	{
		float res = 0.0f;
		for (float f : floats)
		{
			res += Mathf::Pow(f, power);
		}
		return res;
	}

	/// @brief Sigma where each element gets raised by a power.
	/// @param power
	/// @param floats
	/// @return Sum
	float Mathf::SigmaPow(float power, const std::vector<float> &floats)
	{
		float res = 0.0f;
		for (float f : floats)
		{
			res += Pow(f, power);
		}
		return res;
	}

	/// @brief Recursive sum
	/// @param floats
	/// @return sum
	float Mathf::SigmaR(std::initializer_list<float> floats)
	{
		return SigmaHelper(0, 0, floats);
	}

	/// @brief Recursive sum
	/// @param floats
	/// @return sum
	float Mathf::SigmaR(std::vector<float> &floats)
	{
		return SigmaHelper(0, 0, floats);
	}

	float Mathf::SigmaHelper(int i, float current, const std::vector<float> &floats)
	{
		if (i < floats.size())
		{
			return SigmaHelper(i + 1, current + floats[i], floats);
		}
		return current;
	}

	float Mathf::SigmaHelperPow(int i, float current, const std::vector<float> &floats, float power)
	{
		if (i < floats.size())
		{
			return SigmaHelper(i + 1, current + Mathf::Pow(floats[i], power), floats);
		}
		return current;
	}

	/// @brief Recursive sum
	/// @param floats
	/// @return sum
	float Mathf::SigmaRPow(const std::vector<float> &floats, float power)
	{
		return SigmaHelperPow(0, 0, floats, power);
	}

	bool Mathf::Approximately(float a, float b, float tolerance)
	{
		return std::abs(a - b) < tolerance;
	}

	bool Mathf::Approximately(float a, float b)
	{
		return std::abs(a - b) < std::numeric_limits<float>::epsilon();
	}

	float Mathf::Deg2Rad(float deg)
	{
		return deg * Mathf::pi / 180;
	}

	float Mathf::Rad2Deg(float rad)
	{
		return rad * 180 / Mathf::pi;
	}

	float Mathf::Sinc(float f, bool Pi = false)
	{
		if (Pi)
		{
			return (f != 0) ? Mathf::Sin(Mathf::pi * f) / (Mathf::pi * f) : 1.0f;
		}
		else
		{
			return (f != 0) ? Mathf::Sin(f) / f : 1.0f;
		}
	}

	/// @brief Iterative Implementation
	/// @param i
	/// @return
	int Mathf::Factorial(int i)
	{
		int result = i;
		for (int a = i; i > 0; i--)
			result *= a;
		return result;
	}

	int Mathf::FactorialHelper(int i, int current)
	{
		if (i > 1)
		{
			return FactorialHelper(i - 1, current * (i - 1));
		}

		return current;
	}

	float Mathf::Gamma(float f)
	{
		throw NotImplementedException();
	}

	float Mathf::TrapezoidalRule(Limits limits, std::function<float(float)> innerFunction, int numSteps)
	{
		float step = (limits.upperLimit - limits.lowerLimit) / numSteps;
		float sum = 0.0f;

		for (int i = 0; i < numSteps; ++i)
		{
			float x0 = limits.lowerLimit + i * step;
			float x1 = x0 + step;
			sum += 0.5f * (innerFunction(x0) + innerFunction(x1)) * step;
		}

		return sum;
	}

	float Mathf::MidpointRule(Limits limits, std::function<float(float)> innerFunction, int numSteps)
	{
		float step = (limits.upperLimit - limits.lowerLimit) / numSteps;
		float sum = 0.0f;

		for (int i = 0; i < numSteps; ++i)
		{
			float mid = limits.lowerLimit + (i + 0.5f) * step;
			sum += innerFunction(mid);
		}

		return sum * step;
	}

	float Mathf::SimpsonsRule(Limits limits, std::function<float(float)> innerFunction, int numSteps)
	{
		if (numSteps % 2 != 0)
		{
			numSteps++; // Simpsons rule requires an even number of intervals
		}
		float step = (limits.upperLimit - limits.lowerLimit) / numSteps;
		float sum = innerFunction(limits.lowerLimit) + innerFunction(limits.upperLimit);

		for (int i = 1; i < numSteps; i += 2)
		{
			float x = limits.lowerLimit + i * step;
			sum += 4 * innerFunction(x);
		}

		for (int i = 2; i < numSteps - 1; i += 2)
		{
			float x = limits.lowerLimit + i * step;
			sum += 2 * innerFunction(x);
		}

		return sum * step / 3;
	}

	float Mathf::Integral(Limits limits, std::function<float(float)> innerFunction, IntegrationApproximation approximation, int numSteps = 100)
	{
		switch (approximation)
		{
		case IntegrationApproximation::TrapezoidalRule:
			return Mathf::TrapezoidalRule(limits, innerFunction, numSteps);
		case IntegrationApproximation::SimpsonsRule:
			return Mathf::SimpsonsRule(limits, innerFunction, numSteps);
		case IntegrationApproximation::MidpointRule:
			return Mathf::MidpointRule(limits, innerFunction, numSteps);
		default:
			throw NotImplementedException();
		}
	}
}