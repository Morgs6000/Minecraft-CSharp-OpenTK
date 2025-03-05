using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace RubyDung;

public class Game : GameWindow {
    private int width;
    private int height;

    private Shader shader;
    private Shader shaderGUI;
    private Shader shaderGUI_crosshair;
    private Texture texture;
    private Level level;
    private LevelRenderer levelRenderer;
    private Player player;

    private Tesselator t;
    private Tesselator t_crosshair;
    private int paintTexture = 1;

    public Game(GameWindowSettings gws, NativeWindowSettings nws) : base(gws, nws) {
        CenterWindow();

        width = ClientSize.X;
        height = ClientSize.Y;
    }

    protected override void OnLoad() {
        base.OnLoad();

        GL.ClearColor(0.5f, 0.8f, 1.0f, 0.0f);

        shader = new Shader("src/shaders/shader_vertex.glsl", "src/shaders/shader_fragment.glsl");
        shaderGUI = new Shader("src/shaders/shader_vertex.glsl", "src/shaders/shader_fragment.glsl");
        shaderGUI_crosshair = new Shader("src/shaders/shader_vertex.glsl", "src/shaders/shader_fragment.glsl");

        texture = new Texture("src/textures/terrain.png");

        level = new Level(256, 64, 256);
        levelRenderer = new LevelRenderer(shader, level);
        levelRenderer.OnLoad();

        // wireframe
        //GL.PolygonMode(TriangleFace.FrontAndBack, PolygonMode.Line);

        player = new Player(level);
        player.OnLoad(this);

        GL.Enable(EnableCap.DepthTest);
        GL.Enable(EnableCap.CullFace);

        DrawGUI_OnLoad();
        DrawGUI_crosshair_OnLoad();
    }

    protected override void OnUpdateFrame(FrameEventArgs args) {
        base.OnUpdateFrame(args);

        if(KeyboardState.IsKeyPressed(Keys.Escape)) {
            Close();
        }

        player.OnUpdateFrame(this);

        if(KeyboardState.IsKeyPressed(Keys.D1)) {
            paintTexture = 1;
            DrawGUI_OnUpdateFrame();
        }
        if(KeyboardState.IsKeyPressed(Keys.D2)) {
            paintTexture = 3;
            DrawGUI_OnUpdateFrame();
        }
        if(KeyboardState.IsKeyPressed(Keys.D3)) {
            paintTexture = 4;
            DrawGUI_OnUpdateFrame();
        }
        if(KeyboardState.IsKeyPressed(Keys.D4)) {
            paintTexture = 5;
            DrawGUI_OnUpdateFrame();
        }
        if(KeyboardState.IsKeyPressed(Keys.D6)) {
            paintTexture = 6;
            DrawGUI_OnUpdateFrame();
        }
    }

    protected override void OnRenderFrame(FrameEventArgs args) {
        base.OnRenderFrame(args);

        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

        shader.OnRenderFrame();
        texture.OnRenderFrame();
        
        levelRenderer.OnRenderFrame();

        Matrix4 model = Matrix4.Identity;
        shader.SetMatrix4("model", model);

        Matrix4 view = Matrix4.Identity;
        view *= player.LookAt();
        shader.SetMatrix4("view", view);

        Matrix4 projection = Matrix4.Identity;
        projection *= CreatePerspectiveFieldOfView();
        shader.SetMatrix4("projection", projection);

        DrawGUI_OnRenderFrame();
        DrawGUI_crosshair_OnRenderFrame();

        SwapBuffers();
    }

    protected override void OnFramebufferResize(FramebufferResizeEventArgs e) {
        base.OnFramebufferResize(e);

        GL.Viewport(0, 0, ClientSize.X, ClientSize.Y);

        width = ClientSize.X;
        height = ClientSize.Y;
    }

    public Matrix4 CreatePerspectiveFieldOfView() {
        float fovy      = MathHelper.DegreesToRadians(70.0f);
        float aspect    = (float)width / (float)height;
        float depthNear = 0.05f;
        float depthFar  = 1000.0f;

        return Matrix4.CreatePerspectiveFieldOfView(fovy, aspect, depthNear, depthFar);
    }

    public Matrix4 CreateOrthographicOffCenter() {
        float left = 0.0f;
        float right = (float)width;
        float bottom = 0.0f;
        float top = (float)height;
        float depthNear = 100.0f;
        float depthFar  = 300.0f;

        return Matrix4.CreateOrthographicOffCenter(left, right, bottom, top, depthNear, depthFar);
    }

