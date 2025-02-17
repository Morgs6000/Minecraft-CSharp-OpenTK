using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using RubyDung.src.level;

namespace RubyDung.src;

public class Window : GameWindow {
    private Shader shader;

    private List<float> vertexBuffer = new List<float>();
    private List<int> indiceBuffer = new List<int>();
    private List<float> colorBuffer = new List<float>();

    private int vertices = 0;

    private int VertexArrayObject;
    private int VertexBufferObject;
    private int ElementBufferObject;
    private int ColorBufferObject;

    public Window(GameWindowSettings gws, NativeWindowSettings nws) : base(gws, nws) {
        CenterWindow();
    }

    protected override void OnUpdateFrame(FrameEventArgs args) {
        base.OnUpdateFrame(args);

        if(KeyboardState.IsKeyDown(Keys.Escape)) {
            Close();
        }

        ProcessInput(args);
    }

    protected override void OnLoad() {
        base.OnLoad();

        GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);

        shader = new Shader("../../../src/shaders/Vertex.glsl", "../../../src/shaders/Fragment.glsl");

        LoadTile();
        LoadRectangle();
    }

    protected override void OnRenderFrame(FrameEventArgs args) {
        base.OnRenderFrame(args);

        GL.Clear(ClearBufferMask.ColorBufferBit);

        shader.Render();

        int modelLoc = GL.GetUniformLocation(shader.Handle, "model");

        GL.UniformMatrix4(modelLoc, false, ref rectTransform);
        RenderRectangle0(0, 6);

        Matrix4 identity = Matrix4.Identity;
        GL.UniformMatrix4(modelLoc, false, ref identity);
        RenderRectangle0(6, 6);

        SwapBuffers();
    }

    protected override void OnFramebufferResize(FramebufferResizeEventArgs e) {
        base.OnFramebufferResize(e);

        GL.Viewport(0, 0, e.Width, e.Height);
    }

    private void LoadRectangle() {
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

        ColorBufferObject = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ArrayBuffer, ColorBufferObject);
        GL.BufferData(BufferTarget.ArrayBuffer, colorBuffer.Count * sizeof(float), colorBuffer.ToArray(), BufferUsageHint.StaticDraw);

        GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 0, 0);
        GL.EnableVertexAttribArray(1);
    }

    public void RenderRectangle() {
        GL.BindVertexArray(VertexArrayObject);
        GL.DrawElements(PrimitiveType.Triangles, indiceBuffer.Count, DrawElementsType.UnsignedInt, 0);
    }

    public void RenderRectangle0(int startIndex, int count) {
        GL.BindVertexArray(VertexArrayObject);
        GL.DrawElements(PrimitiveType.Triangles, count, DrawElementsType.UnsignedInt, startIndex * sizeof(int));
    }

    private void Clear() {
        vertices = 0;

        vertexBuffer.Clear();
        indiceBuffer.Clear();
        colorBuffer.Clear();
    }

    public void Init() {
        Clear();
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

    public void Color(float r, float g, float b) {
        colorBuffer.Add(r);
        colorBuffer.Add(g);
        colorBuffer.Add(b);
    }

    public void LoadTile() {
        // Primeiro Retangulo
        Vertex(-0.9f, -0.5f, 0.0f);
        Vertex(-0.1f, -0.5f, 0.0f);
        Vertex(-0.1f,  0.5f, 0.0f);
        Vertex(-0.9f,  0.5f, 0.0f);
        Indice();
        Color(1.0f, 0.5f, 0.2f);
        Color(1.0f, 0.5f, 0.2f);
        Color(1.0f, 0.5f, 0.2f);
        Color(1.0f, 0.5f, 0.2f);

        // Segundo Retangulo
        Vertex( 0.1f, -0.5f,  0.0f);
        Vertex( 0.9f, -0.5f,  0.0f);
        Vertex( 0.9f,  0.5f,  0.0f);
        Vertex( 0.1f,  0.5f,  0.0f);
        Indice();
        Color(1.0f, 1.0f, 0.0f);
        Color(1.0f, 1.0f, 0.0f);
        Color(1.0f, 1.0f, 0.0f);
        Color(1.0f, 1.0f, 0.0f);
    }

    private Matrix4 rectTransform = Matrix4.Identity;

    public void ProcessInput(FrameEventArgs args) {
        float moveSpeed = 1.5f;

        if(KeyboardState.IsKeyDown(Keys.W)) {
            rectTransform *= Matrix4.CreateTranslation(0, moveSpeed * (float)args.Time, 0);
        }
        if(KeyboardState.IsKeyDown(Keys.S)) {
            rectTransform *= Matrix4.CreateTranslation(0, -moveSpeed * (float)args.Time, 0);
        }
        if(KeyboardState.IsKeyDown(Keys.A)) {
            rectTransform *= Matrix4.CreateTranslation(-moveSpeed * (float)args.Time, 0, 0);
        }
        if(KeyboardState.IsKeyDown(Keys.D)) {
            rectTransform *= Matrix4.CreateTranslation(moveSpeed * (float)args.Time, 0, 0);
        }
    }
}
