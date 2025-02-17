using OpenTK.Windowing.Common.Input;
using OpenTK.Windowing.Desktop;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System.Runtime.InteropServices;
using Image = SixLabors.ImageSharp.Image;

namespace RubyDung;

public class Program {
    public static void Main(string[] args) {
        Console.WriteLine("Hello, World!");

        GameWindowSettings gws = GameWindowSettings.Default;
        NativeWindowSettings nws = NativeWindowSettings.Default;

        Image<Rgba32> image = (Image<Rgba32>)Image.Load(Configuration.Default, "../../../assets/icon.jpg");
        image.TryGetSinglePixelSpan(out Span<Rgba32> span);
        byte[] pixels = MemoryMarshal.AsBytes(span).ToArray();
        nws.Icon = new WindowIcon(new OpenTK.Windowing.Common.Input.Image(255, 255, pixels));

        GameWindow gameWindow = new GameWindow(gws, nws);
        gameWindow.Run();
    }
}