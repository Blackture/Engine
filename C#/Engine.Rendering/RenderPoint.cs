using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Engine.Rendering
{
    public class RenderPoint
    {
        public int X;
        public int Y;
        public Color Color;

        public RenderPoint(int x, int y, Color color)
        {
            X = x;
            Y = y;
            Color = color;
        }

        public void LerpColor(Color color, float t) 
        {
            Color = Color.Interpolate(color, t);
        }
    }
}
