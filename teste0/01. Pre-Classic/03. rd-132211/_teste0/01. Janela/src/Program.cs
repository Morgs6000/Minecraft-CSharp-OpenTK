using OpenTK.Windowing.Desktop;

namespace RubyDung.src;

public class Program {
    private static void Main(string[] args) {
        GameWindowSettings gws = GameWindowSettings.Default;

        NativeWindowSettings nws = NativeWindowSettings.Default;
        nws.ClientSize = (1024, 768);
        nws.Title = "Game";

        new Window(gws, nws).Run();
    }
}
