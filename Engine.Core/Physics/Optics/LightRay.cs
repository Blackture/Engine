using Engine.Core.Maths;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Core.Physics.Optics
{
    public class LightRay : Ray
    {
        private int particleCount;

        private List<Photon> photons;

        public LightRay(Straight ray, float rayStart) : base(ray, rayStart)
        {
        }
        public LightRay(Straight ray, float rayStart, float samplingRate) : base(ray, rayStart, samplingRate)
        {
        }
    }
}
