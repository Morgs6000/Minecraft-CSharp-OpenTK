using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using StbImageSharp;

namespace LearnOpenTK.src {
    internal class Game : GameWindow {
        /*
        float[] vertices = {
            // positions          // colors           // texture coords
            -0.5f, -0.5f, 0.0f,   0.0f, 0.0f, 1.0f,   0.0f, 0.0f,  // bottom left
            -0.5f,  0.5f, 0.0f,   1.0f, 1.0f, 0.0f,   0.0f, 1.0f,  // top left 
             0.5f,  0.5f, 0.0f,   1.0f, 0.0f, 0.0f,   1.0f, 1.0f,  // top right
             0.5f, -0.5f, 0.0f,   0.0f, 1.0f, 0.0f,   1.0f, 0.0f   // bottom right
        };
        //*/

        //*
        float[] vertices = {
            -0.5f, -0.5f, -0.5f,  0.0f, 0.0f,
             0.5f, -0.5f, -0.5f,  1.0f, 0.0f,
             0.5f,  0.5f, -0.5f,  1.0f, 1.0f,
             0.5f,  0.5f, -0.5f,  1.0f, 1.0f,
            -0.5f,  0.5f, -0.5f,  0.0f, 1.0f,
            -0.5f, -0.5f, -0.5f,  0.0f, 0.0f,

            -0.5f, -0.5f,  0.5f,  0.0f, 0.0f,
             0.5f, -0.5f,  0.5f,  1.0f, 0.0f,
             0.5f,  0.5f,  0.5f,  1.0f, 1.0f,
             0.5f,  0.5f,  0.5f,  1.0f, 1.0f,
            -0.5f,  0.5f,  0.5f,  0.0f, 1.0f,
            -0.5f, -0.5f,  0.5f,  0.0f, 0.0f,

            -0.5f,  0.5f,  0.5f,  1.0f, 0.0f,
            -0.5f,  0.5f, -0.5f,  1.0f, 1.0f,
            -0.5f, -0.5f, -0.5f,  0.0f, 1.0f,
            -0.5f, -0.5f, -0.5f,  0.0f, 1.0f,
            -0.5f, -0.5f,  0.5f,  0.0f, 0.0f,
            -0.5f,  0.5f,  0.5f,  1.0f, 0.0f,

             0.5f,  0.5f,  0.5f,  1.0f, 0.0f,
             0.5f,  0.5f, -0.5f,  1.0f, 1.0f,
             0.5f, -0.5f, -0.5f,  0.0f, 1.0f,
             0.5f, -0.5f, -0.5f,  0.0f, 1.0f,
             0.5f, -0.5f,  0.5f,  0.0f, 0.0f,
             0.5f,  0.5f,  0.5f,  1.0f, 0.0f,

            -0.5f, -0.5f, -0.5f,  0.0f, 1.0f,
             0.5f, -0.5f, -0.5f,  1.0f, 1.0f,
             0.5f, -0.5f,  0.5f,  1.0f, 0.0f,
             0.5f, -0.5f,  0.5f,  1.0f, 0.0f,
            -0.5f, -0.5f,  0.5f,  0.0f, 0.0f,
            -0.5f, -0.5f, -0.5f,  0.0f, 1.0f,

            -0.5f,  0.5f, -0.5f,  0.0f, 1.0f,
             0.5f,  0.5f, -0.5f,  1.0f, 1.0f,
             0.5f,  0.5f,  0.5f,  1.0f, 0.0f,
             0.5f,  0.5f,  0.5f,  1.0f, 0.0f,
            -0.5f,  0.5f,  0.5f,  0.0f, 0.0f,
            -0.5f,  0.5f, -0.5f,  0.0f, 1.0f
        };  
        //*/

        int[] indices = {  // note that we start from 0!
            0, 1, 2,   // first triangle
            0, 2, 3    // second triangle
        };

        Vector3[] cubePositions = {
            new Vector3( 0.0f,  0.0f,  0.0f),
            new Vector3( 2.0f,  5.0f, -15.0f),
            new Vector3(-1.5f, -2.2f, -2.5f),
            new Vector3(-3.8f, -2.0f, -12.3f),
            new Vector3( 2.4f, -0.4f, -3.5f),
            new Vector3(-1.7f,  3.0f, -7.5f),
            new Vector3( 1.3f, -2.0f, -2.5f),
            new Vector3( 1.5f,  2.0f, -2.5f),
            new Vector3( 1.5f,  0.2f, -1.5f),
            new Vector3(-1.3f,  1.0f, -1.5f)
        };

