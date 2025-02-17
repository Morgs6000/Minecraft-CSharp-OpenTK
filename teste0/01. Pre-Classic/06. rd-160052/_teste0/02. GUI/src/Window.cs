using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using RubyDung.src.level;
using RubyDung.src.level.tile;

namespace RubyDung.src;

public class Window : GameWindow {
    private int width;
    private int height;

    private Shader shader;
    private Shader shaderGUI;

    private Texture texture;
    private Level level;
    private LevelRenderer levelRenderer;
    private Camera camera;

    private bool movementMode = true;
    private bool wireframeMode = false;

    private Tesselator tesselator;
    private int paintTexture = 1;

    public Window(GameWindowSettings gws, NativeWindowSettings nws) : base(gws, nws) {
        width = ClientSize.X;
        height = ClientSize.Y;

        CenterWindow();
    }

    protected override void OnUpdateFrame(FrameEventArgs args) {
        base.OnUpdateFrame(args);

        if(KeyboardState.IsKeyDown(Keys.Escape)) {
            Close();
        }

        if(!KeyboardState.IsKeyDown(Keys.F3)) {
            if(!movementMode) {
                camera.ProcessInput(this, args);
                camera.MouseCallback(this);
            }
            else {
                camera.MouseProcessInput(this, args);
            }
        }
        else {
            if(KeyboardState.IsKeyPressed(Keys.M)) {
                MovementMode();
            }
            if(KeyboardState.IsKeyPressed(Keys.W)) {
                WireframeMode();
            }
        }

        if(KeyboardState.IsKeyPressed(Keys.D1)) {
            paintTexture = 1;
            DrawGUIUpdate();
        }
        if(KeyboardState.IsKeyPressed(Keys.D2)) {
            paintTexture = 3;
            DrawGUIUpdate();
        }
        if(KeyboardState.IsKeyPressed(Keys.D3)) {
            paintTexture = 4;
            DrawGUIUpdate();
        }
        if(KeyboardState.IsKeyPressed(Keys.D4)) {
            paintTexture = 5;
            DrawGUIUpdate();
        }
    }

    private void MovementMode() {
        movementMode = !movementMode;

        CursorState = movementMode ? CursorState.Normal : CursorState.Grabbed;

        if(movementMode) {
            MousePosition = new Vector2(width / 2, height / 2);
        }

        Console.WriteLine($"Modo de Movimentação {(movementMode ? "com o teclado e mouse" : "com o mouse")}");
    }

    private void WireframeMode() {
        wireframeMode = !wireframeMode;

        shader.GetBool("wireframeMode", wireframeMode);

        GL.PolygonMode(TriangleFace.FrontAndBack, wireframeMode ? PolygonMode.Line : PolygonMode.Fill);

        Console.WriteLine($"O modo Wireframe {(wireframeMode ? "está ligado." : "está desligado.")}");
    }

    protected override void OnLoad() {
        base.OnLoad();

        GL.ClearColor(0.5f, 0.8f, 1.0f, 0.0f);

        shader = new Shader("../../../src/shaders/Vertex.glsl", "../../../src/shaders/Fragment.glsl");
        shaderGUI = new Shader("../../../src/shaders/Vertex.glsl", "../../../src/shaders/Fragment.glsl");

        texture = new Texture("../../../src/textures/terrain.png");

        level = new Level(256, 64, 256);
        levelRenderer = new LevelRenderer(level);
        levelRenderer.Load();

        camera = new Camera();

        GL.Enable(EnableCap.DepthTest);
        GL.Enable(EnableCap.CullFace);

        //CursorState = CursorState.Grabbed;
        CursorState = movementMode ? CursorState.Normal : CursorState.Grabbed;

        if(movementMode) {
            camera.MouseCallback(this);
        }

        DrawGUILoad();
    }

    protected override void OnRenderFrame(FrameEventArgs args) {
        base.OnRenderFrame(args);

        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

        shader.Render();
        texture.Render();

        levelRenderer.Render();

        SetupCamera();
        camera.Render(shader, width, height);

        DrawGUIRender();

        SwapBuffers();
    }

    protected override void OnFramebufferResize(FramebufferResizeEventArgs e) {
        base.OnFramebufferResize(e);

        width = e.Width;
        height = e.Height;

        GL.Viewport(0, 0, e.Width, e.Height);
    }

    private void SetupCamera() {
        Matrix4 view = Matrix4.Identity;
        shader.SetMatrix4("view", view);

        Matrix4 projection = Matrix4.Identity;
        projection *= Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(70.0f), (float)width / (float)height, 0.05f, 1000.0f);
        shader.SetMatrix4("projection", projection);
    }

    private void SetupOrthoCamera() {
        Matrix4 view = Matrix4.Identity;
        shaderGUI.SetMatrix4("view", view);

        Matrix4 projection = Matrix4.Identity;
        projection *= Matrix4.CreateOrthographicOffCenter(0.0f, (float)width, 0.0f, (float)height, 100.0f, 300.0f);
        shaderGUI.SetMatrix4("projection", projection);
    }

    private void DrawGUILoad() {
        shaderGUI.Render();

        SetupOrthoCamera();

        Matrix4 view = Matrix4.Identity;
        view *= Matrix4.CreateTranslation(1.5f, -0.5f, -0.5f);
        view *= Matrix4.CreateRotationY(MathHelper.DegreesToRadians(45.0f));
        view *= Matrix4.CreateRotationX(MathHelper.DegreesToRadians(30.0f));
        view *= Matrix4.CreateScale(48.0f, 48.0f, 48.0f);
        view *= Matrix4.CreateTranslation((float)(width - 48.0f), (float)(height - 48.0f), 0.0f);
        view *= Matrix4.CreateTranslation(0.0f, 0.0f, -200.0f);
        shaderGUI.SetMatrix4("view", view);

        tesselator = new Tesselator();
        tesselator.Init();
        Tile.tiles[paintTexture].Load(tesselator, level, -2, 0, 0);
        tesselator.Load();
    }

    private void DrawGUIRender() {
        shaderGUI.Render();
        texture.Render();
        tesselator.Render();
    }

    private void DrawGUIUpdate() {
        tesselator.Init();
        Tile.tiles[paintTexture].Load(tesselator, level, -2, 0, 0);
        tesselator.Load();
    }
}
