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

        float[] vertices = {
            // x0
            x0, y0, z0, // Bottom-left vertex
            x0, y0, z1, // Bottom-right vertex
            x0, y1, z1, // Top-right vertex
            x0, y1, z0, // Top-left vertex

            // x1
            x1, y0, z1, // Bottom-left vertex
            x1, y0, z0, // Bottom-right vertex
            x1, y1, z0, // Top-right vertex
            x1, y1, z1, // Top-left vertex

            // y0
            x0, y0, z0, // Bottom-left vertex
            x1, y0, z0, // Bottom-right vertex
            x1, y0, z1, // Top-right vertex
            x0, y0, z1, // Top-left vertex

            // y1
            x0, y1, z1, // Bottom-left vertex
            x1, y1, z1, // Bottom-right vertex
            x1, y1, z0, // Top-right vertex
            x0, y1, z0, // Top-left vertex

            // z0
            x1, y0, z0, // Bottom-left vertex
            x0, y0, z0, // Bottom-right vertex
            x0, y1, z0, // Top-right vertex
            x1, y1, z0, // Top-left vertex

            // z1
            x0, y0, z1, // Bottom-left vertex
            x1, y0, z1, // Bottom-right vertex
            x1, y1, z1, // Top-right vertex
            x0, y1, z1  // Top-left vertex
        };

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

        float u0 = (float)0 / 16;
        float v0 = (float)(16 - 1) / 16;

        float u1 = u0 + ((float)1 / 16);
        float v1 = v0 + ((float)1 / 16);

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

        GL.UseProgram(this.shaderProgram);

        this.matrix();

        GL.Uniform4(GL.GetUniformLocation(this.shaderProgram, "color"), this.ConvertColorToHex("FF7F33", 255));

        GL.BindTexture(TextureTarget.Texture2D, this.texture);

        GL.BindVertexArray(this.VAO);
        GL.DrawElements(PrimitiveType.Triangles, 6 * 6, DrawElementsType.UnsignedInt, 0);

        this.SwapBuffers();
    }

    // ..:: MATRIX ::..
    private Vector3 eye = new Vector3(0.0f, 0.0f, -3.0f);
    //private Vector3 eye = new Vector3(1.0f, 1.0f, 1.0f);
    //private Vector3 eye = new Vector3(camX, camY, camZ);

    private Vector3 target = new Vector3(0.0f, 0.0f, 1.0f);
    //private Vector3 target = Vector3.Zero;

    //private Vector3 up = new Vector3(0.0f, 1.0f, 0.0f);
    private Vector3 up = Vector3.UnitY;

    //private Vector3 forward = Vector3.UnitX;

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

        //view *= Matrix4.CreateFromAxisAngle(new Vector3(0.5f, 1.0f, -3.0f), (float)GLFW.GetTime());

        // ..:: Look At ::..
        float radius = 10.0f;

        //float camX = (float)(Math.Sin(GLFW.GetTime()) * radius);
        float camX = MathF.Sin((float)GLFW.GetTime()) * radius;

        float camY = (float)(Math.Cos(GLFW.GetTime()) * radius);

        float camZ = (float)(Math.Cos(GLFW.GetTime()) * radius);

        view *= Matrix4.CreateTranslation(0.0f, 0.0f, -10.0f);

        //view *= Matrix4.CreateRotationY(MathHelper.DegreesToRadians(-90.0f));

        view *= Matrix4.LookAt(this.eye, this.eye + this.target, this.up);

        GL.UniformMatrix4(GL.GetUniformLocation(this.shaderProgram, "view"), false, ref view);
    }

    protected override void OnUpdateFrame(FrameEventArgs args) {
        if(this.KeyboardState.IsKeyDown(Keys.Escape)) {
            this.Close();
        }

        this.wireframeMode();
        this.cameraInputs();
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
        //float cameraSpeed = (float)(2.5f * this.deltaTime);
        float cameraSpeed = (float)(4.317f * this.deltaTime);

        float x = 0.0f;
        float y = 0.0f;
        float z = 0.0f;

        if(this.KeyboardState.IsKeyDown(Keys.W)) {
            //this.eye.Z += 1 * cameraSpeed;
            //this.target.Z += 1 * cameraSpeed;
            z++;
        }
        if(this.KeyboardState.IsKeyDown(Keys.S)) {
            //this.eye.Z -= 1 * cameraSpeed;
            //this.target.Z -= 1 * cameraSpeed;
            z--;
        }
        if(this.KeyboardState.IsKeyDown(Keys.A)) {
            //this.eye.X += 1 * cameraSpeed;
            //this.target.X += 1 * cameraSpeed;
            x--;
        }
        if(this.KeyboardState.IsKeyDown(Keys.D)) {
            //this.eye.X -= 1 * cameraSpeed;
            //this.target.X -= 1 * cameraSpeed;
            x++;
        }

        if(this.KeyboardState.IsKeyDown(Keys.Space)) {
            //this.eye.Y += 1 * cameraSpeed;
            //this.target.Y += 1 * cameraSpeed;
            y++;
        }
        if(this.KeyboardState.IsKeyDown(Keys.LeftShift)) {
            //this.eye.Y -= 1 * cameraSpeed;
            //this.target.Y -= 1 * cameraSpeed;
            y--;
        }

        //this.eye += new Vector3(x, y, z) * cameraSpeed;
        //this.target += new Vector3(x, y, z) * cameraSpeed;

        this.eye += x * Vector3.Normalize(Vector3.Cross(this.target, this.up)) * cameraSpeed;
        this.eye += y * this.up * cameraSpeed;
        this.eye += z * Vector3.Normalize(new Vector3(this.target.X, 0.0f, this.target.Z)) * cameraSpeed;

        //Vector3 direction;
        //direction.X = x * cameraSpeed;
        //direction.Y = y * this.up * cameraSpeed;
        //direction.Z = z * this.target * cameraSpeed;
        //this.eye = new Vector3();

        if(this.KeyboardState.IsKeyDown(Keys.Left)) {
            
        }
        if(this.KeyboardState.IsKeyDown(Keys.Right)) {
            
        }
    }

    private static bool firstMouse = true;
    //private float yaw;
    private float yaw = -90.0f;
    private float pitch;

    //private float lastX;
    //private float lastY;
    private Vector2 lastPos;

    private void mouse_callback(double xposIn, double yposIn) {
        float xpos = (float)(xposIn);
        float ypos = (float)(yposIn);

        if(firstMouse) {
            //lastX = xpos;
            //lastY = ypos;
            lastPos = new Vector2(xpos, ypos);
            firstMouse = false;
        }

        //float xoffset = xpos - lastX;
        float xoffset = xpos - lastPos.X;
        //float yoffset = lastY - ypos;
        float yoffset = lastPos.Y - ypos;

        //lastX = xpos;
        //lastPos.X = xpos;
        //lastY = ypos;
        //lastPos.Y = ypos;
        lastPos = new Vector2(xpos, ypos);

        float sensitivity = 0.1f;
        //xoffset *= sensitivity;
        //yoffset *= sensitivity;

        //yaw += xoffset;
        yaw += xoffset * sensitivity;
        //pitch += yoffset;
        pitch += yoffset * sensitivity;

        if(pitch > 89.0f) {
            pitch = 89.0f;
        }
        if(pitch < -89.0f) {
            pitch = -89.0f;
        }

        Vector3 front;
        front.X = MathF.Cos(MathHelper.DegreesToRadians(yaw)) * MathF.Cos(MathHelper.DegreesToRadians(pitch));
        front.Y = MathF.Sin(MathHelper.DegreesToRadians(pitch));
        front.Z = MathF.Sin(MathHelper.DegreesToRadians(yaw)) * MathF.Cos(MathHelper.DegreesToRadians(pitch));
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
