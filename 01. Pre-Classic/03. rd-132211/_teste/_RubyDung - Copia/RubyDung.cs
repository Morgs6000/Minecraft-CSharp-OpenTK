using OpenTK.Mathematics;
using OpenTK.Windowing.Desktop;

namespace RubyDung {
    internal class RubyDung : GameWindow {
        public RubyDung()
            : base(GameWindowSettings.Default, NativeWindowSettings.Default) {
            Title = "Game";
            CenterWindow(new Vector2i(1024, 768));
        }

        static void Main() {
            new RubyDung().Run();
        }
    }
}
