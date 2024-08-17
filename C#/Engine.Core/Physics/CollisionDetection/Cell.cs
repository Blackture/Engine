using Engine.Core.Components;
using Engine.Core.Maths;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Core.Physics.CollisionDetection
{
    public class Cell
    {
        private Vector2 index;
        public List<Mesh3D> meshes;

        public Vector2 Index { get { return index; } }

        public Cell(Vector2 index) 
        {
            this.index = index ?? throw new Exception("Index cannot be null");
            meshes = new List<Mesh3D>();
        }

        public Cell(Vector2 index, Cell cell)
        {
            this.index = index ?? throw new Exception("Index cannot be null");
            meshes = cell.meshes;
        }

        public bool Contains(Mesh3D mesh) => meshes.Contains(mesh);
    }
}
