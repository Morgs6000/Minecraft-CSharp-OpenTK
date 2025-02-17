using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using RubyDung.src.level;

namespace RubyDung.src;

public class Window : GameWindow {
    private Shader shader;
    private LevelRenderer levelRenderer;

    public Window(GameWindowSettings gws, NativeWindowSettings nws) : base(gws, nws) {
        CenterWindow();
    }

    protected override void OnUpdateFrame(FrameEventArgs args) {
        base.OnUpdateFrame(args);

        if(KeyboardState.IsKeyDown(Keys.Escape)) {
            Close();
        }
    }

    protected override void OnLoad() {
        base.OnLoad();

        GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);

        shader = new Shader("../../../src/shaders/Vertex.glsl", "../../../src/shaders/Fragment.glsl");

        levelRenderer = new LevelRenderer();
    }

    protected override void OnRenderFrame(FrameEventArgs args) {
        base.OnRenderFrame(args);

        GL.Clear(ClearBufferMask.ColorBufferBit);

        shader.Render();

        levelRenderer.Render();

        Matrix4 view = Matrix4.Identity;
        view *= Matrix4.CreateTranslation(0.0f, 0.0f, -3.0f);
        view *= Matrix4.CreateScale(48.0f);
        shader.SetMatrix4("view", view);
        SetupCamera3();

        SwapBuffers();
    }

    protected override void OnFramebufferResize(FramebufferResizeEventArgs e) {
        base.OnFramebufferResize(e);

        GL.Viewport(0, 0, e.Width, e.Height);
    }

    public void SetupCamera1() {
        Matrix4 projection = Matrix4.Identity;
        projection *= Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45.0f), (float)Size.X / (float)Size.Y, 0.1f, 100.0f);
        shader.SetMatrix4("projection", projection);
    }

    public void SetupCamera2() {
        Matrix4 projection = Matrix4.Identity;
        projection *= Matrix4.CreatePerspectiveOffCenter(0.0f, (float)Size.X, 0.0f, (float)Size.Y, 0.1f, 100.0f);
        shader.SetMatrix4("projection", projection);
    }

    public void SetupCamera3() {
        Matrix4 projection = Matrix4.Identity;
        projection *= Matrix4.CreateOrthographicOffCenter(0.0f, (float)Size.X, 0.0f, (float)Size.Y, 0.1f, 300.0f);
        shader.SetMatrix4("projection", projection);
    }

    public void SetupCamera4() {
        Matrix4 projection = Matrix4.Identity;
        projection *= Matrix4.CreateOrthographic((float)Size.X, (float)Size.Y, 0.1f, 300.0f);
        shader.SetMatrix4("projection", projection);
    }
}
