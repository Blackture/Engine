using Engine.Core.Maths;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Core.Components
{
    public class Edge3D : Component
    {
        public Vertex3D A;
        public Vertex3D B;

        private Straight s;

        public Edge3D(Vertex3D A, Vertex3D B, Object3D dependency) : base(dependency)
        {
            s = new Straight(A.LocalPosition, B.LocalPosition, Straight.LineSetupType._2Points);
        }

        /// <summary>
        /// Gets a position vector on the edge
        /// t needs to be a value between 0 and 1
        /// </summary>
        /// <param name="t"></param>
        /// <returns>Returns a <see cref="Vertex3D"></see>.</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public Vertex3D GetVertexOnEdge(float t)
        {
            if (0 <= t && t <= 1)
            {
                return new Vertex3D(s.GetPointAt(t), Dependency);
            }
            else throw new ArgumentOutOfRangeException("T is out of range.");
        }
    }
}
