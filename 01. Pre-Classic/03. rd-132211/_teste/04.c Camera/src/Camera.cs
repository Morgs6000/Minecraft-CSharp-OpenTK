using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace RubyDung.src {
    internal class Camera {
        Vector3 cameraPos = new Vector3(0.0f, 0.0f, 0.3f);
        Vector3 cameraFront = new Vector3(0.0f, 0.0f, -1.0f);
        Vector3 cameraUp = new Vector3(0.0f, 1.0f, 0.0f);

        float deltaTime = 0.0f;
        float lastFrame = 0.0f;

        float yaw = -90.0f;
        float pitch = 0.0f;

        float lastX = 400.0f;
        float lastY = 300.0f;

        bool firstMouse = true;

        float fov = 45.0f;

        public void use(Shader shader, int width, int height) {
            /*
            Vector3 cameraTarget = new Vector3(0.0f, 0.0f, 0.0f);
            Vector3 cameraDirection = Vector3.Normalize(this.cameraPos - cameraTarget);

            Vector3 up = new Vector3(0.0f, 1.0f, 0.0f);
            Vector3 cameraRight = Vector3.Normalize(Vector3.Cross(up, cameraDirection));

            //Vector3 cameraUp = Vector3.Cross(cameraDirection, cameraRight);

            float radius = 10.0f;
            float camX = MathF.Sin((float)GLFW.GetTime()) * radius;
            float camZ = MathF.Cos((float)GLFW.GetTime()) * radius;
            */

            Matrix4 view;
            view = Matrix4.LookAt(this.cameraPos, this.cameraPos + this.cameraFront, this.cameraUp);

            // ..::::..

            //Matrix4 model = Matrix4.Identity;
            //model = Matrix4.CreateRotationX(MathHelper.DegreesToRadians((float)GLFW.GetTime() * 50.0f));

            //Matrix4 view = Matrix4.Identity;
            //view = Matrix4.CreateTranslation(0.0f, 0.0f, -3.0f);

            //Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45.0f), width / height, 0.1f, 100.0f);
            Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(this.fov), width / height, 0.1f, 100.0f);

            //int modelLoc = GL.GetUniformLocation(shader.ID, "model");
            //GL.UniformMatrix4(modelLoc, false, ref model);

            int viewLoc = GL.GetUniformLocation(shader.ID, "view");
            GL.UniformMatrix4(viewLoc, false, ref view);

            int projectionLoc = GL.GetUniformLocation(shader.ID, "projection");
            GL.UniformMatrix4(projectionLoc, false, ref projection);

            // ..::::..

            float currentFrame = (float)GLFW.GetTime();
            deltaTime = currentFrame - lastFrame;
            lastFrame = currentFrame;
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

        public void processInput(KeyboardState input) {
            float cameraSpeed = 4.317f * this.deltaTime;

            if(input.IsKeyDown(Keys.W)) {
                this.cameraPos += Vector3.Normalize(new Vector3(this.cameraFront.X, 0, this.cameraFront.Z)) * cameraSpeed;
            }
            if(input.IsKeyDown(Keys.S)) {
                this.cameraPos -= Vector3.Normalize(new Vector3(this.cameraFront.X, 0, this.cameraFront.Z)) * cameraSpeed;
            }
            if(input.IsKeyDown(Keys.A)) {
                this.cameraPos -= Vector3.Normalize(Vector3.Cross(this.cameraFront, this.cameraUp)) * cameraSpeed;
            }
            if(input.IsKeyDown(Keys.D)) {
                this.cameraPos += Vector3.Normalize(Vector3.Cross(this.cameraFront, this.cameraUp)) * cameraSpeed;
            }

            if(input.IsKeyDown(Keys.Space)) {
                this.cameraPos += this.cameraUp * cameraSpeed;
            }
            if(input.IsKeyDown(Keys.LeftShift)) {
                this.cameraPos -= this.cameraUp * cameraSpeed;
            }
        }

        public void mouse_callback(double xpos, double ypos) {
            if(this.firstMouse) {
                this.lastX = (float)xpos;
                this.lastY = (float)ypos;
                this.firstMouse = false;
            }

            float xoffset = (float)xpos - this.lastX;
            float yoffset = (float)ypos - this.lastY;
            this.lastX = (float)xpos;
            this.lastY = (float)ypos;

            float sensitivity = 0.1f;
            xoffset *= sensitivity;
            yoffset *= sensitivity;

            this.yaw += xoffset;
            this.pitch -= yoffset;

            if(this.pitch > 89.0f) {
                this.pitch = 89.0f;
            }
            if(this.pitch < -89.0f) {
                this.pitch = -89.0f;
            }

            Vector3 direction;
            direction.X = (float)Math.Cos(MathHelper.DegreesToRadians(this.yaw)) * (float)Math.Cos(MathHelper.DegreesToRadians(this.pitch));
            direction.Y = (float)Math.Sin(MathHelper.DegreesToRadians(this.pitch));
            direction.Z = (float)Math.Sin(MathHelper.DegreesToRadians(this.yaw)) * (float)Math.Cos(MathHelper.DegreesToRadians(this.pitch));
            this.cameraFront = Vector3.Normalize(direction);
        }

        /*
        public void cursor_state(CursorState cursor) {
            cursor = CursorState.Grabbed;
        }
        */

        public void scrool_callback(double xoffset, double yoffset) {
            this.fov -= (float)yoffset;

            if(this.fov < 1.0f) {
                this.fov = 1.0f;
            }
            if(this.fov > 45.0f) {
                this.fov = 45.0f;
            }
        }

        public void zBuffer() {
            GL.Enable(EnableCap.DepthTest);
        }
    }
}
