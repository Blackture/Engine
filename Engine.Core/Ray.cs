using Engine.Core.Maths;
using Engine.Core.Physics.CollisionDetection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Core
{
    public class Ray : IParticleHandler, IRay
    {
        private Straight3D ray;
        private float rayStart;
        private float samplingRate;
        private Particle particle;

        public Particle Particle { get { return particle; } }

        public Ray(Straight3D ray, float rayStart)
        {
            this.ray = ray;
            this.rayStart = rayStart;
            samplingRate = 1f;
        }
        public Ray(Straight3D ray, float rayStart, float samplingRate)
        {
            this.ray = ray;
            this.rayStart = rayStart;
            this.samplingRate = samplingRate;
        }
      
        public async Task<List<ParticleCollision3D>> Emit(float distance)
        {
            await Task.Run(() => Emission(distance, this));
            return particle.GetCollisions();
        }

        private void Emission(float distance, Ray ray) 
        {
            ray.particle = new Particle(ray.ray.GetPointAt(ray.rayStart));
            for (float particlePosition = 0; particlePosition < distance; particlePosition += ray.samplingRate)
            {
                ray.particle?.SetPosition(ray.ray.GetPointAt(ray.rayStart + particlePosition));
                if (Mathf.Approximately(particlePosition, distance)) ray.EliminateParticle(ref ray.particle);
            }
        }

        public virtual void EliminateParticle(ref Particle particle)
        {
            particle = null;
        }
        public virtual void EliminateAllParticles()
        {
            EliminateParticle(ref particle);
        }

        public void OnRayCollision(List<ParticleCollision3D> collisions) 
        {

        }
    }
}
