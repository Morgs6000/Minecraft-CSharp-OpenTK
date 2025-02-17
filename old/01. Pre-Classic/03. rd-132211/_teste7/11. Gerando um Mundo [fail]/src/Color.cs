using OpenTK.Mathematics;

namespace RubyDung.src;

public class Color {
    public static Color4 RGBA(int red, int green, int blue, int alpha) {
        float fr = (float)red / 255;
        float fg = (float)green / 255;
        float fb = (float)blue / 255;
        float fa = (float)alpha / 255;

        return new Color4(alpha, red, green, blue);
    }

    public static Color4 Hex(string hex, int alpha) {
        int r = Convert.ToInt32(hex.Substring(0, 2), 16);
        int g = Convert.ToInt32(hex.Substring(2, 2), 16);
        int b = Convert.ToInt32(hex.Substring(4, 2), 16);

        return RGBA(r, g, b, alpha);
    }
}
