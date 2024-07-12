using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using RubyDung.src.level;
using System.Drawing;

namespace RubyDung.src;

public class Program : GameWindow {
    private int width;
    private int height;

    private Shader shader;
    private Texture texture;
    private LevelRenderer levelRenderer;

    private bool isWireframe = false;

    private Program(GameWindowSettings gws, NativeWindowSettings nws) : base(gws, nws) {
        this.width = ClientSize.X;
        this.height = ClientSize.Y;

        CenterWindow();
    }

    protected override void OnFramebufferResize(FramebufferResizeEventArgs e) {
        this.width = e.Width;
        this.height = e.Height;

        GL.Viewport(0, 0, e.Width, e.Height);
    }

    protected override void OnLoad() {
        GL.Enable(EnableCap.DepthTest);

        this.shader = new Shader("shader.vert", "shader.frag");

        this.shader.use();

        this.levelRenderer = new LevelRenderer();
        this.texture = new Texture("terrain.png");
    }

    protected override void OnRenderFrame(FrameEventArgs args) {
        this.processInput();

        GL.ClearColor(0.5f, 0.8f, 1.0f, 0.0f);
        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

        this.texture.bind();

        this.matrix();

        this.shader.use();

        this.levelRenderer.render();

        SwapBuffers();
    }

    private void matrix() {
        this.matrixProjection();
        this.matrixView();
        //this.matrixModel();
    }

    private void matrixProjection() {
        Matrix4 projection = Matrix4.Identity;

        //projection *= CreatePerspectiveFieldOfView();
        //projection *= CreatePerspectiveOffCenter();
        projection *= CreateOrthographic();
        //projection *= CreateOrthographicOffCenter();

        this.shader.setMatrix4("projection", projection);

        //Console.WriteLine("Matriz de Projeção:");
        //Console.WriteLine(projection);
    }

    private Matrix4 CreatePerspectiveFieldOfView() {
        float fovy = MathHelper.DegreesToRadians(60.0f);
        float aspect = (float)this.width / (float)this.height;
        float depthNear = 0.3f;
        float depthFar = 1000.0f;

        return Matrix4.CreatePerspectiveFieldOfView(fovy, aspect, depthNear, depthFar);
    }

    private Matrix4 CreatePerspectiveOffCenter() {
        //float left = 0.0f;
        //float right = (float)this.width;
        //float bottom = 0.0f;
        //float top = (float)this.height;

        //float left = -(float)this.width / 2.0f;
        //float right = (float)this.width / 2.0f;
        //float bottom = -(float)this.height / 2.0f;
        //float top = (float)this.height;

        float left = -1.0f;
        float right = 1.0f;
        float bottom = -1.0f;
        float top = 1.0f;

        float depthNear = 0.1f;
        float depthFar = 100.0f;

        return Matrix4.CreatePerspectiveOffCenter(left, right, bottom, top, depthNear, depthFar);
    }

    private Matrix4 CreateOrthographic() {
        float width = (float)this.width;
        float height = (float)this.height;
        float depthNear = 0.3f;
        float depthFar = 1000.0f;

        return Matrix4.CreateOrthographic(width, height, depthNear, depthFar);
    }

    private Matrix4 CreateOrthographicOffCenter() {
        float left = 0.0f;
        float right = (float)this.width;
        float bottom = 0.0f;
        float top = (float)this.height;
        float depthNear = 0.3f;
        float depthFar = 1000.0f;

        return Matrix4.CreateOrthographicOffCenter(left, right, bottom, top, depthNear, depthFar);
    }

    private void matrixView() {
        // Configura a matriz de visualização isométrica
        Vector3 eye = new Vector3(-1, 1, 1) * 10; // Posição da câmera
        Vector3 target = Vector3.Zero; // Onde a câmera está olhando
        Vector3 up = Vector3.UnitY; // Direção "para cima" da câmera

        Matrix4 view = Matrix4.Identity;

        view *= Matrix4.LookAt(eye, target, up);

        //view *= Matrix4.CreateTranslation(0.0f, 0.0f, -10.0f);

        view *= Matrix4.CreateScale(10.0f);

        this.shader.setMatrix4("view", view);
    }

    private void matrixModel() {
        Matrix4 model = Matrix4.Identity;

        //*
        Vector2 position = new Vector2(200.0f, 200.0f);
        Vector2 size = new Vector2(300.0f, 400.0f);
        float rotate = 45.0f;

        //model *= Matrix4.CreateScale(new Vector3(size.X, size.Y, 1.0f)); // última escala

        //model *= Matrix4.CreateTranslation(new Vector3(-0.5f * size.X, -0.5f * size.Y, 0.0f)); // mover origem de volta
        //model *= Matrix4.CreateFromAxisAngle(new Vector3(0.0f, 0.0f, 1.0f), MathHelper.DegreesToRadians(rotate)); // depois gira
        //model *= Matrix4.CreateTranslation(new Vector3(0.5f * size.X, 0.5f * size.Y, 0.0f)); // move a origem da rotação para o centro do quadrante

        //model *= Matrix4.CreateTranslation(new Vector3(position.X, position.Y, 0.0f)); // primeira tradução (as transformações são: a escala acontece primeiro, depois a rotação e depois a tradução final acontece; ordem inversa)
        //*/

        //model *= Matrix4.CreateScale(1.0f);
        //model *= Matrix4.CreateScale(1.0f, 1.0f, 1.0f);

        this.shader.setMatrix4("model", model);
    }

    private void processInput() {
        // fechar janela
        if(KeyboardState.IsKeyDown(Keys.Escape)) {
            Close();
        }

        // wireframe mode
        if(KeyboardState.IsKeyDown(Keys.F3) && KeyboardState.IsKeyPressed(Keys.W)) {
            this.isWireframe = !this.isWireframe;

            this.shader.setBool("isWireframe", this.isWireframe);

            GL.PolygonMode(MaterialFace.FrontAndBack, this.isWireframe ? PolygonMode.Line : PolygonMode.Fill);
        }
    }

    private static void Main(string[] args) {
        GameWindowSettings gws = GameWindowSettings.Default;

        NativeWindowSettings nws = NativeWindowSettings.Default;
        nws.ClientSize = (1024, 768);
        //nws.ClientSize = (800, 600);
        nws.Title = "Game";

        new Program(gws, nws).Run();
    }
}