using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace ConsoleApp1.src;

public class Window : GameWindow {
    private int width;
    private int height;

    public Window(GameWindowSettings gws, NativeWindowSettings nws) : base(gws, nws) {
        this.width = this.ClientSize.X;
        this.height = this.ClientSize.Y;
        
        CenterWindow();
    }

    // ..:: Shader ::..
    private Shader shader;
    private Shader shaderGUI;

    // ..:: Triangle ::..
    private List<float> vertexBuffer = new List<float>();
    private List<uint> indiceBuffer = new List<uint>();
    /*
    float[] vertexBuffer = {
        -0.5f, -0.5f,  0.0f, // bottom left  // 0
         0.5f, -0.5f,  0.0f, // bottom right // 1
         0.5f,  0.5f,  0.0f, // top right    // 2
        -0.5f,  0.5f,  0.0f  // top left     // 3
    };

    uint[] indiceBuffer = {
        0, 1, 2, // first triangle
        0, 2, 3  // second triangle
    };
    */

    private int vertices = 0;

    private int vertexArrayObject;
    private int vertexBufferObject;
    private int elementBufferObject;

    private void Flush() {
        // vertex array object
        this.vertexArrayObject = GL.GenVertexArray();
        GL.BindVertexArray(this.vertexArrayObject);

        // vertex buffer object
        this.vertexBufferObject = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ArrayBuffer, this.vertexBufferObject);
        GL.BufferData(BufferTarget.ArrayBuffer, this.vertexBuffer.Count * sizeof(float), this.vertexBuffer.ToArray(), BufferUsageHint.StaticDraw);

        GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
        GL.EnableVertexAttribArray(0);

