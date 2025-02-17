using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace ConsoleApp1.src;

public class Window : GameWindow {
    private int widht;
    private int height;

    public Window(GameWindowSettings gws, NativeWindowSettings nws) : base(gws, nws) {
        this.widht = this.ClientSize.X;
        this.height = this.ClientSize.Y;
        
        CenterWindow();
    }

    // ..:: Shader ::..
    private Shader shader;
    private Shader shader2;

    // ..:: Triangle ::..
    private List<float> vertexBuffer = new List<float>();
    private List<uint> indiceBuffer = new List<uint>();
    
    private int vertices = 0;

    private int vertexArrayObject;
    private int vertexBufferObject;
    private int elementBufferObject;

    private void Flush() {
        this.vertexBufferObject = GL.GenBuffer();

        GL.BindBuffer(BufferTarget.ArrayBuffer, this.vertexBufferObject);

        GL.BufferData(BufferTarget.ArrayBuffer, this.vertexBuffer.Count * sizeof(float), this.vertexBuffer.ToArray(), BufferUsageHint.StaticDraw);

        this.vertexArrayObject = GL.GenVertexArray();
        GL.BindVertexArray(this.vertexArrayObject);

        GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);

        GL.EnableVertexAttribArray(0);

        this.elementBufferObject = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ElementArrayBuffer, this.elementBufferObject);
        GL.BufferData(BufferTarget.ElementArrayBuffer, this.indiceBuffer.Count * sizeof(uint), this.indiceBuffer.ToArray(), BufferUsageHint.StaticDraw);
    }

    private void UseFlush() {
        GL.BindVertexArray(this.vertexArrayObject);
        GL.DrawElements(PrimitiveType.Triangles, this.indiceBuffer.Count, DrawElementsType.UnsignedInt, 0);
    }

    public void Vertex(float x, float y, float z) {
        this.vertexBuffer.Add(x);
        this.vertexBuffer.Add(y);
        this.vertexBuffer.Add(z);
    }

    public void Indice() {
        this.indiceBuffer.Add(0 + (uint)this.vertices);
        this.indiceBuffer.Add(1 + (uint)this.vertices);
        this.indiceBuffer.Add(2 + (uint)this.vertices);

        this.indiceBuffer.Add(0 + (uint)this.vertices);
        this.indiceBuffer.Add(2 + (uint)this.vertices);
        this.indiceBuffer.Add(3 + (uint)this.vertices);

        this.vertices += 4;
    }

    protected override void OnFramebufferResize(FramebufferResizeEventArgs e) {
        base.OnFramebufferResize(e);

        this.widht = e.Width;
        this.height = e.Height;

        GL.Viewport(0, 0, this.widht, this.height);
    }

    protected override void OnLoad() {
        base.OnLoad();

        GL.ClearColor(0.5f, 0.8f, 1.0f, 0.0f);

        this.Vertex(-0.9f, -0.5f,  0.0f);
        this.Vertex(-0.0f, -0.5f,  0.0f);
        this.Vertex(-0.0f,  0.5f,  0.0f);
        this.Vertex(-0.9f,  0.5f,  0.0f);
        this.Indice();

        this.Flush();

        this.shader = new Shader("shaderVertex.glsl", "shaderFragment.glsl");
        this.shader.SetColor4("setColor", new Color4(1.0f, 0.5f, 0.2f, 1.0f));
        //this.shader.Use();

        //this.UseFlush();

        this.Vertex(0.0f, -0.5f, 0.0f);
        this.Vertex(0.9f, -0.5f, 0.0f);
        this.Vertex(0.9f, 0.5f, 0.0f);
        this.Vertex(0.0f, 0.5f, 0.0f);
        this.Indice();

        this.Flush();

        this.shader2 = new Shader("shaderVertex.glsl", "shaderFragment.glsl");
        this.shader2.SetColor4("setColor", new Color4(1.0f, 1.0f, 0.0f, 1.0f));
        //this.shader.SetColor4("setColor", new Color4(1.0f, 1.0f, 0.0f, 1.0f));
        this.shader2.Use();
        //this.shader.Use();

        //this.UseFlush();
    }

    protected override void OnRenderFrame(FrameEventArgs args) {
        base.OnRenderFrame(args);

        GL.Clear(ClearBufferMask.ColorBufferBit);
        
        this.shader.Use();
        //this.shader2.Use();

        this.UseFlush();

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
