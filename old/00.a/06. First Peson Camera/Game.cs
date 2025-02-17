using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using StbImageSharp;

namespace openTk_Minecraft_Clone_Tutorial_Series {
    // Game class that inherets from the Game Window Class
    internal class Game : GameWindow {
        List<Vector3> vertices = new List<Vector3>() {
            // front face
            new Vector3(-0.5f,  0.5f,  0.5f), // topleft vert
            new Vector3( 0.5f,  0.5f,  0.5f), // topright vert
            new Vector3( 0.5f, -0.5f,  0.5f), // bottomright vert
            new Vector3(-0.5f, -0.5f,  0.5f), // bottomleft vert
            // right face
            new Vector3( 0.5f,  0.5f,  0.5f), // topleft vert
            new Vector3( 0.5f,  0.5f, -0.5f), // topright vert
            new Vector3( 0.5f, -0.5f, -0.5f), // bottomright vert
            new Vector3( 0.5f, -0.5f,  0.5f), // bottomleft vert
            // back face
            new Vector3( 0.5f,  0.5f, -0.5f), // topleft vert
            new Vector3(-0.5f,  0.5f, -0.5f), // topright vert
            new Vector3(-0.5f, -0.5f, -0.5f), // bottomright vert
            new Vector3( 0.5f, -0.5f, -0.5f), // bottomleft vert
            // left face
            new Vector3(-0.5f,  0.5f, -0.5f), // topleft vert
            new Vector3(-0.5f,  0.5f,  0.5f), // topright vert
            new Vector3(-0.5f, -0.5f,  0.5f), // bottomright vert
            new Vector3(-0.5f, -0.5f, -0.5f), // bottomleft vert
            // top face
            new Vector3(-0.5f,  0.5f, -0.5f), // topleft vert
            new Vector3( 0.5f,  0.5f, -0.5f), // topright vert
            new Vector3( 0.5f,  0.5f,  0.5f), // bottomright vert
            new Vector3(-0.5f,  0.5f,  0.5f), // bottomleft vert
            // bottom face
            new Vector3(-0.5f, -0.5f,  0.5f), // topleft vert
            new Vector3( 0.5f, -0.5f,  0.5f), // topright vert
            new Vector3( 0.5f, -0.5f, -0.5f), // bottomright vert
            new Vector3(-0.5f, -0.5f, -0.5f)  // bottomleft vert
        };

        List<Vector2> texCoords = new List<Vector2>() {
            new Vector2(0.0f, 1.0f),
            new Vector2(1.0f, 1.0f),
            new Vector2(1.0f, 0.0f),
            new Vector2(0.0f, 0.0f),

            new Vector2(0.0f, 1.0f),
            new Vector2(1.0f, 1.0f),
            new Vector2(1.0f, 0.0f),
            new Vector2(0.0f, 0.0f),

            new Vector2(0.0f, 1.0f),
            new Vector2(1.0f, 1.0f),
            new Vector2(1.0f, 0.0f),
            new Vector2(0.0f, 0.0f),

            new Vector2(0.0f, 1.0f),
            new Vector2(1.0f, 1.0f),
            new Vector2(1.0f, 0.0f),
            new Vector2(0.0f, 0.0f),

            new Vector2(0.0f, 1.0f),
            new Vector2(1.0f, 1.0f),
            new Vector2(1.0f, 0.0f),
            new Vector2(0.0f, 0.0f),

            new Vector2(0.0f, 1.0f),
            new Vector2(1.0f, 1.0f),
            new Vector2(1.0f, 0.0f),
            new Vector2(0.0f, 0.0f)
        };

        uint[] indices = {
            // first face
            // top triangle
            0, 1, 2,
            // bottom triangle
            2, 3, 0,

            4, 5, 6,
            6, 7, 4,

            8, 9, 10,
            10, 11, 8,

            12, 13, 14,
            14, 15, 12,

            16, 17, 18,
            18, 19, 16,

            20, 21, 22,
            22, 23, 20
        };

        // Render Pipeline vars
        int vao;
        int shaderProgram;
        int textureVBO;
        int ebo;
        int textureID;

        // camera
        Camera camera;

        // transformation variables
        float yRot = 0.0f;

        // width and height of screen
        int width;
        int height;

        // Constructor that sets the width, height, and calls the base constructor (GameWindow's Constructor) with default args
        public Game(int width, int height) : base(GameWindowSettings.Default, NativeWindowSettings.Default) {
            this.width = width;
            this.height = height;

            // center window
            CenterWindow(new Vector2i(width, height));
        }

        protected override void OnResize(ResizeEventArgs e) {
            base.OnResize(e);

            GL.Viewport(0, 0, e.Width, e.Height);

            this.width = e.Width;
            this.height = e.Height;
        }

