using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace RubyDung.src {
    public class Camera {
        Vector3 cameraPos = new Vector3(0.0f, 0.0f, 0.3f);
        Vector3 cameraFront = new Vector3(0.0f, 0.0f, -1.0f);
        Vector3 cameraUp = new Vector3(0.0f, 1.0f, 0.0f);

        float fov = 60.0f;

        float deltaTime = 0.0f;
        float lastFrame = 0.0f;

        bool firstMouse;
        bool rightMouseButtonPressed = false;
        bool leftMouseButtonPressed = false;

        float lastX = 400.0f;
        float lastY = 300.0f;
        Vector2 laspPos = (400.0f, 300.0f);

        float yaw = -90.0f;
        float pitch;

        public void loadCamera(Shader shader, int width, int height) {
            // ..:: Model ::..
            Matrix4 model = Matrix4.Identity;
            model = Matrix4.CreateTranslation(0.0f, 0.0f, -32.0f);

            int modelLoc = GL.GetUniformLocation(shader.shaderProgram, "model");
            GL.UniformMatrix4(modelLoc, false, ref model);

            // ..:: View ::..
            Matrix4 view = Matrix4.Identity;
            view = Matrix4.LookAt(this.cameraPos, this.cameraPos + this.cameraFront, this.cameraUp);

            int viewLoc = GL.GetUniformLocation(shader.shaderProgram, "view");
            GL.UniformMatrix4(viewLoc, false, ref view);

            // ..:: Projection ::..
            Matrix4 projection;
            projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(fov), (float)width / (float)height, 0.1f, 100.0f);

            int projectionLoc = GL.GetUniformLocation(shader.shaderProgram, "projection");
            GL.UniformMatrix4(projectionLoc, false, ref projection);

            this.time();
        }

        public void time() {
            float currentFrame = (float)GLFW.GetTime();
            deltaTime = currentFrame - lastFrame;
            lastFrame = currentFrame;
        }

        public void processInput(KeyboardState input) {
            float cameraSpeed = 4.317f * deltaTime;

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

        public void mouse_processInput(MouseState mouseState) {
            float scrollOffset = mouseState.ScrollDelta.Y; // Use ScrollDelta to get the scroll change
            float scrollSensitivity = 2.0f; // Adjust this value to increase or decrease scroll sensitivity
            if(scrollOffset != 0) {
                cameraPos += cameraFront * scrollOffset * scrollSensitivity;
            }

            // Right mouse button to rotate
            if(mouseState.IsButtonDown(MouseButton.Right)) {
                if(!rightMouseButtonPressed) {
                    rightMouseButtonPressed = true;
                    firstMouse = true;
                }

                mouse_callback(mouseState.X, mouseState.Y);
            }
            else {
                rightMouseButtonPressed = false;
            }

            // Left mouse button to move
            if(mouseState.IsButtonDown(MouseButton.Left) || mouseState.IsButtonDown(MouseButton.Middle)) {
                if(!leftMouseButtonPressed) {
                    leftMouseButtonPressed = true;
                    firstMouse = true;
                }

                move_camera(mouseState.X, mouseState.Y);
            }
            else {
                leftMouseButtonPressed = false;
            }
        }

        public void mouse_callback(float xpos, float ypos) {
            if(firstMouse) {
                lastX = xpos;
                lastY = ypos;
                //laspPos = new Vector2(xpos, ypos);
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

        public void move_camera(float xpos, float ypos) {
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

            // Calculate right and up vectors based on current orientation
            Vector3 right = Vector3.Normalize(Vector3.Cross(cameraFront, cameraUp));
            Vector3 up = Vector3.Normalize(Vector3.Cross(right, cameraFront));

            cameraPos -= right * xoffset;
            cameraPos += up * yoffset;
        }

        public void zBuffer() {
            GL.Enable(EnableCap.DepthTest);

            GL.Enable(EnableCap.CullFace);
            GL.CullFace(CullFaceMode.Front);
        }
    }
}
