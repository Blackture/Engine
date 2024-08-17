using Engine.Core.Maths;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Core.Physics.Optics
{
    public class Photon : Particle
    {
        private int maxBounces;

        public Photon(Vector3 position) : base(position) 
        {

        }
    }
}
