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
    float[] vertices = {
        -0.5f, -0.5f,  0.0f, // bottom left  // 0
         0.5f, -0.5f,  0.0f, // bottom right // 1
         0.5f,  0.5f,  0.0f, // top right    // 2
        -0.5f,  0.5f,  0.0f  // top left     // 3
    };

    uint[] indices = {
        0, 1, 2, // first triangle
        0, 2, 3  // second triangle
    };

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
        GL.BufferData(BufferTarget.ArrayBuffer, this.vertices.Length * sizeof(float), this.vertices, BufferUsageHint.StaticDraw);

        GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
        GL.EnableVertexAttribArray(0);

        // element buffer object
        this.elementBufferObject = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ElementArrayBuffer, this.elementBufferObject);
        GL.BufferData(BufferTarget.ElementArrayBuffer, this.indices.Length * sizeof(uint), this.indices, BufferUsageHint.StaticDraw);
    }

    private void UseFlush() {
        GL.BindVertexArray(this.vertexArrayObject);

        //GL.DrawArrays(PrimitiveType.Triangles, 0, 3);
        GL.DrawElements(PrimitiveType.Triangles, this.indices.Length, DrawElementsType.UnsignedInt, 0);
    }

    // ..:: Draw GUI ::..
    private void DrawGUI() {
        this.shaderGUI.Use();
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

        this.Flush();

        this.shader = new Shader("shaderVertex.glsl", "shaderFragment.glsl");
        this.shader.Use();

        this.view = Matrix4.CreateTranslation(0.0f, 0.0f, -10.0f);

        this.projection = this.CreatePerspectiveFieldOfView();

        //this.shaderGUI = new Shader("shaderVertex.glsl", "shaderFragment.glsl");
    }

    protected override void OnRenderFrame(FrameEventArgs args) {
        base.OnRenderFrame(args);

        GL.Clear(ClearBufferMask.ColorBufferBit);

        this.shader.Use();

        this.shader.SetMatrix4("view", this.view);
        this.shader.SetMatrix4("projection", this.projection);

        this.UseFlush();

        //this.DrawGUI();

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
