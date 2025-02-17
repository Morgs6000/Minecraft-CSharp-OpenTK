using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace ConsoleApp1.src;

public class Window : GameWindow {
    private int widht;
    private int height;

    public Window(GameWindowSettings gws, NativeWindowSettings nws) : base(gws, nws) {
        this.widht = this.ClientSize.X;
        this.height = this.ClientSize.Y;
        
        CenterWindow();
    }

    protected override void OnFramebufferResize(FramebufferResizeEventArgs e) {
        base.OnFramebufferResize(e);

        this.widht = e.Width;
        this.height = e.Height;

        GL.Viewport(0, 0, this.widht, this.height);
    }

    protected override void OnRenderFrame(FrameEventArgs args) {
        base.OnRenderFrame(args);

        GL.ClearColor(0.5f, 0.8f, 1.0f, 0.0f);
        GL.Clear(ClearBufferMask.ColorBufferBit);

        SwapBuffers();
    }

    protected override void OnUpdateFrame(FrameEventArgs args) {
        base.OnUpdateFrame(args);

        if(KeyboardState.IsKeyDown(Keys.Escape)) {
            Close();
        }
    }
}