        protected override void OnLoad() {
            base.OnLoad();

            vao = GL.GenVertexArray();

            // bind the vao
            GL.BindVertexArray(vao);

            // --- Vertices VBO ---

            int vbo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Count * Vector3.SizeInBytes, vertices.ToArray(), BufferUsageHint.StaticDraw);

            // put the vertex VBO in slot 0 of our VAO

            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 0, 0);
            GL.EnableVertexArrayAttrib(vao, 0);

            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

            // --- Texture VBO ---

            textureVBO = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, textureVBO);
            GL.BufferData(BufferTarget.ArrayBuffer, texCoords.Count * Vector2.SizeInBytes, texCoords.ToArray(), BufferUsageHint.StaticDraw);

            // put the texture VBO in slot 1 of our VAO

            GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 0, 0);
            GL.EnableVertexArrayAttrib(vao, 1);

            GL.BindBuffer(BufferTarget.ArrayBuffer, 0); // unbinding the vbo

            GL.BindVertexArray(0);

            ebo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ebo);
            GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, BufferUsageHint.StaticDraw);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);

            // create the shader program
            shaderProgram = GL.CreateProgram();

            int vertexShader = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(vertexShader, LoadShaderSource("Default.vert"));
            GL.CompileShader(vertexShader);

            int fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fragmentShader, LoadShaderSource("Default.frag"));
            GL.CompileShader(fragmentShader);

            GL.AttachShader(shaderProgram, vertexShader);
            GL.AttachShader(shaderProgram, fragmentShader);

            GL.LinkProgram(shaderProgram);

            // delete the shaders
            GL.DeleteShader(vertexShader);
            GL.DeleteShader(fragmentShader);

            // --- TEXTURES ---
            textureID = GL.GenTexture();
            // activate the texture in the unit
            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2D, textureID);

            // texture parameters
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMagFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);

            // load image
            StbImage.stbi_set_flip_vertically_on_load(1);
            ImageResult dirtTexture = ImageResult.FromStream(File.OpenRead("../../../Textures/dirt.png"), ColorComponents.RedGreenBlueAlpha);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, dirtTexture.Width, dirtTexture.Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, dirtTexture.Data);
            // unbind the texture
            GL.BindTexture(TextureTarget.Texture2D, 0);

            GL.Enable(EnableCap.DepthTest);

            camera = new Camera(width, height, Vector3.Zero);
            CursorState = CursorState.Grabbed;
        }

        protected override void OnUnload() {
            base.OnUnload();

            GL.DeleteVertexArray(vao);
            GL.DeleteBuffer(ebo);
            GL.DeleteTexture(textureID);
            GL.DeleteProgram(shaderProgram);
        }

        protected override void OnRenderFrame(FrameEventArgs args) {
            base.OnRenderFrame(args);

            GL.ClearColor(0.3f, 0.3f, 1.0f, 1.0f);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            // draw our triangle
            GL.UseProgram(shaderProgram);

            GL.BindTexture(TextureTarget.Texture2D, textureID);

            // transformation matrices
            Matrix4 model = Matrix4.Identity;
            Matrix4 view = camera.GetViewMatrix();
            Matrix4 projection = camera.GetProjectionMatrix();

            model = Matrix4.CreateRotationY(yRot);
            yRot += 0.001f;
                
            Matrix4 translation = Matrix4.CreateTranslation(0.0f, 0.0f, -3.0f);

            model *= translation;

            int modelLocation = GL.GetUniformLocation(shaderProgram, "model");
            int viewLocation = GL.GetUniformLocation(shaderProgram, "view");
            int projectionLocation = GL.GetUniformLocation(shaderProgram, "projection");

            GL.UniformMatrix4(modelLocation, true, ref model);
            GL.UniformMatrix4(viewLocation, true, ref view);
            GL.UniformMatrix4(projectionLocation, true, ref projection);

            GL.BindVertexArray(vao);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ebo);
            //GL.DrawArrays(PrimitiveType.Triangles, 0, 3);
            GL.DrawElements(PrimitiveType.Triangles, indices.Length, DrawElementsType.UnsignedInt, 0);

            Context.SwapBuffers();
        }

        protected override void OnUpdateFrame(FrameEventArgs args) {
            base.OnUpdateFrame(args);

            MouseState mouse = MouseState;
            KeyboardState input = KeyboardState;

            camera.Update(input, mouse, args);
        }

        // Function to load a text file and return its contents as a string
        public static string LoadShaderSource(string filePath) {
            string shaderSource = "";

            try {
                using(StreamReader reader = new StreamReader("../../../Shaders/" + filePath)) {
                    shaderSource = reader.ReadToEnd();
                }
            }
            catch(Exception e) {
                Console.WriteLine("Failed to load shader source file: " + e.Message);
            }

            return shaderSource;
        }
    }
}
