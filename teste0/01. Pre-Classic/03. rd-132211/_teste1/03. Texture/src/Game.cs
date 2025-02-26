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

public class Tesselator {
    private float[] vertexBuffer = {
        -0.5f, -0.5f, 0.0f, // inferior esquerto => indice 0
         0.5f, -0.5f, 0.0f, // inferior direito  => indice 1
         0.5f,  0.5f, 0.0f, // superior direito  => indice 2
        -0.5f,  0.5f, 0.0f  // superior esquerdo => indice 3
    };

    private uint[] indicesBuffer = {
        0, 1, 2, // primeiro triângulo
        0, 2, 3  // segundo triângulo
    };

    private float[] texCoordBuffer = {
        0.0f, 0.0f, // inferior esquerto
        1.0f, 0.0f, // inferior direito
        1.0f, 1.0f, // superior direito
        0.0f, 1.0f  // superior esquerdo
    };

    private bool hasTexture = false;

    private int vertexArrayObject;
    private int vertexBufferObject;
    private int elementBufferObject;
    private int textureBufferObject;

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
        GL.BufferData(BufferTarget.ArrayBuffer, vertexBuffer.Length * sizeof(float), vertexBuffer, BufferUsageHint.StreamDraw);

        int aPos = shader.GetAttribLocation("aPos");
        GL.VertexAttribPointer(aPos, 3, VertexAttribPointerType.Float, false, 0, 0);
        GL.EnableVertexAttribArray(aPos);

        /* ..:: Element Buffer Object ::.. */
        elementBufferObject = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ElementArrayBuffer, elementBufferObject);
        GL.BufferData(BufferTarget.ElementArrayBuffer, indicesBuffer.Length * sizeof(uint), indicesBuffer, BufferUsageHint.StreamDraw);

        /* ..:: Texture Buffer Object ::.. */
        if(hasTexture) {
            textureBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, textureBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, texCoordBuffer.Length * sizeof(float), texCoordBuffer, BufferUsageHint.StreamDraw);

            int aTexCoord = shader.GetAttribLocation("aTexCoord");
            GL.VertexAttribPointer(aTexCoord, 2, VertexAttribPointerType.Float, false, 0, 0);
            GL.EnableVertexAttribArray(aTexCoord);
        }
    }

    public void OnRenderFrame() {
        shader.SetBool("hasTexture", hasTexture);

        GL.BindVertexArray(vertexArrayObject);
        GL.DrawElements(PrimitiveType.Triangles, indicesBuffer.Length, DrawElementsType.UnsignedInt, 0);
    }
}
