using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Core.Physics.CollisionDetection;

namespace Engine.Core
{
    public interface IRay
    {
        void OnRayCollision(List<ParticleCollision3D> collisions);
    }
}
