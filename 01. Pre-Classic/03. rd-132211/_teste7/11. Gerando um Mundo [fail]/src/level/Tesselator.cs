using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace RubyDung.src.level;

public class Tesselator {
    private float[] vertexBuffer = new float[300000];
    private int[] indiceBuffer = new int[600000];
    private float[] texCoordBuffer = new float[200000];

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

        this.clear();
    }

    private void setVAO() {
        GL.GenVertexArrays(1, out this.VAO);

        GL.BindVertexArray(this.VAO);
    }

    private void setVBO() {
        GL.GenBuffers(1, out this.VBO);

        GL.BindBuffer(BufferTarget.ArrayBuffer, this.VBO);
        GL.BufferData(BufferTarget.ArrayBuffer, this.vertexBuffer.Length * sizeof(float), this.vertexBuffer, BufferUsageHint.StaticDraw);

        GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 0, 0);
        GL.EnableVertexAttribArray(0);
    }

    private void setEBO() {
        GL.GenBuffers(1, out this.EBO);

        GL.BindBuffer(BufferTarget.ElementArrayBuffer, this.EBO);
        GL.BufferData(BufferTarget.ElementArrayBuffer, this.indiceBuffer.Length * sizeof(int), this.indiceBuffer, BufferUsageHint.StaticDraw);
    }

    private void setTBO() {
        GL.GenBuffers(1, out this.TBO);

        GL.BindBuffer(BufferTarget.ArrayBuffer, this.TBO);
        GL.BufferData(BufferTarget.ArrayBuffer, this.texCoordBuffer.Length * sizeof(float), this.texCoordBuffer, BufferUsageHint.StaticDraw);

        GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 0, 0);
        GL.EnableVertexAttribArray(1);
    }

    private void clearBind() {
        // Clear VBO
        GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

        // Clear VAO
        GL.BindVertexArray(0);
    }

    private void clear() {
        Array.Clear(this.vertexBuffer, 0, this.vertexBuffer.Length);
        Array.Clear(this.indiceBuffer, 0, this.indiceBuffer.Length);
        Array.Clear(this.texCoordBuffer, 0, this.texCoordBuffer.Length);

        this.vertices = 0;
    }

    public void init() {
        this.clear();

        this.hasTexture = false;
    }

    public void bind() {
        GL.BindVertexArray(this.VAO);

        //GL.DrawArrays(PrimitiveType.Triangles, 0, 3);
        //GL.DrawArrays(PrimitiveType.Triangles, 0, 6);
        GL.DrawElements(PrimitiveType.Triangles, this.indiceBuffer.Length, DrawElementsType.UnsignedInt, 0);
    }

    public void tex(float u, float v) {
        this.hasTexture = true;

        this.u = u;
        this.v = v;
    }

    public void vertex(float x, float y, float z) {
        this.vertexBuffer[this.vertices * 3 + 0] = x;
        this.vertexBuffer[this.vertices * 3 + 1] = y;
        this.vertexBuffer[this.vertices * 3 + 2] = z;

        if(this.hasTexture) {
            this.texCoordBuffer[this.vertices * 2 + 0] = this.u;
            this.texCoordBuffer[this.vertices * 2 + 1] = this.v;
        }

        this.vertices++;

        if(this.vertices % 4 == 0) {
            int indices = this.vertices - 4;

            this.indiceBuffer[indices * 6 + 0] = 0 + indices;
            this.indiceBuffer[indices * 6 + 1] = 1 + indices;
            this.indiceBuffer[indices * 6 + 2] = 2 + indices;

            this.indiceBuffer[indices * 6 + 3] = 0 + indices;
            this.indiceBuffer[indices * 6 + 4] = 2 + indices;
            this.indiceBuffer[indices * 6 + 5] = 3 + indices;
        }

        if(this.vertices == 100000) {
            this.flush();
        }
    }
}
