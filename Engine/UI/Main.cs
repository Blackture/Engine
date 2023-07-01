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
            Matrix3x3 matrix1 = new Matrix3x3(1, 1, 1, 1, 1, 1, 1, 1, 1);
            Matrix3x3 matrix2 = new Matrix3x3(2, 2, 2, 2, 2, 2, 2, 2, 2);
            Matrix m = new Matrix(new List<Vector> { new Vector(2,-1,3), new Vector(0,5,2), new Vector(1,-1,-2) });
            bool test = m.GetAdjointMatrix(out Matrix n);
            MessageBox.Show(test.ToString());
            Matrix res = n;
            //MessageBox.Show($@"[{res[0, 0],0}|{res[0, 1],0}|{res[0, 2],0}]
            //[{res[1, 0],0}|{res[1, 1],0}|{res[1, 2],0}]
            //[{res[2, 0],0}|{res[2, 1],0}|{res[2, 2],0}]");
            MessageBox.Show($@"             [{res[0, 0],0}|{res[0, 1],0}|{res[0, 2],0}]
            [{res[1, 0],0}|{res[1, 1],0}|{res[1, 2],0}]
            [{res[2, 0],0}|{res[2, 1],0}|{res[2, 2],0}]");
            MessageBox.Show($"{res[1,2]}");
        }
    }
}
