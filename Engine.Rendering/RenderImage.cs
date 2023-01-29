using System.Windows.Forms;
using System.Drawing;
using Engine.Core.Maths;
using System.Security.Cryptography.X509Certificates;
using System.Collections.Generic;
using System;

namespace Engine.Rendering
{
    public class RenderImage
    {
        private PictureBox pictureBox;
        private Bitmap bitmap;
        private Graphics renderGraphics;
        private Projection projection;

        public RenderImage(Projection projection, ref PictureBox pictureBox)
        {
            this.projection = projection;
            this.pictureBox = pictureBox;
            bitmap = new Bitmap(pictureBox.Width, pictureBox.Height);
            renderGraphics = Graphics.FromImage(bitmap);
        }

        public void Render()
        {
            // Example 3D scene
            Vector[] scene = new Vector[] {
                new Vector(1, 1, 1),
                new Vector(2, 2, 2),
                new Vector(3, 3, 3)
            };

            foreach (Vector vector in scene)
            {
                Vector projectedVector = Project(vector, Vector.Zero, 90);
                try
                {
                    int x = (int)projectedVector.X1;
                    int y = (int)projectedVector.X2;
                    renderGraphics.FillRectangle(Brushes.Red, x, y, 5, 5);
                }
                catch (OverflowException ex)
                {
                    // Handle the overflow exception here.
                    // For example, you could log the error or display a message to the user.
                }
            }

            pictureBox.Image = bitmap;
        }

        private Vector Project(Vector vector, Vector cameraPosition, int fov)
        {
            Vector translatedVector = vector - cameraPosition;
            Vector projectedVector = new Vector(0, 0, 0);

            switch (projection)
            {
                case Projection.Perspective:
                    float distance = fov / (float)translatedVector.X3;
                    projectedVector.X1 = distance * translatedVector.X1;
                    projectedVector.X2 = distance * translatedVector.X2;
                    break;
                case Projection.Orthographic:
                    projectedVector.X1 = translatedVector.X1;
                    projectedVector.X2 = translatedVector.X2;
                    break;
                default:
                    throw new ArgumentException("Invalid projection type");
            }

            return projectedVector;
        }
    }
}