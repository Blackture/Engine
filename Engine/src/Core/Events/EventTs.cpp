#include "EventTs.h"

namespace Engine::Core::Events
{
    template <typename... Ts>
    void EventTs<Ts...>::AddListener(const std::function<void(Ts...)> &method)
    {
        collection.push_back(method);
    }

    template <typename... Ts>
    void EventTs<Ts...>::RemoveListener(const std::function<void(Ts...)> &method)
    {
        collection.erase(std::remove_if(collection.begin(), collection.end(),
                                        [&](const std::function<void(Ts...)> &storedMethod)
                                        {
                                            return storedMethod.target<void(Ts...)>() == method.target<void(Ts...)>();
                                        }),
                         collection.end());
    }

    template <typename... Ts>
    void EventTs<Ts...>::RemoveAllListeners()
    {
        collection.clear();
    }

    template <typename... Ts>
    void EventTs<Ts...>::Invoke(Ts... ts)
    {
        for (const auto &method : collection)
        {
            method(ts...);
        }
    }

    template <typename... Ts>
    EventTs<Ts...> &EventTs<Ts...>::operator+=(const std::function<void(Ts...)> &method)
    {
        AddListener(method);
        return *this;
    }

    template <typename... Ts>
    EventTs<Ts...> &EventTs<Ts...>::operator-=(const std::function<void(Ts...)> &method)
    {
        RemoveListener(method);
        return *this;
    }
}