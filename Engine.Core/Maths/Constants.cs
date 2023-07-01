using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Core.Maths
{
    public class Constants
    {
        public static readonly float PiQuarter;

        static Constants()
        {
            PiQuarter = Mathf.Sin(Mathf.pi / 4.0f);
        }
    }
}
