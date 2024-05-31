using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using RubyDung.src.level;

namespace RubyDung.src {
    internal class RubyDung : GameWindow {
        private int width;
        private int height;

        Tesselator t = new Tesselator();
        Tile tile = new Tile();

        Shader shader;
        Texture texture;

        Camera camera = new Camera();

        public RubyDung(int width, int height, string title)
            : base(GameWindowSettings.Default, new NativeWindowSettings() {
                ClientSize = (width, height),
                Title = title
            }){
            this.width = width;
            this.height = height;

            CenterWindow();
        }

        protected override void OnLoad() {
            base.OnLoad();

            GL.ClearColor(0.5f, 0.8f, 1.0f, 0.0f);

            this.texture = new Texture();
            this.shader = new Shader("shader.vert", "shader.frag");
            this.tile.render(this.t);
            this.t.flush();

            this.camera.zBuffer();
            //this.camera.cursor_state(CursorState);
            CursorState = CursorState.Grabbed;
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

            Wireframe mode = new Wireframe(input);

            this.camera.processInput(input);
            this.camera.mouse_callback(MouseState.X, MouseState.Y);
        }

        protected override void OnMouseWheel(MouseWheelEventArgs e) {
            base.OnMouseWheel(e);

            this.camera.scrool_callback(e.OffsetX, e.OffsetY);
        }

        protected override void OnRenderFrame(FrameEventArgs args) {
            base.OnRenderFrame(args);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            this.camera.use(this.shader, this.width, this.height);

            this.texture.use();
            this.shader.use();
            this.t.use(this.shader);

            SwapBuffers();
        }

        static void Main(string[] args) {
            new RubyDung(1024, 768, "Game").Run();
        }
    }
}
