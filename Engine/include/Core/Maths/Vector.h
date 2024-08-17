#ifndef VECTOR_H
#define VECTOR_H

#include <vector>
#include <stdexcept>
#include "Interval.h"
#include "Mathf.h"
#include "FloatEnumerator.h"
#include "Event.h"

using namespace Engine::Core::Events;

namespace Engine::Core::Maths
{
    class Vector
    {
    private:
        std::vector<float> values;
        int dimension;
        float length;
        float lengthSquared;
        std::vector<float> normalized;
        Event onValueChanged;

        void CalculateNormalization();
        void OnValueChanged();

    public:
        Vector(const std::vector<float> &values);
        Vector(const std::initializer_list<float> &values);
        Vector(const Vector &v);
        Vector();

        void Instantiate(const std::vector<float> &values);

        Vector Normalize();

        float GetLength();

        float GetValue(int index) const;
        void SetValue(float value, int index);
        void AddValue(float value);

        int Dimension() const { return dimension; }
        float Length() const { return length; }
        float LengthSquared() const { return lengthSquared; }
        Vector Normalized() const;

        bool IsValidIndex(int index) const;
        bool IsZeroVector() const;
        bool IsDimensionEqual(const Vector &a, const Vector &b) const;

        float DotProduct(const Vector &vector) const;
        Vector ScalarProduct(float scalar) const;
        Vector CrossProduct(const Vector &b) const;
        Vector Addition(const Vector &vector) const;
        Vector Subtraction(const Vector &vector) const;
        Vector Division(float scalar) const;

        static float Dot(const Vector &a, const Vector &b);
        static Vector Cross(const Vector &a, const Vector &b);
        static Vector Scalar(const Vector &a, float f);
        static Vector Addition(const Vector &a, const Vector &b);
        static Vector Subtraction(const Vector &a, const Vector &b);
        static Vector Division(const Vector &a, float f);

        // Overloaded operators
        friend Vector operator+(const Vector &v1, const Vector &v2);
        friend Vector operator-(const Vector &v1, const Vector &v2);
        friend float operator*(const Vector &v1, const Vector &v2);
        friend Vector operator*(const Vector &v1, float f);
        friend Vector operator*(float f, const Vector &v1);
        friend Vector operator/(const Vector &v1, float f);

        //To Matrix missing

        FloatEnumerator GetEnumerator() const;

        float Vector::operator[](int index) const;
        float &operator[](int index);
    };
}

#endif // VECTOR_H
