using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using StbImageSharp;
using System;
using System.Drawing;

namespace RubyDung.src;

public class RubyDung : GameWindow {
    private int width;
    private int height;

    private static void Main(string[] args) {
        Console.WriteLine("Hello, World!");

        GameWindowSettings gws = GameWindowSettings.Default;

        NativeWindowSettings nws = NativeWindowSettings.Default;
        nws.ClientSize = (1024, 768);
        nws.Title = "Game";

        new RubyDung(gws, nws).Run();
    }

    public RubyDung(GameWindowSettings gws, NativeWindowSettings nws) : base(gws, nws) {
        this.width = nws.ClientSize.X;
        this.height = nws.ClientSize.Y;

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
    private int VAO; // Vertex Array Object;
    private int VBO; // Vertex Buffer Object
    private int EBO; // Element Buffer Object;
    private int TBO; // Texture Buffer Object

    // Tamanho da textura
    private int widthIma = 256;
    private int heightIma = 256;

    // Tamanho do corte
    private float W = 16;
    private float H = 16;
    private float D = 16;

    // Deslocamento
    private float X = 0;
    private float Y = 0;

    private float scaleFactor = 1.0f;

    float[] vertices = new float[300000];
    //List<Vector3> vertices = new List<Vector3>();
    int[] indices = new int[300000];
    //List<int> indices = new List<int>();

    int vertices_indicies = 0;
    int indicies_vertices = 0;

    private void vertex(float x, float y, float z) {
        //*
        int index = vertices_indicies * 3;

        vertices[index + 0] = x;
        vertices[index + 1] = y;
        vertices[index + 2] = z;

        vertices_indicies++;
        //*/
        //vertices.Add(new Vector3(x, y, z));
    }

    private void triangle() {
        //*
        int index = indicies_vertices * 6;

        // Primeiro triângulo
        indices[index + 0] = 0 + indicies_vertices;
        indices[index + 1] = 1 + indicies_vertices;
        indices[index + 2] = 2 + indicies_vertices;

        // Segundo triângulo
        indices[index + 3] = 0 + indicies_vertices;
        indices[index + 4] = 2 + indicies_vertices;
        indices[index + 5] = 3 + indicies_vertices;

        indicies_vertices += 4;
        //*/
        /*
        // first triangle
        this.indices.Add(0 + this.indicies_vertices);
        this.indices.Add(1 + this.indicies_vertices);
        this.indices.Add(2 + this.indicies_vertices);

        // second triangle
        this.indices.Add(0 + this.indicies_vertices);
        this.indices.Add(2 + this.indicies_vertices);
        this.indices.Add(3 + this.indicies_vertices);

        indicies_vertices += 4;
        */
    }

    private void block() {
        float posX = (this.width / 2.0f) / scaleFactor;
        float posY = (this.height / 2.0f) / scaleFactor;
        float posZ = 0.0f;

        float x0 = posX + 0.0f;
        float y0 = posY + 0.0f;
        float z0 = posZ + 0.0f;

        float x1 = posX + (1.0f * W);
        float y1 = posY + (1.0f * H);
        float z1 = posZ + (1.0f * D);

        //*
        // -X
        vertex(x0, y0, z0);
        vertex(x0, y0, z1);
        vertex(x0, y1, z1);
        vertex(x0, y1, z0);
        triangle();


        // +X
        vertex(x1, y0, z1);
        vertex(x1, y0, z0);
        vertex(x1, y1, z0);
        vertex(x1, y1, z1);
        triangle();

        // -Y
        vertex(x0, y0, z0);
        vertex(x1, y0, z0);
        vertex(x1, y0, z1);
        vertex(x0, y0, z1);
        triangle();

        // +Y
        vertex(x0, y1, z1);
        vertex(x1, y1, z1);
        vertex(x1, y1, z0);
        vertex(x0, y1, z0);
        triangle();

        // -Z
        vertex(x1, y0, z0);
        vertex(x0, y0, z0);
        vertex(x0, y1, z0);
        vertex(x1, y1, z0);
        triangle();
        //*/

        // +Z
        vertex(x0, y0, z1);
        vertex(x1, y0, z1);
        vertex(x1, y1, z1);
        vertex(x0, y1, z1);
        triangle();
    }

    private void Triangle(Vector3 size) {
        /*
        float x0 = -(size.X / 2.0f);
        float y0 = -(size.X / 2.0f);
        float z0 = -(size.X / 2.0f);

        float x1 = (size.X / 2.0f);
        float y1 = (size.X / 2.0f);
        float z1 = (size.X / 2.0f);
        //*/

        /*
        float posX = (this.width / 2.0f) / scaleFactor;
        float posY = (this.height / 2.0f) / scaleFactor;
        float posZ = 0.0f;

        float x0 = posX + 0.0f;
        float y0 = posY + 0.0f;

        float x1 = posX + (1.0f * W);
        float y1 = posY + (1.0f * H);
        */

        /*
        // configura dados de vértice (e buffer(s)) e configura atributos de vértice
        float[] vertices = {
            x0, y0,  // bottom left
            x1, y0,  // bottom right
            x1, y1,  // top right
            x0, y1   // top left
        };

        int[] indices = { // observe que começamos do 0!
            0, 1, 2,  // primeiro Triângulo
            0, 2, 3   // segundo Triângulo
        };
        */

        this.block();

        int tex = 0;

        //float u0 = (float)tex / 16.0f;
        //float u1 = u0 + (1.0f / 16.0f);
        //float v0 = ((16.0f - 1.0f) - (float)tex) / 16.0f;
        //float v1 = v0 + (1.0f / 16.0f);

        float u0 = X / widthIma;
        float u1 = u0 + ((float)W / widthIma);
        float v0 = (float)((heightIma - Y) - H) / heightIma;
        float v1 = v0 + (H / heightIma);

        float[] texCoords = {
            u0, v0,  // bottom left
            u1, v0,  // bottom right
            u1, v1,  // top right
            u0, v1   // top left
        };

        GL.GenVertexArrays(1, out this.VAO);
        GL.GenBuffers(1, out this.VBO);
        GL.GenBuffers(1, out this.EBO);
        GL.GenBuffers(1, out this.TBO);

        // vincule o objeto Vertex Array primeiro, depois vincule e defina buffer(s) de vértice(s) e então configure atributos de vértice(s).
        GL.BindVertexArray(this.VAO);

        GL.BindBuffer(BufferTarget.ArrayBuffer, this.VBO);
        GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);
        //GL.BufferData(BufferTarget.ArrayBuffer, vertices.Count * Vector3.SizeInBytes, vertices.ToArray(), BufferUsageHint.StaticDraw);

        GL.BindBuffer(BufferTarget.ElementArrayBuffer, EBO);
        GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(int), indices, BufferUsageHint.StaticDraw);
        //GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Count * sizeof(int), indices.ToArray(), BufferUsageHint.StaticDraw);

