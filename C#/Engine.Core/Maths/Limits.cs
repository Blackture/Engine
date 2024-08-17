using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Core.Maths
{
    public struct Limits
    {
        public float lowerLimit;
        public float upperLimit;
        /// <summary>
        /// The increment is the internal step width
        /// </summary>
        public float increment;

        public bool Contains(float f)
        {
            bool res = false;
            if (f >= lowerLimit && f <= upperLimit)
            {
                res = true;
                if (increment != 0)
                {
                    float fbyi = f / increment;
                    int x = Mathf.RoundToInt(fbyi);
                    float y = fbyi - x;
                    res = Mathf.Approximately(y, 0);
                }
            }
            return res;
        }
    }
}
