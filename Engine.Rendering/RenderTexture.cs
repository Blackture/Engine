using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using Engine.Core.Maths;
using Engine.Core.Components;
using Engine.Core;

namespace Engine.Rendering
{
    public class RenderTexture
    {
        private int textureWidth, textureHeight;
        private int halfTextureWidth, halfTextureHeight;
        private float pixelDensity;
        private float pd;

        public RenderTexture(PictureBox pictureBox, float pixelDensity = 90)
        {
            Image img = pictureBox.Image;
            textureWidth = img.Size.Width;
            textureHeight = img.Size.Height;
            halfTextureHeight = Mathf.RoundToInt(textureHeight / 2);
            halfTextureWidth = Mathf.RoundToInt(textureWidth / 2);
            this.pixelDensity = pixelDensity;
            pd = 1 / pixelDensity;
        }

        public Pixel RenderPixel(float x, float y, Camera camera)
        {
            float ds = ((x - halfTextureWidth) * pixelDensity) / halfTextureWidth;
            float dt = ((y - halfTextureHeight) * pixelDensity) / halfTextureHeight;

            return camera.RenderPoint(dt, ds);
        }
    }
}
