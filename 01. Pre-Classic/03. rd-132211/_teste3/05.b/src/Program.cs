using LearnOpenGL.src.level;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using StbImageSharp;

namespace LearnOpenGL.src;

public class Program {
    // settings
    private const int width = 1024;
    private const int height = 768;

    private static void Main(string[] args) {
        Console.WriteLine("Hello, World!");

        var gws = GameWindowSettings.Default;

        var nws = NativeWindowSettings.Default;
        nws.ClientSize = (width, height);
        nws.Title = "LearnOpenGL";

        // criação de janela glfw
        // --------------------
        var window = new GameWindow(gws, nws);
        window.CenterWindow();

        window.FramebufferResize += delegate (FramebufferResizeEventArgs args) {
            framebuffer_size_callback(window, width, height);
        };

        // construir e compilar nosso programa shader
        // ------------------------------------
        Shader shader = new Shader("../../../src/shaders/shader_vs.glsl", "../../../src/shaders/shader_fs.glsl"); // você pode nomear seus arquivos de shader como quiser

        // configura dados de vértice (e buffer(s)) e configura atributos de vértice
        // ------------------------------------------------------------------
        Tesselator tesselator = new Tesselator();

        // carrega e cria uma textura
        // -------------------------
        Texture texture1 = new Texture("../../../src/textures/dirt.png");
        Texture texture2 = new Texture("../../../src/textures/grass_side.png");
        /*
        int texture1, texture2;
        // textura 1
        // ---------
        GL.GenTextures(1, out texture1);
        GL.BindTexture(TextureTarget.Texture2D, texture1);
        // define os parâmetros de quebra de textura
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat); // define o empacotamento de textura para GL_REPEAT (método de empacotamento padrão)
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);
        // define os parâmetros de filtragem de textura
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.LinearMipmapLinear);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
        // O FileSystem::getPath(...) faz parte do repositório GitHub para que possamos encontrar arquivos em qualquer IDE/plataforma; substitua-o pelo seu próprio caminho de imagem.
        StbImage.stbi_set_flip_vertically_on_load(1);
        ImageResult image = ImageResult.FromStream(File.OpenRead("../../../src/textures/container.jpg"), ColorComponents.RedGreenBlueAlpha);
        if (image.Data != null)
        {
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, image.Width, image.Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, image.Data);
            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
        }
        else
        {
            Console.WriteLine("Failed to load texture");
        }
        // textura 2
        // ---------
        GL.GenTextures(1, out texture2);
        GL.BindTexture(TextureTarget.Texture2D, texture2);
        // define os parâmetros de quebra de textura
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat); // define o empacotamento de textura para GL_REPEAT (método de empacotamento padrão)
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);
        // define os parâmetros de filtragem de textura
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.LinearMipmapLinear);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
        // carrega a imagem, cria textura e gera mipmaps
        StbImage.stbi_set_flip_vertically_on_load(1);
        image = ImageResult.FromStream(File.OpenRead("../../../src/textures/awesomeface.png"), ColorComponents.RedGreenBlueAlpha);
        if (image.Data != null)
        {
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, image.Width, image.Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, image.Data);
            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
        }
        else
        {
            Console.WriteLine("Failed to load texture");
        }
        */

        // diz ao opengl para cada sampler a qual unidade de textura ele pertence (só precisa ser feito uma vez)
        // -------------------------------------------------------------------------------------------
        shader.use(); // não se esqueça de ativar/usar o shader antes de definir os uniformes!
        // configure-o manualmente assim:
        GL.Uniform1(GL.GetAttribLocation(shader.ID, "texture1"), 0);
        // ou configure-o através da classe de textura
        shader.setInt("texture2", 1);

        // loop de renderização
        // -----------
        window.RenderFrame += delegate (FrameEventArgs args) {
            // entrada
            // -----
            processInput(window);

            // renderizar
            // ------
            GL.ClearColor(0.5f, 0.8f, 1.0f, 0.0f);
            GL.Clear(ClearBufferMask.ColorBufferBit);

            // vincula texturas em unidades de textura correspondentes
            texture1.Bind(TextureUnit.Texture0);
            texture2.Bind(TextureUnit.Texture1);
            /*
            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2D, texture1);
            GL.ActiveTexture(TextureUnit.Texture1);
            GL.BindTexture(TextureTarget.Texture2D, texture2);
            */

            // renderiza o contêiner
            shader.use();
            tesselator.use();

            // glfw: troca buffers e pesquisa eventos IO (teclas pressionadas/liberadas, mouse movido etc.)
            // -------------------------------------------------------------------------------
            window.SwapBuffers();
        };

        // opcional: desalocar todos os recursos assim que eles tiverem sobrevivido ao seu propósito:
        // ------------------------------------------------------------------------
        //GL.DeleteVertexArrays(1, ref VAO);
        //GL.DeleteBuffers(1, ref VBO);
        //GL.DeleteBuffers(1, ref EBO);

        window.Run();
    }

    // processar todas as entradas: consultar o GLFW se as teclas relevantes foram pressionadas/liberadas neste quadro e reagir de acordo
    // ---------------------------------------------------------------------------------------------------------
    private static void processInput(GameWindow window) {
        var input = window.KeyboardState;

        if(input.IsKeyDown(Keys.Escape)) {
            window.Close();
        }
    }

    // glfw: sempre que o tamanho da janela for alterado (por sistema operacional ou redimensionamento do usuário), esta função de retorno de chamada é executada
    // ---------------------------------------------------------------------------------------------
    private static void framebuffer_size_callback(GameWindow window, int width, int height) {
        // certifique-se de que a viewport corresponda às novas dimensões da janela; observe que a largura e a altura serão significativamente maiores do que as especificadas nas telas retina.
        GL.Viewport(0, 0, width, height);
    }
}
