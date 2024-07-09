using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using StbImageSharp;

namespace RubyDung.src;

public class RubyDung : GameWindow {
    private int width;
    private int height;

    private static void Main(string[] args) {
        Console.WriteLine("Hello, World!");

        new RubyDung(1024, 768, "Game").Run();
    }

    public RubyDung(int widht, int height, string title)
        : base(GameWindowSettings.Default, new NativeWindowSettings() {
            ClientSize = (widht, height),
            Title = title
        }) {
        this.width = widht;
        this.height = height;

        CenterWindow();
    }

    // ..:: construir e compilar nosso programa shader ::..
    // --------------------------------------------------
    private int shaderProgram;

    private void Shader() {
        int success;
        string infoLog;

        string vertexShaderSource = File.ReadAllText("../../../src/shaders/shader.vert");
        string fragmentShaderSource = File.ReadAllText("../../../src/shaders/shader.frag");

        // shader de vértice
        int vertexShader = GL.CreateShader(ShaderType.VertexShader);
        GL.ShaderSource(vertexShader, vertexShaderSource);
        GL.CompileShader(vertexShader);

        // verifica erros de compilação do shader
        GL.GetShader(vertexShader, ShaderParameter.CompileStatus, out success);
        if(success == 0) {
            GL.GetShaderInfoLog(vertexShader, out infoLog);
            Console.WriteLine("ERROR::SHADER::VERTEX::COMPILATION_FAILED\n" + infoLog);
        }

        // shader de fragmento
        int fragmentShader;
        fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
        GL.ShaderSource(fragmentShader, fragmentShaderSource);
        GL.CompileShader(fragmentShader);

        // verifica erros de compilação do shader
        GL.GetShader(fragmentShader, ShaderParameter.CompileStatus, out success);
        if(success == 0) {
            GL.GetShaderInfoLog(fragmentShader, out infoLog);
            Console.WriteLine("ERROR::SHADER::FRAGMENT::COMPILATION_FAILED\n" + infoLog);
        }

        // vincula shaders
        this.shaderProgram = GL.CreateProgram();

        GL.AttachShader(this.shaderProgram, vertexShader);
        GL.AttachShader(this.shaderProgram, fragmentShader);
        GL.LinkProgram(this.shaderProgram);

        // verifica se há erros de vinculação
        GL.GetProgram(this.shaderProgram, GetProgramParameterName.LinkStatus, out success);
        if(success == 0) {
            GL.GetProgramInfoLog(this.shaderProgram, out infoLog);
            Console.WriteLine("ERROR::SHADER::PROGRAM::LINKING_FAILED\n" + infoLog);
        }

        GL.DeleteShader(vertexShader);
        GL.DeleteShader(fragmentShader);
    }

    // ..:: carrega e cria uma textura ::..
    // --------------------------------------------------
    private int texture;

