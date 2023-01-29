using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Rendering
{
    public static class ColorExtension
    {
        public static Color Interpolate(this Color c1, Color c2, float t)
        {
            float r = c1.R + (c2.R - c1.R) * t;
            float g = c1.G + (c2.G - c1.G) * t;
            float b = c1.B + (c2.B - c1.B) * t;
            return Color.FromArgb((int)r, (int)g, (int)b);
        }
    }
}
