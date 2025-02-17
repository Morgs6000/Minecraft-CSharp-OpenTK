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
    private Tesselator t = new Tesselator();

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

    protected override void OnLoad() {
        this.shader = new Shader("vertexShader.glsl", "fragmentShader.glsl");

        Tile.triangle.render(t);
        this.t.flush();
    }

    protected override void OnRenderFrame(FrameEventArgs args) {
        GL.ClearColor(Color.Hex("7FCCFF", 255));
        GL.Clear(ClearBufferMask.ColorBufferBit);

        this.shader.use();

        //this.shader.setColor("color", 1.0f, 0.5f, 0.2f, 1.0f);
        //this.shader.setColor("color", Color.RGBA(255, 127, 51, 255));
        this.shader.setColor("color", Color.Hex("FF7F33", 255));

        this.t.bind();

        this.SwapBuffers();
    }

    protected override void OnUpdateFrame(FrameEventArgs args) {
        if(this.KeyboardState.IsKeyDown(Keys.Escape)) {
            this.Close();
        }
    }

    private static void Main(string[] args) {
        GameWindowSettings gws = GameWindowSettings.Default;

        NativeWindowSettings nws = NativeWindowSettings.Default;
        nws.ClientSize = (1024, 768);
        nws.Title = "Game";

        new RubyDung(gws, nws).Run();
    }
}
