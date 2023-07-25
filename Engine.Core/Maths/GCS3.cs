using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Core.Maths;
using Engine.Core.Components;
using Engine.Core.Physics.Optics;

namespace Engine.Core.Maths
{
    //Global Coordinate System
    public class GCS3
    {
        public static readonly Straight[] Axes = new Straight[3] { Straight.x1_Axis, Straight.x2_Axis, Straight.x3_Axis };

        public List<LCS3> LocalSystems = new List<LCS3>();
        public List<LightSource> LightSources = new List<LightSource>();
        
        public GCS3() 
        {
            
        }

        public void Add(LCS3 system)
        {
            LocalSystems.Add(system);
        }

        public void Remove(LCS3 system)
        {
            LocalSystems.Remove(system);
        }

        public void Add(LightSource light)
        {
            LightSources.Add(light);
        }

        public void Remove(LightSource light)
        { 
            LightSources.Remove(light);
        }
    }
}
