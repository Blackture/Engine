using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Core.Maths;
using Engine.Core.Components;
using Engine.Core.Physics.Optics;
using Engine.Core.Physics.CollisionDetection;
using System.Dynamic;
using System.Runtime.Serialization;

namespace Engine.Core.Maths
{
    //Global Coordinate System
    public class GCS3
    {
        public static readonly Straight[] Axes = new Straight[3] { Straight.x1_Axis, Straight.x2_Axis, Straight.x3_Axis };

        private List<LCS3> localSystems; //local systems that don't have a Parent
        private List<LightSource> lightSources;
        private List<LCS3Element> registeredLocalSystems; //all local systems
        private List<LCS3ElementGroup> rlsg; //registered local systems grouped

        public List<LightSource> LightSources => lightSources;

        private SpatialPartitioning spatialPartitioning;

        public SpatialPartitioning SpatialPartitioning { get { return spatialPartitioning; } }
        
        public GCS3() 
        {
            localSystems = new List<LCS3>();
            lightSources = new List<LightSource>();
            rlsg = new List<LCS3ElementGroup>();
        }

        /// <summary>
        /// Adds a local system to the base of the scene
        /// </summary>s
        /// <param name="system"></param>
        public void AddLCSToBase(LCS3 system)
        {
            localSystems.Add(system);
        }
        /// <summary>
        /// Remove a local system from the base of the scene
        /// </summary>
        /// <param name="system"></param>
        public void RemoveFromBase(LCS3 system)
        {
            localSystems.Remove(system);
        }
        /// <summary>
        /// Register local system
        /// </summary>
        /// <param name="system"></param>
        public void RegisterLCS(LCS3 system)
        {
            Guid groupID;
            int depth = 0;
            if (system.Parent != null)
            {
                LCS3Element p = GetLCS3Element(system.Parent);
                groupID = p.GroupID;
                if (system.Parent.ChildCount > 1)
                {
                    LCS3 s = system.Parent.GetChilds().Find(x => x != system);
                    LCS3Element c = GetLCS3Element(s);
                    depth = c.Depth;
                }
                else
                {
                    depth = p.Depth + 1;
                }
            }
            else
            {
                groupID = Guid.NewGuid();
            }
            LCS3Element element = new LCS3Element(depth, system, system.Parent, groupID);
            registeredLocalSystems.Add(element);
            if (GroupExists(groupID))
            {
                GetGroup(groupID).Add(element);
            }
            else
            {
                LCS3ElementGroup group = new LCS3ElementGroup(groupID, this);
                group.Add(element);
                rlsg.Add(group);
            }
        }
        public void ReregisterAllLCS()
        {
            registeredLocalSystems.Clear();
            foreach (LCS3 system in localSystems) 
            {
                RegisterLCS(system);
                List<LCS3> systems = system.GetChilds();
                foreach (LCS3 s in systems)
                {
                    RegisterLCS(s);
                }
            }
        }
        public void ReregisterLCS(LCS3 system)
        {
            UnregisterLCS(system);
            RegisterLCS(system);
        }
        /// <summary>
        /// Unregister local system
        /// </summary>
        /// <param name="system"></param>
        public void UnregisterLCS(LCS3 system)
        {
            GetGroupContaining(system)?.Remove(GetLCS3Element(system));
            registeredLocalSystems.RemoveAt(GetLCS3ElementIndex(system));
        }
        public LCS3Element GetLCS3Element(LCS3 system)
        {
            return registeredLocalSystems.Find(x => x.Lcs == system);
        }
        public int GetLCS3ElementIndex(LCS3 system)
        {
            return registeredLocalSystems.FindIndex(x => x.Lcs == system);
        }
        public LCS3ElementGroup GetGroup(Guid GroupID)
        {
            return rlsg.Find(x => x.GroupID == GroupID);
        }
        public LCS3ElementGroup GetGroupContaining(LCS3 system)
        {
            LCS3Element element = GetLCS3Element(system);
            return GetGroup(element.GroupID);
        }
        public bool GroupExists(Guid GroupID)
        {
            return rlsg.Exists(x => x.GroupID == GroupID);
        }

        public void Add(LightSource light)
        {
            lightSources.Add(light);
        }

        public void Remove(LightSource light)
        { 
            lightSources.Remove(light);
        }
    }
}
