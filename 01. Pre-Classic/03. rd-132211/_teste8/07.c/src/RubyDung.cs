using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using StbImageSharp;

namespace RubyDung.src;

public class RubyDung : GameWindow {
    private int width;
    private int height;

    public RubyDung(GameWindowSettings gws, NativeWindowSettings nws) : base(gws, nws) {
        this.width = this.ClientSize.X;
        this.height = this.ClientSize.Y;
        
        this.CenterWindow();
    }

    protected override void OnFramebufferResize(FramebufferResizeEventArgs e) {
        this.width = e.Width;
        this.height = e.Height;

        this.framebuffer_size_callback(e.Width, e.Height);
    }

    // ..:: Shader ::..
    Shader shader;

    // ..:: Triangle ::..
    private float[] vertices = {
        // vertices     // texture coords
        -0.5f, -0.5f,   0.0f, 0.0f,       // bottom left  // 0
         0.5f, -0.5f,   1.0f, 0.0f,       // bottom right // 1
         0.5f,  0.5f,   1.0f, 1.0f,       // top right    // 2
        -0.5f,  0.5f,   0.0f, 1.0f        // top left     // 3
    };

    private int[] indices = {
        0, 1, 2, // first Triangle
        0, 2, 3  // second Triangle
    };

    private int VAO; // Vertex Array Object
    private int VBO; // Vertex Buffer Object
    private int EBO; // Element Buffer Object

    private void Triangle() {
        // Vertex Array Object
        GL.GenVertexArrays(1, out this.VAO);

        GL.BindVertexArray(this.VAO);

        // Vertex Buffer Object
        GL.GenBuffers(1, out this.VBO);

        GL.BindBuffer(BufferTarget.ArrayBuffer, this.VBO);
        GL.BufferData(BufferTarget.ArrayBuffer, this.vertices.Length * sizeof(float), this.vertices, BufferUsageHint.StaticDraw);

        // position attribute
        GL.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, 4 * sizeof(float), 0);
        GL.EnableVertexAttribArray(0);

        // texture coord attribute
        GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 4 * sizeof(float), 2 * sizeof(float));
        GL.EnableVertexAttribArray(1);

        // Element Buffer Object
        GL.GenBuffers(1, out this.EBO);

        GL.BindBuffer(BufferTarget.ElementArrayBuffer, this.EBO);
        GL.BufferData(BufferTarget.ElementArrayBuffer, this.indices.Length * sizeof(int), this.indices, BufferUsageHint.StaticDraw);

        // Clear
        GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

        GL.BindVertexArray(0);
    }

    // ..:: Texture ::..
    //Texture texture;
    private TextureAtlas textureAtlas;
    private int textureId;

    protected override void OnLoad() {
        this.shader = new Shader("vertexShader.glsl", "fragmentShader.glsl");
        this.Triangle();
        //this.texture = new Texture("terrain.png");

        string texturesPath = "../../../src/textures/blocks";
        string[] textureFiles = Directory.GetFiles(texturesPath, "*.png", SearchOption.AllDirectories);

        int atlasWidth = 256; // Largura do atlas
        int atlasHeight = 256; // Altura do atlas

        textureAtlas = new TextureAtlas(atlasWidth, atlasHeight); // Dimensões do atlas (ajuste conforme necessário)

        int x = 0, y = 0;
        int rowHeight = 0;

        foreach(var filePath in textureFiles) {
            using(var stream = File.OpenRead(filePath)) {
                var image = ImageResult.FromStream(stream, ColorComponents.RedGreenBlueAlpha);
                string textureName = Path.GetFileNameWithoutExtension(filePath);
                int textureWidth = image.Width;
                int textureHeight = image.Height;

                if(x + textureWidth > atlasWidth) // Verifica se há espaço suficiente na linha atual
                {
                    x = 0;
                    y += rowHeight;
                    rowHeight = 0;
                    if(y + textureHeight > atlasHeight) // Verifica se há espaço suficiente na coluna
                    {
                        throw new Exception("O atlas de texturas está cheio. Não há espaço suficiente para adicionar mais texturas.");
                    }
                }

                textureAtlas.AddTexture(textureName, image.Data, textureWidth, textureHeight, x, y);
                x += textureWidth; // Avança para a próxima posição
                rowHeight = Math.Max(rowHeight, textureHeight); // Mantém a altura da linha atual
            }
        }

        textureId = textureAtlas.CreateTexture(); // Cria a textura do OpenGL a partir do atlas
    }

    protected override void OnRenderFrame(FrameEventArgs args) {
        this.processInput();

        GL.ClearColor(0.5f, 0.8f, 1.0f, 0.0F);
        GL.Clear(ClearBufferMask.ColorBufferBit);

        //this.texture.bind();
        GL.BindTexture(TextureTarget.Texture2D, textureId);

        this.shader.use();

        GL.BindVertexArray(this.VAO);
        GL.DrawElements(PrimitiveType.Triangles, 6, DrawElementsType.UnsignedInt, 0);

        this.SwapBuffers();
    }

    private bool isWireframe = false;

    private void processInput() {
        // CLose Window
        if(this.KeyboardState.IsKeyDown(Keys.Escape)) {
            this.Close();
        }

        // Wireframe
        if(this.KeyboardState.IsKeyDown(Keys.F3) && this.KeyboardState.IsKeyPressed(Keys.W)) {
            this.isWireframe = !this.isWireframe;

            //GL.Uniform1(GL.GetUniformLocation(this.shaderProgram, "isWireframe"), this.isWireframe ? 1 : 0);
            this.shader.setBool("isWireframe", isWireframe);

            GL.PolygonMode(MaterialFace.FrontAndBack, this.isWireframe ? PolygonMode.Line : PolygonMode.Fill);
        }
    }

    private void framebuffer_size_callback(int width, int height) {
        GL.Viewport(0, 0, width, height);
    }

    private static void Main(string[] args) {
        GameWindowSettings gws = GameWindowSettings.Default;

        NativeWindowSettings nws = NativeWindowSettings.Default;
        nws.ClientSize = (1024, 768);
        nws.Title = "Game";

        new RubyDung(gws, nws).Run();
    }
}
