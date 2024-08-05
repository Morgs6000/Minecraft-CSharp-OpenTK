using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using RubyDung.src.level;
using RubyDung.src.level.tile;

namespace RubyDung.src;

public class RubyDung : GameWindow {
    private int width;
    private int height;

    private Shader shader;
    //private Texture texture;

    private Level level;
    private LevelRenderer levelRenderer;

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

    private int paintTexture = 1;
    private Shader shaderGui;
    Tesselator t;

    private void drawGuiLoad() {
        this.shaderGui = new Shader("vertexShader.glsl", "fragmentShader.glsl");

        //this.texture = new Texture("terrain.png");
    }

    private void drawGui() {
        this.shaderGui.use();

        Matrix4 projection = Matrix4.Identity;
        //projection *= Matrix4.CreateOrthographic((float)this.width, (float)this.height, 0.3f, 1000.0f);
        projection *= Matrix4.CreateOrthographicOffCenter(0.0f, (float)this.width, 0.0f, (float)this.height, 0.3f, 1000.0f);
        this.shaderGui.setMat4("projection", projection);

        Matrix4 view = Matrix4.Identity;
        view *= Matrix4.CreateTranslation(1.5f, -0.5f, -0.5f);

        view *= Matrix4.LookAt(new Vector3(1.0f, 1.0f, 1.0f), new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 1.0f, 0.0f));
        //view *= Matrix4.CreateRotationY(45.0f);
        //view *= Matrix4.CreateRotationX(30.0f);

        view *= Matrix4.CreateTranslation(0.0f, 0.0f, -10.0f);

        view *= Matrix4.CreateScale(48.0f, 48.0f, 48.0f);
        view *= Matrix4.CreateTranslation((float)(this.width - 48), (float)(this.height - 48), 0.0f);
        this.shaderGui.setMat4("view", view);

        //this.texture.bind();

        t = new Tesselator();
        t.init();
        Tile.tiles[this.paintTexture].render(t, this.level, -2, 0, 0);
        t.flush();

        t.bind();
    }

    protected override void OnLoad() {
        this.openGL_settins();

        this.shader = new Shader("vertexShader.glsl", "fragmentShader.glsl");

        //this.texture = new Texture("terrain.png");
        Textures.loadTexture("terrain.png", TextureMinFilter.Nearest, TextureMagFilter.Nearest);

        this.level = new Level(256, 64, 256);
        this.levelRenderer = new LevelRenderer(this.level);

        this.CursorState = CursorState.Grabbed;

        this.drawGuiLoad();
    }

    protected override void OnRenderFrame(FrameEventArgs args) {
        GL.ClearColor(Color.Hex("7FCCFF", 255));
        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

        this.shader.use();

        this.matrix.matrix(this.width, this.height, this.shader);

        //this.texture.bind();

        this.levelRenderer.render();

        this.drawGui();

        this.SwapBuffers();
    }

    protected override void OnUpdateFrame(FrameEventArgs args) {
        KeyboardState input = this.KeyboardState;

        if(input.IsKeyDown(Keys.Escape)) {
            this.Close();
        }

        Wireframe.Mode(input, this.shader);

        if(!input.IsKeyDown(Keys.F3)) {
            this.matrix.processInput(input);
        }

        this.matrix.mouse_callback(this.MouseState.X, this.MouseState.Y);

        if(input.IsKeyDown(Keys.D1)) {
            this.paintTexture = 1;
        }
        if(input.IsKeyDown(Keys.D2)) {
            this.paintTexture = 3;
        }
        if(input.IsKeyDown(Keys.D3)) {
            this.paintTexture = 4;
        }
        if(input.IsKeyDown(Keys.D4)) {
            this.paintTexture = 5;
        }
    }

    private static void Main(string[] args) {
        GameWindowSettings gws = GameWindowSettings.Default;

        NativeWindowSettings nws = NativeWindowSettings.Default;
        nws.ClientSize = (1024, 768);
        nws.Title = "Game";

        new RubyDung(gws, nws).Run();
    }
}
