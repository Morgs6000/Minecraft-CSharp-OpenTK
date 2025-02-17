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
    
    private List<float> vertexBuffer = new List<float>();
    private List<int> indiceBuffer = new List<int>();
    private List<float> texCoordBuffer = new List<float>();

    private int vertices = 0;

    private int VertexArrayObject;
    private int VertexBufferObject;
    private int ElementBufferObject;
    private int TextureBufferObject;

    private bool wireframeMode = false;

    private Vector3 eye = new Vector3(0.0f, 0.0f, -3.0f);
    private Vector3 target = new Vector3(0.0f, 0.0f, 1.0f);
    private Vector3 up = new Vector3(0.0f, 1.0f, 0.0f);

    private Vector2 lastPos;

    private float pitch;        //xRot
    private float yaw = -90.0f; //yRot

    private bool firstMouse = true;

    private float fov = 60.0f;

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
            ProcessInput(args);
            MouseCallback();
        }
        else {
            if(KeyboardState.IsKeyPressed(Keys.W)) {
                Wireframe();
            }
        }
    }

    private void ProcessInput(FrameEventArgs args) {
        float speed = 1.5f;

        float x = 0.0f;
        float y = 0.0f;
        float z = 0.0f;

        if(KeyboardState.IsKeyDown(Keys.W)) {
            z++;
        }
        if(KeyboardState.IsKeyDown(Keys.S)) {
            z--;
        }
        if(KeyboardState.IsKeyDown(Keys.A)) {
            x--;
        }
        if(KeyboardState.IsKeyDown(Keys.D)) {
            x++;
        }

        if(KeyboardState.IsKeyDown(Keys.Space)) {
            y++;
        }
        if(KeyboardState.IsKeyDown(Keys.LeftShift)) {
            y--;
        }

        eye += x * Vector3.Normalize(Vector3.Cross(target, up)) * speed * (float)args.Time;
        eye += y * up * speed * (float)args.Time;
        eye += z * Vector3.Normalize(new Vector3(target.X, 0.0f, target.Z)) * speed * (float)args.Time;
    }

    private void MouseCallback() {
        float sensitivity = 0.2f;

        if(firstMouse) {
            lastPos = new Vector2(MouseState.X, MouseState.Y);
            firstMouse = false;
        }
        else {
            float deltaX = MouseState.X - lastPos.X;
            float deltaY = MouseState.Y - lastPos.Y;
            lastPos = new Vector2(MouseState.X, MouseState.Y);

            yaw += deltaX * sensitivity;
            pitch -= deltaY * sensitivity;

            if(pitch > 89.0f) {
                pitch = 89.0f;
            }
            if(pitch < -89.0f) {
                pitch = -89.0f;
            }
        }

        target.X = (float)Math.Cos(MathHelper.DegreesToRadians(pitch)) * (float)Math.Cos(MathHelper.DegreesToRadians(yaw));
        target.Y = (float)Math.Sin(MathHelper.DegreesToRadians(pitch));
        target.Z = (float)Math.Cos(MathHelper.DegreesToRadians(pitch)) * (float)Math.Sin(MathHelper.DegreesToRadians(yaw));
        target = Vector3.Normalize(target);
    }

    private void Wireframe() {
        wireframeMode = !wireframeMode;

        shader.GetBool("wireframeMode", wireframeMode);

        GL.PolygonMode(TriangleFace.FrontAndBack, wireframeMode ? PolygonMode.Line : PolygonMode.Fill);

        Console.WriteLine($"O modo Wireframe {(wireframeMode ? "está ligado." : "está desligado.")}");
    }

    protected override void OnLoad() {
        base.OnLoad();

        GL.ClearColor(0.5f, 0.8f, 1.0f, 0.0f);

        shader = new Shader("../../../src/shaders/Vertex.glsl", "../../../src/shaders/Fragment.glsl");
        texture = new Texture("../../../src/textures/terrain.png");

        LoadTile(0, 0, 0);
        LoadBlock();

        GL.Enable(EnableCap.DepthTest);
        GL.Enable(EnableCap.CullFace);

        CursorState = CursorState.Grabbed;
    }

    protected override void OnRenderFrame(FrameEventArgs args) {
        base.OnRenderFrame(args);

        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

        shader.Render();
        texture.Render();

        RenderBlock();

        Matrix4 view = Matrix4.Identity;
        view *= Matrix4.CreateTranslation(0.0f, 0.0f, -10.0f);
        view *= Matrix4.LookAt(eye, eye + target, up);
        shader.SetMatrix4("view", view);

        Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(fov), (float)width / (float)height, 0.05f, 1000.0f);
        shader.SetMatrix4("projection", projection);

        SwapBuffers();
    }

    protected override void OnFramebufferResize(FramebufferResizeEventArgs e) {
        base.OnFramebufferResize(e);

        width = e.Width;
        height = e.Height;

        GL.Viewport(0, 0, e.Width, e.Height);
    }

    public void LoadBlock() {
        VertexArrayObject = GL.GenVertexArray();
        GL.BindVertexArray(VertexArrayObject);

        VertexBufferObject = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);
        GL.BufferData(BufferTarget.ArrayBuffer, vertexBuffer.Count * sizeof(float), vertexBuffer.ToArray(), BufferUsageHint.StaticDraw);

        GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 0, 0);
        GL.EnableVertexAttribArray(0);

        ElementBufferObject = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);
        GL.BufferData(BufferTarget.ElementArrayBuffer, indiceBuffer.Count * sizeof(int), indiceBuffer.ToArray(), BufferUsageHint.StaticDraw);

        TextureBufferObject = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ArrayBuffer, TextureBufferObject);
        GL.BufferData(BufferTarget.ArrayBuffer, texCoordBuffer.Count * sizeof(float), texCoordBuffer.ToArray(), BufferUsageHint.StaticDraw);

        GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 0, 0);
        GL.EnableVertexAttribArray(1);
    }

    public void RenderBlock() {
        GL.BindVertexArray(VertexArrayObject);
        GL.DrawElements(PrimitiveType.Triangles, indiceBuffer.Count, DrawElementsType.UnsignedInt, 0);
    }

    public void Vertex(float x, float y, float z) {
        vertexBuffer.Add(x);
        vertexBuffer.Add(y);
        vertexBuffer.Add(z);
    }

    public void Indice() {
        indiceBuffer.Add(0 + vertices);
        indiceBuffer.Add(1 + vertices);
        indiceBuffer.Add(2 + vertices);

        indiceBuffer.Add(0 + vertices);
        indiceBuffer.Add(2 + vertices);
        indiceBuffer.Add(3 + vertices);

        vertices += 4;
    }

    public void Tex(float u, float v) {
        texCoordBuffer.Add(u);
        texCoordBuffer.Add(v);
    }

    public void LoadTile(int x, int y, int z) {
        float x0 = (float)x + 0.0f;
        float y0 = (float)y + 0.0f;
        float z0 = (float)z + 0.0f;

        float x1 = (float)x + 1.0f;
        float y1 = (float)y + 1.0f;
        float z1 = (float)z + 1.0f;

        float u0 = (float)0 / 16.0f;
        float u1 = u0 + (1.0f / 16.0f);
        float v0 = (16.0f - 1.0f) / 16.0f;
        float v1 = v0 + (1.0f / 16.0f);

        //x0
        Vertex(x0, y0, z0);
        Vertex(x0, y0, z1);
        Vertex(x0, y1, z1);
        Vertex(x0, y1, z0);

        Indice();

        Tex(u0, v0);
        Tex(u1, v0);
        Tex(u1, v1);
        Tex(u0, v1);

        //x1
        Vertex(x1, y0, z1);
        Vertex(x1, y0, z0);
        Vertex(x1, y1, z0);
        Vertex(x1, y1, z1);

        Indice();

        Tex(u0, v0);
        Tex(u1, v0);
        Tex(u1, v1);
        Tex(u0, v1);

        //y0
        Vertex(x0, y0, z0);
        Vertex(x1, y0, z0);
        Vertex(x1, y0, z1);
        Vertex(x0, y0, z1);

        Indice();

        Tex(u0, v0);
        Tex(u1, v0);
        Tex(u1, v1);
        Tex(u0, v1);

        //y1
        Vertex(x0, y1, z1);
        Vertex(x1, y1, z1);
        Vertex(x1, y1, z0);
        Vertex(x0, y1, z0);

        Indice();

        Tex(u0, v0);
        Tex(u1, v0);
        Tex(u1, v1);
        Tex(u0, v1);

        //z0
        Vertex(x1, y0, z0);
        Vertex(x0, y0, z0);
        Vertex(x0, y1, z0);
        Vertex(x1, y1, z0);

        Indice();

        Tex(u0, v0);
        Tex(u1, v0);
        Tex(u1, v1);
        Tex(u0, v1);

        //z1
        Vertex(x0, y0, z1);
        Vertex(x1, y0, z1);
        Vertex(x1, y1, z1);
        Vertex(x0, y1, z1);

        Indice();

        Tex(u0, v0);
        Tex(u1, v0);
        Tex(u1, v1);
        Tex(u0, v1);
    }
}
