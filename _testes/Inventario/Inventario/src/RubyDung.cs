using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace Inventario.src {
    internal class RubyDung : GameWindow {
        private float[] vertices = {
            -0.5f, -0.5f,  0.0f,
            -0.5f,  0.5f,  0.0f,
             0.5f,  0.5f,  0.0f,
             0.5f, -0.5f,  0.0f,
        };

        private int[] indices = { 
            0, 1, 2,
            0, 2, 3,
        };

        private float[] texCoord = {
            0.0f, 0.0f,
            0.0f, 1.0f,
            1.0f, 1.0f,
            1.0f, 0.0f,
        };

        public RubyDung(int width, int height, string title)
            : base(GameWindowSettings.Default, new NativeWindowSettings() {
                ClientSize = (width, height),
                Title = title
            }) {
            CenterWindow();
        }

        protected override void OnFramebufferResize(FramebufferResizeEventArgs e) {
            base.OnFramebufferResize(e);

            GL.Viewport(0, 0, e.Width, e.Height);
        }

        protected override void OnLoad() {
            base.OnLoad();

            GL.ClearColor(0.5f, 0.8f, 1.0f, 0.0f);
        }

        protected override void OnUpdateFrame(FrameEventArgs args) {
            base.OnUpdateFrame(args);
        }

        protected override void OnRenderFrame(FrameEventArgs args) {
            base.OnRenderFrame(args);

            GL.Clear(ClearBufferMask.ColorBufferBit);

            SwapBuffers();
        }

        public static void Main(string[] args) {
            new RubyDung(1024, 768, "Game").Run();
        }
    }
}
