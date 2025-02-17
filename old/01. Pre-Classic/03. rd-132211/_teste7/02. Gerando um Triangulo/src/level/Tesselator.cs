using OpenTK.Graphics.OpenGL4;

namespace RubyDung.src.level;

public class Tesselator {
    private float[] vertexBuffer = new float[300000];

    private int vertices = 0;

    private int VAO; // Vertex Array Object
    private int VBO; // Vertex Buffer Object

    public void flush() {
        this.setVAO();
        this.setVBO();

        this.clearBind();
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

    private void clearBind() {
        // Clear VBO
        GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

        // Clear VAO
        GL.BindVertexArray(0);
    }

    public void bind() {
        GL.BindVertexArray(this.VAO);
        GL.DrawArrays(PrimitiveType.Triangles, 0, 3);
    }

    public void vertex(float x, float y) {
        this.vertexBuffer[this.vertices * 2 + 0] = x;
        this.vertexBuffer[this.vertices * 2 + 1] = y;

        this.vertices++;
    }
}
