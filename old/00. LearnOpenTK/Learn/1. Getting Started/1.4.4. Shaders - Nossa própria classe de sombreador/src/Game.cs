using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System.Reflection.Metadata;

namespace LearnOpenTK.src {
    internal class Game : GameWindow {
        float[] vertices = {
            -0.5f, -0.5f, 0.0f,  // bottom left
            -0.5f,  0.5f, 0.0f,  // top left 
             0.5f,  0.5f, 0.0f,  // top right
             0.5f, -0.5f, 0.0f   // bottom right
        };

        int[] indices = {  // note that we start from 0!
            0, 1, 2,   // first triangle
            0, 2, 3    // second triangle
        };

        int VAO; // Vertex Array Object
        int VBO; // Vertex Buffer Object
        int EBO; // Element Buffer Object

        //int shaderProgram;
        //Shader ourShader = new Shader("shader.vert", "shader.frag");
        Shader ourShader;

        public Game(int width, int height, string title) 
            : base(GameWindowSettings.Default, new NativeWindowSettings() {
                Size = (width, height),
                Title = title
            }) {
            //Title = "Game";
            //CenterWindow(new Vector2i(width, height));
            CenterWindow();
        }

        protected override void OnLoad() {
            base.OnLoad();

            GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);

            /*
            // ..:: Shader ::..

            // Vertex Shader

            string vertexPath = "../../../src/Shaders/shader.vert";

            string vertexShaderSource = File.ReadAllText(vertexPath);

            int vertexShader;
            vertexShader = GL.CreateShader(ShaderType.VertexShader);

            GL.ShaderSource(vertexShader, vertexShaderSource);
            GL.CompileShader(vertexShader);

            // Fragment Shader

            string fragmentPath = "../../../src/Shaders/shader.frag";

            string fragmentShaderSource = File.ReadAllText(fragmentPath);

            int fragmentShader;
            fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource (fragmentShader, fragmentShaderSource);
            GL.CompileShader(fragmentShader);

            // Shader Program

            shaderProgram = GL.CreateProgram();

            GL.AttachShader(shaderProgram, vertexShader);
            GL.AttachShader(shaderProgram, fragmentShader);
            GL.LinkProgram(shaderProgram);

            GL.DeleteShader(vertexShader);
            GL.DeleteShader(fragmentShader);
            */

            ourShader = new Shader("shader.vert", "shader.frag");

            // ..:: Vertex Array Object ::..

            VAO = GL.GenVertexArray();
            GL.BindVertexArray(VAO);

            // ..:: Vertex Buffer Object ::..

            VBO = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, VBO);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

            // ..:: Element Buffer Object ::..

            EBO = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, EBO);
            GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(float), indices, BufferUsageHint.StaticDraw);

            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);
        }

        protected override void OnFramebufferResize(FramebufferResizeEventArgs e) {
            base.OnFramebufferResize(e);

            GL.Viewport(0, 0, e.Width, e.Height);
        }

        protected override void OnUpdateFrame(FrameEventArgs args) {
            base.OnUpdateFrame(args);

            if(KeyboardState.IsKeyDown(Keys.Escape)) {
                Close();
            }

            // ..:: Wireframe Mode ::..

            if(KeyboardState.IsKeyPressed(Keys.Up)) {
                GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);
            }
            if(KeyboardState.IsKeyPressed(Keys.Down)) {
                GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);
            }
        }

        protected override void OnRenderFrame(FrameEventArgs args) {
            base.OnRenderFrame(args);

            GL.Clear(ClearBufferMask.ColorBufferBit);

            // ..:: Código de desenho (em loop de renderização) :: ..
            // 4. desenhe o objeto
            //GL.UseProgram(shaderProgram);
            ourShader.use();
            GL.BindVertexArray(VAO);
            //GL.DrawArrays(PrimitiveType.Triangles, 0, 3);
            GL.DrawElements(PrimitiveType.Triangles, indices.Length, DrawElementsType.UnsignedInt, 0);
            GL.BindVertexArray(0);

            SwapBuffers();
        }

        static void Main(string[] args) {
            new Game(800, 600, "Game").Run();
        }
    }
}
