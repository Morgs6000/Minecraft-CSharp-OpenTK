using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using RubyDung.src.level;

namespace RubyDung.src;

public class Game : GameWindow {
    private Shader shader;
    private Texture texture;
    private Level level;
    private LevelRenderer levelRenderer;
    private Player player;

    public Game(GameWindowSettings gws, NativeWindowSettings nws) : base(gws, nws) {
        CenterWindow();
    }

    protected override void OnLoad() {
        base.OnLoad();

        GL.ClearColor(0.5f, 0.8f, 1.0f, 0.0f);

        shader = new Shader("../../../src/shaders/Vertex.glsl", "../../../src/shaders/Fragment.glsl");

        texture = new Texture("../../../src/textures/terrain.png");
        
        level = new Level(256, 64, 256);
        levelRenderer = new LevelRenderer(level);
        levelRenderer.Load();

        player = new Player(level);

        CursorState = movement ? CursorState.Grabbed : CursorState.Normal;

        if(!movement) {
            player.MouseCallback(this);
        }

        GL.Enable(EnableCap.DepthTest);
        GL.Enable(EnableCap.CullFace);
    }

    protected override void OnFramebufferResize(FramebufferResizeEventArgs e) {
        base.OnFramebufferResize(e);

        GL.Viewport(0, 0, e.Width, e.Height);
    }

    protected override void OnUpdateFrame(FrameEventArgs args) {
        base.OnUpdateFrame(args);

        if(KeyboardState.IsKeyDown(Keys.Escape)) {
            Close();
        }

        if(!KeyboardState.IsKeyDown(Keys.F3)) {
            if(movement) {
                player.ProcessInput(this, args);
                player.MouseCallback(this);
            }
            else {
                player.MouseProcessInput(this);
            }
        }
        else {
            if(KeyboardState.IsKeyPressed(Keys.W)) {
                Wireframe();
            }
            if(KeyboardState.IsKeyPressed(Keys.M)) {
                Movement();
            }
        }
    }

    protected override void OnRenderFrame(FrameEventArgs args) {
        base.OnRenderFrame(args);

        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

        shader.Render();

        texture.Render();

        levelRenderer.Render();

        player.Render(shader, ClientSize.X, ClientSize.Y);

        SwapBuffers();
    }

    /* ..:: Wireframe ::.. */

    private bool wireframe = false;

    private void Wireframe() {
        wireframe = !wireframe;

        shader.SetBool("wireframe", wireframe);

        GL.PolygonMode(TriangleFace.FrontAndBack, wireframe ? PolygonMode.Line : PolygonMode.Fill);

        Console.WriteLine($"Wireframe: {(wireframe ? "ON" : "OFF")}");
    }

    /* ..:: Movement ::.. */

    private bool movement = true;

    private void Movement() {
        movement = !movement;

        CursorState = movement ? CursorState.Grabbed : CursorState.Normal;

        if(!movement) {
            MousePosition = new Vector2(ClientSize.X / 2, ClientSize.Y / 2);
        }

        Console.WriteLine($"Modo de Movimentação {(movement ? "com o teclado e mouse" : "com o mouse")}");
    }
}
