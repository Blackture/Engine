#ifndef EVENTTS_H
#define EVENTTS_H

#include <functional>
#include <vector>
#include <algorithm>

namespace Engine::Core::Events
{
    template <typename... Ts>
    class EventTs
    {
    private:
        std::vector<std::function<void(Ts...)>> collection;

    public:
        // Add a listener to the event
        void AddListener(const std::function<void(Ts...)> &method);

        // Remove a listener from the event
        void RemoveListener(const std::function<void(Ts...)> &method);

        // Remove all listeners
        void RemoveAllListeners();

        // Invoke all listeners
        void Invoke(Ts...);

        // Overload the + operator to add a listener
        EventTs<Ts...> &operator+=(const std::function<void(Ts...)> &method);

        // Overload the - operator to remove a listener
        EventTs<Ts...> &operator-=(const std::function<void(Ts...)> &method)
    };
}

#endif // EVENTTS_H