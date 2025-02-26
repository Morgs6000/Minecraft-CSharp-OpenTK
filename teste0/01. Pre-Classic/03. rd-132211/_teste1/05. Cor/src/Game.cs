using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace RubyDung.src;

public class Game : GameWindow {
    private Shader shader;
    private Texture texture;
    private Tesselator t;

    public Game(GameWindowSettings gws, NativeWindowSettings nws) : base(gws, nws) {
        CenterWindow();
    }

    protected override void OnLoad() {
        base.OnLoad();

        GL.ClearColor(0.5f, 0.8f, 1.0f, 0.0f);

        shader = new Shader("shader_vertex.glsl", "shader_fragment.glsl");
        texture = new Texture("../../../src/textures/terrain.png");
        t = new Tesselator(shader);
        Tile.tile.OnLoad(t);
        t.OnLoad();
    }

    protected override void OnUpdateFrame(FrameEventArgs args) {
        base.OnUpdateFrame(args);

        if(KeyboardState.IsKeyDown(Keys.Escape)) {
            Close();
        }

        if(KeyboardState.IsKeyDown(Keys.F3) && KeyboardState.IsKeyPressed(Keys.W)) {
            Wireframe();
        }
    }

    protected override void OnRenderFrame(FrameEventArgs args) {
        base.OnRenderFrame(args);

        GL.Clear(ClearBufferMask.ColorBufferBit);

        shader.OnRenderFrame();
        texture.OnRenderFrame();
        t.OnRenderFrame();

        SwapBuffers();
    }

    protected override void OnFramebufferResize(FramebufferResizeEventArgs e) {
        base.OnFramebufferResize(e);

        GL.Viewport(0, 0, ClientSize.X, ClientSize.Y);
    }

    /* ..:: Wireframe ::.. */

    private bool wireframe = false;

    private void Wireframe() {
        wireframe = !wireframe;
        shader.SetBool("wireframe", wireframe);
        GL.PolygonMode(TriangleFace.FrontAndBack, wireframe ? PolygonMode.Line : PolygonMode.Fill);
        Console.WriteLine($"Wireframe: {(wireframe ? "ON" : "OFF")}");
    }
}

public class Tile() {
    public static Tile tile = new Tile();

    public void OnLoad(Tesselator t) {
        float u0 = (float)0 / 16.0f;
        float v0 = (16.0f - 1.0f) / 16.0f;

        float u1 = u0 + (1.0f / 16.0f);
        float v1 = v0 + (1.0f / 16.0f);

        t.Vertex(-0.5f, -0.5f,  0.0f);
        t.Vertex( 0.5f, -0.5f,  0.0f);
        t.Vertex( 0.5f,  0.5f,  0.0f);
        t.Vertex(-0.5f,  0.5f,  0.0f);

        t.Indice();

        t.Tex(u0, v0);
        t.Tex(u1, v0);
        t.Tex(u1, v1);
        t.Tex(u0, v1);

        t.Color(1.0f, 0.0f, 0.0f);
    }
}

public class Tesselator {
    private List<float> vertexBuffer = new List<float>();
    private List<int> indiceBuffer = new List<int>();
    private List<float> texCoordBuffer = new List<float>();
    private List<float> colorBuffer = new List<float>();

    private int vertices = 0;

    private bool hasTexture = false;
    private bool hasColor = false;

    private int vertexArrayObject;
    private int vertexBufferObject;
    private int elementBufferObject;
    private int textureBufferObject;
    private int colorBufferObject;

    private Shader shader;

    public Tesselator(Shader shader) {
        this.shader = shader;
    }

    public void OnLoad() {
        /* ..:: Vertex Array Object ::.. */
        vertexArrayObject = GL.GenVertexArray();
        GL.BindVertexArray(vertexArrayObject);

        /* ..:: Vertex Buffer Object ::.. */
        vertexBufferObject = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferObject);
        GL.BufferData(BufferTarget.ArrayBuffer, vertexBuffer.Count * sizeof(float), vertexBuffer.ToArray(), BufferUsageHint.StreamDraw);

        int aPos = shader.GetAttribLocation("aPos");
        GL.VertexAttribPointer(aPos, 3, VertexAttribPointerType.Float, false, 0, 0);
        GL.EnableVertexAttribArray(aPos);

        /* ..:: Element Buffer Object ::.. */
        elementBufferObject = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ElementArrayBuffer, elementBufferObject);
        GL.BufferData(BufferTarget.ElementArrayBuffer, indiceBuffer.Count * sizeof(int), indiceBuffer.ToArray(), BufferUsageHint.StreamDraw);

        /* ..:: Texture Buffer Object ::.. */
        if(hasTexture) {
            textureBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, textureBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, texCoordBuffer.Count * sizeof(float), texCoordBuffer.ToArray(), BufferUsageHint.StreamDraw);

            int aTexCoord = shader.GetAttribLocation("aTexCoord");
            GL.VertexAttribPointer(aTexCoord, 2, VertexAttribPointerType.Float, false, 0, 0);
            GL.EnableVertexAttribArray(aTexCoord);
        }

        /* ..:: Color Buffer Object ::.. */
        if(hasColor) {
            colorBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, colorBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, colorBuffer.Count * sizeof(float), colorBuffer.ToArray(), BufferUsageHint.StreamDraw);

            int aColor = shader.GetAttribLocation("aColor");
            GL.VertexAttribPointer(aColor, 3, VertexAttribPointerType.Float, false, 0, 0);
            GL.EnableVertexAttribArray(aColor);
        }
    }

    public void OnRenderFrame() {
        shader.SetBool("hasTexture", hasTexture);
        shader.SetBool("hasColor", hasColor);

        GL.BindVertexArray(vertexArrayObject);
        GL.DrawElements(PrimitiveType.Triangles, indiceBuffer.Count, DrawElementsType.UnsignedInt, 0);
    }

    public void Vertex(float x, float y, float z) {
        vertexBuffer.Add(x);
        vertexBuffer.Add(y);
        vertexBuffer.Add(z);

        vertices++;
    }

    public void Indice() {
        indiceBuffer.Add(0);
        indiceBuffer.Add(1);
        indiceBuffer.Add(2);

        indiceBuffer.Add(0);
        indiceBuffer.Add(2);
        indiceBuffer.Add(3);
    }

    public void Tex(float u, float v) {
        hasTexture = true;

        texCoordBuffer.Add(u);
        texCoordBuffer.Add(v);
    }

    public void Color(float r, float g, float b) {
        hasColor = true;

        for(int i = 0; i < vertices; i++) {
            colorBuffer.Add(r);
            colorBuffer.Add(g);
            colorBuffer.Add(b);
        }
    }
}
