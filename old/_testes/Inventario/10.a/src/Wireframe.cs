using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace RubyDung.src;

public class Wireframe {
    private PolygonMode polygon;

    public void mode(KeyboardState input) {
        if(input.IsKeyPressed(Keys.PageUp)) {
            this.polygon = PolygonMode.Line;
        }
        if(input.IsKeyPressed(Keys.PageDown)) {
            this.polygon = PolygonMode.Fill;
        }

        GL.PolygonMode(MaterialFace.FrontAndBack, this.polygon);
    }
}
