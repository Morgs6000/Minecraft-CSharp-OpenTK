using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace RubyDung.src.level;

public class Tesselator {
    private float[] vertexBuffer = new float[300000];
    private int[] indiceBuffer = new int[600000];

    private int vertices = 0;
    private int indices = 0;

    private int VAO; // Vertex Array Object
    private int VBO; // Vertex Buffer Object
    private int EBO; // Element Buffer Object

    public void flush() {
        this.setVAO();
        this.setVBO();
        this.setEBO();

        this.clearBind();

        //GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);
    }

    private void setVAO() {
        GL.GenVertexArrays(1, out this.VAO);

        GL.BindVertexArray(this.VAO);
    }

    private void setVBO() {
        GL.GenBuffers(1, out this.VBO);

        GL.BindBuffer(BufferTarget.ArrayBuffer, this.VBO);
        GL.BufferData(BufferTarget.ArrayBuffer, this.vertexBuffer.Length * sizeof(float), this.vertexBuffer, BufferUsageHint.StaticDraw);

        GL.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, 0, 0);
        GL.EnableVertexAttribArray(0);
    }

    private void setEBO() {
        GL.GenBuffers(1, out this.EBO);

        GL.BindBuffer(BufferTarget.ElementArrayBuffer, this.EBO);
        GL.BufferData(BufferTarget.ElementArrayBuffer, this.indiceBuffer.Length * sizeof(int), this.indiceBuffer, BufferUsageHint.StaticDraw);
    }

    private void clearBind() {
        // Clear VBO
        GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

        // Clear VAO
        GL.BindVertexArray(0);
    }

    public void bind() {
        GL.BindVertexArray(this.VAO);

        //GL.DrawArrays(PrimitiveType.Triangles, 0, 3);
        //GL.DrawArrays(PrimitiveType.Triangles, 0, 6);
        GL.DrawElements(PrimitiveType.Triangles, this.indiceBuffer.Length, DrawElementsType.UnsignedInt, 0);
    }

    public void vertex(float x, float y) {
        this.vertexBuffer[this.vertices * 2 + 0] = x;
        this.vertexBuffer[this.vertices * 2 + 1] = y;

        this.vertices++;
    }

    public void indice(int i) {
        this.indiceBuffer[this.indices * 1 + 0] = i;

        this.indices++;
    }
}
