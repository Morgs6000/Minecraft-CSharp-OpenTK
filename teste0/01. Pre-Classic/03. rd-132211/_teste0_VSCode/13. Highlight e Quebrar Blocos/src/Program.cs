using OpenTK.Mathematics;
using OpenTK.Windowing.Desktop;

namespace RubyDung.src;

public class Program {
    private static void Main(string[] args) {
        Console.WriteLine("Hello, World!");

        GameWindowSettings gws = GameWindowSettings.Default;

        NativeWindowSettings nws = NativeWindowSettings.Default;
        nws.ClientSize = new Vector2i(1024, 768);
        nws.Title = "Game";

        new Game(gws, nws).Run();
    }
}
