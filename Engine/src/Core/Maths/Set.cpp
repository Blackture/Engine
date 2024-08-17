#include <Set.h>

namespace Engine::Core::Maths
{
    template <typename T>
    Set<T>::Set(float *ptr = nullptr)
    {
        containingValues = NULL;
    }

    template <typename T>
    Set<T>::Set(std::initializer_list<T> values)
    {
        if (!values.empty())
        {
            containingValues.assign(values);
        }
        if constexpr (std::is_same<T, float>::value)
        {
            intervals = std::vector<Interval>();
        }
    }

    template <typename T>
    int Set<T>::Cardinality() const
    {
        if (containingValues.empty())
        {
            return -1;
        }
        return containingValues.size();
    }

    template <typename T>
    bool Set<T>::Contains(const T &value) const
    {
        return !containingValues.empty() && std::find(containingValues.begin(), containingValues.end(), value) != containingValues.end();
    }

    template <typename T>
    void Set<T>::AddInterval(const Interval &interval)
    {
        if constexpr (std::is_same<T, float>::value)
        {
            intervals.push_back(interval);
        }
        else
        {
            std::cerr << "This should be a float-set to add an interval" << std::endl;
        }
    }
}