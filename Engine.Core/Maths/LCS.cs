using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Core.Maths
{
    //Local Coordinate System
    public class LCS
    {
        public readonly Straight[] axis = new Straight[3] { Straight.x1_Axis, Straight.x2_Axis, Straight.x3_Axis };

        public Vector GlobalPosition { get => globalPosition; set => globalPosition = value; }
        public Vector LocalPosition { get => localPosition; set => localPosition = value; }
        public Vector Rotation { get => rotation; set => rotation = value; }
        public Vector Scale { get => scale; set => scale = value;  }

        private Vector globalPosition;
        private Vector localPosition;
        private Vector rotation;
        private Vector scale;

        public LCS() { }
    }
}
