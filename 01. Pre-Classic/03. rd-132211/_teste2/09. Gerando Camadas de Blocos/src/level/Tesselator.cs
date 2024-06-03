using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace RubyDung.src.level;

public class Tesselator {
    private List<Vector3> vertexBuffer = new List<Vector3>();
    private List<int> triangleBuffer = new List<int>();
    private List<Vector2> texCoordBuffer = new List<Vector2>();

    private int VAO; // Vertex Array Object
    private int VBO; // Vertex Buffer Object
    private int TBO; // Texture Buffer Object
    private int EBO; // Element Buffer Object

    private int vertices;

    //private int texX;
    //private int texY;

    private float col = 16.0f;
    private float row = 16.0f;

    public void flush() {
        // ..:: Vertex Array Object ::..
        this.VAO = GL.GenVertexArray();
        GL.BindVertexArray(this.VAO);

        // ..:: Vertex Array Object ::..
        this.VBO = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ArrayBuffer, this.VBO);
        GL.BufferData(BufferTarget.ArrayBuffer, this.vertexBuffer.Count * Vector3.SizeInBytes, vertexBuffer.ToArray(), BufferUsageHint.StaticDraw);

        GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
        GL.EnableVertexAttribArray(0);

        // ..:: Texture Array Object ::..
        this.TBO = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ArrayBuffer, this.TBO);
        GL.BufferData(BufferTarget.ArrayBuffer, this.texCoordBuffer.Count * Vector2.SizeInBytes, this.texCoordBuffer.ToArray(), BufferUsageHint.StaticDraw);

        GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 2 * sizeof(float), 0);
        GL.EnableVertexAttribArray(1);

        // ..:: Element Buffer Object ::..
        this.EBO = GL.GenBuffer();

        GL.BindBuffer(BufferTarget.ElementArrayBuffer, this.EBO);
        GL.BufferData(BufferTarget.ElementArrayBuffer, this.triangleBuffer.Count * sizeof(int), triangleBuffer.ToArray(), BufferUsageHint.StaticDraw);
    }

    public void bind() {
        GL.BindVertexArray(this.VAO);
        //GL.DrawArrays(PrimitiveType.Triangles, 0, 3);
        GL.DrawElements(PrimitiveType.Triangles, this.triangleBuffer.Count, DrawElementsType.UnsignedInt, 0);
    }

    public void vertex(float x, float y, float z) {
        this.vertexBuffer.Add(new Vector3(x, y, z));
    }

    public void triangle() {
        // first triangle
        this.triangleBuffer.Add(0 + this.vertices);
        this.triangleBuffer.Add(1 + this.vertices);
        this.triangleBuffer.Add(2 + this.vertices);

        // second triangle
        this.triangleBuffer.Add(0 + this.vertices);
        this.triangleBuffer.Add(2 + this.vertices);
        this.triangleBuffer.Add(3 + this.vertices);

        this.vertices += 4;
    }

    public void tex(float texX, float texY) {
        float u0 = texX / this.col;
        float u1 = u0 + 1.0f / this.col;
        float v0 = (this.row - 1.0f - texY) / this.row;
        float v1 = v0 + 1.0f / this.row;

        this.texCoordBuffer.Add(new Vector2(u0, v0));
        this.texCoordBuffer.Add(new Vector2(u0, v1));
        this.texCoordBuffer.Add(new Vector2(u1, v1));
        this.texCoordBuffer.Add(new Vector2(u1, v0));
    }
}
