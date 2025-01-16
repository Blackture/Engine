#include "Vector3.h"
#include "Mathf.h"
#include "Vector.h"

namespace Engine::Core::Maths
{
    // Static constants
    const Vector3 Vector3::Zero = Vector3(0.0f, 0.0f, 0.0f);
    const Vector3 Vector3::One = Vector3(1.0f, 1.0f, 1.0f);


    Vector3::Vector3(float x1, float x2, float x3)
        : x1(x1), x2(x2), x3(x3), length(0), lengthSquared(0)
    {
        calculateNormalization();
    }

    Vector3::Vector3(const Vector3 &vector)
        : x1(vector.x1), x2(vector.x2), x3(vector.x3), length(vector.length), lengthSquared(vector.lengthSquared)
    {
        std::copy(std::begin(vector.normalized), std::end(vector.normalized), normalized);
    }

    // Getter methods
    float Vector3::GetX1() const { return x1; }
    void Vector3::SetX1(float value)
    {
        x1 = value;
        length = getLength();
        calculateNormalization();
    }
    float Vector3::GetX2() const { return x2; }
    void Vector3::SetX2(float value)
    {
        x2 = value;
        length = getLength();
        calculateNormalization();
    }
    float Vector3::GetX3() const { return x3; }
    void Vector3::SetX3(float value)
    {
        x3 = value;
        length = getLength();
        calculateNormalization();
    }
    float Vector3::GetLength() const { return length; }
    float Vector3::GetLengthSquared() const { return lengthSquared; }
    Vector3 Vector3::GetNormalized() const { return Vector3(normalized[0], normalized[1], normalized[2]); }
    
    int Vector3::Dimension() const { return 3; }

    // Index operator
    float Vector3::operator[](int xindex) const
    {
        switch (xindex)
        {
        case 0:
            return x1;
        case 1:
            return x2;
        case 2:
            return x3;
        default:
            throw std::out_of_range("Index out of range");
        }
    }

    float &Vector3::operator[](int xindex)
    {
        switch (xindex)
        {
        case 0:
            return x1;
        case 1:
            return x2;
        case 2:
            return x3;
        default:
            throw std::out_of_range("Index out of range");
        }
    }

    // Operator overloads
    Vector3 operator*(float f, const Vector3 &v)
    {
        return Vector3(v.x1 * f, v.x2 * f, v.x3 * f);
    }

    Vector3 operator*(const Vector3 &v, float f)
    {
        return v * f;
    }

    float operator*(const Vector3 &v1, const Vector3 &v2)
    {
        return v1.x1 * v2.x1 + v1.x2 * v2.x2 + v1.x3 * v2.x3;
    }

    Vector3 operator+(const Vector3 &v1, const Vector3 &v2)
    {
        return Vector3(v1.x1 + v2.x1, v1.x2 + v2.x2, v1.x3 + v2.x3);
    }

    Vector3 operator-(const Vector3 &v1, const Vector3 &v2)
    {
        return Vector3(v1.x1 - v2.x1, v1.x2 - v2.x2, v1.x3 - v2.x3);
    }

    Vector3 operator/(const Vector3 &v, float f)
    {
        return v * (1 / f);
    }

    bool operator==(const Vector3 &v1, const Vector3 &v2)
    {
        return v1[0] == v2[0] && v1[1] == v2[1] && v1[2] == v2[2];
    }

    bool operator!=(const Vector3 &v1, const Vector3 &v2)
    {
        return v1[0] != v2[0] && v1[1] != v2[1] && v1[2] != v2[2];
    }

    Vector3::operator Vector() const {
        return Vector({X1(),X2(),X3()});
    }

    // Vector operations
    Vector3 Vector3::Normalize()
    {
        calculateNormalization();
        return GetNormalized();
    }

    float Vector3::getLength()
    {
        this->lengthSquared = x1 * x1 + x2 * x2 + x3 * x3;
        this->length = Mathf::Sqrt(lengthSquared);
        return length;
    }

    Vector3 Vector3::CrossProduct(const Vector3 &v) const
    {
        return Vector3(
            x2 * v.x3 - x3 * v.x2,
            x3 * v.x1 - x1 * v.x3,
            x1 * v.x2 - x2 * v.x1);
    }

    float Vector3::DotProduct(const Vector3 &v) const
    {
        return (*this) * v;
    }

    Vector3 Vector3::ScalarProduct(float scalar) const
    {
        return (*this) * scalar;
    }

    Vector3 Vector3::Addition(const Vector3 &vector) const
    {
        return (*this) + vector;
    }

    Vector3 Vector3::Subtraction(const Vector3 &v) const
    {
        return (*this) - v;
    }

    Vector3 Vector3::Division(float scalar) const
    {
        return (*this) / scalar;
    }

    bool Vector3::IsPerpendicularTo(const Vector3 &v) const
    {
        return std::abs(DotProduct(v)) < 1e-6;
    }

    Vector3 Vector3::GetNormalVector(int zeroAt) const
    {
        switch (zeroAt)
        {
        case 1:
            return CrossProduct(Vector3(0, x2, x3));
        case 2:
            return CrossProduct(Vector3(x1, 0, x3));
        case 3:
            return CrossProduct(Vector3(x1, x2, 0));
        default:
            return Zero;
        }
    }

    bool Vector3::IsZero() const
    {
        return (*this == Zero);
    }


    std::string Vector3::ToString() const
    {
        return "(" + std::to_string(x1) + ", " + std::to_string(x2) + ", " + std::to_string(x3) + ")^T";
    }

    Vector3 Vector3::Project(const Vector3 &to) const
    {
        float dotProduct = DotProduct(to);
        float lengthSquared = to.GetLength();
        return to * (dotProduct / lengthSquared);
    }

    Vector3 Vector3::CrossProduct(Vector3 u, Vector3 v) {
        return u.CrossProduct(v);
    }

    float Vector3::DotProduct(Vector3 u, Vector3 v) {
        return u * v;
    }

    Vector3 Vector3::Normalize(Vector3 v) {
        v.Normalize();
        return Vector3(v.normalized[0],v.normalized[1],v.normalized[2]);
    }

    float Vector3::GetLength(Vector3 v) {
        return v.GetLength();
    }

    float AngleBetween(Vector3 &a, Vector3 &b) {
        if (a == Vector3::Zero && b == Vector3::Zero) return 0.0f;
        return Mathf::Acos(a * b / (a.GetLength() * b.GetLength()) * 180 / Mathf::pi);
    }

    bool OrthogonalityCheck(Vector3 a, Vector3 b) {
        bool res = false;
        if (a != Vector3::Zero && b != Vector3::Zero) {
            if (a * b == 0) res = true;
        }
        return res;
    }

    void Vector3::calculateNormalization()
    {
        if (length == 0)
        {
            normalized[0] = normalized[1] = normalized[2] = 0;
        }
        else
        {
            normalized[0] = x1 / length;
            normalized[1] = x2 / length;
            normalized[2] = x3 / length;
        }
    }
}