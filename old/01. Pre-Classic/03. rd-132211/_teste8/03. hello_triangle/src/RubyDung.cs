using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace RubyDung.src;

public class RubyDung : GameWindow {
    private int width;
    private int height;

    private string vertexShaderSource = File.ReadAllText("../../../src/vertexShader.glsl");
    private string fragmentShaderSource = File.ReadAllText("../../../src/fragmentShader.glsl");

    public RubyDung(GameWindowSettings gws, NativeWindowSettings nws) : base(gws, nws) {
        this.width = this.ClientSize.X;
        this.height = this.ClientSize.Y;
        
        this.CenterWindow();
    }

    protected override void OnFramebufferResize(FramebufferResizeEventArgs e) {
        this.width = e.Width;
        this.height = e.Height;

        this.framebuffer_size_callback(e.Width, e.Height);
    }

    // ..:: Shader ::..
    private int shaderProgram;

    private void Shader() {
        int success;
        string infoLog;

        // vertex shader
        int vertexShader = GL.CreateShader(ShaderType.VertexShader);
        GL.ShaderSource(vertexShader, this.vertexShaderSource);
        GL.CompileShader(vertexShader);

        GL.GetShader(vertexShader, ShaderParameter.CompileStatus, out success);
        if(success == 0) {
            GL.GetShaderInfoLog(vertexShader, out infoLog);
            Console.WriteLine($"ERROR::SHADER::VERTEX::COMPILATION_FAILED\n{infoLog}");
        }

        // fragment sahder
        int fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
        GL.ShaderSource(fragmentShader, this.fragmentShaderSource);
        GL.CompileShader(fragmentShader);

        GL.GetShader(fragmentShader, ShaderParameter.CompileStatus, out success);
        if(success == 0) {
            GL.GetShaderInfoLog(fragmentShader, out infoLog);
            Console.WriteLine($"ERROR::SHADER::FRAGMENT::COMPILATION_FAILED\n{infoLog}");
        }

        // link shaders
        this.shaderProgram = GL.CreateProgram();
        GL.AttachShader(this.shaderProgram, vertexShader);
        GL.AttachShader(this.shaderProgram, fragmentShader);
        GL.LinkProgram(this.shaderProgram);

        GL.GetProgram(this.shaderProgram, GetProgramParameterName.LinkStatus, out success);
        if(success == 0) {
            GL.GetProgramInfoLog(this.shaderProgram, out infoLog);
            Console.WriteLine($"ERROR::SHADER::PROGRAM::LINKING_FAILED\n{infoLog}");
        }

        GL.DeleteShader(vertexShader);
        GL.DeleteShader(fragmentShader);
    }

    // ..:: Triangle ::..
    float[] vertices = {
        -0.5f, -0.5f, // left  
         0.5f, -0.5f, // right 
         0.0f,  0.5f  // top 
    };

    private int VAO; // Vertex Array Object
    private int VBO; // Vertex Buffer Object

    private void Triangle() {
        // Vertex Array Object
        GL.GenVertexArrays(1, out this.VAO);

        GL.BindVertexArray(this.VAO);

        // Vertex Buffer Object
        GL.GenBuffers(1, out this.VBO);

        GL.BindBuffer(BufferTarget.ArrayBuffer, this.VBO);
        GL.BufferData(BufferTarget.ArrayBuffer, this.vertices.Length * sizeof(float), this.vertices, BufferUsageHint.StaticDraw);

        GL.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, 0, 0);
        GL.EnableVertexAttribArray(0);

        // Clear
        GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

        GL.BindVertexArray(0);

        // Wireframe
        //GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);
    }

    protected override void OnLoad() {
        this.Shader();
        this.Triangle();
    }

    protected override void OnRenderFrame(FrameEventArgs args) {
        this.processInput();

        GL.ClearColor(0.5f, 0.8f, 1.0f, 0.0F);
        GL.Clear(ClearBufferMask.ColorBufferBit);

        GL.UseProgram(this.shaderProgram);
        GL.BindVertexArray(this.VAO);
        GL.DrawArrays(PrimitiveType.Triangles, 0, 3);

        this.SwapBuffers();
    }

    private void processInput() {
        if(this.KeyboardState.IsKeyDown(Keys.Escape)) {
            this.Close();
        }
    }

    private void framebuffer_size_callback(int width, int height) {
        GL.Viewport(0, 0, width, height);
    }

    private static void Main(string[] args) {
        GameWindowSettings gws = GameWindowSettings.Default;

        NativeWindowSettings nws = NativeWindowSettings.Default;
        nws.ClientSize = (1024, 768);
        nws.Title = "Game";

        new RubyDung(gws, nws).Run();
    }
}
