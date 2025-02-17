using OpenTK.Graphics.OpenGL4;

namespace RubyDung.src.level;

public class Tesselator {
    private float[] vertices = {
        -0.5f, -0.5f, 0.0f, //Bottom-left vertex
         0.5f, -0.5f, 0.0f, //Bottom-right vertex
         0.0f,  0.5f, 0.0f  //Top vertex
    };

    private int VertexArrayObject;
    private int VertexBufferObject;

    public void Load() {
        VertexArrayObject = GL.GenVertexArray();
        GL.BindVertexArray(VertexArrayObject);

        VertexBufferObject = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);
        GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

        GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
        GL.EnableVertexAttribArray(0);
    }

    public void Render() {
        GL.BindVertexArray(VertexArrayObject);
        GL.DrawArrays(PrimitiveType.Triangles, 0, 3);
    }
}
