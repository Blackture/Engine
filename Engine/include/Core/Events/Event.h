#ifndef EVENT_H
#define EVENT_H

#include <functional>
#include <vector>
#include <algorithm>

namespace Engine::Core::Events
{
    class Event
    {
    private:
        std::vector<std::function<void()>> collection;

    public:
        // Add a listener to the event
        void AddListener(const std::function<void()> &method);

        // Remove a listener from the event
        void RemoveListener(const std::function<void()> &method);

        // Remove all listeners
        void RemoveAllListeners();

        // Invoke all listeners
        void Invoke();

        // Overload the + operator to add a listener
        Event &operator+=(const std::function<void()> &method);

        // Overload the - operator to remove a listener
        Event &operator-=(const std::function<void()> &method);
    };
}

#endif //EVENT_H