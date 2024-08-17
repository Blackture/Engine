cmake_minimum_required(VERSION 3.10)
project(Engine)

set(CMAKE_CXX_STANDARD 17)

# Point to the vcpkg toolchain file
set(CMAKE_TOOLCHAIN_FILE "C:/Users/Admin/vcpkg/scripts/buildsystems/vcpkg.cmake")
set(CMAKE_PREFIX_PATH "C:/Users/Admin/vcpkg/installed/x64-windows/share/sdl2")

find_package(SDL2 REQUIRED)
find_package(OpenGL REQUIRED)

add_executable(Engine Engine.cpp)
target_link_libraries(Engine SDL2::SDL2 OpenGL::GL)