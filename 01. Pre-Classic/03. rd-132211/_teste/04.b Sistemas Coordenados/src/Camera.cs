using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace RubyDung.src {
    internal class Camera {
        public void use(Shader shader, int width, int height) {
            //Matrix4 model = Matrix4.Identity;
            //model = Matrix4.CreateRotationX(MathHelper.DegreesToRadians((float)GLFW.GetTime() * 50.0f));

            Matrix4 view = Matrix4.Identity;
            view = Matrix4.CreateTranslation(0.0f, 0.0f, -3.0f);

            Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45.0f), width / height, 0.1f, 100.0f);

            //int modelLoc = GL.GetUniformLocation(shader.ID, "model");
            //GL.UniformMatrix4(modelLoc, false, ref model);

            int viewLoc = GL.GetUniformLocation(shader.ID, "view");
            GL.UniformMatrix4(viewLoc, false, ref view);

            int projectionLoc = GL.GetUniformLocation(shader.ID, "projection");
            GL.UniformMatrix4(projectionLoc, false, ref projection);
        }

        public void use2(Vector3[] cubePositions, int i, Shader shader, List<int> triangleList) {
            Matrix4 model = Matrix4.Identity;
            model = Matrix4.CreateTranslation(cubePositions[i]);

            float angle = 20.0f * i;

            model *= Matrix4.CreateRotationX(MathHelper.DegreesToRadians(angle));
            model *= Matrix4.CreateRotationY(MathHelper.DegreesToRadians(angle * 0.3f));
            model *= Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(angle * 0.5f));

            int modelLoc = GL.GetUniformLocation(shader.ID, "model");
            GL.UniformMatrix4(modelLoc, false, ref model);

            GL.DrawElements(PrimitiveType.Triangles, triangleList.Count, DrawElementsType.UnsignedInt, 0);
        }

        public void zBuffer() {
            GL.Enable(EnableCap.DepthTest);
        }
    }
}
