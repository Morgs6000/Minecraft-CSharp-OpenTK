using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Breakout;

public class Game : GameWindow {
    public int Width;
    public int Height;

    public SpriteRenderer Renderer;

    public Game(int widht, int height, string title)
        : base(GameWindowSettings.Default, new NativeWindowSettings() {
            ClientSize = new Vector2i(widht, height),
            Title = title,
            Flags = ContextFlags.ForwardCompatible
        }) {
        this.Width = widht;
        this.Height = height;

        CenterWindow();
    }

    protected override void OnLoad() {
        // render
        GL.ClearColor(0.0f , 0.0f, 0.0f, 1.0f);

        // OpenGL configuration
        GL.Enable(EnableCap.Blend);
        GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);

        // load shaders
        ResourceManager.LoadShader("../../../shaders/sprite.vert", "../../../shaders/sprite.frag", null, "sprite");

        // configure shaders
        Matrix4 projection = Matrix4.CreateOrthographicOffCenter(0.0f, this.Width, this.Height, 0.0f, -1.0f, 1.0f);
        ResourceManager.GetShader("sprite").Use().SetInteger("image", 0);
        ResourceManager.GetShader("sprite").SetMatrix4("projection", projection);

        // set render-specific controls
        Renderer = new SpriteRenderer(ResourceManager.GetShader("sprite"));

        // load textures
        ResourceManager.LoadTexture("../../../textures/awesomeface.png", true, "face");

        base.OnLoad();
    }

    protected override void OnUpdateFrame(FrameEventArgs args) {
        base.OnUpdateFrame(args);
    }

    protected override void OnRenderFrame(FrameEventArgs args) {
        // render
        GL.Clear(ClearBufferMask.ColorBufferBit);

        Renderer.DrawSprite(ResourceManager.GetTexture("face"), new Vector2(200.0f, 200.0f), new Vector2(300.0f, 400.0f), 45.0f, new Vector3(0.0f, 1.0f, 0.0f));

        SwapBuffers();

        base.OnRenderFrame(args);
    }

    protected override void OnUnload() {
        // delete all resources as loaded using the resource manager
        ResourceManager.Clear();

        base.OnUnload();
    }

    protected override void OnKeyDown(KeyboardKeyEventArgs e) {
        // when a user presses the escape key, we set the WindowShouldClose property to true, closing the application
        if(e.Key == Keys.Escape) {
            Close();
        }

        base.OnKeyDown(e);
    }

    protected override void OnKeyUp(KeyboardKeyEventArgs e) {
        base.OnKeyUp(e);
    }

    protected override void OnResize(ResizeEventArgs e) {
        // make sure the viewport matches the new window dimensions; note that width and 
        // height will be significantly larger than specified on retina displays.
        GL.Viewport(0, 0, Size.X, Size.Y);

        base.OnResize(e);
    }

    private static void Main(string[] args) {
        //Console.WriteLine("Hello, World!");

        new Game(800, 600, "Breakout").Run();
    }
}