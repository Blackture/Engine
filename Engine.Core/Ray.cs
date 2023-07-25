using Engine.Core.Maths;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Core
{
    public class Ray
    {
        private Straight ray;
        private float rayStart;
        private float rayEnd;
        private float samplingRate;

        public Ray(Straight ray, float rayStart)
        {
            this.ray = ray;
            this.rayStart = rayStart;
            samplingRate = 1f;
        }
        public Ray(Straight ray, float rayStart, float samplingRate)
        {
            this.ray = ray;
            this.rayStart = rayStart;
            this.samplingRate = samplingRate;
        }

        public Particle Emit(float distance)
        {
            Particle particle = new Particle();
            for (float particlePosition = 0; particlePosition < distance; particlePosition += samplingRate)
            {
                particle.SetPosition(ray.GetPointAt(rayStart + particlePosition));
            }
            return particle;
        }
    }
}