    private void DrawGUI_OnLoad() {
        shaderGUI.OnRenderFrame();

        Matrix4 model = Matrix4.Identity;
        shaderGUI.SetMatrix4("model", model);

        Matrix4 view = Matrix4.Identity;
        view *= Matrix4.CreateTranslation(1.5f, -0.5f, -0.5f);
        view *= Matrix4.CreateRotationY(MathHelper.DegreesToRadians(45.0f));
        view *= Matrix4.CreateRotationX(MathHelper.DegreesToRadians(30.0f));
        view *= Matrix4.CreateScale(48.0f, 48.0f, 48.0f);
        view *= Matrix4.CreateTranslation((float)(width - 48.0f), (float)(height - 48.0f), 0.0f);
        view *= Matrix4.CreateTranslation(0.0f, 0.0f, -200.0f);
        shaderGUI.SetMatrix4("view", view);

        Matrix4 projection = Matrix4.Identity;
        projection *= CreateOrthographicOffCenter();
        shaderGUI.SetMatrix4("projection", projection);

        t = new Tesselator(shaderGUI);
        t.Init();
        Tile.tiles[paintTexture].OnLoad(t, level, -2, 0, 0);
        t.OnLoad();
    }

    private void DrawGUI_OnRenderFrame() {
        shaderGUI.OnRenderFrame();
        texture.OnRenderFrame();
        t.OnRenderFrame();
    }

    private void DrawGUI_OnUpdateFrame() {
        t.Init();
        Tile.tiles[paintTexture].OnLoad(t, level, -2, 0, 0);
        t.OnLoad();

        shaderGUI.OnRenderFrame();
        texture.OnRenderFrame();
        t.OnRenderFrame();
    }

    private void DrawGUI_crosshair_OnLoad() {
        shaderGUI_crosshair.OnRenderFrame();

        Matrix4 model_crosshair = Matrix4.Identity;
        shaderGUI_crosshair.SetMatrix4("model", model_crosshair);

        Matrix4 view_crosshair = Matrix4.Identity;
        view_crosshair *= Matrix4.CreateTranslation(0.0f, 0.0f, -3.0f);
        view_crosshair *= Matrix4.CreateScale(48.0f, 48.0f, 48.0f);
        view_crosshair *= Matrix4.CreateTranslation((float)(width / 2), (float)(height / 2), 0.0f);
        shaderGUI_crosshair.SetMatrix4("view", view_crosshair);

        Matrix4 projection_crosshair = Matrix4.Identity;
        projection_crosshair *= CreateOrthographicOffCenter();
        shaderGUI_crosshair.SetMatrix4("projection", projection_crosshair);

        t_crosshair = new Tesselator(shaderGUI_crosshair);

        int wc = width / 2;
        int hc = height / 2;

        t_crosshair.Init();

        // t_crosshair.Vertex((float)(wc - 0), (float)(hc - 8), 0.0f);
        // t_crosshair.Vertex((float)(wc + 1), (float)(hc - 8), 0.0f);
        // t_crosshair.Vertex((float)(wc + 1), (float)(hc + 9), 0.0f);
        // t_crosshair.Vertex((float)(wc - 0), (float)(hc + 9), 0.0f);
        t_crosshair.Vertex(-0.5f, -0.05f, 0.0f);
        t_crosshair.Vertex( 0.5f, -0.05f, 0.0f);
        t_crosshair.Vertex( 0.5f,  0.05f, 0.0f);
        t_crosshair.Vertex(-0.5f,  0.05f, 0.0f);
        t_crosshair.Indice();

        // t_crosshair.Vertex((float)(wc + 9), (float)(hc - 0), 0.0f);
        // t_crosshair.Vertex((float)(wc - 8), (float)(hc - 0), 0.0f);
        // t_crosshair.Vertex((float)(wc - 8), (float)(hc + 1), 0.0f);
        // t_crosshair.Vertex((float)(wc + 9), (float)(hc + 1), 0.0f);
        t_crosshair.Vertex(-0.05f, -0.5f, 0.0f);
        t_crosshair.Vertex( 0.05f, -0.5f, 0.0f);
        t_crosshair.Vertex( 0.05f,  0.5f, 0.0f);
        t_crosshair.Vertex(-0.05f,  0.5f, 0.0f);
        t_crosshair.Indice();

        t_crosshair.OnLoad();
    }

    private void DrawGUI_crosshair_OnRenderFrame() {
        shaderGUI_crosshair.OnRenderFrame();
        t_crosshair.OnRenderFrame();
    }
}
