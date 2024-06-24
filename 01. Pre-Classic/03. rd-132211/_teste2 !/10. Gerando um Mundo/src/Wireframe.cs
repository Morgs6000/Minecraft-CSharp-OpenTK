using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace RubyDung.src;

public class Wireframe {
    public void mode(KeyboardState input, Shader shader) {
        if(input.IsKeyDown(Keys.PageUp)) {
            GL.Uniform1(GL.GetUniformLocation(shader.ID, "isWireframe"), 1);
            GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);
        }
        if(input.IsKeyDown(Keys.PageDown)) {
            GL.Uniform1(GL.GetUniformLocation(shader.ID, "isWireframe"), 0);
            GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);
        }
    }
}
