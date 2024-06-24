using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using RubyDung.src.level;

namespace RubyDung.src;

public class RubyDung : GameWindow {
    private int width;
    private int height;

    private Level level;
    private LevelRenderer levelRenderer;

    private Shader shader = new Shader();
    private Wireframe wireframe = new Wireframe();
    private Texture texture = new Texture();
    private Player player = new Player();

    public RubyDung(int width, int height, string title)
        : base(GameWindowSettings.Default, new NativeWindowSettings() {
            ClientSize = (width, height),
            Title = title
        }) {
        this.width = width;
        this.height = height;

        CenterWindow();
    }

    protected override void OnFramebufferResize(FramebufferResizeEventArgs e) {
        this.width = e.Width;
        this.height = e.Height;

        GL.Viewport(0, 0, e.Width, e.Height);

        base.OnFramebufferResize(e);
    }

    protected override void OnUpdateFrame(FrameEventArgs args) {
        KeyboardState input = KeyboardState;

        if(input.IsKeyDown(Keys.Escape)) {
            Close();
        }

        this.wireframe.mode(input, this.shader);
        this.player.processInput(input);
        this.player.mouse_callback(MouseState.X, MouseState.Y);

        base.OnUpdateFrame(args);
    }

    protected override void OnLoad() {
        GL.ClearColor(0.5f, 0.8f, 1.0f, 0.0f);

        this.level = new Level(256, 64, 256);
        this.levelRenderer = new LevelRenderer(this.level);

        this.shader.load();
        this.texture.load();
        this.player.zBuffer();
        CursorState = CursorState.Grabbed;

        base.OnLoad();
    }

    protected override void OnRenderFrame(FrameEventArgs args) {
        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

        this.levelRenderer.render();

        this.shader.render();
        this.texture.render();
        this.player.render(this.shader, this.width, this.height);

        SwapBuffers();

        base.OnRenderFrame(args);
    }

    static void Main(string[] args) {
        new RubyDung(1024, 768, "Game").Run();
    }
}
