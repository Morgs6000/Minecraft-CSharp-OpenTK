using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace RubyDung;

public class Game : GameWindow {
    private Shader shader;
    private Shader wireframe_shader;
    private Texture texture;
    private Tesselator t;
    private Tesselator wireframe_t;
    private Player player;

    public Game(GameWindowSettings gws, NativeWindowSettings nws) : base(gws, nws) {
        CenterWindow();
    }

    protected override void OnLoad() {
        base.OnLoad();

        GL.ClearColor(0.5f, 0.8f, 1.0f, 0.0f);

        shader = new Shader("src/shaders/shader_vertex.glsl", "src/shaders/shader_fragment.glsl");
        wireframe_shader = new Shader("src/shaders/shader_vertex.glsl", "src/shaders/wireframe_fragment.glsl");
        texture = new Texture("src/textures/terrain.png");

        t = new Tesselator(shader);
        Tile.tile.OnLoad(t);
        t.OnLoad();

        wireframe_t = new Tesselator(shader);
        Tile.tile.OnLoad(wireframe_t);
        wireframe_t.hasTexture = false;
        wireframe_t.OnLoad();

        // wireframe
        //GL.PolygonMode(TriangleFace.FrontAndBack, PolygonMode.Line);

        player = new Player();
        player.OnLoad(this);

        GL.Enable(EnableCap.DepthTest);
        GL.Enable(EnableCap.CullFace);
    }

    protected override void OnUpdateFrame(FrameEventArgs args) {
        base.OnUpdateFrame(args);

        if(KeyboardState.IsKeyPressed(Keys.Escape)) {
            Close();
        }

        player.OnUpdateFrame(this);
    }

    protected override void OnRenderFrame(FrameEventArgs args) {
        base.OnRenderFrame(args);

        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

        shader.OnRenderFrame();
        texture.OnRenderFrame();
        t.OnRenderFrame();

        Matrix4 model = Matrix4.Identity;
        shader.SetMatrix4("model", model);

        Matrix4 view = Matrix4.Identity;
        view *= player.GetLookAt();
        shader.SetMatrix4("view", view);

        Matrix4 projection = Matrix4.Identity;
        projection *= player.GetCreatePerspectiveFieldOfView(ClientSize);
        shader.SetMatrix4("projection", projection);

        wireframe_shader.OnRenderFrame();
        GL.PolygonMode(TriangleFace.FrontAndBack, PolygonMode.Line);
        wireframe_t.OnRenderFrame();

        wireframe_shader.SetMatrix4("model", model);
        wireframe_shader.SetMatrix4("view", view);
        wireframe_shader.SetMatrix4("projection", projection);

        SwapBuffers();
    }

    protected override void OnFramebufferResize(FramebufferResizeEventArgs e) {
        base.OnFramebufferResize(e);

        GL.Viewport(0, 0, ClientSize.X, ClientSize.Y);
    }
}
