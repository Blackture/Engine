using Engine.Core.Maths;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Engine.Rendering
{
    public static class RenderingConstants
    {
        /// <summary>
        /// Defines the Unit size in units
        /// </summary>
        public const int Unit = 1;
        public static readonly float Sin135 = Mathf.Sin(135);

        /// <summary>
        /// Only applies when rendering with the orthographic perspective
        /// UnitXY multiplied with sqrt(2) / 2
        /// </summary>
        public static int _screenHeight;
        public static int _screenWidth;
        public static int _halfScreenHeight;
        public static int _halfScreenWidth;
        public static int FoV = 90;

        public static int ScreenHeight { get { return _screenHeight; } set { _screenHeight = value; _halfScreenHeight = (int)Mathf.Round(value / 2.0f, 0, MidpointRounding.AwayFromZero); } }
        public static int ScreenWidth { get { return _screenWidth; } set { _screenWidth = value; _halfScreenWidth = (int)Mathf.Round(value / 2.0f, 0, MidpointRounding.AwayFromZero); } }
        public static int HalfScreenHeight { get { return _halfScreenHeight; } }
        public static int HalfScreenWidth { get { return _halfScreenWidth; } }
    }
}
