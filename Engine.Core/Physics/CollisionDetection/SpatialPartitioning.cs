using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Core.Maths;
using Engine.Core.Components;
using System.CodeDom;

namespace Engine.Core.Physics.CollisionDetection
{
    public class SpatialPartitioning
    {
        private Plane x1x2Plane;
        /// <summary>
        /// partitioning grid
        /// </summary>
        private Grid grid;
        /// <summary>
        /// Cell size of the grid in engine units.
        /// </summary>
        private float cellSize = 10f;

        public float CellSize
        {
            get => cellSize;
            set { cellSize = (value > 0) ? value : cellSize; }
        }

        public SpatialPartitioning()
        {
            x1x2Plane = new Plane(new Vector3(0, 0, 1), Vector3.Zero);
            grid = new Grid();
        }

        /// <summary>
        /// Projects <paramref name="point"/> onto the partitioning grid.
        /// </summary>
        /// <param name="point"></param>
        /// <returns>Return all meshes within the cell, the point's in</returns>
        public List<Mesh3D> GetMeshesAt(Vector3 point)
        {
            Vector3 projected = point.Project(x1x2Plane);
            Vector2 index = new Vector2(Mathf.Ceiling(projected.X1 / CellSize), Mathf.Ceiling(projected.X2 / CellSize));
            return grid.GetMeshesInCell(index);
        }

        public void MapMesh(Mesh3D mesh)
        {
            List<Vertex3D> vertices = mesh.GetAllVertices();
            foreach (Vertex3D vertex in vertices)
            {
                Vector3 projected = vertex.GlobalPosition.Project(x1x2Plane);
                Vector2 index = new Vector2(Mathf.Ceiling(projected.X1 / CellSize), Mathf.Ceiling(projected.X2 / CellSize));
                if (!grid.CellContains(index, mesh))
                {
                    grid.AddMeshToCell(index, mesh);
                }
            }
        }
        public void RemoveMesh(Mesh3D mesh)
        {
            grid.RemoveMeshFromAllCells(mesh);
        }
        public void UpdateMesh(Mesh3D mesh)
        {
            RemoveMesh(mesh);
            MapMesh(mesh);
        }
    }
}
