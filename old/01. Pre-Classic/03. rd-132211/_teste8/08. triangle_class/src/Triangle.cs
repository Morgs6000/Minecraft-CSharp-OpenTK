using OpenTK.Graphics.OpenGL4;

namespace RubyDung.src;

public class Triangle {
    private float[] vertices = {
        // vertices     // texture coords
        -0.5f, -0.5f,   0.0f, 0.0f,       // bottom left  // 0
         0.5f, -0.5f,   1.0f, 0.0f,       // bottom right // 1
         0.5f,  0.5f,   1.0f, 1.0f,       // top right    // 2
        -0.5f,  0.5f,   0.0f, 1.0f        // top left     // 3
    };

    private int[] indices = {
        0, 1, 2, // first Triangle
        0, 2, 3  // second Triangle
    };

    private int VAO; // Vertex Array Object
    private int VBO; // Vertex Buffer Object
    private int EBO; // Element Buffer Object

    public Triangle() {
        // Vertex Array Object
        GL.GenVertexArrays(1, out this.VAO);

        GL.BindVertexArray(this.VAO);

        // Vertex Buffer Object
        GL.GenBuffers(1, out this.VBO);

        GL.BindBuffer(BufferTarget.ArrayBuffer, this.VBO);
        GL.BufferData(BufferTarget.ArrayBuffer, this.vertices.Length * sizeof(float), this.vertices, BufferUsageHint.StaticDraw);

        // position attribute
        GL.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, 4 * sizeof(float), 0);
        GL.EnableVertexAttribArray(0);

        // texture coord attribute
        GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 4 * sizeof(float), 2 * sizeof(float));
        GL.EnableVertexAttribArray(1);

        // Element Buffer Object
        GL.GenBuffers(1, out this.EBO);

        GL.BindBuffer(BufferTarget.ElementArrayBuffer, this.EBO);
        GL.BufferData(BufferTarget.ElementArrayBuffer, this.indices.Length * sizeof(int), this.indices, BufferUsageHint.StaticDraw);

        // Clear
        GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

        GL.BindVertexArray(0);
    }

    public void render() {
        GL.BindVertexArray(this.VAO);
        GL.DrawElements(PrimitiveType.Triangles, 6, DrawElementsType.UnsignedInt, 0);
    }
}
