#include <FloatEnumerator.h>
#include <vector>
#include <stdexcept>

namespace Engine::Core::Maths
{
    FloatEnumerator::FloatEnumerator(const std::vector<float> &data)
        : data(data), enumPos(-1), disposed(false)
    {
    }

    FloatEnumerator::~FloatEnumerator()
    {
        Dispose(false);
    }

    void FloatEnumerator::Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (disposing)
            {
                // Dispose of managed resources
            }

            // Dispose of unmanaged resources

            disposed = true;
        }
    }

    bool FloatEnumerator::MoveNext()
    {
        enumPos++;
        return enumPos < data.size();
    }

    void FloatEnumerator::Reset()
    {
        enumPos = -1;
    }

    float FloatEnumerator::Current() const
    {
        if (enumPos < 0 || enumPos >= data.size())
        {
            throw std::out_of_range("Enumerator is out of range.");
        }
        return data[enumPos];
    }

    void FloatEnumerator::Dispose()
    {
        Dispose(true);
    }
}