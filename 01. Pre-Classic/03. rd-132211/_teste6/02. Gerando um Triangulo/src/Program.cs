using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace RubyDung.src;

public class Program : GameWindow {
    private Program(GameWindowSettings gws, NativeWindowSettings nws) : base(gws, nws) {
        this.CenterWindow();
    }

    protected override void OnFramebufferResize(FramebufferResizeEventArgs e) {
        GL.Viewport(0, 0, e.Width, e.Height);
    }

    // ..:: CREATE SHADER ::..
    private int shaderProgram;

    private void Shader() {
        int success;
        string infoLog;

        // ..:: VERTEX SHADER ::..
        string vertexShaderSource = File.ReadAllText("../../../src/shaders/vertexShader.glsl");

        int vertexShader = GL.CreateShader(ShaderType.VertexShader);

        GL.ShaderSource(vertexShader, vertexShaderSource);
        GL.CompileShader(vertexShader);

        GL.GetShader(vertexShader, ShaderParameter.CompileStatus, out success);

        if(success == 0) {
            GL.GetShaderInfoLog(vertexShader, out infoLog);

            Console.WriteLine($"ERROR::SHADER::VERTEX::COMPILATION_FAILED\n{infoLog}");
        }

        // ..:: FRAGMENT SHADER ::..
        string fragmentShaderSource = File.ReadAllText("../../../src/shaders/fragmentShader.glsl");

        int fragmentShader = GL.CreateShader(ShaderType.FragmentShader);

        GL.ShaderSource(fragmentShader, fragmentShaderSource);
        GL.CompileShader(fragmentShader);

        GL.GetShader(fragmentShader, ShaderParameter.CompileStatus, out success);

        if(success == 0) {
            GL.GetShaderInfoLog(fragmentShader, out infoLog);

            Console.WriteLine($"ERROR::SHADER::FRAGMENT::COMPILATION_FAILED\n{infoLog}");
        }

        // ..:: SHADER PROGRAM ::..
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

    // ..:: CREATE TRIANGLE ::..
    private int VAO; // Vertex Array Object
    private int VBO; // Vertex Buffer Object

    private void DrawTriangle() {
        float[] vertices = {
            -0.5f, -0.5f, // Bottom-left vertex
             0.5f, -0.5f, // Bottom-right vertex
             0.0f,  0.5f  // Top vertex
        };

        // ..:: VERTEX ARRAY OBJECT ::..
        GL.GenVertexArrays(1, out this.VAO);

        GL.BindVertexArray(this.VAO);

        // ..:: VERTEX BUFFER OBJECT ::..
        GL.GenBuffers(1, out this.VBO);

        GL.BindBuffer(BufferTarget.ArrayBuffer, this.VBO);

        GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

        GL.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, 0, 0);
        GL.EnableVertexAttribArray(0);
    }

    protected override void OnLoad() {
        this.Shader();
        this.DrawTriangle();
    }

    protected override void OnRenderFrame(FrameEventArgs args) {
        GL.ClearColor(this.ConvertColorToHex("7fccff", 255));
        GL.Clear(ClearBufferMask.ColorBufferBit);

        GL.UseProgram(this.shaderProgram);

        //GL.Uniform4(GL.GetUniformLocation(this.shaderProgram, "color"), 1.0f, 0.5f, 0.2f, 1.0f);
        //GL.Uniform4(GL.GetUniformLocation(this.shaderProgram, "color"), this.ConvertColorToRGBA(255, 127, 51, 255));
        GL.Uniform4(GL.GetUniformLocation(this.shaderProgram, "color"), this.ConvertColorToHex("FF7F33", 255));

        GL.BindVertexArray(this.VAO);
        GL.DrawArrays(PrimitiveType.Triangles, 0, 3);

        this.SwapBuffers();
    }

    protected override void OnUpdateFrame(FrameEventArgs args) {
        if(this.KeyboardState.IsKeyDown(Keys.Escape)) {
            this.Close();
        }
    }

    private Color4 ConvertColorToRGBA(int r, int g, int b, int a) {
        float fr = (float)r / 255;
        float fg = (float)g / 255;
        float fb = (float)b / 255;
        float fa = (float)a / 255;

        return new Color4(fr, fg, fb, fa);
    }

    private Color4 ConvertColorToHex(string hex, int a) {
        int fr = Convert.ToInt32(hex.Substring(0, 2), 16);
        int fg = Convert.ToInt32(hex.Substring(2, 2), 16);
        int fb = Convert.ToInt32(hex.Substring(4, 2), 16);
        int fa = a / 255;

        return this.ConvertColorToRGBA(fr, fg, fb, fa);
    }

    private static void Main(string[] args) {
        GameWindowSettings gws = GameWindowSettings.Default;

        NativeWindowSettings nws = NativeWindowSettings.Default;
        nws.ClientSize = (1024, 768);
        nws.Title = "Game";

        new Program(gws, nws).Run();
    }
}
