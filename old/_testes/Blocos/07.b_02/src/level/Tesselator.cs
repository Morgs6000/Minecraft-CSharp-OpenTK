using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace RubyDung.src.level;

public class Tesselator {
    private List<Vector3> vertexBuffer = new List<Vector3>();
    private List<int> triangleBuffer = new List<int>();
    private List<Vector2> texCoordBuffer = new List<Vector2>();
    private List<Vector3> colorBuffer = new List<Vector3>();

    private int VAO; // Vertex Array Object
    private int VBO; // Vertex Buffer Object
    private int TBO; // Texture Buffer Object
    private int CBO; // Color Buffer Object
    private int EBO; // Element Buffer Object

    private int vertices;

    public void flush() {
        // ..:: Vertex Array Object ::..
        VAO = GL.GenVertexArray();
        GL.BindVertexArray(VAO);

        // ..:: Vertex Array Object ::..
        VBO = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ArrayBuffer, VBO);
        GL.BufferData(BufferTarget.ArrayBuffer, vertexBuffer.Count * Vector3.SizeInBytes, vertexBuffer.ToArray(), BufferUsageHint.StaticDraw);

        GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 0, 0);
        GL.EnableVertexAttribArray(0);

        // ..:: Texture Buffer Object ::..
        TBO = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ArrayBuffer, TBO);
        GL.BufferData(BufferTarget.ArrayBuffer, texCoordBuffer.Count * Vector2.SizeInBytes, texCoordBuffer.ToArray(), BufferUsageHint.StaticDraw);

        GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 0, 0);
        GL.EnableVertexAttribArray(1);

        // ..:: Color Buffer Object ::..
        CBO = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ArrayBuffer, CBO);
        GL.BufferData(BufferTarget.ArrayBuffer, colorBuffer.Count * Vector3.SizeInBytes, colorBuffer.ToArray(), BufferUsageHint.StaticDraw);

        GL.VertexAttribPointer(2, 3, VertexAttribPointerType.Float, false, 0, 0);
        GL.EnableVertexAttribArray(2);

        // ..:: Element Buffer Object ::..
        EBO = GL.GenBuffer();

        GL.BindBuffer(BufferTarget.ElementArrayBuffer, EBO);
        GL.BufferData(BufferTarget.ElementArrayBuffer, triangleBuffer.Count * sizeof(int), triangleBuffer.ToArray(), BufferUsageHint.StaticDraw);
    }

    public void render() {
        GL.BindVertexArray(VAO);
        //GL.DrawArrays(PrimitiveType.Triangles, 0, 3);
        GL.DrawElements(PrimitiveType.Triangles, triangleBuffer.Count, DrawElementsType.UnsignedInt, 0);
    }

    public void vertex(float x, float y, float z) {
        vertexBuffer.Add(new Vector3(x, y, z));
    }

    public void triangle() {
        // first triangle
        triangleBuffer.Add(0 + vertices);
        triangleBuffer.Add(1 + vertices);
        triangleBuffer.Add(2 + vertices);

        // second triangle
        triangleBuffer.Add(0 + vertices);
        triangleBuffer.Add(2 + vertices);
        triangleBuffer.Add(3 + vertices);

        vertices += 4;
    }

    public void tex(float u, float v) {
        texCoordBuffer.Add(new Vector2(u, v));
    }

    public void color(float r, float g, float b) {
        colorBuffer.Add(new Vector3(r, g, b));
        colorBuffer.Add(new Vector3(r, g, b));
        colorBuffer.Add(new Vector3(r, g, b));
        colorBuffer.Add(new Vector3(r, g, b));
    }
}
