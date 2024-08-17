#ifndef FLOATENUMERATOR_H
#define FLOATENUMERATOR_H

#include <vector>
#include <stdexcept>

namespace Engine::Core::Maths
{
    class FloatEnumerator
    {
    private:
        const std::vector<float>& data;
        int enumPos;
        bool disposed;

    protected:
        virtual void Dispose(bool disposing);

    public:
        FloatEnumerator(const std::vector<float>& data);

        ~FloatEnumerator();

        bool MoveNext();

        void Reset();

        float Current() const;

        void Dispose();
    };
}

#endif // FLOATENUMERATOR_H
