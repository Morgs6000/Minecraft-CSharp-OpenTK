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
    private Player player;

    private Shader shader;
    private Texture texture = new Texture();

    public RubyDung(GameWindowSettings gws, NativeWindowSettings nws) : base(gws, nws) {
        this.width = this.ClientSize.X;
        this.height = this.ClientSize.Y;

        this.CenterWindow();
    }

    protected override void OnFramebufferResize(FramebufferResizeEventArgs e) {
        this.width = e.Width;
        this.height = e.Height;

        GL.Viewport(0, 0, e.Width, e.Height);
    }

    protected override void OnUpdateFrame(FrameEventArgs args) {
        KeyboardState input = this.KeyboardState;

        if(input.IsKeyDown(Keys.Escape)) {
            this.Close();
        }

        Wireframe.Mode(input, this.shader);
        this.player.processInput(input);
        this.player.mouse_callback(this.MouseState.X, this.MouseState.Y);
    }

    protected override void OnLoad() {
        this.level = new Level(256, 64, 256);
        this.levelRenderer = new LevelRenderer(this.level);
        this.player = new Player();

        this.shader = new Shader("vertexShader.glsl", "fragmentShader.glsl");
        this.texture.load();
        this.player.zBuffer();
        this.CursorState = CursorState.Grabbed;
    }

    protected override void OnRenderFrame(FrameEventArgs args) {
        GL.ClearColor(0.5f, 0.8f, 1.0f, 0.0f);
        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

        this.levelRenderer.render();

        this.shader.use();
        this.texture.render();
        this.player.render(this.shader, this.width, this.height);

        this.SwapBuffers();
    }

    private static void Main(string[] args) {
        GameWindowSettings gws = GameWindowSettings.Default;

        NativeWindowSettings nws = NativeWindowSettings.Default;
        nws.ClientSize = (1024, 768);
        nws.Title = "Game";

        new RubyDung(gws, nws).Run();
    }
}
