using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Engine.Core.Maths;
using Engine.Rendering;

namespace Engine.Display
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            RenderImage renderImage = new RenderImage(Projection.Orthographic, ref viewPort);
            renderImage.Render();
        }
    }
}
