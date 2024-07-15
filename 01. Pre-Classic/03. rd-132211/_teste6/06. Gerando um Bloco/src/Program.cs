using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using StbImageSharp;
using static System.Net.Mime.MediaTypeNames;

namespace RubyDung.src;

public class Program : GameWindow {
    private int width;
    private int height;

    private Program(GameWindowSettings gws, NativeWindowSettings nws) : base(gws, nws) {
        this.width = this.ClientSize.X;
        this.height = this.ClientSize.Y;

        this.CenterWindow();
    }

    protected override void OnFramebufferResize(FramebufferResizeEventArgs e) {
        this.width = e.Width;
        this.height = e.Height;

        GL.Viewport(0, 0, e.Width, e.Height);
    }

    private void openGL_settings() {
        GL.Enable(EnableCap.DepthTest);

        GL.Enable(EnableCap.CullFace);
        GL.CullFace(CullFaceMode.Back);
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

    // ..:: CREATE TEXTURE ::..
    private int texture;

    private void Texture() {
        GL.GenTextures(1, out this.texture);
        GL.BindTexture(TextureTarget.Texture2D, this.texture);

        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);

        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);

        StbImage.stbi_set_flip_vertically_on_load(1);

        ImageResult image = ImageResult.FromStream(File.OpenRead("../../../src/textures/terrain.png"), ColorComponents.RedGreenBlueAlpha);

