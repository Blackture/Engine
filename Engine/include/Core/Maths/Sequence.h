#ifndef SEQUENCE_H
#define SEQUENCE_H

#include <Mathf.h>
#include <vector>
#include <cmath>


namespace Engine::Core::Maths
{
    class Sequence
    {
        public:
            std::vector<float> oftenUsedValues = std::vector<float>(5);

            Sequence(std::function<float(float)> a, Set<float> where, int allocationSize = 10);

            float GetValue(float f);

        private:
            std::function<float(float)> a;
            Set<float> where;
            int currentIndex;
    };
}

#endif //SEQUENCE_H