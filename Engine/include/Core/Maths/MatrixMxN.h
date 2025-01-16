#ifndef MATRIXMXN_H
#define MATRIXMXN_H

#include <Mathf.h>
#include <Vector.h>
#include <Vector3.h>
#include <cmath>
#include <vector>
#include <Matrix3x3.h>

namespace Engine::Core::Maths
{
    class MatrixMxN
    {
        struct Accessor
        {
            public:
                __readonly int r;
                __readonly int c;
                __readonly float value;
                __readonly bool set;

                Accessor(int &row, int &col) {
                    r = row;
                    c = col;
                    set = false;
                }

                Accessor(int &row, int &col, float &val) {
                    r = row;
                    c = col;
                    value = val;
                    set = true;
                }
        };

        public:
            int RowCount() const { return rowCount; }
            int ColumnCount() const { return columnCount; }

            float operator[](Accessor &a) const;
            float &operator[](Accessor &a);

            MatrixMxN();
            MatrixMxN(std::vector<Vector> rows);
            MatrixMxN(int &r, int &c);

            Vector* GetRow(int &r);
            Vector* GetColumn(int &c);
            void AddRow(Vector &row);
            void AddColumn(Vector &col);
            void InsertRow(Vector &row, int &index);
            void RemoveRow(int &index);
            void RemoveColumn(int &index);
            void SwapRows(int &from, int &to);
            MatrixMxN ScalarMultiplication(float &f);
            MatrixMxN ScalarDivision(float &f);
            MatrixMxN Multiplication(MatrixMxN m);
            MatrixMxN Addition(MatrixMxN m);
            MatrixMxN Substraction(MatrixMxN m);
            Vector RowAddition(int &l, int &r);
            Vector RowSubstraction(int &l, int &r);
            void Transpose();
            bool GetSubMatrix(int &r, int &c, MatrixMxN &subMatrix);
            bool Get2x2SubMatrices(MatrixMxN m, std::vector<MatrixMxN> &subs);
            bool GetMinorMatrix(MatrixMxN &minor);
            bool GetCofactorMatrix(MatrixMxN &fac);
            bool GetDeterminant(float &det);
            bool GetAdjointMatrix(MatrixMxN &adj);
            bool ToVector3(Vector3 &v);
            bool ToVector(Vector &v);

            void RowOperation(int &target, int &source, MatrixOperation &operation, float f = 0); 
            MatrixMxN Operation(MatrixMxN &m, MatrixOperation &operation, float f = 0);

            static MatrixMxN ScalarMultiplication(MatrixMxN &n, float &f);
            static MatrixMxN ScalarDivision(MatrixMxN &n, float &f);
            static MatrixMxN Multiplication(MatrixMxN &n, MatrixMxN &m);
            static MatrixMxN Addition(MatrixMxN &n, MatrixMxN &m);
            static MatrixMxN Substraction(MatrixMxN &n, MatrixMxN &m);
            
            static void RowOperation(MatrixMxN &n, int &target, int &source, MatrixOperation operation, float f = 0);
            static MatrixMxN Multiplication(MatrixMxN &n, MatrixMxN &m, MatrixOperation operation, float f = 0);

            static MatrixMxN GetIdentityMatrix(int &n);
            static bool GetInverse(MatrixMxN &m, MatrixMxN &inverse);

            static MatrixMxN operator*(float &f, MatrixMxN &n);
            static MatrixMxN operator*(MatrixMxN &n, float &f);
            static MatrixMxN operator*(MatrixMxN &m, MatrixMxN &n);
            static MatrixMxN operator+(MatrixMxN &m, MatrixMxN &n);
            static MatrixMxN operator-(MatrixMxN &m, MatrixMxN &n);
            static MatrixMxN operator-(MatrixMxN &m);
            static MatrixMxN operator/(MatrixMxN &m, float &f);

            operator Matrix3x3() const;
        private:
            int rowCount;
            int columnCount;
            std::vector<Vector> rows;
            std::vector<Vector> cols;

            void instantiate(std::vector<Vector> rows);
            std::vector<Vector> getColumns();
            bool validateIndex(int &index, bool column = false) const;
            bool validateIndexes(int &r, int &c) const;
            bool validateDimensions(std::vector<Vector> &rows, int &count) const;
            float getValue(int &r, int &c) const;
            void setValue(int &r, int &c, float &value);

    };
}

#endif //MATRIXMXN_H