using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System.Reflection.Metadata;

namespace LearnOpenTK.src {
    internal class Game : GameWindow {
        Shader shader;
        Tile tile = new Tile();
        Tesselator t = new Tesselator();

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

            shader = new Shader("shader.vert", "shader.frag");
            tile.Render(t);
            t.Flush();
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
            shader.Use();
            t.Use();
            //GL.DrawArrays(PrimitiveType.Triangles, 0, 3);
            t.Render();
            GL.BindVertexArray(0);

            SwapBuffers();
        }

        static void Main(string[] args) {
            new Game(800, 600, "Game").Run();
        }
    }
}
