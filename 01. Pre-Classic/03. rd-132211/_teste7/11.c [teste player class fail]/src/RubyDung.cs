using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using RubyDung.src.level;

namespace RubyDung.src;

public class RubyDung : GameWindow {
    private int width;
    private int height;

    private Shader shader;
    private Texture texture;

    private Level level;
    private LevelRenderer levelRenderer;
    private Player player;

    private Matrix matrix = new Matrix();

    public RubyDung(GameWindowSettings gws, NativeWindowSettings nws) : base(gws, nws) {
        this.width = this.ClientSize.X;
        this.height = this.ClientSize.Y;

        this.CenterWindow();
    }

    protected override void OnFramebufferResize(FramebufferResizeEventArgs e) {
        this.width = e.Width;
        this.height = e.Height;

        GL.Viewport(0, 0, e.Width, e.Height);
    }

    private void openGL_settins() {
        GL.Enable(EnableCap.DepthTest);

        GL.Enable(EnableCap.CullFace);
        GL.CullFace(CullFaceMode.Back);
    }

    protected override void OnLoad() {
        float fr = 0.5f;
        float fg = 0.8f;
        float fb = 1.0f;

        GL.ClearColor(fr, fg, fb, 0.0f);

        this.openGL_settins();

        this.shader = new Shader("vertexShader.glsl", "fragmentShader.glsl");

        this.texture = new Texture("terrain.png");

        this.level = new Level(256, 64, 256);
        this.levelRenderer = new LevelRenderer(this.level);
        this.player = new Player();

        this.CursorState = CursorState.Grabbed;
    }

    public void tick(KeyboardState input) {
        this.player.tick(input, this.deltaTime, this.eye, this.target, this.up);
    }

    private Vector3 eye = new Vector3(0.0f, 0.0f, 3.0f);
    private Vector3 target = new Vector3(0.0f, 0.0f, -1.0f);
    private Vector3 up = new Vector3(0.0f, 1.0f, 0.0f);

    private void setupCampera() {
        Matrix4 projection = Matrix4.Identity;
        projection *= Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(70.0f), (float)this.width / (float)this.height, 0.05f, 1000.0f);
        this.shader.setMat4("projection", projection);

        Matrix4 view = Matrix4.Identity;
        view *= Matrix4.CreateRotationY(MathHelper.DegreesToRadians(180.0f));
        view *= Matrix4.CreateTranslation(0.0f, 0.0f, -10.0f);
        view *= Matrix4.LookAt(this.eye, this.eye + this.target, this.up);
        this.shader.setMat4("view", view);
    }

    private float deltaTime = 0.0f;
    private float lastFrame = 0.0f;

    public void time() {
        float curretFrame = (float)GLFW.GetTime();

        this.deltaTime = curretFrame - this.lastFrame;
        this.lastFrame = curretFrame;
    }

    protected override void OnRenderFrame(FrameEventArgs args) {
        this.time();

        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

        this.shader.use();

        //this.matrix.matrix(this.width, this.height, this.shader);
        this.setupCampera();

        this.texture.bind();

        this.levelRenderer.render();

        this.SwapBuffers();
    }

    protected override void OnUpdateFrame(FrameEventArgs args) {
        KeyboardState input = this.KeyboardState;

        if(input.IsKeyDown(Keys.Escape)) {
            this.Close();
        }

        Wireframe.Mode(input, this.shader);

        if(!input.IsKeyDown(Keys.F3)) {
            //this.matrix.processInput(input);
            this.tick(input);
            //this.player.tick(input, this.deltaTime, this.eye, this.target, this.up);
        }

        //this.matrix.mouse_callback(this.MouseState.X, this.MouseState.Y);
    }

    private static void Main(string[] args) {
        GameWindowSettings gws = GameWindowSettings.Default;

        NativeWindowSettings nws = NativeWindowSettings.Default;
        nws.ClientSize = (1024, 768);
        nws.Title = "Game";

        new RubyDung(gws, nws).Run();
    }
}
