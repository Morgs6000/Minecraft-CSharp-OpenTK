using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace LearnOpenTK {
    internal class Game : GameWindow {
        public Game(int width, int height, string title)
            : base(GameWindowSettings.Default, new NativeWindowSettings() {
                Size = (width, height),
                Title = title
            }) {

            CenterWindow();
        }

        protected override void OnUpdateFrame(FrameEventArgs args) {
            base.OnUpdateFrame(args);

            if(KeyboardState.IsKeyDown(Keys.Escape)) {
                Console.WriteLine("A tecla Escape foi pressionada");
                Close();
            }
        }
    }
}
