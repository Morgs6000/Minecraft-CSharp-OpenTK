using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace Inventory.src;

public class Program : GameWindow {
    private int width;
    private int height;

    private Program(GameWindowSettings gws, NativeWindowSettings nws) : base(gws, nws) {
        this.width = nws.ClientSize.X;
        this.height = nws.ClientSize.Y;

        CenterWindow();
    }

    protected override void OnFramebufferResize(FramebufferResizeEventArgs e) {
        GL.Viewport(0, 0, this.width, this.height);
    }

    protected override void OnLoad() {
        
    }

    protected override void OnRenderFrame(FrameEventArgs args) {
        GL.ClearColor(0.5f, 0.8f, 1.0f, 0.0f);
        GL.Clear(ClearBufferMask.ColorBufferBit);

        SwapBuffers();
    }

    private static void Main(string[] args) {
        GameWindowSettings gws = GameWindowSettings.Default;

        NativeWindowSettings nws = NativeWindowSettings.Default;
        nws.ClientSize = (1024, 768);
        nws.Title = "Teste de Inventario";

        new Program(gws, nws).Run();
    }
}
