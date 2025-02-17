using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Breakout;

public class Game : GameWindow {
    public Game(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings)
        : base(gameWindowSettings, nativeWindowSettings) {
        CenterWindow();
    }

    protected override void OnLoad() {
        // render
        GL.ClearColor(0.0f , 0.0f, 0.0f, 1.0f);

        // OpenGL configuration
        GL.Enable(EnableCap.Blend);
        GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);

        base.OnLoad();
    }

    protected override void OnUpdateFrame(FrameEventArgs args) {
        base.OnUpdateFrame(args);
    }

    protected override void OnRenderFrame(FrameEventArgs args) {
        // render
        GL.Clear(ClearBufferMask.ColorBufferBit);

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

        var gameWindowSettings = GameWindowSettings.Default;
        var nativeWindowSettings = new NativeWindowSettings() {
            Size = new Vector2i(800, 600),
            Title = "Breakout",
            Flags = ContextFlags.ForwardCompatible
        };

        var window = new Game(gameWindowSettings, nativeWindowSettings);

        window.Run();
    }
}