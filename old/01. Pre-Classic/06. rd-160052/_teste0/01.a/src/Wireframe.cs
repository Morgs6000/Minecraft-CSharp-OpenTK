using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace RubyDung.src;

public class Wireframe {
    private static bool isWireframe = false;

    public static void Mode(KeyboardState input, Shader shader) {
        if(input.IsKeyDown(Keys.F3) && input.IsKeyPressed(Keys.W)) {
            isWireframe = !isWireframe;

            shader.setBool("isWireframe", isWireframe);

            GL.PolygonMode(MaterialFace.FrontAndBack, isWireframe ? PolygonMode.Line : PolygonMode.Fill);
        }
    }
}
