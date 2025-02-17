using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using RubyDung.src.level;
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
    Tesselator t = new Tesselator();

    // ..:: Texture ::..
    //Texture texture;
    private TextureAtlas textureAtlas;
    private int textureId;

    protected override void OnLoad() {
        this.shader = new Shader("vertexShader.glsl", "fragmentShader.glsl");

        //this.texture = new Texture("terrain.png");

        string texturesPath = "../../../src/textures/blocks";
        string[] textureFiles = Directory.GetFiles(texturesPath, "*.png", SearchOption.AllDirectories);
        int textureSize = 16; // Supondo que todas as texturas são de 16x16 pixels
        int atlasWidth = 256; // Largura do atlas
        int atlasHeight = 256; // Altura do atlas

        textureAtlas = new TextureAtlas(atlasWidth, atlasHeight); // Dimensões do atlas (ajuste conforme necessário)

        int x = 0, y = 0;

        foreach(var filePath in textureFiles) {
            using(var stream = File.OpenRead(filePath)) {
                var image = ImageResult.FromStream(stream, ColorComponents.RedGreenBlueAlpha);
                string textureName = Path.GetFileNameWithoutExtension(filePath);

                if(x + textureSize > atlasWidth) // Verifica se há espaço suficiente na linha atual
                {
                    x = 0;
                    y += textureSize;
                    if(y + textureSize > atlasHeight) // Verifica se há espaço suficiente na coluna
                    {
                        throw new Exception("O atlas de texturas está cheio. Não há espaço suficiente para adicionar mais texturas.");
                    }
                }

                textureAtlas.AddTexture(textureName, image.Data, image.Width, image.Height, x, y);
                x += textureSize; // Avança para a próxima posição
            }
        }

        textureId = textureAtlas.CreateTexture(); // Cria a textura do OpenGL a partir do atlas

        Tile.tile.render(this.t, textureAtlas);
        this.t.flush();
    }

    protected override void OnRenderFrame(FrameEventArgs args) {
        this.processInput();

        GL.ClearColor(0.5f, 0.8f, 1.0f, 0.0F);
        GL.Clear(ClearBufferMask.ColorBufferBit);

        //this.texture.bind();
        GL.BindTexture(TextureTarget.Texture2D, textureId);

        this.shader.use();
        this.t.render();

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
