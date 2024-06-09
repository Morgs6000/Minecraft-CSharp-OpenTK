using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace RubyDung.src {
    public class RubyDung : GameWindow {
        private Shader shader = new Shader();
        //private DrawTriangle drawTriangle = new DrawTriangle();
        private DrawSquare drawSquare = new DrawSquare();
        private Wireframe wireframe = new Wireframe();

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

        protected override void OnUpdateFrame(FrameEventArgs args) {
            base.OnUpdateFrame(args);

            KeyboardState input = KeyboardState;

            if(input.IsKeyDown(Keys.Escape)) {
                Close();
            }

            this.wireframe.mode(input);
        }

        protected override void OnLoad() {
            base.OnLoad();

            GL.ClearColor(0.5f, 0.8f, 1.0f, 0.0f);

            this.shader.loadShader();
            //this.drawTriangle.loadTriangle();
            this.drawSquare.loadSquare();
        }

        protected override void OnRenderFrame(FrameEventArgs args) {
            base.OnRenderFrame(args);

            GL.Clear(ClearBufferMask.ColorBufferBit);

            this.shader.use();
            //this.drawTriangle.bind();
            this.drawSquare.bind();

            SwapBuffers();
        }

        static void Main(string[] args) {
            new RubyDung(1024, 768, "Game").Run();
        }
    }
}
