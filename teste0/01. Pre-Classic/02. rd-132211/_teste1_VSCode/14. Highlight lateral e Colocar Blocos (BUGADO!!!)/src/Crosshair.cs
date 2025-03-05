using OpenTK.Graphics.OpenGL4;

namespace RubyDung;

public class Crosshair {
    private Shader shader;
    private int vao;
    private int vbo;

    public Crosshair() {
        shader = new Shader("src/shaders/crosshair_vertex.glsl", "src/shaders/crosshair_fragment.glsl");

        float[] vertices = {
            // Linha horizontal
            -0.02f,  0.0f,
             0.02f,  0.0f,
            // Linha vertical
             0.0f, -0.02f,
             0.0f,  0.02f
        };

        // Gerar e configurar o VAO e VBO
        vao = GL.GenVertexArray();
        vbo = GL.GenBuffer();

        GL.BindVertexArray(vao);

        GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
        GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

        GL.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, 2 * sizeof(float), 0);
        GL.EnableVertexAttribArray(0);

        GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        GL.BindVertexArray(0);
    }

    public void OnRenderFrame() {
        shader.OnRenderFrame();

        GL.BindVertexArray(vao);
        GL.DrawArrays(PrimitiveType.Lines, 0, 4);
        GL.BindVertexArray(0);
    }
}