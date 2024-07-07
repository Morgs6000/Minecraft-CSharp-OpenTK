using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace RubyDung.src;

public class RubyDung : GameWindow {
    private int width;
    private int height;

    private static void Main(string[] args) {
        Console.WriteLine("Hello, World!");

        new RubyDung(1024, 768, "Game").Run();
    }

    public RubyDung(int widht, int height, string title)
        : base(GameWindowSettings.Default, new NativeWindowSettings() {
            ClientSize = (widht, height),
            Title = title
        }) {
        this.width = widht;
        this.height = height;

        CenterWindow();
    }

    protected override void OnFramebufferResize(FramebufferResizeEventArgs e) {
        base.OnFramebufferResize(e);

        this.width = e.Width;
        this.height = e.Height;

        GL.Viewport(0, 0, e.Width, e.Height);
    }

    // loop de renderização
    protected override void OnRenderFrame(FrameEventArgs args) {
        base.OnRenderFrame(args);

        // entrada
        this.processInput();

        // renderizar
        GL.ClearColor(0.5f, 0.8f, 1.0f, 0.0f);
        GL.Clear(ClearBufferMask.ColorBufferBit);

        // verificar e chamar eventos e trocar os buffers
        SwapBuffers();
    }

    public void processInput() {
        if(KeyboardState.IsKeyDown(Keys.Escape)) {
            Close();
        }
    }
}
