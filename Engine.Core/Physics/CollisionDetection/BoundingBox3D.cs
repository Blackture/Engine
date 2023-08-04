using Engine.Core.Components;
using Engine.Core.Maths;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Core.Physics.CollisionDetection
{
    public class BoundingBox3D
    {
        private int boundingDepth;

        public float maxX1;
        public float maxX2;
        public float maxX3;
        public float minX1;
        public float minX2;
        public float minX3;

        public int BoundingDepth => boundingDepth;

        public BoundingBox3D(float maxX1, float maxX2, float maxX3, float minX1, float minX2, float minX3)
        {
            this.maxX1 = maxX1;
            this.maxX2 = maxX2;
            this.maxX3 = maxX3;
            this.minX1 = minX1;
            this.minX2 = minX2;
            this.minX3 = minX3;
        }
        public BoundingBox3D(BoundingBox3D box)
        {
            maxX1 = box.maxX1;
            maxX2 = box.maxX2;
            maxX3 = box.maxX3;
            minX1 = box.minX1;
            minX2 = box.minX2;
            minX3 = box.minX3;
        }

        /// <summary>
        /// Checks if the position vector is within the boundaries.
        /// </summary>
        /// <param name="globalPosition"></param>
        /// <returns>True if the position is within the boundaries; else false</returns>
        public bool Contains(Vector3 globalPosition)
        {
            bool resX1 = false;
            bool resX2 = false;
            bool resX3 = false;

            if (globalPosition.X1 >= minX1 && globalPosition.X1 <= maxX1) resX1 = true;
            if (globalPosition.X2 >= minX2 && globalPosition.X2 <= maxX2) resX2 = true;
            if (globalPosition.X3 >= minX3 && globalPosition.X3 <= maxX3) resX3 = true;

            return resX1 && resX2 && resX3;
        }

        public static BoundingBox3D GetBoundingBox(Mesh3D mesh)
        {
            List<Vertex3D> vertices = mesh.GetAllVertices();
            float maxX1 = vertices[0].GlobalPosition.X1;
            float maxX2 = vertices[0].GlobalPosition.X2;
            float maxX3 = vertices[0].GlobalPosition.X3;
            float minX1 = vertices[0].GlobalPosition.X1;
            float minX2 = vertices[0].GlobalPosition.X2;
            float minX3 = vertices[0].GlobalPosition.X3;
            vertices.RemoveAt(0);

            foreach (Vertex3D v in vertices)
            {
                maxX1 = (v.GlobalPosition.X1 > maxX1) ? v.GlobalPosition.X1 : maxX1;
                maxX2 = (v.GlobalPosition.X2 > maxX2) ? v.GlobalPosition.X2 : maxX2;
                maxX3 = (v.GlobalPosition.X3 > maxX3) ? v.GlobalPosition.X3 : maxX3;

                minX1 = (v.GlobalPosition.X1 < minX1) ? v.GlobalPosition.X1 : minX1;
                minX2 = (v.GlobalPosition.X2 < minX2) ? v.GlobalPosition.X2 : minX2;
                minX3 = (v.GlobalPosition.X3 < minX3) ? v.GlobalPosition.X3 : minX3;
            }

            return new BoundingBox3D(maxX1, maxX2, maxX3, minX1, minX2, minX3);
        }

        public static implicit operator BoundingBox3D(Mesh3D m) { return GetBoundingBox(m); }
    }
}
