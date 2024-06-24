using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using RubyDung.src.level;
using System.Collections.Generic;

namespace RubyDung.src;

public class RubyDung {
    // configurações
    private const int width = 1024;
    private const int height = 768;

    // camera
    private static Player player = new Player(new Vector3(0.0f, 0.0f, 3.0f));

    // tempo
    private static float deltaTime = 0.0f; // tempo entre o quadro atual e o último quadro
    private static float lastFrame = 0.0f;

    private static void Main(string[] args) {
        var gws = GameWindowSettings.Default;

        var nws = NativeWindowSettings.Default;
        nws.ClientSize = (width, height);
        nws.Title = "Game";

        // criação de janela glfw
        // --------------------
        var window = new GameWindow(gws, nws);
        window.CenterWindow();

        window.FramebufferResize += delegate (FramebufferResizeEventArgs args) {
            framebuffer_size_callback(window, width, height);
        };

        window.UpdateFrame += delegate (FrameEventArgs args) {
            //player.mouse_callback(window, window.MouseState.X, window.MouseState.Y);
            player.mouse_processInput(window);
        };

        window.MouseWheel += delegate (MouseWheelEventArgs args) {
            //player.scroll_callback(window, args.OffsetX, args.OffsetY);
        };

        // configurar o estado opengl global
        // -----------------------------
        GL.Enable(EnableCap.DepthTest);
        GL.Enable(EnableCap.CullFace);
        GL.CullFace(CullFaceMode.Front);
        window.CursorState = CursorState.Grabbed;

        // construir e compilar nosso programa shader
        // ------------------------------------
        Shader shader = new Shader("../../../src/shaders/shaderVert.glsl", "../../../src/shaders/shaderFrag.glsl");

        // configura dados de vértice (e buffer(s)) e configura atributos de vértice
        // ------------------------------------------------------------------
        Tesselator tesselator = new Tesselator();

        // carrega e cria uma textura
        // -------------------------
        Texture texture = new Texture();

        // loop de renderização
        // -----------
        window.RenderFrame += delegate (FrameEventArgs args) {
            // lógica de tempo por quadro
            // --------------------
            float currentFrame = (float)(GLFW.GetTime());
            deltaTime = currentFrame - lastFrame;
            lastFrame = currentFrame;

            // entrada
            // -----
            processInput(window);

            // renderizar
            // ------
            GL.ClearColor(0.5f, 0.8f, 1.0f, 0.0f);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            // liga a textura
            texture.use();

            // ativar shader
            shader.use();

            // passa a matriz de projeção para o shader (observe que neste caso ela poderia mudar todos os frames)
            Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(player.Zoom), (float)width / (float)height, 0.1f, 100.0f);
            shader.setMat4("projection", projection);

            // transformação de câmera/visualização            
            Matrix4 view = player.GetViewMatrix();
            shader.setMat4("view", view);

            // renderiza o triângulo
            tesselator.use();
            tesselator.wireframe_mode(window.KeyboardState, shader);

            Matrix4 model = Matrix4.CreateTranslation(new Vector3(0.0f, 0.0f, -2.0f));
            shader.setMat4("model", model);

            // glfw: troca buffers e pesquisa eventos IO (teclas pressionadas/liberadas, mouse movido etc.)
            // -------------------------------------------------------------------------------
            window.SwapBuffers();
        };

        window.Run();
    }

    // processar todas as entradas: consultar o GLFW se as teclas relevantes foram pressionadas/liberadas neste quadro e reagir de acordo
    // ---------------------------------------------------------------------------------------------------------
    private static void processInput(GameWindow window) {
        var input = window.KeyboardState;

        if(input.IsKeyDown(Keys.Escape)) {
            window.Close();
        }

        player.ProcessKeyboard(input, deltaTime);
    }

    // glfw: sempre que o tamanho da janela for alterado (por sistema operacional ou redimensionamento do usuário), esta função de retorno de chamada é executada
    // ---------------------------------------------------------------------------------------------
    private static void framebuffer_size_callback(GameWindow window, int width, int height) {
        // certifique-se de que a viewport corresponda às novas dimensões da janela; observe que a largura e a altura serão significativamente maiores do que as especificadas nas telas retina.
        GL.Viewport(0, 0, width, height);
    }
}
