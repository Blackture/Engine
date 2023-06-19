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

        public Vector3 GlobalPosition { get => globalPosition; set => globalPosition = value; }
        public Vector3 LocalPosition { get => localPosition; set => localPosition = value; }
        public Vector3 Rotation { get => rotation; set => rotation = value; }
        public Vector3 Scale { get => scale; set => scale = value;  }

        private Vector3 globalPosition;
        private Vector3 localPosition;
        private Vector3 rotation;
        private Vector3 scale;

        public LCS() { }
    }
}
