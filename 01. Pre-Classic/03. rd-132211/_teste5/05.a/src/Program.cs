using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using StbImageSharp;

namespace RubyDung.src;

public class Program : GameWindow {
    private int width;
    private int height;

    private bool isWireframe = false;

    private Program(GameWindowSettings gws, NativeWindowSettings nws) : base(gws, nws) {
        CenterWindow();
    }

    protected override void OnFramebufferResize(FramebufferResizeEventArgs e) {
        this.width = e.Width;
        this.height = e.Height;

        GL.Viewport(0, 0, e.Width, e.Height);
    }

    // ..:: SHADER ::..

    public int shaderProgram;

    private void Shader(string vertexPath, string fragmentPath) {
        string vertexShaderSource = File.ReadAllText($"../../../src/shaders/{vertexPath}");
        string fragmentShaderSource = File.ReadAllText($"../../../src/shaders/{fragmentPath}");

        int success;
        string infoLog;

        // sombreador de vértice
        int vertexShader = GL.CreateShader(ShaderType.VertexShader);
        GL.ShaderSource(vertexShader, vertexShaderSource);
        GL.CompileShader(vertexShader);
        // verifique se há erros de compilação do shader        
        GL.GetShader(vertexShader, ShaderParameter.CompileStatus, out success);
        if(success == 0) {
            GL.GetShaderInfoLog(vertexShader, out infoLog);
            Console.WriteLine($"ERROR::SHADER::VERTEX::COMPILATION_FAILED\n{infoLog}");
        }

        // sombreador de fragmento
        int fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
        GL.ShaderSource(fragmentShader, fragmentShaderSource);
        GL.CompileShader(fragmentShader);
        // verifique se há erros de compilação do shader
        GL.GetShader(fragmentShader, ShaderParameter.CompileStatus, out success);
        if(success == 0) {
            GL.GetShaderInfoLog(fragmentShader, out infoLog);
            Console.WriteLine($"ERROR::SHADER::FRAGMENT::COMPILATION_FAILED\n{infoLog}");
        }

        // shaders de link
        this.shaderProgram = GL.CreateProgram();
        GL.AttachShader(this.shaderProgram, vertexShader);
        GL.AttachShader(this.shaderProgram, fragmentShader);
        GL.LinkProgram(this.shaderProgram);
        // verifique se há erros de compilação do shader
        GL.GetProgram(this.shaderProgram, GetProgramParameterName.LinkStatus, out success);
        if(success == 0) {
            GL.GetShaderInfoLog(this.shaderProgram, out infoLog);
            Console.WriteLine($"ERROR::SHADER::PROGRAM::LINKING_FAILED\n{infoLog}");
        }

        // exclui os shaders, pois eles estão vinculados ao nosso programa agora e não são mais necessários
        GL.DeleteShader(vertexShader);
        GL.DeleteShader(fragmentShader);
    }

    private void useShader() {
        GL.UseProgram(this.shaderProgram);
    }

    private void setBool(string name, bool value) {
        GL.Uniform1(GL.GetUniformLocation(this.shaderProgram, name), value ? 1 : 0);
    }

    // ..:: TESSELATOR ::..

    private float[] vertices = {
        // positions    // texture coords
        -0.5f, -0.5f,   0.0f, 0.0f,   // top right
         0.5f, -0.5f,   1.0f, 0.0f,   // bottom right
         0.5f,  0.5f,   1.0f, 1.0f,   // bottom left
        -0.5f,  0.5f,   0.0f, 1.0f    // top left
    };

    private int[] indices = {
        0, 1, 2, // first triangle
        0, 2, 3  // second triangle
    };

    private int VAO; // Vertex Array Object
    private int VBO; // Vertex Buffer Object
    private int EBO; // Element Buffer Object

    private void Tesseletor() {
        GL.GenVertexArrays(1, out this.VAO);
        GL.GenBuffers(1, out this.VBO);
        GL.GenBuffers(1, out this.EBO);

        GL.BindVertexArray(this.VAO);

        GL.BindBuffer(BufferTarget.ArrayBuffer, this.VBO);
        GL.BufferData(BufferTarget.ArrayBuffer, this.vertices.Length * sizeof(float), this.vertices, BufferUsageHint.StaticDraw);

        GL.BindBuffer(BufferTarget.ElementArrayBuffer, this.EBO);
        GL.BufferData(BufferTarget.ElementArrayBuffer, this.indices.Length * sizeof(int), this.indices, BufferUsageHint.StaticDraw);

        // atributo de posição
        GL.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, 4 * sizeof(float), 0);
        GL.EnableVertexAttribArray(0);

        // atributo de coordenação de textura
        GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 4 * sizeof(float), 2 * sizeof(float));
        GL.EnableVertexAttribArray(1);

        GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

        GL.BindVertexArray(0);
    }

    private void bindTesseletor() {
        GL.BindVertexArray(this.VAO);
        GL.DrawElements(PrimitiveType.Triangles, 6, DrawElementsType.UnsignedInt, 0);
    }

    // ..:: TEXTURE ::..

    private int texture;

    private void Texture(string texturePath) {
        GL.GenTextures(1, out texture);
        GL.BindTexture(TextureTarget.Texture2D, texture);

        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);

        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);

        StbImage.stbi_set_flip_vertically_on_load(1);

        ImageResult image = ImageResult.FromStream(File.OpenRead($"../../../src/textures/{texturePath}"), ColorComponents.RedGreenBlueAlpha);

        if(image.Data != null) {
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, image.Width, image.Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, image.Data);
            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
        }
        else {
            Console.WriteLine("Failed to load texture");
        }
    }

    private void bindTexture() {
        GL.BindTexture(TextureTarget.Texture2D, texture);
    }

    // ..:: LOAD ::..

    protected override void OnLoad() {
        this.Shader("shader.vert", "shader.frag");

        this.Tesseletor();

        this.Texture("terrain.png");
    }

    // ..:: RENDER ::..

    protected override void OnRenderFrame(FrameEventArgs args) {
        this.processInput();

        GL.ClearColor(0.5f, 0.8f, 1.0f, 0.0f);
        GL.Clear(ClearBufferMask.ColorBufferBit);

        this.bindTexture();

        this.useShader();

        this.bindTesseletor();

        SwapBuffers();
    }

    private void processInput() {
        // fechar janela
        if(KeyboardState.IsKeyDown(Keys.Escape)) {
            Close();
        }

        // wireframe mode
        if(KeyboardState.IsKeyDown(Keys.F3) && KeyboardState.IsKeyPressed(Keys.W)) {
            this.isWireframe = !this.isWireframe;

            this.setBool("isWireframe", this.isWireframe);

            GL.PolygonMode(MaterialFace.FrontAndBack, this.isWireframe ? PolygonMode.Line : PolygonMode.Fill);
        }
    }

    private static void Main(string[] args) {
        GameWindowSettings gws = GameWindowSettings.Default;

        NativeWindowSettings nws = NativeWindowSettings.Default;
        nws.ClientSize = (1024, 768);
        nws.Title = "Game";

        new Program(gws, nws).Run();
    }
}