        if(image.Data != null) {
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, image.Width, image.Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, image.Data);
            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
        }
        else {
            Console.WriteLine("Failed to load texture");
        }
    }

    // ..:: CREATE TRIANGLE ::..
    private int VAO; // Vertex Array Object
    private int VBO; // Vertex Buffer Object
    private int EBO; // Element Buffer Object
    private int TBO; // Texture Buffer Object

    private void DrawTriangle() {
        float x0 = -0.5f;
        float y0 = -0.5f;
        float z0 = -0.5f;

        float x1 = 0.5f;
        float y1 = 0.5f;
        float z1 = 0.5f;

        /*
        float[] vertices = {
            // x0
            x0, y0, z0, // Bottom-left vertex  // 0
            x0, y0, z1, // Bottom-right vertex // 1
            x0, y1, z1, // Top-right vertex    // 2
            x0, y1, z0, // Top-left vertex     // 3

            // x1
            x1, y0, z1, // Bottom-left vertex  // 4
            x1, y0, z0, // Bottom-right vertex // 5
            x1, y1, z0, // Top-right vertex    // 6
            x1, y1, z1, // Top-left vertex     // 7

            // y0
            x0, y0, z0, // Bottom-left vertex  // 8
            x1, y0, z0, // Bottom-right vertex // 9
            x1, y0, z1, // Top-right vertex    // 10
            x0, y0, z1, // Top-left vertex     // 11

            // y1
            x0, y1, z1, // Bottom-left vertex  // 12
            x1, y1, z1, // Bottom-right vertex // 13
            x1, y1, z0, // Top-right vertex    // 14
            x0, y1, z0, // Top-left vertex     // 15

            // z0
            x1, y0, z0, // Bottom-left vertex  // 16
            x0, y0, z0, // Bottom-right vertex // 17
            x0, y1, z0, // Top-right vertex    // 18
            x1, y1, z0, // Top-left vertex     // 19

            // z1
            x0, y0, z1, // Bottom-left vertex  // 20
            x1, y0, z1, // Bottom-right vertex // 21
            x1, y1, z1, // Top-right vertex    // 22
            x0, y1, z1  // Top-left vertex     // 23
        };
        */

        /*
        int[] indices = {
            // x0
            0, 1, 2,    // first triangle
            0, 2, 3,    // second triangle

            // x1
            4, 5, 6,    // first triangle
            4, 6, 7,    // second triangle

            // y0
            8, 9, 10,   // first triangle
            8, 10, 11,  // second triangle

            // y1
            12, 13, 14, // first triangle
            12, 14, 15, // second triangle

            // z0
            16, 17, 18, // first triangle
            16, 18, 19, // second triangle

            // z1
            20, 21, 22, // first triangle
            20, 22, 23  // second triangle
        };
        */

        float[] vertices = {
            x0, y0, z1, // 0

            x1, y0, z1, // 1

            x1, y1, z1, // 2

            x0, y1, z1, // 3

            x1, y0, z0, // 4

            x0, y0, z0, // 5

            x0, y1, z0, // 6

            x1, y1, z0  // 7
        };

        int[] indices = {
            // x0
            5, 0, 3,    // first triangle
            5, 3, 6,    // second triangle

            // x1
            1, 4, 7,    // first triangle
            1, 7, 2,    // second triangle

            // y0
            5, 4, 1,    // first triangle
            5, 1, 0,    // second triangle

            // y1
            3, 2, 7,    // first triangle
            3, 7, 6,    // second triangle

            // z0
            4, 5, 6,    // first triangle
            4, 6, 7,    // second triangle

            // z1
            0, 1, 2,    // first triangle
            0, 2, 3     // second triangle
        };

        float u0 = (float)0 / 16;
        float v0 = (float)(16 - 1) / 16;

        float u1 = u0 + ((float)1 / 16);
        float v1 = v0 + ((float)1 / 16);

        float[] texCoords = {
            u0, v0, // 0
            u1, v0, // 1
            u1, v1, // 2
            u0, v1, // 3

            u0, v0, // 4
            u1, v0, // 5
            u1, v1, // 6
            u0, v1, // 7
        };

        /*
        float[] texCoords = {
            // x0
            u0, v0, // Bottom-left vertex
            u1, v0, // Bottom-right vertex
            u1, v1, // Top-right vertex
            u0, v1, // Top-left vertex

            // x1
            u0, v0, // Bottom-left vertex
            u1, v0, // Bottom-right vertex
            u1, v1, // Top-right vertex
            u0, v1, // Top-left vertex

            // y0
            u0, v0, // Bottom-left vertex
            u1, v0, // Bottom-right vertex
            u1, v1, // Top-right vertex
            u0, v1, // Top-left vertex

            // y1
            u0, v0, // Bottom-left vertex
            u1, v0, // Bottom-right vertex
            u1, v1, // Top-right vertex
            u0, v1, // Top-left vertex

            // z0
            u0, v0, // Bottom-left vertex
            u1, v0, // Bottom-right vertex
            u1, v1, // Top-right vertex
            u0, v1, // Top-left vertex

            // z1
            u0, v0, // Bottom-left vertex
            u1, v0, // Bottom-right vertex
            u1, v1, // Top-right vertex
            u0, v1  // Top-left vertex
        };
        */

        // ..:: VERTEX ARRAY OBJECT ::..
        GL.GenVertexArrays(1, out this.VAO);

        GL.BindVertexArray(this.VAO);

        // ..:: VERTEX BUFFER OBJECT ::..
        GL.GenBuffers(1, out this.VBO);

        GL.BindBuffer(BufferTarget.ArrayBuffer, this.VBO);
        GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

        GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 0, 0);
        GL.EnableVertexAttribArray(0);

        // ..:: ELEMENT BUFFER OBJECT ::..
        GL.GenBuffers(1, out this.EBO);

        GL.BindBuffer(BufferTarget.ElementArrayBuffer, this.EBO);
        GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(int), indices, BufferUsageHint.StaticDraw);

        // ..:: TEXTURE BUFFER OBJECT ::..
        GL.GenBuffers(1, out this.TBO);

        GL.BindBuffer(BufferTarget.ArrayBuffer, this.TBO);
        GL.BufferData(BufferTarget.ArrayBuffer, texCoords.Length * sizeof(float), texCoords, BufferUsageHint.StaticDraw);

        GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 0, 0);
        GL.EnableVertexAttribArray(1);
    }

    protected override void OnLoad() {
        this.openGL_settings();
        this.Shader();
        this.Texture();
        this.DrawTriangle();
    }

    protected override void OnRenderFrame(FrameEventArgs args) {
        GL.ClearColor(this.ConvertColorToHex("7fccff", 255));
        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

        GL.UseProgram(this.shaderProgram);

        this.matrix();

        GL.Uniform4(GL.GetUniformLocation(this.shaderProgram, "color"), this.ConvertColorToHex("FF7F33", 255));

        GL.BindTexture(TextureTarget.Texture2D, this.texture);

        GL.BindVertexArray(this.VAO);
        GL.DrawElements(PrimitiveType.Triangles, 6 * 6, DrawElementsType.UnsignedInt, 0);

        this.SwapBuffers();
    }

    // ..:: MATRIX ::..
    private void matrix() {
        // ..:: MATRIX PROJECTION ::..
        Matrix4 projection = Matrix4.Identity;

        // ..:: Create Perspective Field Of View ::..
        float fovy = MathHelper.DegreesToRadians(60.0f);
        float aspect = (float)this.width / (float)this.height;
        float depthNear = 0.3f;
        float depthFar = 1000.0f;

        projection *= Matrix4.CreatePerspectiveFieldOfView(fovy, aspect, depthNear, depthFar);

        GL.UniformMatrix4(GL.GetUniformLocation(this.shaderProgram, "projection"), false, ref projection);

        // ..:: MATRIX VIEW ::..
        Matrix4 view = Matrix4.Identity;

        view *= Matrix4.CreateFromAxisAngle(new Vector3(0.5f, 1.0f, -3.0f), (float)GLFW.GetTime());

        view *= Matrix4.CreateTranslation(0.0f, 0.0f, -10.0f);

        GL.UniformMatrix4(GL.GetUniformLocation(this.shaderProgram, "view"), false, ref view);
    }

    protected override void OnUpdateFrame(FrameEventArgs args) {
        if(this.KeyboardState.IsKeyDown(Keys.Escape)) {
            this.Close();
        }

        this.wireframeMode();
    }

    // ..:: WIREFRAME MODE ::..
    private bool isWireframe = false;

    private void wireframeMode() {
        if(this.KeyboardState.IsKeyDown(Keys.F3) && this.KeyboardState.IsKeyPressed(Keys.W)) {
            this.isWireframe = !this.isWireframe;

            GL.Uniform1(GL.GetUniformLocation(this.shaderProgram, "isWireframe"), this.isWireframe ? 1 : 0);

            GL.PolygonMode(MaterialFace.FrontAndBack, this.isWireframe ? PolygonMode.Line : PolygonMode.Fill);
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
