using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace RubyDung.src;

public class Window : GameWindow {
    private Tesselator t = new Tesselator();
    private Shader shader;
    private Texture texture;

    private bool isWireframe = false;

    public Window(GameWindowSettings gws, NativeWindowSettings nws) : base(gws, nws) {
        CenterWindow();
    }

    protected override void OnUpdateFrame(FrameEventArgs args) {
        base.OnUpdateFrame(args);

        // Fechar janela
        if(KeyboardState.IsKeyDown(Keys.Escape)) {
            Close();
        }

        // Wireframe
        if(KeyboardState.IsKeyDown(Keys.F3) && KeyboardState.IsKeyPressed(Keys.W)) {
            isWireframe = !isWireframe;

            shader.GetBool("isWireframe", isWireframe);

            GL.PolygonMode(MaterialFace.FrontAndBack, isWireframe ? PolygonMode.Line : PolygonMode.Fill);

            Console.WriteLine($"O modo Wireframe {(isWireframe ? "está ligado" : "está desligado")}");
        }
    }

    protected override void OnLoad() {
        base.OnLoad();

        GL.ClearColor(0.5f, 0.8f, 1.0f, 0.0f);

        shader = new Shader("../../../src/shaders/Vertex.glsl", "../../../src/shaders/Fragment.glsl");
        shader.Use();

        texture = new Texture("../../../src/textures/terrain.png");
        texture.Use();

        Tile.rock.render(t, 0, 0, 0);
        t.flush(shader);
    }

    protected override void OnRenderFrame(FrameEventArgs args) {
        base.OnRenderFrame(args);

        GL.Clear(ClearBufferMask.ColorBufferBit);

        shader.Use();
        texture.Use();

        t.Use();

        SwapBuffers();
    }

    protected override void OnFramebufferResize(FramebufferResizeEventArgs e) {
        base.OnFramebufferResize(e);

        GL.Viewport(0, 0, e.Width, e.Height);
    }
}
