using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace RubyDung.src {
    internal class Wireframe {
        public Wireframe(KeyboardState input) {
            if(input.IsKeyPressed(Keys.PageUp)) {
                GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);
            }
            if(input.IsKeyPressed(Keys.PageDown)) {
                GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);
            }
        }
    }
}
