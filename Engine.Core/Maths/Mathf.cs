using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Core.Maths
{
    public static class Mathf
    {
        public const float e = (float)Math.E;
        public const float pi = (float)Math.PI;

        public static float Acos(float f)
        {
            return Convert.ToSingle(Math.Acos(f));
        }

        public static float Asin(float f)
        {
            return Convert.ToSingle(Math.Asin(f));
        }

        public static float Atan(float f)
        {
            return Convert.ToSingle(Math.Atan(f));
        }

        public static float Abs(float f)
        {
            return Math.Abs(f);
        }

        public static float Atan2(float x, float y)
        {
            return Convert.ToSingle(Math.Atan2(x, y));
        }

        public static float Ceiling(float f)
        {
            return Convert.ToSingle(Math.Ceiling(f));
        }

        public static float Cos(float f)
        {
            return Convert.ToSingle(Math.Cos(f));
        }

        public static float Cosh(float f)
        {
            return Convert.ToSingle(Math.Cosh(f));
        }

        public static float Exp(float f)
        {
            return Convert.ToSingle(Math.Exp(f));
        }

        public static float Floor(float f)
        {
            return Convert.ToSingle(Math.Floor(f));
        }

        public static float IEEERemainder(float x, float y)
        {
            return Convert.ToSingle(Math.IEEERemainder(x, y));
        }

        public static float Log(float f)
        {
            return Convert.ToSingle(Math.Log(f));
        }

        public static float Log(float f, float newBase)
        {
            return Convert.ToSingle(Math.Log(f, newBase));
        }

        public static float Log10(float f)
        {
            return Convert.ToSingle(Math.Log10(f));
        }

        public static float Max(float val1, float val2)
        {
            return Math.Max(val1, val2);
        }

        public static float Min(float val1, float val2)
        {
            return Math.Min(val1, val2);
        }

        public static float Pow(float b, float p)
        {      
            return Convert.ToSingle(Math.Pow(b, p));
        }

        public static float Round(float f)
        {
            return Convert.ToSingle(Math.Round(f));
        }

        public static float Round(float f, int digits)
        {
            return Convert.ToSingle(Math.Round(f, digits));
        }

        public static float Round(float f, int digits, MidpointRounding midpointRounding)
        {
            return Convert.ToSingle(Math.Round(f, digits, midpointRounding));
        }

        public static int Sign(float f)
        {
            return Math.Sign(f);
        }

        public static float Sin(float f)
        {
            return Convert.ToSingle(Math.Sin(f));
        }

        public static float Sinh(float f)
        {
            return Convert.ToSingle(Math.Sinh(f));
        }

        public static float Sqrt(float f)
        {
            
            return Convert.ToSingle(Math.Sqrt(f));
        }

        public static float NthSqrt(float f, int n)
        {
            return Convert.ToSingle(Math.Pow((float)f, 1.0f / n));
        }

        public static float Tan(float f)
        {
            return Convert.ToSingle(Math.Tan(f));
        }

        public static float Tanh(float f)
        {
            return Convert.ToSingle(Math.Tanh(f));
        }

        public static float Truncate(float f)
        {
            return Convert.ToSingle(Math.Truncate(f));
        }

        public static float Pi(Func<float, float> innerFunction, int limit1, int limit2)
        {
            float res = 0.0f;
            for (float i = limit1; i < limit2; i++)
            {
                res *= innerFunction(i);
            }
            return res;
        }

        public static float Pi(Func<float,float> innerFunction, float limit1, float limit2, float increment)
        {
            float res = 0.0f;
            for (float i = limit1; i < limit2; i +=  increment)
            {
                res *= innerFunction(i);
            }
            return res;
        }

        public static float Sigma(Func<float, float> innerFunction, float limit1, float limit2, float increment)
        {
            float res = 0.0f;
            for (float i = limit1; i < limit2; i += increment)
            {
                res += innerFunction(i);
            }
            return res;
        }

        public static float Sigma(Func<float, float> innerFunction, int limit1, int limit2)
        {
            float res = 0.0f;
            for (float i = limit1; i < limit2; i++)
            {
                res += innerFunction(i);
            }
            return res;
        }

        public static bool Approximately(float a, float b, float tolerance = 0.0001f)
        {
            return Math.Abs(a - b) < tolerance;
        }

        public static bool Approximately(float a, float b)
        {
            return Math.Abs(a - b) < float.Epsilon;
        }

        public static float Deg2Rad(float deg)
        {
            return deg * pi / 180;
        }
    }
}
