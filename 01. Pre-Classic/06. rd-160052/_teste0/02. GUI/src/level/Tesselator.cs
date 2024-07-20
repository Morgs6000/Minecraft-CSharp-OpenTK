using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace RubyDung.src.level;

public class Tesselator {
    private List<float> vertexBuffer = new List<float>();
    private List<int> indiceBuffer = new List<int>();
    private List<float> texCoordBuffer = new List<float>();

    private int vertices = 0;

    private float u;
    private float v;

    private bool hasTexture = false;

    private int VAO; // Vertex Array Object
    private int VBO; // Vertex Buffer Object
    private int EBO; // Element Buffer Object
    private int TBO; // Texture Buffer Object

    public void flush() {
        this.setVAO();
        this.setVBO();
        this.setEBO();
        this.setTBO();

        this.clearBind();

        //this.clear();
    }

    private void setVAO() {
        GL.GenVertexArrays(1, out this.VAO);

        GL.BindVertexArray(this.VAO);
    }

    private void setVBO() {
        GL.GenBuffers(1, out this.VBO);

        GL.BindBuffer(BufferTarget.ArrayBuffer, this.VBO);
        GL.BufferData(BufferTarget.ArrayBuffer, this.vertexBuffer.Count * sizeof(float), this.vertexBuffer.ToArray(), BufferUsageHint.StaticDraw);

        GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 0, 0);
        GL.EnableVertexAttribArray(0);
    }

    private void setEBO() {
        GL.GenBuffers(1, out this.EBO);

        GL.BindBuffer(BufferTarget.ElementArrayBuffer, this.EBO);
        GL.BufferData(BufferTarget.ElementArrayBuffer, this.indiceBuffer.Count * sizeof(int), this.indiceBuffer.ToArray(), BufferUsageHint.StaticDraw);
    }

    private void setTBO() {
        GL.GenBuffers(1, out this.TBO);

        GL.BindBuffer(BufferTarget.ArrayBuffer, this.TBO);
        GL.BufferData(BufferTarget.ArrayBuffer, this.texCoordBuffer.Count * sizeof(float), this.texCoordBuffer.ToArray(), BufferUsageHint.StaticDraw);

        GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 0, 0);
        GL.EnableVertexAttribArray(1);
    }

    private void clearBind() {
        // Clear VBO
        GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

        // Clear VAO
        GL.BindVertexArray(0);
    }

    public void bind() {
        GL.BindVertexArray(this.VAO);

        GL.DrawElements(PrimitiveType.Triangles, this.indiceBuffer.Count, DrawElementsType.UnsignedInt, 0);
    }

    private void clear() {
        this.vertexBuffer.Clear();
        this.indiceBuffer.Clear();
        this.texCoordBuffer.Clear();

        this.vertices = 0;
    }

    public void init() {
        this.clear();

        this.hasTexture = false;
    }

    public void vertexUV(float x, float y, float z, float u, float v) {
        this.tex(u, v);
        this.vertex(x, y, z);
    }

    public void vertex(float x, float y, float z) {
        this.vertexBuffer.Add(x);
        this.vertexBuffer.Add(y);
        this.vertexBuffer.Add(z);

        if(this.hasTexture) {
            this.texCoordBuffer.Add(this.u);
            this.texCoordBuffer.Add(this.v);
        }

        this.vertices++;

        if(this.vertices % 4 == 0) {
            int indices = this.vertices - 4;

            this.indiceBuffer.Add(0 + indices);
            this.indiceBuffer.Add(1 + indices);
            this.indiceBuffer.Add(2 + indices);

            this.indiceBuffer.Add(0 + indices);
            this.indiceBuffer.Add(2 + indices);
            this.indiceBuffer.Add(3 + indices);
        }
    }

    public void tex(float u, float v) {
        this.hasTexture = true;

        this.u = u;
        this.v = v;
    }
}
