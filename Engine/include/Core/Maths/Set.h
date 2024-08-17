#ifndef SET_H
#define SET_H

#include <vector>
#include <type_traits>
#include <iostream> // For debug purposes
#include <Interval.h>

namespace Engine::Core::Maths
{
    template <typename T>
    class Set
    {
    protected:
        std::vector<Interval> intervals;
        std::vector<T> containingValues;

    public:
        Set(float *ptr = nullptr);

        // Constructor accepting a variable number of arguments
        Set(std::initializer_list<T> values);

        // Calculate the cardinality of the set
        virtual int Cardinality() const;

        // Check if a value is in the set
        virtual bool Contains(const T &value) const;

        // Add an interval to the set
        void AddInterval(const Interval &interval);
    };
}

#endif // SET_H