        // atributo de posição
        GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 0, 0);
        GL.EnableVertexAttribArray(0);

        GL.BindBuffer(BufferTarget.ArrayBuffer, this.TBO);
        GL.BufferData(BufferTarget.ArrayBuffer, texCoords.Length * sizeof(float), texCoords, BufferUsageHint.StaticDraw);

        // atributo de coordenação de textura
        GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 0, 0);
        GL.EnableVertexAttribArray(1);

        // observe que isso é permitido, a chamada para glVertexAttribPointer registrou VBO como o objeto de buffer de vértice vinculado do atributo de vértice para que depois possamos desvincular com segurança
        GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

        // lembre-se: NÃO desvincule o EBO enquanto um VAO estiver ativo, pois o objeto buffer do elemento vinculado ESTÁ armazenado no VAO; mantenha o EBO vinculado.
        //GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);

        // Você pode desvincular o VAO posteriormente para que outras chamadas VAO não modifiquem acidentalmente este VAO, mas isso raramente acontece. Modificar outros VAOs requer uma chamada para glBindVertexArray de qualquer maneira, então geralmente não desvinculamos VAOs (nem VBOs) quando não é diretamente necessário.
        GL.BindVertexArray(0);
    }

    private Vector3 cameraPos = new Vector3(0.0f, 0.0f, 0.3f);
    private Vector3 cameraFront = new Vector3(0.0f, 0.0f, -1.0f);
    private Vector3 cameraUp = new Vector3(0.0f, 1.0f, 0.0f);

    private float yaw = -60.0f;
    private float pitch = -45.0f;

    private void Camera() {
        this.model_matrix();
        this.view_matrix();
        this.projection_matrix();

        Vector3 direction;
        direction.X = (float)Math.Cos(MathHelper.DegreesToRadians(yaw)) * (float)Math.Cos(MathHelper.DegreesToRadians(pitch));
        direction.Y = (float)Math.Sin(MathHelper.DegreesToRadians(pitch));
        direction.Z = (float)Math.Sin(MathHelper.DegreesToRadians(yaw)) * (float)Math.Cos(MathHelper.DegreesToRadians(pitch));
        cameraFront = Vector3.Normalize(direction);
    }

    private void model_matrix() {
        Matrix4 model = Matrix4.Identity;

        model *= Matrix4.CreateTranslation(-W / 2.0f, -H / 2.0f, 0.0f);

        // Scale Factor
        model *= Matrix4.CreateScale(scaleFactor);

        // Scale
        model *= Matrix4.CreateScale(1.0f, 1.0f, 1.0f);

        // Rotation
        model *= Matrix4.CreateRotationX(0.0f);
        model *= Matrix4.CreateRotationY(0.0f);
        model *= Matrix4.CreateRotationZ(0.0f);

        // Position
        //model *= Matrix4.CreateTranslation(0.0f, 0.0f, 0.0f);
        //model *= Matrix4.CreateTranslation(this.width / 2.0f, this.height / 2.0f, 0.0f);
        //model *= Matrix4.CreateTranslation(16.0f, 16.0f, 0.0f);

        int modelLoc = GL.GetUniformLocation(shaderProgram, "model");
        GL.UniformMatrix4(modelLoc, false, ref model);
    }

    //private Vector3 cameraPos = new Vector3(0.0f, 0.0f, 0.3f);
    //private Vector3 cameraFront = new Vector3(0.0f, 0.0f, -1.0f);
    //private Vector3 cameraUp = new Vector3(0.0f, 1.0f, 0.0f);
    //private Vector3 cameraRight;

    private void view_matrix() {
        Matrix4 view = Matrix4.Identity;

        view *= Matrix4.LookAt(this.cameraPos, this.cameraPos + this.cameraFront, this.cameraUp);

        int viewLoc = GL.GetUniformLocation(shaderProgram, "view");
        GL.UniformMatrix4(viewLoc, false, ref view);
    }

    private void projection_matrix() {
        Matrix4 projection = Matrix4.Identity;

        projection *= Matrix4.CreateOrthographicOffCenter(0.0f, (float)this.width, 0.0f, (float)this.height, -100.0f, 100.0f);

        int projectionLoc = GL.GetUniformLocation(shaderProgram, "projection");
        GL.UniformMatrix4(projectionLoc, false, ref projection);
    }

    protected override void OnLoad() {
        // configurar o estado opengl global
        GL.Enable(EnableCap.DepthTest);
        GL.Enable(EnableCap.CullFace);
        GL.CullFace(CullFaceMode.Back);

        this.Shader();
        this.Texture();
        this.Triangle(new Vector3(16, 16, 16));
    }

    // loop de renderização
    protected override void OnRenderFrame(FrameEventArgs args) {
        // entrada
        this.processInput();

        // renderizar
        GL.ClearColor(0.5f, 0.8f, 1.0f, 0.0f);
        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

        // liga a textura
        GL.BindTexture(TextureTarget.Texture2D, this.texture);

        GL.UseProgram(this.shaderProgram);
        this.Camera();

        //this.Color(1.0f, 0.36f, 0.95f, 1.0f);
        this.Color(255, 91, 241, 128);
        //this.Color("FF5BF1", 255);

        //this.Color1(1.0f, 0.36f, 0.95f, 1.0f);
        //this.Color2(255, 91, 241, 255);
        //this.Color3("FF5BF1");

        // renderiza o triângulo
        GL.BindVertexArray(this.VAO);
        GL.DrawElements(PrimitiveType.Triangles, indices.Length, DrawElementsType.UnsignedInt, 0);

        // glfw: troca buffers e pesquisa eventos IO (teclas pressionadas/liberadas, mouse movido etc.)
        SwapBuffers();
    }

    private void Color1(float r, float g, float b, float a) {
        int colorLoc = GL.GetUniformLocation(shaderProgram, "color");
        GL.Uniform4(colorLoc, new Vector4(r, g, b, a));
    }

    private void Color2(int r, int g, int b, int a) {
        int colorLoc = GL.GetUniformLocation(shaderProgram, "color");

        float fr = r / 255.0f;
        float fg = g / 255.0f;
        float fb = b / 255.0f;
        float fa = a / 255.0f;

        GL.Uniform4(colorLoc, new Vector4(fr, fg, fb, fa));
    }

    private void Color3(string hex) {
        int colorLoc = GL.GetUniformLocation(shaderProgram, "color");

        // Verifica se o comprimento da string hex é válido
        if(hex.Length != 6) {
            throw new ArgumentException("A string hexadecimal deve ter 6 dígitos.");
        }

        // Remover o caractere '#' se presente
        hex = hex.TrimStart('#');

        // Converter o valor hexadecimal em componentes RGBA
        int r = Convert.ToInt32(hex.Substring(0, 2), 16);
        int g = Convert.ToInt32(hex.Substring(2, 2), 16);
        int b = Convert.ToInt32(hex.Substring(4, 2), 16);

        // Converte os componentes RGB para o intervalo de 0 a 1
        float fr = r / 255.0f;
        float fg = g / 255.0f;
        float fb = b / 255.0f;
        float fa = 1.0f;

        GL.Uniform4(colorLoc, new Vector4(fr, fg, fb, fa));
    }

    private Vector4 ConvertColorToHex(string hex, int a) {
        // Verifica se o comprimento da string hex é válido
        if(hex.Length != 6) {
            throw new ArgumentException("A string hexadecimal deve ter 6 dígitos.");
        }

        // Converter o valor hexadecimal em componentes RGBA
        int fr = Convert.ToInt32(hex.Substring(0, 2), 16);
        int fg = Convert.ToInt32(hex.Substring(2, 2), 16);
        int fb = Convert.ToInt32(hex.Substring(4, 2), 16);
        int fa = a / 255;

        return this.ConvertColorToRGBA(fr, fg, fb, fa);
    }

    private Vector4 ConvertColorToRGBA(int r, int g, int b, int a) {
        float fr = r / 255.0f;
        float fg = g / 255.0f;
        float fb = b / 255.0f;
        float fa = a / 255.0f;

        return new Vector4(fr, fg, fb, fa);
    }

    private void Color(string hex, int a) {
        int colorLoc = GL.GetUniformLocation(shaderProgram, "color");
        GL.Uniform4(colorLoc, ConvertColorToHex("FFFFFF", 255));
    }

    private void Color(int r, int g, int b, int a) {
        int colorLoc = GL.GetUniformLocation(shaderProgram, "color");
        GL.Uniform4(colorLoc, ConvertColorToRGBA(r, g, b, a));
    }

    private void Color(float r, float g, float b, float a) {
        int colorLoc = GL.GetUniformLocation(shaderProgram, "color");
        GL.Uniform4(colorLoc, new Vector4(r, g, b, a));
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
        this.width = e.Width;
        this.height = e.Height;

        GL.Viewport(0, 0, this.width, this.height);
    }
}
