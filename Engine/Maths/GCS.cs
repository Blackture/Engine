using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Core.Maths
{
    //Global Coordinate System
    public class GCS
    {
        public Straight[] Axes = new Straight[3] { Straight.x1_Axis, Straight.x2_Axis, Straight.x3_Axis };

        public List<LCS> LocalSystems = new List<LCS>();
        
        public GCS() 
        {
            
        }
    }
}
