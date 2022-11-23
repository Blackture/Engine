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

        public Vector GlobalPosition { get; }
        public Vector Rotation { get; }
        public Vector Scale { get; }
    }
}
