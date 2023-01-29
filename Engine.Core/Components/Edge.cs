using Engine.Core.Maths;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Core.Components
{
    public class Edge : Component
    {
        public Vertex A;
        public Vertex B;

        private Straight s;

        public Edge(Vertex A, Vertex B, Object3D dependency) : base(dependency)
        {
            s = new Straight(A.LocalPosition, B.LocalPosition, Straight.LineSetupType._2Points);
        }

        /// <summary>
        /// Gets a position vector on the edge
        /// t needs to be a value between 0 and 1
        /// </summary>
        /// <param name="t"></param>
        /// <returns>Returns a <see cref="Vertex"></see>.</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public Vertex GetVertexOnEdge(float t)
        {
            if (0 <= t && t <= 1)
            {
                return new Vertex(s.GetPointAt(t), dependency);
            }
            else throw new ArgumentOutOfRangeException("T is out of range.");
        }
    }
}
