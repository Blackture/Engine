using Engine.Core.Components;
using Engine.Core.Maths;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Core.Physics.CollisionDetection
{
    public class Grid
    {
        private List<Cell> cells;

        public Cell this[Vector2 index]
        {
            get => GetCell(index);
            set => SetCell(index, value);
        }

        public Grid()
        {
            cells = new List<Cell>();
        }

        public Cell GetCell(Vector2 index)
        {
            Cell c = null;
            foreach (Cell cell in cells)
            {
                if (cell.Index == index)
                {
                    c = cell;
                }
            }
            return c;
        }
        public Cell SetCell(Vector2 index, Cell value)
        {
            Cell c = null;
            if (Contains(index))
            {
                Cell cell = GetCell(index);
                cell = new Cell(index, value);
                c = cell;
            }
            else
            {
                Cell cell = new Cell(index);
                cells.Add(cell);
                c = cell;
            }
            return c;
        }

        public List<Mesh3D> GetMeshesInCell(Vector2 index)
        {
            List<Mesh3D> meshes = GetCell(index).meshes;
            return meshes;
        }
        public List<Mesh3D> GetMeshesInCells(params Vector2[] indexes)
        {
            List<Mesh3D> meshes = new List<Mesh3D>();
            foreach (Cell cell in cells)
            {
                if (indexes.Contains(cell.Index))
                {
                    meshes.AddRange(cell.meshes);
                }
            }
            return meshes;
        }

        public bool Contains(Vector2 index)
        {
            bool res = false;
            foreach (Cell cell in cells)
            {
                if (cell.Index == index)
                {
                    res = true;
                }
            }
            return res;
        }

        public void AddMeshToCell(Vector2 index, Mesh3D mesh)
        {
            this[index].meshes.Add(mesh);
        }
        public void RemoveMeshFromCell(Vector2 index, Mesh3D mesh)
        {
            this[index].meshes.Remove(mesh);
        }
        public bool CellContains(Vector2 index, Mesh3D mesh)
        {
            return this[index].Contains(mesh);
        }
        public void RemoveMeshFromAllCells(Mesh3D mesh)
        {
            foreach (Cell cell in cells)
            {
                if (cell.Contains(mesh))
                {
                    this[cell.Index].meshes.Remove(mesh);
                }
            }
        }
    }
}
