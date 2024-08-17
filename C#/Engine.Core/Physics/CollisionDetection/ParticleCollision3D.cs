using Engine.Core.Components;
using Engine.Core.Maths;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Core.Physics.CollisionDetection
{
    public readonly struct ParticleCollision3D
    {
        private readonly Vector3 at;
        private readonly Object3D object3D;
        private readonly Mesh3D meshHit;
        private readonly Face3D faceHit;

        /// <summary>
        /// Global position of the collision
        /// </summary>
        public Vector3 At { get { return at; } }
        public Object3D Object { get { return object3D; } }
        /// <summary>
        /// The mesh that was involved in the collision
        /// </summary>
        public Mesh3D MeshHit { get { return meshHit; } }
        /// <summary>
        /// The specific face of the mesh that was involved in the collision
        /// </summary>
        public Face3D Face { get { return faceHit; } }

        public ParticleCollision3D(Vector3 at, Object3D object3D, Mesh3D meshHit, Face3D faceHit)
        {
            this.at = at;
            this.object3D = object3D;
            this.meshHit = meshHit;
            this.faceHit = faceHit;
        }
    }
}
