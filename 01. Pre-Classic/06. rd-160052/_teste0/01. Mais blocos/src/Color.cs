using SystemDrawingColor = System.Drawing.Color;

namespace RubyDung.src;

public class Color {
    public static SystemDrawingColor RGBA(int red, int green, int blue, int alpha) {
        return SystemDrawingColor.FromArgb(alpha, red, green, blue);
    }

    public static SystemDrawingColor Hex(string hex, int alpha) {
        int r = Convert.ToInt32(hex.Substring(0, 2), 16);
        int g = Convert.ToInt32(hex.Substring(2, 2), 16);
        int b = Convert.ToInt32(hex.Substring(4, 2), 16);

        return RGBA(r, g, b, alpha);
    }
}
