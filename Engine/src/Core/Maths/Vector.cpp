#include "Vector.h"
#include <vector>
#include "Vector3.h"

namespace Engine::Core::Maths
{
    float Vector::Length() const { return length; }
    int Vector::Dimension() const { return dimension; }
    float Vector::LengthSquared() const { return lengthSquared; }
    Vector Vector::Normalized() const { return Vector(normalized); }

    float Vector::operator[](int index) const
    {
        return GetValue(index);
    }
    float &Vector::operator[](int index)
    {
        return values.at(index); // Using at() for bounds checking
    }

    float Vector::GetValue(int index) const
    {
        if (IsValidIndex(index))
        {
            return values[index];
        }
        throw std::out_of_range("Invalid index.");
    }
    void Vector::SetValue(float value, int index)
    {
        if (IsValidIndex(index))
        {
            values[index] = value;
            onValueChanged.Invoke();
        }
    }
    void Vector::AddValue(float value)
    {
        this->values.push_back(value);
        onValueChanged.Invoke();
    }

    Vector::Vector(const std::vector<float> &values)
        : values(values), dimension(values.size())
    {
        Instantiate(values);
    }
    Vector::Vector(const std::initializer_list<float> &values)
        : values(values), dimension(values.size())
    {
        Instantiate(values);
    }
    Vector::Vector(const Vector &v)
        : values(v.values), dimension(v.dimension), length(v.length),
          lengthSquared(v.lengthSquared), normalized(v.normalized)
    {
        for (int i = 0; i < v.Dimension(); i++)
        {
            values.push_back(v[i]);
        }
    }
    Vector::Vector()
    {
        Instantiate({0.0f, 0.0f, 0.0f});
    }

    void Vector::Instantiate(const std::vector<float> &values)
    {
        this->values.clear();
        this->values = values;
        dimension = static_cast<int>(this->values.size());
        if (dimension != 0)
        {
            length = GetLength();
            CalculateNormalization();
        }

        // Recreate the event and add a listener
        onValueChanged = Events::Event();
        onValueChanged.AddListener([this]()
                                   { OnValueChanged(); });
    }

    Vector Vector::Normalize()
    {
        this->CalculateNormalization();
        return this->Normalized();
    }

    void Vector::CalculateNormalization()
    {
        normalized.clear();
        for (float value : values)
        {
            normalized.push_back(value / length);
        }
    }

    void Vector::OnValueChanged()
    {
        dimension = values.size();
        length = GetLength();
        CalculateNormalization();
    }

    float Vector::GetLength()
    {
        float res = Mathf::SigmaPow(2, values);
        float res = Mathf::SigmaPow(2, values);
        res = Mathf::Sqrt(res);
        this->lengthSquared = Mathf::Pow(res, 2);
        return res;
    }

    bool Vector::IsValidIndex(int index) const
    {
        return (index >= 0 && index < values.size());
    }

    bool Vector::IsDimensionEqual(const Vector &a, const Vector &b) const
    {
        return a.Dimension() == b.Dimension();
    }

    bool Vector::IsZeroVector() const
    {
        return Mathf::Sigma(this->values) == 0.0f;
    }

    float Vector::DotProduct(const Vector &vector) const
    {
        if (IsDimensionEqual(*this, vector))
        {
            float res = 0;
            for (int i = 0; i < dimension; i++)
            {
                res += values[i] * vector[i];
            }
            return res;
        }
        throw std::invalid_argument("The vectors have different dimensions.");
    }

    Vector Vector::ScalarProduct(float scalar) const
    {
        Vector res;
        for (int i = 0; i < Dimension(); i++)
        {
            res.AddValue(scalar * values[i]);
        }
        return res;
    }

    Vector Vector::CrossProduct(const Vector &b) const
    {
        if (!IsDimensionEqual(*this, b))
        {
            throw std::invalid_argument("Vectors must have the same dimension.");
        }

        Vector res;
        switch (Dimension())
        {
        case 1:
            res = {values[0] * b[0]};
            break;
        case 2:
        {
            Vector3 _a(values[0], values[1], 0);
            Vector3 _b(b[0], b[1], 0);
            Vector3 c = Vector3::CrossProduct(_a, _b);
            res = Vector(c.X3); // Only the Z-component
            break;
        }
        case 3:
        {
            Vector3 __a(values[0], values[1], values[2]);
            Vector3 __b(b[0], b[1], b[2]);
            Vector3 _c = Vector3::CrossProduct(__a, __b);
            res = {_c.X1, _c.X2, _c.X3};
            break;
        }
        default:
            throw std::invalid_argument("Cross Product is only defined for 1 to 3-dimensional vectors.");
        }
        return res;
    }

    Vector Vector::Addition(const Vector &vector) const
    {
        if (IsDimensionEqual(*this, vector))
        {
            Vector res;
            for (int i = 0; i < Dimension(); i++)
            {
                res.AddValue(values[i] + vector[i]);
            }
            return res;
        }
        throw std::invalid_argument("Vectors must have the same dimension.");
    }

    Vector Vector::Subtraction(const Vector &vector) const
    {
        if (IsDimensionEqual(*this, vector))
        {
            Vector res;
            for (int i = 0; i < Dimension(); i++)
            {
                res.AddValue(values[i] - vector[i]);
            }
            return res;
        }
        throw std::invalid_argument("Vectors must have the same dimension.");
    }

    Vector Vector::Division(float scalar) const
    {
        Vector res;
        for (int i = 0; i < Dimension(); i++)
        {
            res.AddValue(values[i] / scalar);
        }
        return Vector(res);
    }

    float Vector::Dot(const Vector &a, const Vector &b)
    {
        return a.DotProduct(b);
    }

    Vector Vector::Cross(const Vector &a, const Vector &b)
    {
        return a.CrossProduct(b);
    }

    Vector Vector::Scalar(const Vector &a, float f)
    {
        return a.ScalarProduct(f);
    }

    Vector Vector::Addition(const Vector &a, const Vector &b)
    {
        return a.Addition(b);
    }

    Vector Vector::Subtraction(const Vector &a, const Vector &b)
    {
        return a.Subtraction(b);
    }

    Vector Vector::Division(const Vector &a, float f)
    {
        return a.Division(f);
    }

    // Addition of two vectors
    Vector operator+(const Vector &v1, const Vector &v2) {
        return v1.Addition(v2);
    }

    // Subtraction of two vectors
    Vector operator-(const Vector &v1, const Vector &v2) {
        return v1.Subtraction(v2);
    }

    // Dot product of two vectors
    float operator*(const Vector &v1, const Vector &v2) {
        return v1.DotProduct(v2);
    }

    // Scalar multiplication (Vector * float)
    Vector operator*(const Vector &v1, float f) {
        return v1.ScalarProduct(f);
    }

    // Scalar multiplication (float * Vector)
    Vector operator*(float f, const Vector &v1) {
        return v1.ScalarProduct(f);
    }
    
    // Scalar multiplication (float * Vector)
    Vector operator/(float f, const Vector &v1) {
        return v1.Division(f);
    }

    FloatEnumerator Vector::GetEnumerator() const
    {
        return FloatEnumerator(values);
    }

}
