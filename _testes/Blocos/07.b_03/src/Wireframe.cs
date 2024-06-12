using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace RubyDung.src {
    public class Wireframe {
        private PolygonMode polygon;
        private Shader shader;

        public Wireframe(Shader shader) {
            this.shader = shader;
        }

        public void mode(KeyboardState input) {
            if(input.IsKeyPressed(Keys.PageUp)) {
                this.polygon = PolygonMode.Line;
                shader.setUniform("isWireframe", true);
                shader.setUniform("wireframeColor", new Vector4(0.0f, 0.0f, 0.0f, 1.0f));
            }
            if(input.IsKeyPressed(Keys.PageDown)) {
                this.polygon = PolygonMode.Fill;
                shader.setUniform("isWireframe", false);
            }

            GL.PolygonMode(MaterialFace.FrontAndBack, this.polygon);
        }
    }
}
