using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using RubyDung.src.level;

namespace RubyDung.src;

public class Window : GameWindow {
    private int width;
    private int height;

    private Shader shader;
    private Texture texture;
    private Level level;
    private LevelRenderer levelRenderer;

    private bool wireframe_mode = false;

    private Camera camera = new Camera();

    private bool paused = false;

    public Window(GameWindowSettings gws, NativeWindowSettings nws) : base(gws, nws) {
        width = ClientSize.X;
        height = ClientSize.Y;

        CenterWindow();
    }

    protected override void OnUpdateFrame(FrameEventArgs args) {
        base.OnUpdateFrame(args);

        // Pausar o Jogo
        if(KeyboardState.IsKeyPressed(Keys.Escape)) {
            //Close();
            if(!camera.movement_mode) {
                paused = true;

                CursorState = CursorState.Normal;
                MousePosition = new Vector2(width / 2, height / 2);

                Console.WriteLine("O mouse esta solto");
            }
        }

        if(!KeyboardState.IsKeyDown(Keys.F3)) {
            // Movimentação
            if(!camera.movement_mode) {
                camera.ProcessInput(args, KeyboardState);

                if(!paused) {
                    camera.MouseCallback(MouseState);
                }
            }
            else {
                camera.MouseProcessInput(MouseState);
            }
        }
        else {
            // Modo Wireframe
            if(KeyboardState.IsKeyPressed(Keys.W)) {
                wireframe_mode = !wireframe_mode;

                shader.GetBool("wireframe_mode", wireframe_mode);

                GL.PolygonMode(MaterialFace.FrontAndBack, wireframe_mode ? PolygonMode.Line : PolygonMode.Fill);

                Console.WriteLine($"O modo Wireframe {(wireframe_mode ? "está ligado" : "está desligado")}");
            }
            // Modo de Movimentação
            if(KeyboardState.IsKeyPressed(Keys.M)) {
                camera.MovementMode(this, width, height);
            }
        }
    }

    protected override void OnMouseDown(MouseButtonEventArgs e) {
        base.OnMouseDown(e);

        if(!camera.movement_mode) {
            if(paused && e.Button == MouseButton.Left) {
                paused = false;
                camera.firstMouse = true;
                CursorState = CursorState.Grabbed;
                Console.WriteLine("O mouse esta preso");
            }
        }
    }

    protected override void OnLoad() {
        base.OnLoad();

        GL.ClearColor(0.5f, 0.8f, 1.0f, 0.0f);

        shader = new Shader("../../../src/shaders/Vertex.glsl", "../../../src/shaders/Fragment.glsl");
        //shader.Render();

        texture = new Texture("../../../src/textures/terrain.png");
        //texture.Render();

        level = new Level(256, 64, 256);
        levelRenderer = new LevelRenderer(level, shader);

        GL.Enable(EnableCap.DepthTest);
        GL.Enable(EnableCap.CullFace);

        //CursorState = CursorState.Grabbed;
        CursorState = camera.movement_mode ? CursorState.Normal : CursorState.Grabbed;

        if(camera.movement_mode) {
            MousePosition = new Vector2(width / 2, height / 2);
        }

        Console.WriteLine($"firstMouse = {camera._firstMouse}");
    }

    protected override void OnRenderFrame(FrameEventArgs args) {
        base.OnRenderFrame(args);

        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

        shader.Render();
        texture.Render();

        levelRenderer.Render();

        camera.Use(shader, width, height);

        SwapBuffers();
    }

    protected override void OnFramebufferResize(FramebufferResizeEventArgs e) {
        base.OnFramebufferResize(e);

        width = e.Width;
        height = e.Height;

        GL.Viewport(0, 0, e.Width, e.Height);
    }
}
