using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace RubyDung.src {
    internal class Camera {
        public void use(Shader shader) {
            Matrix4 trans = Matrix4.Identity;
            trans = Matrix4.CreateTranslation(0.5f, -0.5f, 0.0f) * trans;
            trans = Matrix4.CreateRotationZ((float)GLFW.GetTime()) * trans;

            int transformLoc = GL.GetUniformLocation(shader.ID, "transform");
            GL.UniformMatrix4(transformLoc, false, ref trans);
        }
    }
}
