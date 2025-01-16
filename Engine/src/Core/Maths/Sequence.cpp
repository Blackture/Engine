#include <Sequence.h>
#include <limits>

namespace Engine::Core::Maths
{
    Sequence::Sequence(std::function<float(float)> a, Set<float> where, int allocationSize = 10)
    {
        this->oftenUsedValues = std::vector<float>(allocationSize);
        this->a = a;
        this->where = where;
        this->currentIndex = 0;
    }

    //Get Value in a sequence
    float Sequence::GetValue(float f)
    {
        if (!this->where.Contains(f))
            return std::numeric_limits<float>::quiet_NaN();
        
        // Store the calculated value at the current index
        this->oftenUsedValues[this->currentIndex] = this->a(f);
        
        // Return the value just stored
        float returnValue = this->oftenUsedValues[this->currentIndex];
        
        // Update currentIndex with modulo to cycle back to 0 after 9
        this->currentIndex = (this->currentIndex + 1) % 10;
        
        return returnValue;
    }
}