        // element buffer object
        this.elementBufferObject = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ElementArrayBuffer, this.elementBufferObject);
        GL.BufferData(BufferTarget.ElementArrayBuffer, this.indiceBuffer.Count * sizeof(uint), this.indiceBuffer.ToArray(), BufferUsageHint.StaticDraw);
    }

    private void UseFlush() {
        GL.BindVertexArray(this.vertexArrayObject);

        //GL.DrawArrays(PrimitiveType.Triangles, 0, 3);
        GL.DrawElements(PrimitiveType.Triangles, this.indiceBuffer.Count, DrawElementsType.UnsignedInt, 0);
    }

    private void Clear() {
        this.vertexBuffer.Clear();
        this.indiceBuffer.Clear();

        this.vertices = 0;
    }

    public void Init() {
        this.Clear();
    }

    public void Vertex(float x, float y, float z) {
        this.vertexBuffer.Add(x);
        this.vertexBuffer.Add(y);
        this.vertexBuffer.Add(z);

        //this.vertices++;

        /*
        if(this.vertices % 4 == 0) {
            uint indices = (uint)this.vertices - 4;

            this.indiceBuffer.Add(0 + indices);
            this.indiceBuffer.Add(1 + indices);
            this.indiceBuffer.Add(2 + indices);

            this.indiceBuffer.Add(0 + indices);
            this.indiceBuffer.Add(2 + indices);
            this.indiceBuffer.Add(3 + indices);
        }
        */
    }

    public void Indice() {
        //this.vertices -= 4;

        this.indiceBuffer.Add(0 + (uint)this.vertices);
        this.indiceBuffer.Add(1 + (uint)this.vertices);
        this.indiceBuffer.Add(2 + (uint)this.vertices);

        this.indiceBuffer.Add(0 + (uint)this.vertices);
        this.indiceBuffer.Add(2 + (uint)this.vertices);
        this.indiceBuffer.Add(3 + (uint)this.vertices);

        this.vertices += 4;
    }

    public void Render(int x, int y, int z) {
        float x0 = x + 0.0f;
        float y0 = y + 0.0f;
        float z0 = z + 0.0f;

        float x1 = x + 1.0f;
        float y1 = y + 1.0f;
        float z1 = z + 1.0f;

        // x0
        this.Vertex(x0, y0, z0);
        this.Vertex(x0, y0, z1);
        this.Vertex(x0, y1, z1);
        this.Vertex(x0, y1, z0);
        this.Indice();

        // x1
        this.Vertex(x1, y0, z1);
        this.Vertex(x1, y0, z0);
        this.Vertex(x1, y1, z0);
        this.Vertex(x1, y1, z1);
        this.Indice();

        // y0
        this.Vertex(x0, y0, z0);
        this.Vertex(x1, y0, z0);
        this.Vertex(x1, y0, z1);
        this.Vertex(x0, y0, z1);
        this.Indice();

        // y1
        this.Vertex(x0, y1, z1);
        this.Vertex(x1, y1, z1);
        this.Vertex(x1, y1, z0);
        this.Vertex(x0, y1, z0);
        this.Indice();

        // z0
        this.Vertex(x1, y0, z0);
        this.Vertex(x0, y0, z0);
        this.Vertex(x0, y1, z0);
        this.Vertex(x1, y1, z0);
        this.Indice();

        // z1
        this.Vertex(x0, y0, z1);
        this.Vertex(x1, y0, z1);
        this.Vertex(x1, y1, z1);
        this.Vertex(x0, y1, z1);
        this.Indice();
    }

    private void SetupOrthoCamera() {
        Matrix4 projection = Matrix4.Identity;

        projection *= Matrix4.CreateOrthographicOffCenter(0.0f, (float)this.width, 0.0f, (float)this.height, 100.0f, 300.0f);

        this.shaderGUI.SetMatrix4("projection", projection);

        Matrix4 view = Matrix4.Identity;
        this.shaderGUI.SetMatrix4("view", view);
    }

    // ..:: Draw GUI ::..
    private void DrawGUI() {
        this.shaderGUI.Use();

        this.SetupOrthoCamera();

        Matrix4 view = Matrix4.Identity;
        view *= Matrix4.CreateTranslation(1.5f, -0.5f, -0.5f);

        view *= Matrix4.CreateFromAxisAngle(new Vector3(0.0f, 1.0f, 0.0f), MathHelper.DegreesToRadians(45.0f));

        view *= Matrix4.CreateFromAxisAngle(new Vector3(1.0f, 0.0f, 0.0f), MathHelper.DegreesToRadians(30.0f));

        view *= Matrix4.CreateScale(48.0f, 48.0f, 48.0f);
        view *= Matrix4.CreateTranslation((float)(this.width - 48), (float)(this.height - 48), 0.0f);

        view *= Matrix4.CreateTranslation(0.0f, 0.0f, -200.0f);

        this.shaderGUI.SetMatrix4("view", view);

        this.Init();
        this.Render(-2, 0, 0);

        this.Flush();

        this.UseFlush();
    }

    protected override void OnFramebufferResize(FramebufferResizeEventArgs e) {
        base.OnFramebufferResize(e);

        this.width = e.Width;
        this.height = e.Height;

        GL.Viewport(0, 0, this.width, this.height);
    }

    private Matrix4 view;
    private Matrix4 projection;

    private Matrix4 CreatePerspectiveFieldOfView() {
        float fovy = MathHelper.DegreesToRadians(60.0f);
        float aspect = (float)this.width / (float)this.height;
        float depthNear = 0.3f;
        float depthFar = 1000.0f;

        return Matrix4.CreatePerspectiveFieldOfView(fovy, aspect, depthNear, depthFar);
    }

    protected override void OnLoad() {
        base.OnLoad();

        GL.ClearColor(0.5f, 0.8f, 1.0f, 0.0f);

        this.shader = new Shader("shaderVertex.glsl", "shaderFragment.glsl");

        this.Init();
        this.Render(0, 0, 0);
        this.Flush();

        this.shaderGUI = new Shader("shaderVertex.glsl", "shaderFragment.glsl");
    }

    protected override void OnRenderFrame(FrameEventArgs args) {
        base.OnRenderFrame(args);

        GL.Clear(ClearBufferMask.ColorBufferBit);

        this.shader.Use();

        this.view = Matrix4.CreateTranslation(0.0f, 0.0f, -10.0f);

        this.projection = this.CreatePerspectiveFieldOfView();

        this.shader.SetMatrix4("view", this.view);
        this.shader.SetMatrix4("projection", this.projection);

        this.UseFlush();

        this.DrawGUI();

        SwapBuffers();
    }

    private bool isWireframe = false;

    protected override void OnUpdateFrame(FrameEventArgs args) {
        base.OnUpdateFrame(args);

        // close window
        if(KeyboardState.IsKeyDown(Keys.Escape)) {
            Close();
        }

        // wireframe
        if(KeyboardState.IsKeyDown(Keys.F3) && KeyboardState.IsKeyPressed(Keys.W)) {
            this.isWireframe = !this.isWireframe;

            //GL.Uniform1(GL.GetUniformLocation(this.shader.handle, "isWireframe"), this.isWireframe ? 1 : 0);
            this.shader.SetBool("isWireframe", this.isWireframe);

            GL.PolygonMode(MaterialFace.FrontAndBack, this.isWireframe ? PolygonMode.Line : PolygonMode.Fill);
        }
    }
}
