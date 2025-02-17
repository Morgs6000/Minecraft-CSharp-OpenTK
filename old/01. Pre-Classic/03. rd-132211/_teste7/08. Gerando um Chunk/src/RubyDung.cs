using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using RubyDung.src.level;

namespace RubyDung.src;

public class RubyDung : GameWindow {
    private int widht;
    private int height;

    private Shader shader;
    private Texture texture;

    private Chunk chunk = new Chunk();

    private Matrix matrix = new Matrix();

    public RubyDung(GameWindowSettings gws, NativeWindowSettings nws) : base(gws, nws) {
        this.widht = this.ClientSize.X;
        this.height = this.ClientSize.Y;

        this.CenterWindow();
    }

    protected override void OnFramebufferResize(FramebufferResizeEventArgs e) {
        this.widht = e.Width;
        this.height = e.Height;

        GL.Viewport(0, 0, e.Width, e.Height);
    }

    private void openGL_settins() {
        GL.Enable(EnableCap.DepthTest);

        GL.Enable(EnableCap.CullFace);
        GL.CullFace(CullFaceMode.Back);
    }

    protected override void OnLoad() {
        this.openGL_settins();

        this.shader = new Shader("vertexShader.glsl", "fragmentShader.glsl");

        this.texture = new Texture("terrain.png");

        this.chunk.rebuild();

        this.CursorState = CursorState.Grabbed;
    }

    protected override void OnRenderFrame(FrameEventArgs args) {
        GL.ClearColor(Color.Hex("7FCCFF", 255));
        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

        this.shader.use();

        this.matrix.matrix(this.widht, this.height, this.shader);

        this.texture.bind();

        this.chunk.render();

        this.SwapBuffers();
    }

    protected override void OnUpdateFrame(FrameEventArgs args) {
        KeyboardState input = this.KeyboardState;

        if(input.IsKeyDown(Keys.Escape)) {
            this.Close();
        }

        Wireframe.Mode(input, this.shader);

        if(!input.IsKeyDown(Keys.F3)) {
            this.matrix.processInput(input);
        }

        this.matrix.mouse_callback(this.MouseState.X, this.MouseState.Y);
    }

    private static void Main(string[] args) {
        GameWindowSettings gws = GameWindowSettings.Default;

        NativeWindowSettings nws = NativeWindowSettings.Default;
        nws.ClientSize = (1024, 768);
        nws.Title = "Game";

        new RubyDung(gws, nws).Run();
    }
}
