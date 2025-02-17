using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System.Reflection.Metadata;
using System.Xml.Linq;

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
    private int handle;

    public void Shader(string vertexPath, string fragmentPath) {
        int success;

        // vertex shader
        string vertexShaderSource = File.ReadAllText($"../../../src/shaders/{vertexPath}");
        int vertexShader = GL.CreateShader(ShaderType.VertexShader);
        GL.ShaderSource(vertexShader, vertexShaderSource);
        GL.CompileShader(vertexShader);

        GL.GetShader(vertexShader, ShaderParameter.CompileStatus, out success);
        if(success == 0) {
            string infoLog = GL.GetShaderInfoLog(vertexShader);
            Console.WriteLine(infoLog);
        }

        // fragment shader
        string fragmentShaderSource = File.ReadAllText($"../../../src/shaders/{fragmentPath}");
        int fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
        GL.ShaderSource(fragmentShader, fragmentShaderSource);
        GL.CompileShader(fragmentShader);

        GL.GetShader(fragmentShader, ShaderParameter.CompileStatus, out success);
        if(success == 0) {
            string infoLog = GL.GetShaderInfoLog(fragmentShader);
            Console.WriteLine(infoLog);
        }

        // link shader
        this.handle = GL.CreateProgram();

        GL.AttachShader(this.handle, vertexShader);
        GL.AttachShader(this.handle, fragmentShader);

        GL.LinkProgram(this.handle);

        GL.GetProgram(this.handle, GetProgramParameterName.LinkStatus, out success);
        if(success == 0) {
            string infoLog = GL.GetProgramInfoLog(this.handle);
            Console.WriteLine(infoLog);
        }

        // delete shader
        GL.DetachShader(this.handle, vertexShader);
        GL.DetachShader(this.handle, fragmentShader);
        GL.DeleteShader(vertexShader);
        GL.DeleteShader(fragmentShader);
    }

    public void Use() {
        GL.UseProgram(this.handle);
    }

    // ..:: Triangle ::..
    float[] vertices = {
        -0.5f, -0.5f, // bottom left  // 0
         0.5f, -0.5f, // bottom right // 1
         0.5f,  0.5f, // top right    // 2
        -0.5f,  0.5f  // top left     // 3
    };

    uint[] indices = {
        0, 1, 2, // first triangle
        0, 2, 3  // second triangle
    };

    private int vertexArrayObject;
    private int vertexBufferObject;
    private int elementBufferObject;

    private void Flush() {
        this.vertexBufferObject = GL.GenBuffer();

        GL.BindBuffer(BufferTarget.ArrayBuffer, this.vertexBufferObject);

        GL.BufferData(BufferTarget.ArrayBuffer, this.vertices.Length * sizeof(float), this.vertices, BufferUsageHint.StaticDraw);

        this.vertexArrayObject = GL.GenVertexArray();
        GL.BindVertexArray(this.vertexArrayObject);

        GL.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, 2 * sizeof(float), 0);

        GL.EnableVertexAttribArray(0);

        this.elementBufferObject = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ElementArrayBuffer, this.elementBufferObject);
        GL.BufferData(BufferTarget.ElementArrayBuffer, this.indices.Length * sizeof(uint), this.indices, BufferUsageHint.StaticDraw);
    }

    private void UseFlush() {
        GL.BindVertexArray(this.vertexArrayObject);

        //GL.DrawArrays(PrimitiveType.Triangles, 0, 3);
        GL.DrawElements(PrimitiveType.Triangles, this.indices.Length, DrawElementsType.UnsignedInt, 0);
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

        this.Flush();

        this.Shader("shaderVertex.glsl", "shaderFragment.glsl");
        this.Use();
    }

    protected override void OnRenderFrame(FrameEventArgs args) {
        base.OnRenderFrame(args);

        GL.Clear(ClearBufferMask.ColorBufferBit);

        this.Use();

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

            GL.Uniform1(GL.GetUniformLocation(this.handle, "isWireframe"), this.isWireframe ? 1 : 0);

            GL.PolygonMode(MaterialFace.FrontAndBack, this.isWireframe ? PolygonMode.Line : PolygonMode.Fill);
        }
    }
}
