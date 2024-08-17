using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using Engine.Core.Components;
using Engine.Core.Maths;
using Engine.Core.Physics.CollisionDetection;

namespace Engine.Core
{
    public class LCS3ElementGroup
    {
        private Guid groupID;
        private Dictionary<LCS3Element, int> elementsByDepth;
        private List<LCS3Element> elements;
        private int maxDepth;
        private BoundingBox3D bounding;
        private List<BoundingBox3D> boundingByDepth;
        private GCS3 gcs;

        public Guid GroupID => groupID;
        /// <summary>
        /// Amount of layers the parent tree diagram has.
        /// </summary>
        public int MaxDepth
        {
            get { return maxDepth; }
            private set { maxDepth = value; }
        }
        public BoundingBox3D Bounding => bounding;

        public LCS3ElementGroup(Guid groupID, GCS3 dependency)
        {
            this.groupID = groupID;
            elements = new List<LCS3Element>();
            elementsByDepth = new Dictionary<LCS3Element, int>();
            boundingByDepth = new List<BoundingBox3D>();
            gcs = dependency;
        }
        public LCS3ElementGroup(Guid groupID, List<LCS3Element> elements, GCS3 dependency)
        {
            this.groupID = groupID;
            this.elements = elements;
            elementsByDepth = new Dictionary<LCS3Element, int>();
            gcs = dependency;
            foreach (LCS3Element e in elements)
            {
                elementsByDepth.Add(e, e.Depth);
            }
        }

        private void CheckForNewMaxDepth(int depth, bool removeOperation = false)
        {
            if (removeOperation)
            {
                if (!elementsByDepth.ContainsValue(depth) && depth == MaxDepth)
                {
                    MaxDepth--;
                }
                
            }
            else if (MaxDepth < depth) MaxDepth = depth;
        }

        /// <summary>
        /// Calculates the new boundings of a <paramref name="depth"/> and recalculates all parental layers.
        /// </summary>
        /// <param name="depth"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        private BoundingBox3D CalculateBoudnings(int depth)
        {
            BoundingBox3D res = null;
            List<BoundingBox3D> allBoundings = new List<BoundingBox3D>();

            if (depth == MaxDepth)
            {
                List<LCS3> allLcs = GetAllLCS3sByDepth(depth);
                List<LCS3> allLCSWithMeshs = allLcs.FindAll(x => x.Dependency.GetComponent<Mesh3D>() != null);
                foreach (LCS3 lcs in allLCSWithMeshs)
                {
                    allBoundings.Add(BoundingBox3D.GetBoundingBox(lcs.Dependency.GetComponent<Mesh3D>()));
                }
                BoundingVolume3D volume = new BoundingVolume3D(allBoundings);
                if (boundingByDepth.Count <= depth) boundingByDepth.Add(volume.Boundings);
                else boundingByDepth[depth] = volume.Boundings;
                res = volume.Boundings;
            }
            else if (depth < MaxDepth)
            {
                allBoundings.Add(new BoundingBox3D(boundingByDepth[depth + 1]));
                List<LCS3> allLcs = GetAllLCS3sByDepth(depth);
                List<LCS3> allLCSWithMeshs = allLcs.FindAll(x => x.Dependency.GetComponent<Mesh3D>() != null);
                foreach (LCS3 lcs in allLCSWithMeshs)
                {
                    allBoundings.Add(BoundingBox3D.GetBoundingBox(lcs.Dependency.GetComponent<Mesh3D>()));
                }
                BoundingVolume3D volume = new BoundingVolume3D(allBoundings);
                if (boundingByDepth.Count <= depth) boundingByDepth.Add(volume.Boundings);
                else boundingByDepth[depth] = volume.Boundings;
                res = volume.Boundings;
            }
            else throw new ArgumentOutOfRangeException($"{depth} was out of range.");


            if (depth > 1)
            {
                for (int i = depth - 1; i >= 0; i--)
                {
                    boundingByDepth[i] = CalculateBoudnings(i);
                }
            }
            else if (depth == 1)
            {
                boundingByDepth[0] = CalculateBoudnings(0);
            }
            bounding = boundingByDepth[0];

            return res;
        }

        public bool Contains(LCS3Element element) => elements.Contains(element);
        public void Add(LCS3Element element)
        {
            elements.Add(element);
            CheckForNewMaxDepth(element.Depth);
            if (element.Lcs.Dependency.GetComponent<Mesh3D>() != null)
            {
                CalculateBoudnings(element.Depth);
            }
        }
        public void Remove(LCS3Element element)
        {
            elements.Remove(element);
            elementsByDepth.Remove(element);
            if (element.Lcs.Dependency.GetComponent<Mesh3D>() != null)
            {
                CalculateBoudnings(element.Depth);
            }
            CheckForNewMaxDepth(element.Depth, true);
        }
        public List<LCS3Element> GetAllByDepth(int depth) => elements.FindAll(e => e.Depth == depth);
        public List<LCS3> GetAllLCS3sByDepth(int depth)
        {
            List<LCS3Element> elements = GetAllByDepth(depth);
            List<LCS3> res = new List<LCS3>();
            foreach (LCS3Element e in elements)
            {
                res.Add(e);
            }
            return res;
        }
        public BoundingBox3D GetBounding(int depth)
        {
            BoundingBox3D res;
            if (depth < boundingByDepth.Count)
            {
                res = boundingByDepth[depth];
            }
            else throw new ArgumentOutOfRangeException($"{depth} was out of range.");
            return res;
        }

        /// <summary>
        /// The InBoundingsMesh method checks if a given global position is within a hierarchy of bounding volumes at different depths, and returns an array of all of those bounding volumes through its out parameter, containing only bounding volumes that contain a mesh
        /// </summary>
        /// <param name="depth"></param>
        /// <param name="globalPosition"></param>
        /// <param name="hits"></param>
        /// <returns>A bool indicating whether or not any bounding volumes were found.</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public bool InBoundingsMesh(int depth, Vector3 globalPosition, out Mesh3D[] hits)
        {
            bool res = false;
            List<Mesh3D> meshes = new List<Mesh3D>();
            if (depth >= boundingByDepth.Count) throw new ArgumentOutOfRangeException("Depth was out of range.");
            if (boundingByDepth[depth].Contains(globalPosition))
            {
                if (ContainsMesh(depth, out List<Mesh3D> meshs))
                {
                    meshes.AddRange(meshs);
                }

                if (depth < MaxDepth)
                {
                    if (InBoundingsMesh(depth + 1, globalPosition, out Mesh3D[] ms))
                    {
                        meshes.AddRange(ms);
                    }
                }
                res = meshes.Count > 0;
            }
            hits = meshes.ToArray();
            return res;
        }

        public bool ContainsMesh(int depth, out List<Mesh3D> meshes)
        {
            meshes = new List<Mesh3D>();
            List<LCS3> ls = GetAllLCS3sByDepth(depth).FindAll(x => x.Dependency.GetComponent<Mesh3D>() != null);
            foreach (LCS3 l in ls)
            {
                meshes.Add(l.Dependency.GetComponent<Mesh3D>());
            }
            return ls.Count > 0 && ls != null;
        }
    }
}