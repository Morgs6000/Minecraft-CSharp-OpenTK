using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using RubyDung.src.level;
using System.Drawing;

namespace RubyDung.src;

public class Program : GameWindow {
    private int width;
    private int height;

    private Shader shader;
    private Texture texture;
    private LevelRenderer levelRenderer;

    private Inputs input;
    private Matrix matrix;

    //private Vector3 position;

    private Program(GameWindowSettings gws, NativeWindowSettings nws) : base(gws, nws) {
        this.width = ClientSize.X;
        this.height = ClientSize.Y;

        CenterWindow();
    }

    protected override void OnFramebufferResize(FramebufferResizeEventArgs e) {
        this.width = e.Width;
        this.height = e.Height;

        GL.Viewport(0, 0, e.Width, e.Height);
    }

    protected override void OnLoad() {
        this.openGL_settings();

        this.shader = new Shader("shader.vert", "shader.frag");

        this.input = new Inputs(KeyboardState, this, this.shader/*, this.position*/);

        //this.shader.use();

        this.levelRenderer = new LevelRenderer();
        this.texture = new Texture("terrain.png");
    }

    private void openGL_settings() {
        GL.Enable(EnableCap.DepthTest);

        GL.Enable(EnableCap.CullFace);
        GL.CullFace(CullFaceMode.Back);
    }

    protected override void OnRenderFrame(FrameEventArgs args) {
        this.input.processInput();

        GL.ClearColor(0.5f, 0.8f, 1.0f, 0.0f);
        //GL.Clear(ClearBufferMask.ColorBufferBit);
        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

        this.texture.bind();

        this.matrix = new Matrix(this.width, this.height, this.shader, this.input.position);

        this.shader.use();

        this.levelRenderer.render();

        SwapBuffers();
    }

    private static void Main(string[] args) {
        GameWindowSettings gws = GameWindowSettings.Default;

        NativeWindowSettings nws = NativeWindowSettings.Default;
        nws.ClientSize = (1024, 768);
        //nws.ClientSize = (800, 600);
        nws.Title = "Game";

        new Program(gws, nws).Run();
    }
}