using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using StbImageSharp;
using System.Collections.Generic;

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

    private Level level;
    private LevelRenderer levelRenderer;

    protected override void OnLoad() {
        //this.level.level(16, 16, 16);
        this.level = new Level(256, 64, 256);
        this.levelRenderer = new LevelRenderer(this.level);

        //this.chunk.chunkGen();
        //this.chunk.DrawTriangle();

        this.Shader();
        this.Texture();

        this.openGL_settings();

        this.CursorState = CursorState.Grabbed;
    }

    private float deltaTime = 0.0f;
    private float lastFrame = 0.0f;

    protected override void OnRenderFrame(FrameEventArgs args) {
        float currentFrame = (float)(GLFW.GetTime());
        this.deltaTime = currentFrame - this.lastFrame;
        this.lastFrame = currentFrame;

        GL.ClearColor(this.ConvertColorToHex("7fccff", 255));
        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

        this.levelRenderer.render();

        GL.BindTexture(TextureTarget.Texture2D, this.texture);

        GL.UseProgram(this.shaderProgram);

        this.matrix();

        //GL.Uniform4(GL.GetUniformLocation(this.shaderProgram, "color"), this.ConvertColorToHex("FF7F33", 255));

        this.SwapBuffers();
    }

    // ..:: MATRIX ::..
    private Vector3 eye = new Vector3(0.0f, 0.0f, 3.0f);
    private Vector3 target = new Vector3(0.0f, 0.0f, -1.0f);
    private Vector3 up = Vector3.UnitY;

    private void matrix() {
        // ..:: MATRIX PROJECTION ::..
        Matrix4 projection = Matrix4.Identity;

        // ..:: Create Perspective Field Of View ::..
        float fovy = MathHelper.DegreesToRadians(60.0f);
        float aspect = (float)this.width / (float)this.height;
        float depthNear = 0.01f;
        float depthFar = 1000.0f;

        projection *= Matrix4.CreatePerspectiveFieldOfView(fovy, aspect, depthNear, depthFar);

        GL.UniformMatrix4(GL.GetUniformLocation(this.shaderProgram, "projection"), false, ref projection);

        // ..:: MATRIX VIEW ::..
        Matrix4 view = Matrix4.Identity;

        // ..:: Look At ::..
        view *= Matrix4.CreateRotationY(MathHelper.DegreesToRadians(180.0f));

        view *= Matrix4.CreateTranslation(0.0f, 0.0f, -10.0f);

        view *= Matrix4.LookAt(this.eye, this.eye + this.target, this.up);

        GL.UniformMatrix4(GL.GetUniformLocation(this.shaderProgram, "view"), false, ref view);
    }

    protected override void OnUpdateFrame(FrameEventArgs args) {
        if(this.KeyboardState.IsKeyDown(Keys.Escape)) {
            this.Close();
        }

        this.wireframeMode();

        if(!this.KeyboardState.IsKeyDown(Keys.F3)) {
            this.cameraInputs();
        }

        this.mouse_callback(this.MouseState.X, this.MouseState.Y);
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

    // ..:: Camera Inputs ::..
    private void cameraInputs() {
        float cameraSpeed = (float)(4.317f * this.deltaTime);

        float x = 0.0f;
        float y = 0.0f;
        float z = 0.0f;

        if(this.KeyboardState.IsKeyDown(Keys.W)) {
            z++;
        }
        if(this.KeyboardState.IsKeyDown(Keys.S)) {
            z--;
        }
        if(this.KeyboardState.IsKeyDown(Keys.A)) {
            x++;
        }
        if(this.KeyboardState.IsKeyDown(Keys.D)) {
            x--;
        }

        if(this.KeyboardState.IsKeyDown(Keys.Space)) {
            y++;
        }
        if(this.KeyboardState.IsKeyDown(Keys.LeftShift)) {
            y--;
        }

        this.eye += x * Vector3.Normalize(Vector3.Cross(-this.target, this.up)) * cameraSpeed;
        this.eye += y * this.up * cameraSpeed;
        this.eye += z * Vector3.Normalize(new Vector3(this.target.X, 0.0f, this.target.Z)) * cameraSpeed;
    }

    private static bool firstMouse = true;
    private float yaw = -90.0f;
    private float pitch;

    private float lastX;
    private float lastY;

    private void mouse_callback(double xposIn, double yposIn) {
        float xpos = (float)(xposIn);
        float ypos = (float)(yposIn);

        if(firstMouse) {
            this.lastX = xpos;
            this.lastY = ypos;
            firstMouse = false;
        }

        float xoffset = xpos - this.lastX;
        float yoffset = this.lastY - ypos;

        this.lastX = xpos;
        this.lastY = ypos;

        float sensitivity = 0.1f;
        xoffset *= sensitivity;
        yoffset *= sensitivity;

        this.yaw += xoffset;
        this.pitch += yoffset;

        if(this.pitch > 89.0f) {
            this.pitch = 89.0f;
        }
        if(this.pitch < -89.0f) {
            this.pitch = -89.0f;
        }

        Vector3 front;
        front.X = MathF.Cos(MathHelper.DegreesToRadians(this.yaw)) * MathF.Cos(MathHelper.DegreesToRadians(this.pitch));
        front.Y = MathF.Sin(MathHelper.DegreesToRadians(this.pitch));
        front.Z = MathF.Sin(MathHelper.DegreesToRadians(this.yaw)) * MathF.Cos(MathHelper.DegreesToRadians(this.pitch));
        this.target = Vector3.Normalize(front);
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
