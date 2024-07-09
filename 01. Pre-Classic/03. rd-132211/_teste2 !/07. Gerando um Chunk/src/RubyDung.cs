using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace RubyDung.src
{
    public class RubyDung : GameWindow {
        private int width;
        private int height;

        private Shader shader = new Shader();
        //private DrawTriangle drawTriangle = new DrawTriangle();
        //private DrawSquare drawSquare = new DrawSquare();
        //private DrawTexture drawTexture = new DrawTexture();
        //private DrawBlock drawBlock = new DrawBlock();
        private DrawChunk drawChunk = new DrawChunk();
        private Wireframe wireframe = new Wireframe();
        private Texture texture = new Texture();
        private Camera camera = new Camera();

        public RubyDung(int width, int height, string title)
            : base(GameWindowSettings.Default, new NativeWindowSettings() {
                ClientSize = (width, height),
                Title = title
            }) {
            this.width = width;
            this.height = height;

            CenterWindow();
        }

        protected override void OnFramebufferResize(FramebufferResizeEventArgs e) {
            base.OnFramebufferResize(e);

            this.width = e.Width;
            this.height = e.Height;

            GL.Viewport(0, 0, e.Width, e.Height);
        }

        protected override void OnUpdateFrame(FrameEventArgs args) {
            base.OnUpdateFrame(args);

            KeyboardState input = KeyboardState;

            if(input.IsKeyDown(Keys.Escape)) {
                Close();
            }

            this.wireframe.mode(input);
            this.camera.processInput(input);
            this.camera.mouse_callback(MouseState.X, MouseState.Y);
        }

        protected override void OnLoad() {
            base.OnLoad();

            GL.ClearColor(0.5f, 0.8f, 1.0f, 0.0f);

            this.shader.loadShader();
            //this.drawTriangle.loadTriangle();
            //this.drawSquare.loadSquare();
            //this.drawTexture.loadSquare();
            //this.drawBlock.loadBlock();
            this.drawChunk.loadChunk();
            this.texture.loadTexture();
            this.camera.zBuffer();
            CursorState = CursorState.Grabbed;
        }

        protected override void OnRenderFrame(FrameEventArgs args) {
            base.OnRenderFrame(args);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            this.shader.use();
            //this.drawTriangle.bind();
            //this.drawSquare.bind();
            //this.drawTexture.bind();
            //this.drawBlock.bind();
            this.drawChunk.bind();
            this.texture.bind();
            this.camera.loadCamera(this.shader, this.width, this.height);

            SwapBuffers();
        }

        static void Main(string[] args) {
            new RubyDung(1024, 768, "Game").Run();
        }
    }
}
