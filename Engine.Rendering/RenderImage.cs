using System.Windows.Forms;
using System.Drawing;
using Engine.Core.Maths;
using System.Security.Cryptography.X509Certificates;
using System.Collections.Generic;
using System;
using static System.Windows.Forms.AxHost;

namespace Engine.Rendering
{
    public class RenderImage
    {
        private Projection projection;
        private PictureBox pictureBox;
        private Bitmap bitmap;
        private Graphics renderGraphics;

        public RenderImage(Projection projection, ref PictureBox pictureBox)
        {
            this.projection = projection;
            this.pictureBox = pictureBox;
            RenderingConstants.ScreenWidth = pictureBox.Width;
            RenderingConstants.ScreenHeight = pictureBox.Height;
            bitmap = new Bitmap(pictureBox.Width, pictureBox.Height);
            renderGraphics = Graphics.FromImage(bitmap);
        }

        public void Render()
        {
            OrthographicRendering(3, Straight.x1_Axis, 500 * (5 + Mathf.Sin(135)));
            OrthographicRendering(3, Straight.x2_Axis, 500);
            OrthographicRendering(3, Straight.x3_Axis, 500);
            renderGraphics.Save();
            pictureBox.Image = bitmap;
        }

        public int[] ConvertPoint(Point p)
        {
            if (p.Y <= pictureBox.Height && p.X <= pictureBox.Width)
            {
                return new int[] { p.X, pictureBox.Height - p.Y };
            }
            else return null;
        }

        public int[] ConvertPoint(int x, int y, int thickness)
        {
            if (x < RenderingConstants.ScreenWidth - thickness || x >= 0)
            {
                if (y < RenderingConstants.ScreenHeight - thickness || y >= 0)
                {
                    return new int[] { x, (int)Mathf.Round(pictureBox.Height - y - thickness / 2.0f, 0, MidpointRounding.AwayFromZero) };
                }
                else return null;
            }
            else return null;
        }

        public int[] Project(Vector v, int scale)
        {
            switch (projection)
            {
                case Projection.Orthographic:
                    float _x = RenderingConstants.HalfScreenWidth / 1.5f + (v.X2 * RenderingConstants.Unit * scale / 2 - v.X1 * RenderingConstants.Unit * scale / 2 * Mathf.Sin(135));
                    float _y = RenderingConstants.HalfScreenHeight / 1.5f + (v.X3 * RenderingConstants.Unit * scale / 2 - v.X1 * RenderingConstants.Unit * scale / 2 * Mathf.Sin(135));

                    int x = (int)Mathf.Round(_x, 0, MidpointRounding.AwayFromZero);
                    int y = (int)Mathf.Round(_y, 0, MidpointRounding.AwayFromZero);
                    return ConvertPoint(x, y, RenderingConstants.Unit * scale);
            }

            return null;
        }

        public void OrthographicRendering(int scale, Straight s, float d)
        {
            for (float i = 0; i <= d * RenderingConstants.Unit * scale; i += RenderingConstants.Unit * scale / 2)
            {
                int[] p = Project(s.GetPointAt(i), scale / 2);
                renderGraphics.FillEllipse(Brushes.Black, p[0], p[1], scale, scale);
            }
        }
    }
}