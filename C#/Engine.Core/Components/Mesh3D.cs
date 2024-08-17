using Engine.Core.Physics.CollisionDetection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Core.Components
{
    public class Mesh3D : Component
    {
        private List<Vertex3D> vertices;
        private List<Edge3D> edges;
        private List<Face3D> faces;
        //private UVMap uv; > implement a uv map class dependend on a mesh, defining which faces which colors should have and where the seams are to apply a new material
        private readonly BoundingBox3D boundingBox;

        public int VerticesCount => vertices.Count;
        public int EdgesCount => edges.Count;
        public int FacesCount => faces.Count;

        public Mesh3D(Object3D dependency) : base(dependency)
        {
            vertices = new List<Vertex3D>();
            edges = new List<Edge3D>();
            faces = new List<Face3D>();
            boundingBox = GetBoundingBox();
        }

        public Mesh3D(List<Vertex3D> vertices, List<Edge3D> edges, List<Face3D> faces, Object3D dependency) : base(dependency)
        {
            this.vertices = vertices;
            this.edges = edges;
            this.faces = faces;
            boundingBox = GetBoundingBox();
        }

        public void AddVertex(Vertex3D vertex)
        {
            vertices.Add(vertex);
        }

        public void AddEdge(Edge3D edge)
        {
            edges.Add(edge);
        }
        public void AddEdge(Vertex3D v1, Vertex3D v2)
        {
            Edge3D e = new Edge3D(v1, v2, Dependency);
            edges.Add(e);
        }
        public void AddEdge(Vertex3D[] vertices)
        {
            if (vertices.Length == 2)
            {
                Edge3D e = new Edge3D(vertices[0], vertices[1], Dependency); 
                edges.Add(e);
            }
            else throw new ArgumentException();
        }

        public void AddFace(Face3D face)
        {
            faces.Add(face);
        }
        public void AddFace(Vertex3D[] vertices, Edge3D[] edges)
        {
            Face3D f = new Face3D(vertices, edges, Dependency);
            faces.Add(f);
        }
        public void AddFace(Vertex3D[] vertices)
        {
            List<Edge3D> edges = new List<Edge3D>();
            for(int i = 0; i < vertices.Length - 2; i += 2)
            {
                edges.Add(new Edge3D(vertices[i], vertices[i + 1], Dependency));
            } 
            Face3D f = new Face3D(vertices, edges.ToArray(), Dependency);
            faces.Add(f);
        }

        public Vertex3D GetVertex(int index) { return vertices[index]; }
        public List<Vertex3D> GetAllVertices() 
        {
            Vertex3D[] all = new Vertex3D[VerticesCount];
            vertices.CopyTo(all);
            return all.ToList();
        }
        public Vertex3D FindVertex(Vertex3D vertex) => vertices.Find(x => x == vertex);
        public Edge3D GetEdge(int index) { return edges[index]; }
        public Edge3D FindEdge(Edge3D edge) => edges.Find(x => x == edge);
        public Face3D GetFace(int index) { return faces[index]; }
        public List<Face3D> GetAllFaces()
        {
            Face3D[] all = new Face3D[FacesCount];
            faces.CopyTo(all);
            return all.ToList();
        }
        public Face3D FindFace(Face3D face) => faces.Find(x => x == face);
        public int FindIndex(Vertex3D vertex) => vertices.FindIndex(x => x == vertex);
        public int FindIndex(Edge3D edge) => edges.FindIndex(x => x == edge);
        public int FindIndex(Face3D face) => faces.FindIndex(x => x == face);
        public BoundingBox3D GetBoundingBox() 
        {
            return (BoundingBox3D)this;
        }
    }
}
