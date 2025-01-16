#include "MatrixMxN.h"
#include "Vector.h"

namespace Engine::Core::Maths
{
    float MatrixMxN::operator[](Accessor &a) const {
        if (a.set) throw new std::exception("Invalid Accessor");
        else return getValue(a.r, a.c);
    }

    float &MatrixMxN::operator[](Accessor &a) {
        if (a.set) {
            setValue(a.r,a.c,a.value);
        }
        else {
            float f = getValue(a.r,a.c);
            return f;
        }
    } 

    void MatrixMxN::setValue(int &r, int &c, float &value) {
        if (validateIndexes(r,c)) {
            rows[r][c] = value;
        }
        else throw new std::out_of_range("Row: " + std::to_string(r) + " or " + std::to_string(c) + " is out of range.");
    }

    float MatrixMxN::getValue(int &r, int &c) const {
        if (validateIndexes(r, c)) {
            return rows[r][c];
        }
        else throw new std::out_of_range("Row: " + std::to_string(r) + " or " + std::to_string(c) + " is out of range.");
    }

    /// @brief Checks if the provided index is within the valid range.
    /// @details
    /// This function verifies whether the input index is valid based on the provided parameters.
    /// - **r**: The row index to validate.
    /// - **c**: Set to true if the index represents a column instead of a row.
    /// @param r The row index to check.
    /// @param c Whether to treat the index as a column index.
    /// @return True if the index is within the valid range; false otherwise.
    bool MatrixMxN::validateIndex(int &index, bool column = false) const {
        bool res = true;
        switch (column)
        {
            case true:
                if (index >= ColumnCount() || index < 0) res = false;
                break;
            case false:
                if (index >= RowCount() || index < 0) res = false;
                break;
        }
        return res;
    }

    bool MatrixMxN::validateIndexes(int &r, int &c) const {
        bool res = true;
        if (r >= RowCount() || r < 0) res = false;
        if (c >= ColumnCount() || r < 0) res = false;
        return res;
    }

    /// @brief Verifies if all row vectors have the same dimension.
    /// @param rows The row vectors to check.
    /// @param dim [out] The common dimension of the rows, if they are consistent.
    /// @return True if all rows have the same dimension; otherwise, false.
    bool MatrixMxN::validateDimensions(std::vector<Vector> &rows, int &dim) const {
        bool res = true;
        dim = rows[0].Dimension();
        for (int i = 0; i < RowCount() - 1; i++) {
            Vector v = rows[i];
            if (dim != v.Dimension()) res = false;
        }
        if (!res) dim = -1;
        return res;
    }

    Vector* MatrixMxN::GetRow(int &r) {
        if (validateIndex(r)) {
            Vector* v = new Vector();
            for (int i = 0; i < rows[r].Dimension() - 1; i++) {
                v->AddValue(rows[r][i]);
            }
            return v;
        }
        else throw new std::out_of_range("Index " + std::to_string(r) + " is out of range.");
    }
}