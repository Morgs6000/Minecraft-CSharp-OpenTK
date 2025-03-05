using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace RubyDung;

public class Game : GameWindow {
    private Shader shader;
    private Texture texture;
    private Level level;
    private LevelRenderer levelRenderer;
    private Player player;
    private AABB aabb;
    private Raycast raycast;
    private Crosshair crosshair;

    public Game(GameWindowSettings gws, NativeWindowSettings nws) : base(gws, nws) {
        CenterWindow();
    }

    protected override void OnLoad() {
        base.OnLoad();

        GL.ClearColor(0.5f, 0.8f, 1.0f, 0.0f);

        shader = new Shader("src/shaders/shader_vertex.glsl", "src/shaders/shader_fragment.glsl");
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

        aabb = new AABB(player, level);
        raycast = new Raycast(player, level, levelRenderer);
        crosshair = new Crosshair();
    }

    protected override void OnUpdateFrame(FrameEventArgs args) {
        base.OnUpdateFrame(args);

        if(KeyboardState.IsKeyPressed(Keys.Escape)) {
            Close();
        }

        player.OnUpdateFrame(this);
        aabb.OnUpdateFrame();
        raycast.OnUpdateFrame(this);
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
        view *= Matrix4.CreateTranslation(0.0f, ((player.height / 2) - 1.62f), 0.0f);
        view *= player.GetLookAt();
        shader.SetMatrix4("view", view);

        Matrix4 projection = Matrix4.Identity;
        projection *= player.GetCreatePerspectiveFieldOfView(ClientSize);
        shader.SetMatrix4("projection", projection);
        
        raycast.OnRenderFrame(ClientSize);
        crosshair.OnRenderFrame();

        SwapBuffers();
    }

    protected override void OnFramebufferResize(FramebufferResizeEventArgs e) {
        base.OnFramebufferResize(e);

        GL.Viewport(0, 0, ClientSize.X, ClientSize.Y);
    }
}
