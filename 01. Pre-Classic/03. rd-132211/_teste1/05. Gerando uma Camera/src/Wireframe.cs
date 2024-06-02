using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace RubyDung.src {
    public class Wireframe {
        PolygonMode polygon;

        public Wireframe(KeyboardState input) {
            if(input.IsKeyDown(Keys.PageUp)) {
                this.polygon = PolygonMode.Line;
            }
            if(input.IsKeyDown(Keys.PageDown)) {
                this.polygon = PolygonMode.Fill;
            }

            GL.PolygonMode(MaterialFace.FrontAndBack, this.polygon);
        }
    }
}
