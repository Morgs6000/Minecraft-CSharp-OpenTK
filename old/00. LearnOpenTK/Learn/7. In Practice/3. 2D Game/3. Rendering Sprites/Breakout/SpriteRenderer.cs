using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace Breakout;

public class SpriteRenderer {
    // Render state
    private Shader shader;
    private int quadVAO;

    // Constructor (inits shaders/shapes)
    public SpriteRenderer(Shader shader) {
        this.shader = shader;
        this.initRenderData();
    }

    // Destructor
    public SpriteRenderer() {
        GL.DeleteVertexArray(this.quadVAO);
    }

    // Renders a defined quad textured with given sprite
    public void DrawSprite(Texture2D texture, Vector2 position, Vector2 size = default, float rotate = 0.0f, Vector3 color = default) {
        // prepare transformations
        this.shader.Use();
        Matrix4 model = Matrix4.Identity;

        model *= Matrix4.CreateScale(new Vector3(size.X, size.Y, 1.0f)); // last scale

        model *= Matrix4.CreateTranslation(new Vector3(-0.5f * size.X, -0.5f * size.Y, 0.0f)); // move origin back
        model *= Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(rotate)); // then rotate
        model *= Matrix4.CreateTranslation(new Vector3(0.5f * size.X, 0.5f * size.Y, 0.0f)); // move origin of rotation to center of quad

        model *= Matrix4.CreateTranslation(new Vector3(position.X, position.Y, 0.0f)); // first translate (transformations are: scale happens first, then rotation, and then final translation happens; reversed order)

        this.shader.SetMatrix4("model", model);

        // render textured quad
        this.shader.SetVector3f("spriteColor", color);

        GL.ActiveTexture(TextureUnit.Texture0);
        texture.Bind();

        GL.BindVertexArray(this.quadVAO);
        GL.DrawArrays(PrimitiveType.Triangles, 0, 6);
        GL.BindVertexArray(0);
    }

    // Initializes and configures the quad's buffer and vertex attributes
    private void initRenderData() {
        // configure VAO/VBO
        int VBO;

        float[] vertices = { 
            // pos      // tex
            0.0f, 1.0f, 0.0f, 1.0f,
            1.0f, 0.0f, 1.0f, 0.0f,
            0.0f, 0.0f, 0.0f, 0.0f,

            0.0f, 1.0f, 0.0f, 1.0f,
            1.0f, 1.0f, 1.0f, 1.0f,
            1.0f, 0.0f, 1.0f, 0.0f
        };

        GL.GenVertexArrays(1, out this.quadVAO);
        GL.GenBuffers(1, out VBO);

        GL.BindBuffer(BufferTarget.ArrayBuffer, VBO);
        GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

        GL.BindVertexArray(this.quadVAO);
        GL.EnableVertexAttribArray(0);
        GL.VertexAttribPointer(0, 4, VertexAttribPointerType.Float, false, 4 * sizeof(float), 0);
        GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        GL.BindVertexArray(0);
    }
}
