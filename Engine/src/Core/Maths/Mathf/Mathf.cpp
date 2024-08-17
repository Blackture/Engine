#include "Mathf.h"
#include <cmath>

namespace Engine::Core::Maths
{
	const float Mathf::e = static_cast<float>(M_E);
	const float Mathf::pi = static_cast<float>(M_PI);
	const float Mathf::PiQuarter = Mathf::pi / 4.0f;
	const float Mathf::PiHalf = Mathf::pi / 2.0f;
	const float Mathf::SinePiQuarter = sin(Mathf::PiQuarter);
	const float Mathf::CosinePiQuarter = cos(Mathf::PiQuarter);
}