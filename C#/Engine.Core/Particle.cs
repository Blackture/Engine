using Engine.Core.Maths;
using Engine.Core.Physics.CollisionDetection;
using Engine.Core.SceneManagement;
using Engine.Core.Components;
using Engine.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Core
{
    public class Particle
    {
        private Vector3 position;
        private Event<ParticleCollision3D> onCollision;
        public Vector3 Position { get { return position; } }

        private List<ParticleCollision3D> particleCollisions;

        public Particle(Vector3 position, bool eliminateOnHit = false)
        {
            onCollision = new Event<ParticleCollision3D>();
            onCollision.AddListener(OnCollision);
            particleCollisions = new List<ParticleCollision3D>();
            SetPosition(position);
        }

        ~Particle()
        {
            position = null;
        }

        public void SetPosition(Vector3 newPosition)
        {
            position = newPosition;
            Update();
        }

        public void Update()
        {
            Scene s = SceneManager.Instance.ActiveScene;
            if (s.Type == SceneType.Scene3D)
            {
                if (s.GetGlobalCoordinateSystem(out GCS3 gcs))
                {
                    //Excluding meshes far away
                    List<Mesh3D> meshesSort1 = gcs.SpatialPartitioning.GetMeshesAt(Position);
                    Dictionary<Guid, int> depthGroup = new Dictionary<Guid, int>();
                    foreach (Mesh3D mesh in meshesSort1)
                    {
                        LCS3Element element = gcs.GetLCS3Element(mesh.Dependency.Local);
                        if (!depthGroup.ContainsKey(element.GroupID))
                        {
                            depthGroup.Add(element.GroupID, element.Depth);
                        }
                        if (depthGroup[element.GroupID] > element.Depth) depthGroup[element.GroupID] = element.Depth;
                    }
                    //Find Meshes in Bounding Volumes nearby
                    List<Mesh3D> meshesSort2 = new List<Mesh3D>();
                    foreach (KeyValuePair<Guid, int> dg in depthGroup)
                    {
                        if (gcs.GetGroup(dg.Key).InBoundingsMesh(dg.Value, Position, out Mesh3D[] _meshes))
                        {
                            meshesSort2.AddRange(_meshes);
                        }
                    }
                    //Take only meshes that are found in bounding volumes nearby and weren't excluded the first time
                    List<Mesh3D> meshesSort3 = new List<Mesh3D>();
                    for (int i = 0; i < meshesSort1.Count; i++)
                    {
                        if (meshesSort2.Contains(meshesSort1[i])) meshesSort3.Add(meshesSort1[i]);
                    }
                    //CheckForCollision
                    foreach(Mesh3D mesh in meshesSort3)
                    {
                        
                    }
                }
            }
        }

        public List<ParticleCollision3D> GetCollisions()
        {
            return particleCollisions;
        }

        private void OnCollision(ParticleCollision3D collision)
        {
            particleCollisions.Add(collision);
        }
    }
}
