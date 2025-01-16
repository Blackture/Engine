#ifndef VECTOR3_H
#define VECTOR3_H

#include <cmath>
#include <string>
#include <iostream>
#include <Vector.h>

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
        float X1() const { return x1; };
        float X2() const { return x2; };
        float X3() const { return x3; };

        // Constructors
        Vector3(float x1 = 0.0f, float x2 = 0.0f, float x3 = 0.0f);
        Vector3(const Vector3 &vector);

        // Properties (getter methods in C++)
        float GetX1() const;
        void SetX1(float value);
        float GetX2() const;
        void SetX2(float value);
        float GetX3() const;
        void SetX3(float value);
        float GetLength() const;
        float GetLengthSquared() const;
        Vector3 GetNormalized() const;

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
        operator Vector() const;

        // Vector operations
        Vector3 Normalize();
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
        static Vector3 CrossProduct(Vector3 u, Vector3 v);
        static float DotProduct(Vector3 u, Vector3 v);
        static Vector3 Normalize(Vector3 v);
        static float GetLength(Vector3 v);
        static float AngleBetween(Vector3 v, Vector3 u);
        static float OrthogonalityCheck(Vector3 v, Vector3 u);

    private:
        void calculateNormalization();
        float getLength();
    };
}
#endif // VECTOR3_H