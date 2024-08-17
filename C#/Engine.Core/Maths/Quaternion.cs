using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Core.Maths
{
    [Obsolete("Quite non of it is implemented!")]
    public class Quaternion
    {
        private Vector3 x123;
        private float w;

        public float this[int i]
        {
            get { return GetValue(i, out float f) ? f : float.NaN; }
            set
            {
                SetValue(i, value);
            }
        }

        private bool ValidateIndex(int i)
        {
            return i >= 0 && i < 4;
        }
        public bool GetValue(int i, out float res)
        {
            res = 0;
            if (ValidateIndex(i))
            {
                if (i < 3)
                {
                    res = x123[i];
                }
                else
                {
                    res = w;
                }
            }
            else return false;
            return true;
        }
        public bool SetValue(int i, float value)
        {
            if (ValidateIndex(i))
            {
                if (i < 3)
                {
                    x123[i] = value;
                }
                else
                {
                    w = value;
                }
            }
            else return false;
            return true;
        }
    }
}
