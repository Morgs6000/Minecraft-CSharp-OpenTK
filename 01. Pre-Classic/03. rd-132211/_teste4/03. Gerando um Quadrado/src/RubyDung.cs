using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

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

    // ..:: TRIANGLE ::..
    // --------------------------------------------------
    // configura dados de vértice (e buffer(s)) e configura atributos de vértice
    /*
    private float[] vertices = {
        -0.5f, -0.5f,  // bottom left
         0.5f, -0.5f,  // bottom right
         0.5f,  0.5f,  // top right
        -0.5f,  0.5f   // top left
    };
    */
    private float[] vertices = new float[300000];
    //private Vector2[] vertices = new Vector2[300000];
    //private List<Vector2> vertices = new List<Vector2>();

    private int[] indices = { // observe que começamos do 0!
        0, 1, 3,  // primeiro Triângulo
        1, 2, 3   // segundo Triângulo
    };

    private int vertices_indices = 0;

    private int VAO; // Vertex Array Object;
    private int VBO; // Vertex Buffer Object
    private int EBO; // Element Buffer Object;

    private void Triangle() {
        float x0 = -0.5f;
        float y0 = -0.5f;

        float x1 = 0.5f;
        float y1 = 0.5f;

        this.vertex(x0, y0); // bottom left
        this.vertex(x1, y0); // bottom right
        this.vertex(x1, y1); // top right
        this.vertex(x0, y1); // top left
        //this.vertex();

        GL.GenVertexArrays(1, out this.VAO);
        GL.GenBuffers(1, out this.VBO);
        GL.GenBuffers(1, out this.EBO);

        // vincule o objeto Vertex Array primeiro, depois vincule e defina buffer(s) de vértice(s) e então configure atributos de vértice(s).
        GL.BindVertexArray(this.VAO);

        GL.BindBuffer(BufferTarget.ArrayBuffer, this.VBO);
        GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);
        //GL.BufferData(BufferTarget.ArrayBuffer, vertices.Count * Vector2.SizeInBytes, vertices.ToArray(), BufferUsageHint.StaticDraw);

        GL.BindBuffer(BufferTarget.ElementArrayBuffer, EBO);
        GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(int), indices, BufferUsageHint.StaticDraw);

        GL.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, 0, 0);
        GL.EnableVertexAttribArray(0);

        // observe que isso é permitido, a chamada para glVertexAttribPointer registrou VBO como o objeto de buffer de vértice vinculado do atributo de vértice para que depois possamos desvincular com segurança
        GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

        // lembre-se: NÃO desvincule o EBO enquanto um VAO estiver ativo, pois o objeto buffer do elemento vinculado ESTÁ armazenado no VAO; mantenha o EBO vinculado.
        //GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);

        // Você pode desvincular o VAO posteriormente para que outras chamadas VAO não modifiquem acidentalmente este VAO, mas isso raramente acontece. Modificar outros VAOs requer uma chamada para glBindVertexArray de qualquer maneira, então geralmente não desvinculamos VAOs (nem VBOs) quando não é diretamente necessário.
        GL.BindVertexArray(0);
    }

    /*
    private void vertex3(float x, float y, float z) {
        int index = vertices_indices * 3;

        vertices[index + 0] = x;
        vertices[index + 1] = y;
        vertices[index + 2] = z;

        vertices_indices++;

        Console.WriteLine($"Vertex added: X={x}, Y={y}, Z={z}. Total vertices: {vertices_indices}");
    }
    */

    //*
    private void vertex(float x, float y) {
        vertices[vertices_indices * 2 + 0] = x;
        vertices[vertices_indices * 2 + 1] = y;

        vertices_indices++;

        Console.WriteLine($"Vertex added: X={x}, Y={y}. Total vertices: {vertices_indices}");
    }
    //*/

    /*
    private void vertex() {
        float x0 = -0.5f;
        float x1 = 0.5f;

        float y0 = -0.5f;
        float y1 = 0.5f;

        
        //vertices[0] = x0;  // bottom left - x
        //vertices[1] = y0;  // bottom left - y

        //vertices[2] = x1;   // bottom right - x
        //vertices[3] = y0;  // bottom right - y

        //vertices[4] = x1;   // top right - x
        //vertices[5] = y1;   // top right - y

        //vertices[6] = x0;  // top left - x
        //vertices[7] = y1;   // top left - y
        
        //vertices[0] = new Vector2(x0, y0);
        //vertices[1] = new Vector2(x1, y0);
        //vertices[2] = new Vector2(x1, y1);
        //vertices[3] = new Vector2(x0, y1);
        
        vertices.Add(new Vector2(x0, y0));
        vertices.Add(new Vector2(x1, y0));
        vertices.Add(new Vector2(x1, y1));
        vertices.Add(new Vector2(x0, y1));
    }
    */

    protected override void OnLoad() {
        base.OnLoad();

        this.Shader();
        this.Triangle();
    }

    // loop de renderização
    protected override void OnRenderFrame(FrameEventArgs args) {
        base.OnRenderFrame(args);

        // entrada
        this.processInput();

        // renderizar
        GL.ClearColor(0.5f, 0.8f, 1.0f, 0.0f);
        GL.Clear(ClearBufferMask.ColorBufferBit);

        // desenhamos nosso primeiro triângulo
        GL.UseProgram(this.shaderProgram);
        GL.BindVertexArray(this.VAO); // visto que temos apenas um VAO, não há necessidade de vinculá-lo todas as vezes, mas faremos isso para manter as coisas um pouco mais organizadas
        //GL.DrawArrays(PrimitiveType.Triangles, 0, 3);
        GL.DrawElements(PrimitiveType.Triangles, 6, DrawElementsType.UnsignedInt, 0);
        //GL.BindVertexArray(0); // não há necessidade de desvinculá-lo todas as vezes

        // glfw: troca buffers e pesquisa eventos IO (teclas pressionadas/liberadas, mouse movido etc.)
        SwapBuffers();
    }

    // processar todas as entradas: consultar o GLFW se as teclas relevantes foram pressionadas/liberadas neste quadro e reagir de acordo
    private void processInput() {
        if(KeyboardState.IsKeyDown(Keys.Escape)) {
            Close();
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
