using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using RubyDung.src.level;

namespace RubyDung.src {
    public class RubyDung : GameWindow {
        private int width;
        private int height;

        private Level level;
        private LevelRenderer levelRenderer;
        private Player player;

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

            GL.Viewport(0, 0, e.Width, e.Height);
        }

        protected override void OnLoad() {
            base.OnLoad();

            GL.ClearColor(0.5f, 0.8f, 1.0f, 0.0f);

            this.level = new Level(256, 64, 256);
            this.levelRenderer = new LevelRenderer(this.level);
            this.player = new Player();
            CursorState = CursorState.Grabbed;
        }

        protected override void OnUpdateFrame(FrameEventArgs args) {
            base.OnUpdateFrame(args);

            KeyboardState input = KeyboardState;

            if(KeyboardState.IsKeyDown(Keys.Escape)) {
                Close();
            }

            Wireframe wireframe = new Wireframe(input);

            this.player.turn(MouseState.X, MouseState.Y);
            this.player.tick(input);
        }

        protected override void OnRenderFrame(FrameEventArgs args) {
            base.OnRenderFrame(args);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            this.levelRenderer.render(this.player, this.width, this.height);

            SwapBuffers();
        }

        public static void Main(string[] args) {
            new RubyDung(1024, 768, "Game").Run();
        }
    }
}
