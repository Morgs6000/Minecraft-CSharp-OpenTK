using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Common.Input;
using OpenTK.Windowing.Desktop;
using StbImageSharp;

namespace RubyDung;

public class Program {
    private static void Main(string[] args) {
        Console.WriteLine("Hello, World!");

        GameWindowSettings gws = GameWindowSettings.Default;

        NativeWindowSettings nws = NativeWindowSettings.Default;
        nws.ClientSize = new Vector2i(1024, 768);
        nws.Title = "Game";

        var stream = File.OpenRead("src/textures/openTK.png");
        var image = ImageResult.FromStream(stream, ColorComponents.RedGreenBlueAlpha);
        var icon = new WindowIcon(new Image(image.Width, image.Height, image.Data));
        nws.Icon = icon;

        nws.WindowState = WindowState.Fullscreen;

        new Game(gws, nws).Run();
    }
}
