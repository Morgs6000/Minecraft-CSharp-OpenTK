using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace LearnOpenTK.src {
    internal class Camera {
        Vector3 cameraPos = new Vector3(0.0f, 0.0f, 0.3f);
        Vector3 cameraFront = new Vector3(0.0f, 0.0f, -1.0f);
        Vector3 cameraUp = new Vector3(0.0f, 1.0f, 0.0f);

        float deltaTime = 0.0f;
        float lastFrame = 0.0f;

        bool firstMouse;

        float lastX = 400.0f;
        float lastY = 300.0f;

        float yaw = -90.0f;
        float pitch;

        float fov = 45.0f;

        public void uma_funcao_a_e(Shader ourShader) {
            Vector3 cameraTarget = new Vector3(0.0f, 0.0f, 0.0f);
            Vector3 cameraDirection = Vector3.Normalize(cameraPos - cameraTarget);

            Vector3 up = new Vector3(0.0f, 1.0f, 0.0f);
            Vector3 cameraRight = Vector3.Normalize(Vector3.Cross(up, cameraDirection));

            //Vector3 cameraUp = Vector3.Cross(cameraDirection, cameraRight);

            float radius = 10.0f;
            float camX = (float)Math.Sin((float)GLFW.GetTime()) * radius;
            float camZ = (float)Math.Cos((float)GLFW.GetTime()) * radius;
            Matrix4 view;
            view = Matrix4.LookAt(cameraPos, cameraPos + cameraFront, cameraUp);

            float currentFrame = (float)GLFW.GetTime();
            deltaTime = currentFrame - lastFrame;
            lastFrame = currentFrame;

            Matrix4 projection;
            projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(fov), 800.0f / 600.0f, 0.1f, 100.0f);

            int viewLoc = GL.GetUniformLocation(ourShader.ID, "view");
            GL.UniformMatrix4(viewLoc, false, ref view);

            int projectionLoc = GL.GetUniformLocation(ourShader.ID, "projection");
            GL.UniformMatrix4(projectionLoc, false, ref projection);
        }

        public void processInput(KeyboardState input) {
            float cameraSpeed = 2.5f * deltaTime;

            if(input.IsKeyDown(Keys.W)) {
                //cameraPos += cameraSpeed * cameraFront;
                cameraPos += Vector3.Normalize(new Vector3(cameraFront.X, 0, cameraFront.Z)) * cameraSpeed;
            }
            if(input.IsKeyDown(Keys.S)) {
                //cameraPos -= cameraSpeed * cameraFront;
                cameraPos -= Vector3.Normalize(new Vector3(cameraFront.X, 0, cameraFront.Z)) * cameraSpeed;
            }
            if(input.IsKeyDown(Keys.A)) {
                cameraPos -= Vector3.Normalize(Vector3.Cross(cameraFront, cameraUp)) * cameraSpeed;
            }
            if(input.IsKeyDown(Keys.D)) {
                cameraPos += Vector3.Normalize(Vector3.Cross(cameraFront, cameraUp)) * cameraSpeed;
            }

            if(input.IsKeyDown(Keys.Space)) {
                cameraPos += cameraUp * cameraSpeed;
            }
            if(input.IsKeyDown(Keys.LeftShift)) {
                cameraPos -= cameraUp * cameraSpeed;
            }
        }

        public void mouse_callback(float xpos, float ypos) {
            if(firstMouse) {
                lastX = xpos;
                lastY = ypos;
                firstMouse = false;
            }

            float xoffset = xpos - lastX;
            float yoffset = ypos - lastY;
            lastX = xpos;
            lastY = ypos;

            float sensitivity = 0.1f;
            xoffset *= sensitivity;
            yoffset *= sensitivity;

            yaw += xoffset;
            pitch -= yoffset;

            if(pitch > 89.0f) {
                pitch = 89.0f;
            }
            if(pitch < -89.0f) {
                pitch = -89.0f;
            }

            Vector3 direction;
            direction.X = (float)Math.Cos(MathHelper.DegreesToRadians(yaw)) * (float)Math.Cos(MathHelper.DegreesToRadians(pitch));
            direction.Y = (float)Math.Sin(MathHelper.DegreesToRadians(pitch));
            direction.Z = (float)Math.Sin(MathHelper.DegreesToRadians(yaw)) * (float)Math.Cos(MathHelper.DegreesToRadians(pitch));
            cameraFront = Vector3.Normalize(direction);
        }

        public void scrool_callback(double xoffset, double yoffset) {
            fov -= (float)yoffset;

            if(fov < 1.0f) {
                fov = 1.0f;
            }
            if(fov > 45.0f) {
                fov = 45.0f;
            }
        }
    }
}
