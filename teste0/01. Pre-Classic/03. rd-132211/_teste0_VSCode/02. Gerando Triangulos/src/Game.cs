using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace RubyDung.src;

public class Game : GameWindow {
    private Shader shader;
    private Tesselator t = new Tesselator();

    public Game(GameWindowSettings gws, NativeWindowSettings nws) : base(gws, nws) {
        CenterWindow();
    }

    protected override void OnLoad() {
        base.OnLoad();

        GL.ClearColor(0.5f, 0.8f, 1.0f, 0.0f);

        shader = new Shader("src/shaders/shader_vertex.glsl", "src/shaders/shader_fragment.glsl");
        t.OnLoad();
    }

    protected override void OnUpdateFrame(FrameEventArgs args) {
        base.OnUpdateFrame(args);
    }

    protected override void OnRenderFrame(FrameEventArgs args) {
        base.OnRenderFrame(args);

        GL.Clear(ClearBufferMask.ColorBufferBit);

        shader.OnRenderFrame();
        t.OnRenderFrame();

        SwapBuffers();
    }

    protected override void OnFramebufferResize(FramebufferResizeEventArgs e) {
        base.OnFramebufferResize(e);

        GL.Viewport(0, 0, ClientSize.X, ClientSize.Y);
    }
}

public class Tesselator {
    private float[] vertexBuffer = {
        -0.5f, -0.5f,
         0.5f, -0.5f,
         0.0f,  0.5f
    };

    private int vertexArrayObject;
    private int vertexBufferObject;

    public void OnLoad() {
        /* ..:: Vertex Array Object ::.. */
        vertexArrayObject = GL.GenVertexArray();
        GL.BindVertexArray(vertexArrayObject);

        /* ..:: Vertex Buffer Object ::.. */
        vertexBufferObject = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferObject);
        GL.BufferData(BufferTarget.ArrayBuffer, vertexBuffer.Length * sizeof(float), vertexBuffer, BufferUsageHint.StaticDraw);

        GL.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, 0, 0);
        GL.EnableVertexAttribArray(0);
    }

    public void OnRenderFrame() {
        GL.BindVertexArray(vertexArrayObject);
        GL.DrawArrays(PrimitiveType.Triangles, 0, 3);
    }
}