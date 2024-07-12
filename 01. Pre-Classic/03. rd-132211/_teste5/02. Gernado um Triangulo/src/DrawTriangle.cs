using OpenTK.Graphics.OpenGL4;

namespace RubyDung.src;

public class DrawTriangle {
    private float[] vertices = {
        -0.5f, -0.5f, // Bottom-left vertex
         0.5f, -0.5f, // Bottom-right vertex
         0.0f,  0.5f  // Top vertex
    };

    private int VAO; // Vertex Array Object
    private int VBO; // Vertex Buffer Object

    public DrawTriangle() {
        this.setupVAO();
        this.setupVBO();
        this.clearBind();
    }

    private void setupVAO() {
        GL.GenVertexArrays(1, out VAO);

        GL.BindVertexArray(VAO);
    }

    private void setupVBO() {
        GL.GenBuffers(1, out VBO);

        GL.BindBuffer(BufferTarget.ArrayBuffer, VBO);
        GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

        GL.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, 0, 0);
        GL.EnableVertexAttribArray(0);
    }

    private void clearBind() {
        GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        GL.BindVertexArray(0);
    }

    public void bind() {
        GL.BindVertexArray(VAO);
        GL.DrawArrays(PrimitiveType.Triangles, 0, 3);
    }
}
