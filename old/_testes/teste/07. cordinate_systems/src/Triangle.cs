using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using StbImageSharp;

namespace ConsoleApp1.src;

public class Triangle {
    private string vertexShaderSource = File.ReadAllText("../../../src/shaders/vertexShader.glsl");
    private string fragmentShaderSource = File.ReadAllText("../../../src/shaders/fragmentShader.glsl");

    // ..:: Shader ::..
    private int shaderProgram;

    public void Shader() {
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

    public void use() {
        GL.UseProgram(this.shaderProgram);
    }

    public void setBool(string name, bool value) {
        GL.Uniform1(GL.GetUniformLocation(this.shaderProgram, name), value ? 1 : 0);
    }

    public void setMat4(string name, Matrix4 matrix) {
        GL.UniformMatrix4(GL.GetUniformLocation(this.shaderProgram, name), false, ref matrix);
    }

    // ..:: Triangle ::..
    private List<float> vertexBuffer = new List<float>();
    private List<int> indiceBuffer = new List<int>();
    private List<float> texCoordBuffer = new List<float>();

    private int vertices = 0;

    private float u;
    private float v;

    private bool hasTexture = false;

    private int VAO; // Vertex Array Object
    private int VBO; // Vertex Buffer Object
    private int EBO; // Element Buffer Object
    private int TBO; // Texture Buffer Object

    public void Tris() {
        // Vertex Array Object
        GL.GenVertexArrays(1, out this.VAO);

        GL.BindVertexArray(this.VAO);

        // Vertex Buffer Object
        GL.GenBuffers(1, out this.VBO);

        GL.BindBuffer(BufferTarget.ArrayBuffer, this.VBO);
        GL.BufferData(BufferTarget.ArrayBuffer, this.vertexBuffer.Count * sizeof(float), this.vertexBuffer.ToArray(), BufferUsageHint.StaticDraw);

        GL.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, 0, 0);
        GL.EnableVertexAttribArray(0);

        // Element Buffer Object
        GL.GenBuffers(1, out this.EBO);

        GL.BindBuffer(BufferTarget.ElementArrayBuffer, this.EBO);
        GL.BufferData(BufferTarget.ElementArrayBuffer, this.indiceBuffer.Count * sizeof(int), this.indiceBuffer.ToArray(), BufferUsageHint.StaticDraw);

        // Texture Buffer Object
        GL.GenBuffers(1, out this.TBO);

        GL.BindBuffer(BufferTarget.ArrayBuffer, this.TBO);
        GL.BufferData(BufferTarget.ArrayBuffer, this.texCoordBuffer.Count * sizeof(float), this.texCoordBuffer.ToArray(), BufferUsageHint.StaticDraw);

        GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 0, 0);
        GL.EnableVertexAttribArray(1);

        // Clear
        GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

        GL.BindVertexArray(0);
    }

    public void render() {
        GL.BindVertexArray(this.VAO);
        GL.DrawElements(PrimitiveType.Triangles, 6, DrawElementsType.UnsignedInt, 0);
    }

    private void vertex(float x, float y) {
        this.vertexBuffer.Add(x);
        this.vertexBuffer.Add(y);

        if(this.hasTexture) {
            this.texCoordBuffer.Add(this.u);
            this.texCoordBuffer.Add(this.v);
        }

        this.vertices++;

        if(this.vertices % 4 == 0) {
            int indices = this.vertices - 4;

            this.indiceBuffer.Add(0 + indices);
            this.indiceBuffer.Add(1 + indices);
            this.indiceBuffer.Add(2 + indices);

            this.indiceBuffer.Add(0 + indices);
            this.indiceBuffer.Add(2 + indices);
            this.indiceBuffer.Add(3 + indices);
        }
    }

    public void tex(float u, float v) {
        this.hasTexture = true;

        this.u = u;
        this.v = v;
    }

    // ..:: Texture ::..
    private int texture;

    public void Texture() {
        GL.GenTextures(1, out this.texture);
        GL.BindTexture(TextureTarget.Texture2D, this.texture);

        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);

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

    public void bind() {
        GL.BindTexture(TextureTarget.Texture2D, this.texture);
    }

    // ..:: Tile ::..
    private int _tex = 0;

    public void _render() {
        float x0 = -0.5f;
        float y0 = -0.5f;

        float x1 = 0.5f;
        float y1 = 0.5f;

        float u0 = (float)this._tex / 16.0f;
        float v0 = (16.0f - 1.0f) / 16.0f;

        float u1 = u0 + (1.0f / 16.0f);
        float v1 = v0 + (1.0f / 16.0f);

        this.tex(u0, v0);
        this.vertex(x0, y0);
        this.tex(u1, v0);
        this.vertex(x1, y0);
        this.tex(u1, v1);
        this.vertex(x1, y1);
        this.tex(u0, v1);
        this.vertex(x0, y1);
    }

    // ..:: cordinate_systems ::..
    private int width;
    private int height;

    public void cordinate_systems(int width, int height) {
        this.width = width;
        this.height = height;

        this.projection();
        this.view();
    }

    private void projection() {
        Matrix4 projection = Matrix4.Identity;

        projection *= this.createPerspectiveFieldOfView();
        //projection *= this.createPerspectiveOffCenter();
        //projection *= this.createOrthographic();
        //projection *= this.createOrthographicOffCenter();

        this.setMat4("projection", projection);
    }

    private void view() {
        Matrix4 view = Matrix4.Identity;

        view *= Matrix4.CreateRotationX(MathHelper.DegreesToRadians(-55.0f));
        view *= Matrix4.CreateTranslation(0.0f, 0.0f, -3.0f);
        //view *= Matrix4.CreateTranslation(0.0f, 0.0f, -10.0f);

        //view *= Matrix4.CreateScale(16.0f);

        this.setMat4("view", view);
    }

    private Matrix4 createPerspectiveFieldOfView() {
        float fovy = MathHelper.DegreesToRadians(60.0f);
        float aspect = (float)this.width / (float)this.height;
        float depthNear = 0.3f;
        float depthFar = 1000.0f;

        return Matrix4.CreatePerspectiveFieldOfView(fovy, aspect, depthNear, depthFar);
    }

    // Não sei como funciona
    private Matrix4 createPerspectiveOffCenter() {
        float left = 0.0f;
        float right = (float)this.width;
        float bottom = 0.0f;
        float top = (float)this.height;
        float depthNear = 0.3f;
        float depthFar = 1000.0f;

        return Matrix4.CreatePerspectiveOffCenter(left, right, bottom, top, depthNear, depthFar);
    }

    private Matrix4 createOrthographic() {
        float width = (float)this.width;
        float height = (float)this.height;
        float depthNear = 0.3f;
        float depthFar = 1000.0f;

        return Matrix4.CreateOrthographic(width, height, depthNear, depthFar);
    }

    private Matrix4 createOrthographicOffCenter() {
        float left = 0.0f;
        float right = (float)this.width;
        float bottom = 0.0f;
        float top = (float)this.height;
        float depthNear = 0.3f;
        float depthFar = 1000.0f;

        return Matrix4.CreateOrthographicOffCenter(left, right, bottom, top, depthNear, depthFar);
    }
}
