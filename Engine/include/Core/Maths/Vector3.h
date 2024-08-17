#ifndef VECTOR3_H
#define VECTOR3_H

#include <cmath>
#include <string>
#include <iostream>

namespace Engine::Core::Maths
{
    class Vector3
    {
    public:
        static const Vector3 Zero;
        static const Vector3 One;

    private:
        float x1, x2, x3;
        float length;
        float lengthSquared;
        float normalized[3];

    public:
        // Constructors
        Vector3(float x1 = 0.0f, float x2 = 0.0f, float x3 = 0.0f);
        Vector3(const Vector3 &vector);

        // Properties (getter methods in C++)
        float getX1() const;
        void setX1(float value);
        float getX2() const;
        void setX2(float value);
        float getX3() const;
        void setX3(float value);
        float getLength() const;
        float getLengthSquared() const;
        Vector3 getNormalized() const;

        int Dimension() const;

        // Index operator
        float operator[](int xindex) const;
        float &operator[](int xindex);

        // Operator overloads
        friend Vector3 operator*(float f, const Vector3 &v);
        friend Vector3 operator*(const Vector3 &v, float f);
        friend float operator*(const Vector3 &v1, const Vector3 &v2);
        friend Vector3 operator+(const Vector3 &v1, const Vector3 &v2);
        friend Vector3 operator-(const Vector3 &v1, const Vector3 &v2);
        friend Vector3 operator/(const Vector3 &v, float f);
        friend Vector3 operator==(const Vector3 &v1, Vector3 &v2);

        // Vector operations
        Vector3 Normalize();
        float GetLength();
        float AngleBetween(const Vector3 &v1, const Vector3 &v2) const;
        bool OrthogonalityCheck(const Vector3 &v1, const Vector3 &v2) const;
        Vector3 Normalize(Vector3 v);

        
        Vector3 CrossProduct(const Vector3 &v) const;
        float DotProduct(const Vector3 &v) const;
        Vector3 ScalarProduct(float scalar) const;
        Vector3 Addition(const Vector3 &vector) const;
        Vector3 Subtraction(const Vector3 &v) const;
        Vector3 Division(float scalar) const;
        bool IsPerpendicularTo(const Vector3 &v) const;
        Vector3 GetNormalVector(int zeroAt) const;
        bool IsZero() const;
        std::string ToString() const;
        Vector3 Project(const Vector3 &to) const;

        // Project(Plane) not implemented
        // To Matrix missing

    private:
        void CalculateNormalization();
    };
    // Static constants
    const Vector3 Vector3::Zero = Vector3(0.0f, 0.0f, 0.0f);
    const Vector3 Vector3::One = Vector3(1.0f, 1.0f, 1.0f);
}
#endif // VECTOR3_H