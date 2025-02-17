using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace RubyDung.src;

public class RubyDung : GameWindow {
    private int widht;
    private int height;

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

    protected override void OnRenderFrame(FrameEventArgs args) {
        //GL.ClearColor(0.5f, 0.8f, 1.0f, 0.0f);
        //GL.ClearColor(Color.RGBA(127, 204, 255, 255));
        GL.ClearColor(Color.Hex("7FCCFF", 255));

        GL.Clear(ClearBufferMask.ColorBufferBit);

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
