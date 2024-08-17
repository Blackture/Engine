#include "Event.h"

namespace Engine::Core::Events
{
    void Event::AddListener(const std::function<void()> &method)
    {
        collection.push_back(method);
    }

    void Event::RemoveListener(const std::function<void()> &method)
    {
        collection.erase(std::remove_if(collection.begin(), collection.end(),
                                        [&](const std::function<void()> &storedMethod)
                                        {
                                            return storedMethod.target<void()>() == method.target<void()>();
                                        }),
                         collection.end());
    }

    void Event::RemoveAllListeners()
    {
        collection.clear();
    }

    void Event::Invoke()
    {
        for (const auto &method : collection)
        {
            method();
        }
    }

    Event &Event::operator+=(const std::function<void()> &method)
    {
        AddListener(method);
        return *this;
    }

    Event &Event::operator-=(const std::function<void()> &method)
    {
        RemoveListener(method);
        return *this;
    }
}