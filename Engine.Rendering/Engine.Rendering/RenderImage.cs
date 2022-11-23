using System.Windows.Forms;
using System.Drawing;
using Engine.Core.Maths;
using System.Security.Cryptography.X509Certificates;

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
            bitmap = new Bitmap(pictureBox.Width, pictureBox.Height);
            renderGraphics = Graphics.FromImage(bitmap);
        }

        public void Render()
        {
            int[] p = ConvertPoint(0, 0, 5);
            renderGraphics.FillEllipse(Brushes.DarkRed, p[0], p[1], 5, 5);
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
            if (Mathf.Abs(y) + thickness <= pictureBox.Height && x <= pictureBox.Width)
            {
                return new int[] { x, pictureBox.Height - y - thickness };
            }
            else return null;
        }

        public int[] ProjectTo2D(Vector p)
        {
            return null;
        }
    }
}