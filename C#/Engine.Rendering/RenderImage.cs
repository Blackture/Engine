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
            Vector3[] scene = new Vector3[] {
                new Vector3(1, 1, 1),
                new Vector3(2, 2, 2),
                new Vector3(3, 3, 3)
            };

            foreach (Vector3 vector in scene)
            {
                Vector3 projectedVector = Project(vector, Vector3.Zero, 90);
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

        private Vector3 Project(Vector3 vector, Vector3 cameraPosition, int fov)
        {
            Vector3 translatedVector = vector - cameraPosition;
            Vector3 projectedVector = new Vector3(0, 0, 0);

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