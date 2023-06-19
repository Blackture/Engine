using Engine.Core.Maths;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Core.Components
{
    public class Face : Component
    {
        private Vertex[] vertices;
        private Edge[] edges;
        private Plane plane;
        private float[] limits;

        public Vector3 NormalVector => plane.N.Normalized;

        public Face(Vertex[] vertices, Edge[] edges, Object3D dependency) : base(dependency)
        {
            if (!SetFace(vertices, edges))
            {
                throw new ArgumentOutOfRangeException();
            }
        }

        public Vertex GetVertexOnFace(float s, float t)
        {
            if (s >= limits[0] && s <= limits[1] && t >= limits[2] && t <= limits[3])
            {
                Vector3 v = plane.GetPositionVectorAt(s, t);
                return new Vertex(v, dependency);
            }
            else throw new ArgumentOutOfRangeException();
        }

        /// <summary>
        /// Setups the face and returns if the operation was successfull
        /// </summary>
        /// <param name="vertices"></param>
        /// <param name="edges"></param>
        /// <returns>Returns true if the face is set correctly</returns>
        public bool SetFace(Vertex[] vertices, Edge[] edges)
        {
            bool res = false;
            if (3 <= vertices.Length && vertices.Length <= 4)
            {
                if (3 <= edges.Length && edges.Length <= 4)
                {
                    this.vertices = vertices;
                    this.edges = edges;
                    plane = new Plane(vertices[0].LocalPosition, vertices[1].LocalPosition, vertices[2].LocalPosition, Plane.PlaneSetupType._3PositionVectors);
                    Vector3[] v = new Vector3[vertices.Length];
                    for (int i = 0; i < v.Length; i++)
                    {
                        v[i] = vertices[i].LocalPosition;
                    }
                    limits = GetSTLimits(v);
                    res = true;
                }           
            }
            return res;
        }

        private float[] GetSTLimits(Vector3[] points)
        {
            float[] limits = new float[4];

            // normal = normalized plane normal
            Vector3 normal = plane.N.Normalized;

            // calculate the limits for s and t
            limits[0] = float.MaxValue;
            limits[1] = float.MinValue;
            limits[2] = float.MaxValue;
            limits[3] = float.MinValue;
            for (int i = 0; i < points.Length; i++)
            {
                float s = (normal.X1 * points[i].X1 + normal.X2 * points[i].X2 + normal.X3 * points[i].X3 + plane.B) / (normal.X1 * plane.N.X1 + normal.X2 * plane.N.X2 + normal.X3 * plane.N.X3);
                float t = (plane.N.X2 * points[i].X1 - plane.N.X1 * points[i].X2) / (plane.N.X1 * normal.X2 - plane.N.X2 * normal.X1);
                limits[0] = Math.Min(limits[0], s);
                limits[1] = Math.Max(limits[1], s);
                limits[2] = Math.Min(limits[2], t);
                limits[3] = Math.Max(limits[3], t);
            }
            return limits;
        }
    }
}
