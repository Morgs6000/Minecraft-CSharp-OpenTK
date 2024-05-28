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
        float[] vertices = {
            -0.5f,  0.5f, 0.0f, // top left vertex  - 0
             0.5f,  0.5f, 0.0f, // top right vertex - 1
             0.5f, -0.5f, 0.0f  // bottom right     - 2
            -0.5f, -0.5f, 0.0f  // bottom left      - 3
        };

        uint[] indices = {
            // top triangle
            0, 1, 2,
            // bottom triangle
            2, 3, 0
        };

        // Render Pipeline vars
        int vao;
        int shaderProgram;
        int vbo;
        int ebo;

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

            vbo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 0, 0);
            GL.EnableVertexArrayAttrib(vao, 0);

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
        }

        protected override void OnUnload() {
            base.OnUnload();

            GL.DeleteVertexArray(vao);
            GL.DeleteBuffer(vbo);
            GL.DeleteBuffer(ebo);
            GL.DeleteProgram(shaderProgram);
        }

        protected override void OnRenderFrame(FrameEventArgs args) {
            base.OnRenderFrame(args);

            GL.ClearColor(0.6f, 0.3f, 1.0f, 1.0f);
            GL.Clear(ClearBufferMask.ColorBufferBit);

            // draw our triangle
            GL.UseProgram(shaderProgram);
            GL.BindVertexArray(vao);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ebo);
            //GL.DrawArrays(PrimitiveType.Triangles, 0, 3);
            GL.DrawElements(PrimitiveType.Triangles, indices.Length, DrawElementsType.UnsignedInt, 0);

            Context.SwapBuffers();
        }

        protected override void OnUpdateFrame(FrameEventArgs args) {
            base.OnUpdateFrame(args);
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
