using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace LearnOpenTK.src {
    internal class Game : GameWindow {
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
        }

        protected override void OnRenderFrame(FrameEventArgs args) {
            base.OnRenderFrame(args);

            GL.Clear(ClearBufferMask.ColorBufferBit);

            SwapBuffers();
        }

        static void Main(string[] args) {
            new Game(800, 600, "Game").Run();
        }
    }
}
