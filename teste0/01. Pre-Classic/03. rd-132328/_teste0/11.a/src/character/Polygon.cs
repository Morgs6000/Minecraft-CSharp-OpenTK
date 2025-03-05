namespace RubyDung;

public class Polygon {
    public Vertex[] vertices;
    public int vertexCount;

    public Polygon(Vertex[] vertices) {
        vertexCount = 0;
        this.vertices = vertices;
        vertexCount = vertices.Length;
    }

    public Polygon(Vertex[] vertices, int u0, int v0, int u1, int v1) : this(vertices) {
        vertices[0] = vertices[0].Remap(u1, v0);
        vertices[1] = vertices[1].Remap(u0, v0);
        vertices[2] = vertices[2].Remap(u0, v1);
        vertices[3] = vertices[3].Remap(u1, v1);
    }

    public void Render() {
        
    }
}