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
    private float[] vertices = new float[300000];
    private int[] indices = new int[600000];
    private float[] texCoords = new float[200000];

    private int verticesLength = 0;
    private int indicesLength = 0;
    private int texCoordsLength = 0;

    private int VAO; // Vertex Array Object
    private int VBO; // Vertex Buffer Object
    private int EBO; // Element Buffer Object
    private int TBO; // Texture Buffer Object

    private void DrawTriangle() {
        // ..:: VERTEX ARRAY OBJECT ::..
        GL.GenVertexArrays(1, out this.VAO);

        GL.BindVertexArray(this.VAO);

        // ..:: VERTEX BUFFER OBJECT ::..
        GL.GenBuffers(1, out this.VBO);

        GL.BindBuffer(BufferTarget.ArrayBuffer, this.VBO);
        GL.BufferData(BufferTarget.ArrayBuffer, this.vertices.Length * sizeof(float), this.vertices, BufferUsageHint.StaticDraw);

        GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 0, 0);
        GL.EnableVertexAttribArray(0);

        // ..:: ELEMENT BUFFER OBJECT ::..
        GL.GenBuffers(1, out this.EBO);

        GL.BindBuffer(BufferTarget.ElementArrayBuffer, this.EBO);
        GL.BufferData(BufferTarget.ElementArrayBuffer, this.indices.Length * sizeof(int), this.indices, BufferUsageHint.StaticDraw);

        // ..:: TEXTURE BUFFER OBJECT ::..
        GL.GenBuffers(1, out this.TBO);

        GL.BindBuffer(BufferTarget.ArrayBuffer, this.TBO);
        GL.BufferData(BufferTarget.ArrayBuffer, this.texCoords.Length * sizeof(float), this.texCoords, BufferUsageHint.StaticDraw);

        GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 0, 0);
        GL.EnableVertexAttribArray(1);
    }

    private int tex = 0;

    private void blockGen(int x, int y, int z, int tex) {
        float x0 = (float)x + -0.5f;
        float y0 = (float)y + -0.5f;
        float z0 = (float)z + -0.5f;

        float x1 = (float)x + 0.5f;
        float y1 = (float)y + 0.5f;
        float z1 = (float)z + 0.5f;

        this.tex = tex;

        // x0
        if(!this.isSolidTile(x - 1, y, z)) {
            this.addVertices(x0, y0, z0); // Bottom-left vertex
            this.addVertices(x0, y0, z1); // Bottom-right vertex
            this.addVertices(x0, y1, z1); // Top-right vertex
            this.addVertices(x0, y1, z0); // Top-left vertex

            this.addIndices();

            this.addTexCoords();
        }

        // x1
        if(!this.isSolidTile(x + 1, y, z)) {
            this.addVertices(x1, y0, z1); // Bottom-left vertex
            this.addVertices(x1, y0, z0); // Bottom-right vertex
            this.addVertices(x1, y1, z0); // Top-right vertex
            this.addVertices(x1, y1, z1); // Top-left vertex

            this.addIndices();

            this.addTexCoords();
        }

        // y0
        if(!this.isSolidTile(x, y - 1, z)) {
            this.addVertices(x0, y0, z0); // Bottom-left vertex
            this.addVertices(x1, y0, z0); // Bottom-right vertex
            this.addVertices(x1, y0, z1); // Top-right vertex
            this.addVertices(x0, y0, z1); // Top-left vertex

            this.addIndices();

            this.addTexCoords();
        }

        // y1
        if(!this.isSolidTile(x, y + 1, z)) {
            this.addVertices(x0, y1, z1); // Bottom-left vertex
            this.addVertices(x1, y1, z1); // Bottom-right vertex
            this.addVertices(x1, y1, z0); // Top-right vertex
            this.addVertices(x0, y1, z0); // Top-left vertex

            this.addIndices();

            this.addTexCoords();
        }

        // z0
        if(!this.isSolidTile(x, y, z - 1)) {
            this.addVertices(x1, y0, z0); // Bottom-left vertex
            this.addVertices(x0, y0, z0); // Bottom-right vertex
            this.addVertices(x0, y1, z0); // Top-right vertex
            this.addVertices(x1, y1, z0); // Top-left vertex

            this.addIndices();

            this.addTexCoords();
        }

        // z1
        if(!this.isSolidTile(x, y, z + 1)) {
            this.addVertices(x0, y0, z1); // Bottom-left vertex
            this.addVertices(x1, y0, z1); // Bottom-right vertex
            this.addVertices(x1, y1, z1); // Top-right vertex
            this.addVertices(x0, y1, z1); // Top-left vertex

            this.addIndices();

            this.addTexCoords();
        }
    }

    private void addVertices(float x, float y, float z) {
        this.vertices[this.verticesLength * 3 + 0] = x;
        this.vertices[this.verticesLength * 3 + 1] = y;
        this.vertices[this.verticesLength * 3 + 2] = z;

        this.verticesLength++;
    }

    private void addIndices() {
        // first triangle
        this.indices[this.indicesLength + 0] = 0 + this.verticesLength - 4;
        this.indices[this.indicesLength + 1] = 1 + this.verticesLength - 4;
        this.indices[this.indicesLength + 2] = 2 + this.verticesLength - 4;

        // second triangle
        this.indices[this.indicesLength + 3] = 0 + this.verticesLength - 4;
        this.indices[this.indicesLength + 4] = 2 + this.verticesLength - 4;
        this.indices[this.indicesLength + 5] = 3 + this.verticesLength - 4;

        this.indicesLength += 6;
    }

    private void addTexCoords() {
        float u0 = (float)this.tex / 16;
        float v0 = (float)(16 - 1) / 16;

        float u1 = u0 + ((float)1 / 16);
        float v1 = v0 + ((float)1 / 16);

        this.texCoords[this.texCoordsLength * 2 + 0] = u0;
        this.texCoords[this.texCoordsLength * 2 + 1] = v0;

        this.texCoords[this.texCoordsLength * 2 + 2] = u1;
        this.texCoords[this.texCoordsLength * 2 + 3] = v0;

        this.texCoords[this.texCoordsLength * 2 + 4] = u1;
        this.texCoords[this.texCoordsLength * 2 + 5] = v1;

        this.texCoords[this.texCoordsLength * 2 + 6] = u0;
        this.texCoords[this.texCoordsLength * 2 + 7] = v1;

        this.texCoordsLength += 4;
    }

    private void chunkGen() {
        int x0 = 0;
        int y0 = 0;
        int z0 = 0;

        int x1 = 16;
        int y1 = 16;
        int z1 = 16;

        for(int x = x0; x < x1; x++) {
            for(int y = y0; y < y1; y++) {
                for(int z = z0; z < z1; z++) {

                    if(this.isTile(x, y, z)) {
                        bool tex = y != this.heightBlock * 2 / 3;

                        if(!tex) {
                            this.blockGen(x, y, z, 0);
                        }
                        else {
                            this.blockGen(x, y, z, 1);
                        }
                    }

                }
            }
        }
    }

    public int widthBlock;
    public int heightBlock;
    public int depthBlock;
    private byte[] blocks;

    public void level(int w, int h, int d) {
        this.widthBlock = w;
        this.heightBlock = h;
        this.depthBlock = d;
        this.blocks = new byte[w * h * d];

        for(int x = 0; x < w; x++) {
            for(int y = 0; y < h; y++) {
                for(int z = 0; z < d; z++) {
                    int i = (y * this.depthBlock + z) * this.widthBlock + x;

                    this.blocks[i] = (byte)(y <= h * 2 / 3 ? 1 : 0);
                }
            }
        }
    }

    public bool isTile(int x, int y, int z) {
        if(x >= 0 && y >= 0 && z >= 0 && x < this.widthBlock && y < this.heightBlock && z < this.depthBlock) {
            return this.blocks[(y * this.depthBlock + z) * this.widthBlock + x] == 1;
        }
        else {
            return false;
        }
    }

    public bool isSolidTile(int x, int y, int z) {
        return this.isTile(x, y, z);
    }

    protected override void OnLoad() {
        this.openGL_settings();
        this.Shader();
        this.Texture();

        this.level(16, 16, 16);
        this.chunkGen();
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
        GL.DrawElements(PrimitiveType.Triangles, this.indices.Length, DrawElementsType.UnsignedInt, 0);

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
