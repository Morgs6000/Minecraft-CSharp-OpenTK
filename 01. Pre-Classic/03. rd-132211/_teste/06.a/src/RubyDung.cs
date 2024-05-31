using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using RubyDung.src.level;

namespace RubyDung.src {
    public class RubyDung : GameWindow {
        private int width;
        private int height;
        private Camera camera;

        Tesselator t = new Tesselator();
        //Tile tile = new Tile();

        Shader shader;
        Texture texture;

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

            //this.tile.render(this.t);
            Tile.tile.render(this.t);

            this.t.flush();

            this.camera = new Camera();
            //this.camera.cursor_state();
            //CursorState = CursorState.Grabbed;
            this.camera.zBuffer();
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
            //this.camera.mouse_callback(MouseState.X, MouseState.Y);
            this.camera.arrow_callback(input);
        }

        protected override void OnRenderFrame(FrameEventArgs args) {
            base.OnRenderFrame(args);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            this.texture.use();
            this.shader.use();
            this.t.use();

            this.camera.use(this.shader, this.width, this.height);
            this.camera.time();

            SwapBuffers();
        }

        static void Main(string[] args) {
            new RubyDung(1024, 768, "Game").Run();
        }
    }
}
