#include "Mathf.h"
#include <cmath>
#include <functional>
#include <limits>
#include <initializer_list>
#include <vector>

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

	/// @brief Sigma where each element gets raised by a power.
	/// @param power
	/// @param floats
	/// @return Sum
	float Mathf::SigmaPow(float power, std::initializer_list<float> floats)
	{
		float res = 0.0f;
		for (float f : floats)
		{
			res += std::pow(f, power);
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
			res += Mathf::Pow(f, power);
		}
		return res;
	}
}