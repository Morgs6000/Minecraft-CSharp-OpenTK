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
    private LevelRenderer levelRenderer;

    private bool wireframeMode = false;

    public Window(GameWindowSettings gws, NativeWindowSettings nws) : base(gws, nws) {
        CenterWindow();

        width = ClientSize.X;
        height = ClientSize.Y;
    }

    protected override void OnUpdateFrame(FrameEventArgs args) {
        base.OnUpdateFrame(args);

        if(KeyboardState.IsKeyDown(Keys.Escape)) {
            Close();
        }

        if(!KeyboardState.IsKeyDown(Keys.F3)) {
            ProcessInput(args);
        } 
        else {
            if(KeyboardState.IsKeyPressed(Keys.W)) {
                wireframeMode = !wireframeMode;

                shader.GetBool("wireframeMode", wireframeMode);

                GL.PolygonMode(TriangleFace.FrontAndBack, wireframeMode ? PolygonMode.Line : PolygonMode.Fill);

                Console.WriteLine($"O modo Wireframe {(wireframeMode ? "está ligado." : "está desligado.")}");
            }
        }
    }

    protected override void OnLoad() {
        base.OnLoad();

        GL.ClearColor(0.5f, 0.8f, 1.0f, 0.0f);

        shader = new Shader("../../../src/shaders/Vertex.glsl", "../../../src/shaders/Fragment.glsl");
        texture = new Texture("../../../src/textures/terrain.png");

        levelRenderer = new LevelRenderer();

        //GL.Enable(EnableCap.DepthTest);
        //GL.Enable(EnableCap.CullFace);

        //CursorState = CursorState.Grabbed;
    }

    protected override void OnRenderFrame(FrameEventArgs args) {
        base.OnRenderFrame(args);

        GL.Clear(ClearBufferMask.ColorBufferBit);
        //GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

        shader.Render();
        texture.Render();

        levelRenderer.Render();

        Matrix4 view = Matrix4.Identity;
        view *= Matrix4.CreateScale(48.0f, 48.0f, 48.0f);
        view *= Matrix4.CreateTranslation(0.0f, 0.0f, -200.0f);
        view *= Matrix4.CreateTranslation(cameraPos);
        shader.SetMatrix4("view", view);

        Matrix4 projection = Matrix4.Identity;
        projection *= Matrix4.CreateOrthographicOffCenter(0, (float)width, 0, (float)height, 100.0f, 300.0f);
        shader.SetMatrix4("projection", projection);

        SwapBuffers();
    }

    protected override void OnFramebufferResize(FramebufferResizeEventArgs e) {
        base.OnFramebufferResize(e);

        GL.Viewport(0, 0, e.Width, e.Height);

        width = e.Width;
        height = e.Height;
    }

    /* ..:: Camera ::.. */
    private Vector3 cameraPos = new Vector3(0.0f, 0.0f, 0.0f);

    private void ProcessInput(FrameEventArgs args) {
        float speed = 4.317f;

        float x = 0.0f;
        float y = 0.0f;
        float z = 0.0f;

        if(KeyboardState.IsKeyDown(Keys.W)) {
            y--;
        }
        if(KeyboardState.IsKeyDown(Keys.S)) {
            y++;
        }
        if(KeyboardState.IsKeyDown(Keys.A)) {
            x++;
        }
        if(KeyboardState.IsKeyDown(Keys.D)) {
            x--;
        }

        cameraPos.X += x * speed * (float)args.Time;
        cameraPos.Y += y * speed * (float)args.Time;
    }
}