    private void Texture() {
        GL.GenTextures(1, out this.texture);
        GL.BindTexture(TextureTarget.Texture2D, this.texture); // todas as próximas operações GL_TEXTURE_2D agora terão efeito neste objeto de textura

        // define os parâmetros de quebra de textura
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat); // define o empacotamento de textura para GL_REPEAT (método de empacotamento padrão)
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);

        // define os parâmetros de filtragem de textura
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);

        // O FileSystem::getPath(...) faz parte do repositório GitHub para que possamos encontrar arquivos em qualquer IDE/plataforma; substitua-o pelo seu próprio caminho de imagem.
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

    // ..:: TRIANGLE ::..
    // --------------------------------------------------
    private List<Vector3> vertices = new List<Vector3>();
    private List<int> indices = new List<int>();
    private List<Vector2> texCoords = new List<Vector2>();

    private int verticesIndices;

    private int VAO; // Vertex Array Object;
    private int VBO; // Vertex Buffer Object
    private int EBO; // Element Buffer Object;
    private int TBO; // Texture Buffer Object

    public void vertex() {
        float x0 = -0.5f;
        float y0 = -0.5f;
        float z0 = -0.5f;

        float x1 = 0.5f;
        float y1 = 0.5f;
        float z1 = 0.5f;

        // -x
        this.vertices.Add(new Vector3(x0, y0, z0));  // bottom left
        this.vertices.Add(new Vector3(x0, y1, z0));  // top left
        this.vertices.Add(new Vector3(x0, y1, z1));  // top right
        this.vertices.Add(new Vector3(x0, y0, z1));  // bottom right

        this.triangle();
        this.tex();

        // +x
        this.vertices.Add(new Vector3(x1, y0, z1));  // bottom left
        this.vertices.Add(new Vector3(x1, y1, z1));  // top left
        this.vertices.Add(new Vector3(x1, y1, z0));  // top right
        this.vertices.Add(new Vector3(x1, y0, z0));  // bottom right

        this.triangle();
        this.tex();

        // -y
        this.vertices.Add(new Vector3(x0, y0, z0));  // bottom left
        this.vertices.Add(new Vector3(x0, y0, z1));  // top left
        this.vertices.Add(new Vector3(x1, y0, z1));  // top right
        this.vertices.Add(new Vector3(x1, y0, z0));  // bottom right

        this.triangle();
        this.tex();

        // +y
        this.vertices.Add(new Vector3(x0, y1, z1));  // bottom left
        this.vertices.Add(new Vector3(x0, y1, z0));  // top left
        this.vertices.Add(new Vector3(x1, y1, z0));  // top right
        this.vertices.Add(new Vector3(x1, y1, z1));  // bottom right

        this.triangle();
        this.tex();

        // -z
        this.vertices.Add(new Vector3(x1, y0, z0));  // bottom left
        this.vertices.Add(new Vector3(x1, y1, z0));  // top left
        this.vertices.Add(new Vector3(x0, y1, z0));  // top right
        this.vertices.Add(new Vector3(x0, y0, z0));  // bottom right

        this.triangle();
        this.tex();

        // +z
        this.vertices.Add(new Vector3(x0, y0, z1));  // bottom left
        this.vertices.Add(new Vector3(x0, y1, z1));  // top left
        this.vertices.Add(new Vector3(x1, y1, z1));  // top right
        this.vertices.Add(new Vector3(x1, y0, z1));  // bottom right

        this.triangle();
        this.tex();
    }

    public void triangle() {
        // first triangle
        indices.Add(0 + verticesIndices);
        indices.Add(1 + verticesIndices);
        indices.Add(2 + verticesIndices);

        // second triangle
        indices.Add(0 + verticesIndices);
        indices.Add(2 + verticesIndices);
        indices.Add(3 + verticesIndices);

        verticesIndices += 4;
    }

    public void tex() {
        int tex = 0;

        float u0 = (float)tex / 16.0f;
        float u1 = u0 + (1.0f / 16.0f);
        float v0 = ((16.0f - 1.0f) - (float)tex) / 16.0f;
        float v1 = v0 + (1.0f / 16.0f);

        texCoords.Add(new Vector2(u0, v0));
        texCoords.Add(new Vector2(u0, v1));
        texCoords.Add(new Vector2(u1, v1));
        texCoords.Add(new Vector2(u1, v0));
    }

    private void Triangle() {
        this.vertex();

        GL.GenVertexArrays(1, out this.VAO);
        GL.GenBuffers(1, out this.VBO);
        GL.GenBuffers(1, out this.EBO);
        GL.GenBuffers(1, out this.TBO);

        // vincule o objeto Vertex Array primeiro, depois vincule e defina buffer(s) de vértice(s) e então configure atributos de vértice(s).
        GL.BindVertexArray(this.VAO);

        GL.BindBuffer(BufferTarget.ArrayBuffer, this.VBO);
        GL.BufferData(BufferTarget.ArrayBuffer, this.vertices.Count * Vector3.SizeInBytes, this.vertices.ToArray(), BufferUsageHint.StaticDraw);

        GL.BindBuffer(BufferTarget.ElementArrayBuffer, EBO);
        GL.BufferData(BufferTarget.ElementArrayBuffer, this.indices.Count * sizeof(int), this.indices.ToArray(), BufferUsageHint.StaticDraw);

        // atributo de posição
        GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
        GL.EnableVertexAttribArray(0);

        GL.BindBuffer(BufferTarget.ArrayBuffer, this.TBO);
        GL.BufferData(BufferTarget.ArrayBuffer, this.texCoords.Count * Vector2.SizeInBytes, this.texCoords.ToArray(), BufferUsageHint.StaticDraw);

        // atributo de coordenação de textura
        GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 2 * sizeof(float), 0);
        GL.EnableVertexAttribArray(1);

        // observe que isso é permitido, a chamada para glVertexAttribPointer registrou VBO como o objeto de buffer de vértice vinculado do atributo de vértice para que depois possamos desvincular com segurança
        GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

        // lembre-se: NÃO desvincule o EBO enquanto um VAO estiver ativo, pois o objeto buffer do elemento vinculado ESTÁ armazenado no VAO; mantenha o EBO vinculado.
        //GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);

        // Você pode desvincular o VAO posteriormente para que outras chamadas VAO não modifiquem acidentalmente este VAO, mas isso raramente acontece. Modificar outros VAOs requer uma chamada para glBindVertexArray de qualquer maneira, então geralmente não desvinculamos VAOs (nem VBOs) quando não é diretamente necessário.
        GL.BindVertexArray(0);
    }

    protected override void OnLoad() {
        base.OnLoad();

        this.Shader();
        this.Texture();
        this.Triangle();

        // configurar o estado opengl global
        GL.Enable(EnableCap.DepthTest);
    }

    // loop de renderização
    protected override void OnRenderFrame(FrameEventArgs args) {
        base.OnRenderFrame(args);

        // entrada
        this.processInput();

        // renderizar
        GL.ClearColor(0.5f, 0.8f, 1.0f, 0.0f);
        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

        // liga a textura
        GL.BindTexture(TextureTarget.Texture2D, this.texture);

        // cria transformações
        Matrix4 transform = Matrix4.Identity; // certifique-se de inicializar a matriz para a matriz identidade primeiro
        transform *= Matrix4.CreateRotationX(45.0f);
        transform *= Matrix4.CreateRotationY(45.0f);
        transform *= Matrix4.CreateRotationZ(45.0f);

        // obtém a localização uniforme da matriz e define a matriz
        GL.UseProgram(this.shaderProgram);
        int transformLoc = GL.GetUniformLocation(this.shaderProgram, "transform");
        GL.UniformMatrix4(transformLoc, false, ref transform);

        // renderiza o triângulo
        GL.BindVertexArray(this.VAO);
        GL.DrawElements(PrimitiveType.Triangles, this.indices.Count, DrawElementsType.UnsignedInt, 0);

        // glfw: troca buffers e pesquisa eventos IO (teclas pressionadas/liberadas, mouse movido etc.)
        SwapBuffers();
    }

    // processar todas as entradas: consultar o GLFW se as teclas relevantes foram pressionadas/liberadas neste quadro e reagir de acordo
    private void processInput() {
        if(KeyboardState.IsKeyDown(Keys.Escape)) {
            Close();
        }

        // wirefreame
        if(KeyboardState.IsKeyDown(Keys.PageUp)) {
            GL.Uniform1(GL.GetUniformLocation(this.shaderProgram, "isWireframe"), 0);
            GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);
        }
        if(KeyboardState.IsKeyDown(Keys.PageDown)) {
            GL.Uniform1(GL.GetUniformLocation(this.shaderProgram, "isWireframe"), 1);
            GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);
        }
    }

    // glfw: sempre que o tamanho da janela for alterado (por sistema operacional ou redimensionamento do usuário), esta função de retorno de chamada é executada
    protected override void OnFramebufferResize(FramebufferResizeEventArgs e) {
        base.OnFramebufferResize(e);

        this.width = e.Width;
        this.height = e.Height;

        GL.Viewport(0, 0, e.Width, e.Height);
    }
}