        int VAO; // Vertex Array Object
        int VBO; // Vertex Buffer Object
        int EBO; // Element Buffer Object

        Shader ourShader;

        int texture0;
        int texture1;

        Camera camera = new Camera();

        public Game(int width, int height, string title) 
            : base(GameWindowSettings.Default, new NativeWindowSettings() {
                Size = (width, height),
                Title = title
            }) {
            CenterWindow();
        }

        protected override void OnLoad() {
            base.OnLoad();

            GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);

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

            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));
            GL.EnableVertexAttribArray(1);

            GL.VertexAttribPointer(2, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));
            GL.EnableVertexAttribArray(2);

            // ..:: Shader ::..

            ourShader = new Shader("shader.vert", "shader.frag");

            // ..:: Texture ::..

            texture0 = LeadTexture("../../../src/Textures/container.jpg");
            texture1 = LeadTexture("../../../src/Textures/awesomeface.png");

            ourShader.use();
            ourShader.setInt("texture0", 0);
            ourShader.setInt("texture1", 1);

            GL.Enable(EnableCap.DepthTest);

            CursorState = CursorState.Grabbed;
        }

        private int LeadTexture(string path) {
            int texture = GL.GenTexture();
            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2D, texture);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.LinearMipmapLinear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

            StbImage.stbi_set_flip_vertically_on_load(1);
            ImageResult image = ImageResult.FromStream(File.OpenRead(path), ColorComponents.RedGreenBlueAlpha);

            if(image.Data != null) {
                GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, image.Width, image.Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, image.Data);
                GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
            }
            else {
                Console.WriteLine("Failed to load texture");
            }

            return texture;
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

            //processInput();
            camera.processInput(KeyboardState);
            //mouse_callback(MouseState.X, MouseState.Y);
            camera.mouse_callback(MouseState.X, MouseState.Y);
        }

        protected override void OnMouseWheel(MouseWheelEventArgs e) {
            base.OnMouseWheel(e);

            camera.scrool_callback(e.OffsetX, e.OffsetY);
        }

        protected override void OnRenderFrame(FrameEventArgs args) {
            base.OnRenderFrame(args);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            /*
            Matrix4 model = Matrix4.Identity;
            model = Matrix4.CreateRotationX(MathHelper.DegreesToRadians((float)GLFW.GetTime() * 50.0f * 0.5f));
            model *= Matrix4.CreateRotationY(MathHelper.DegreesToRadians((float)GLFW.GetTime() * 50.0f * 1.0f));
            */

            /*
            Matrix4 view = Matrix4.Identity;
            view = Matrix4.CreateTranslation(0.0f, 0.0f, -3.0f);
            */

            camera.uma_funcao_a_e(ourShader);

            ourShader.use();

            /*
            int modelLoc = GL.GetUniformLocation(ourShader.ID, "model");
            GL.UniformMatrix4(modelLoc, false, ref model);
            */

            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2D, texture0);
            GL.ActiveTexture(TextureUnit.Texture1);
            GL.BindTexture(TextureTarget.Texture2D, texture1);

            GL.BindVertexArray(VAO);

            for(int i = 0; i < 10; i++) {
                Matrix4 model = Matrix4.Identity;
                model = Matrix4.CreateTranslation(cubePositions[i]);

                float angle = 20.0f * i;

                model *= Matrix4.CreateRotationX(MathHelper.DegreesToRadians(angle));
                model *= Matrix4.CreateRotationY(MathHelper.DegreesToRadians(angle * 0.3f));
                model *= Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(angle * 0.5f));

                GL.UniformMatrix4(GL.GetUniformLocation(ourShader.ID, "model"), false, ref model);

                GL.DrawArrays(PrimitiveType.Triangles, 0, 36);
            }

            //GL.DrawElements(PrimitiveType.Triangles, indices.Length, DrawElementsType.UnsignedInt, 0);

            SwapBuffers();
        }

        static void Main(string[] args) {
            new Game(800, 600, "Game").Run();
        }
    }
}
