#ifndef NOT_IMPLEMENTED_EXCEPTION_H
#define NOT_IMPLEMENTED_EXCEPTION_H

#include <stdexcept>
#include <string>

namespace Engine::Core {

class NotImplementedException : public std::logic_error {
public:
    NotImplementedException() 
        : std::logic_error("Not yet implemented") {}
    
    explicit NotImplementedException(const std::string& message) 
        : std::logic_error(message) {}
};

} // namespace Engine::Core

#endif // NOT_IMPLEMENTED_EXCEPTION_H