using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Core.Components
{
    public class Mesh : Component
    {
        private List<Vertex> vertices;
        private List<Edge> edges;
        private List<Face> faces;
        //private UVMap uv; > implement a uv map class dependend on a mesh, defining which faces which colors should have and where the seams are to apply a new material

        public int VerticesCount => vertices.Count;
        public int EdgesCount => edges.Count;
        public int FacesCount => faces.Count;

        public Mesh(Object3D dependency) : base(dependency)
        {
            vertices= new List<Vertex>();
            edges= new List<Edge>();
            faces= new List<Face>();
        }

        public Mesh(List<Vertex> vertices, List<Edge> edges, List<Face> faces, Object3D dependency) : base(dependency)
        {
            this.vertices = vertices;
            this.edges = edges;
            this.faces = faces;
        }

        public void AddVertex(Vertex vertex)
        {
            vertices.Add(vertex);
        }

        public void AddEdge(Edge edge)
        {
            edges.Add(edge);
        }
        public void AddEdge(Vertex v1, Vertex v2)
        {
            Edge e = new Edge(v1, v2, Dependency);
            edges.Add(e);
        }
        public void AddEdge(Vertex[] vertices)
        {
            if (vertices.Length == 2)
            {
                Edge e = new Edge(vertices[0], vertices[1], Dependency); 
                edges.Add(e);
            }
            else throw new ArgumentException();
        }

        public void AddFace(Face face)
        {
            faces.Add(face);
        }
        public void AddFace(Vertex[] vertices, Edge[] edges)
        {
            Face f = new Face(vertices, edges, Dependency);
            faces.Add(f);
        }
        public void AddFace(Vertex[] vertices)
        {
            List<Edge> edges = new List<Edge>();
            for(int i = 0; i < vertices.Length - 2; i += 2)
            {
                edges.Add(new Edge(vertices[i], vertices[i + 1], Dependency));
            } 
            Face f = new Face(vertices, edges.ToArray(), Dependency);
            faces.Add(f);
        }

        public Vertex GetVertex(int index) { return vertices[index]; }
        public Vertex FindVertex(Vertex vertex) => vertices.Find(x => x == vertex);
        public Edge GetEdge(int index) { return edges[index]; }
        public Edge FindEdge(Edge edge) => edges.Find(x => x == edge);
        public Face GetFace(int index) { return faces[index]; }
        public Face FindFace(Face face) => faces.Find(x => x == face);
        public int FindIndex(Vertex vertex) => vertices.FindIndex(x => x == vertex);
        public int FindIndex(Edge edge) => edges.FindIndex(x => x == edge);
        public int FindIndex(Face face) => faces.FindIndex(x => x == face);
    }
}
