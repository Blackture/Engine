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

namespace Engine
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();

            Matrix a = new Matrix(new List<List<float>>()
            {
                new List<float>() { 1, 2, 3, 4 },
                new List<float>() { 4, 5, 6, 5 },
                new List<float>() { 7, 8, 9, 6 },
                new List<float>() { 7, 8, 9, 7 }
            });
            Matrix b = new Matrix(new List<List<float>>()
            {
                new List<float>() { 1, 2, 3, 4 },
                new List<float>() { 4, 5, 6, 5 },
                new List<float>() { 7, 8, 9, 6 },
                new List<float>() { 7, 8, 9, 7 }
            });
            Matrix c = a * b;

            if (c != null)
            {
                // Build a string representation of the matrix
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < c.RowCount; i++)
                {
                    for (int j = 0; j < c.ColumnCount; j++)
                    {
                        sb.Append(c[i, j]);
                        sb.Append(" ");
                    }
                    sb.AppendLine();
                }
                string matrixString = sb.ToString();

                // Show the matrix in a message box
                MessageBox.Show(matrixString);
            }
            else
            {
                MessageBox.Show("The matrices cannot be multiplied!");
            }

            RenderImage renderImage = new RenderImage(Projection.Orthographic, ref viewPort);
            renderImage.Render();
        }
    }
}
