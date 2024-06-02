using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace RubyDung.src {
    public class Player {
        public float xRot = -90.0f;
        public float yRot;

        private Vector3 cameraPos = new Vector3(0.0f, 0.0f, 0.3f);
        private Vector3 cameraFront = new Vector3(0.0f, 0.0f, -1.0f);
        private Vector3 cameraUp = new Vector3(0.0f, 1.0f, 0.0f);

        private float deltaTime = 0.0f;
        private float lastFrame = 0.0f;

        private float lastX = 400.0f;
        private float lastY = 300.0f;

        private bool firstMouse = true;

        public void use(Shader shader, int width, int height) {
            // ..:: Model ::..
            Matrix4 model = Matrix4.Identity;
            model = Matrix4.CreateTranslation(0.0f, 0.0f, -32.0f);

            int modelLoc = GL.GetUniformLocation(shader.ID, "model");
            GL.UniformMatrix4(modelLoc, false, ref model);

            // ..:: View ::..
            Matrix4 view = Matrix4.Identity;
            view = Matrix4.LookAt(this.cameraPos, this.cameraPos + this.cameraFront, this.cameraUp);

            int viewLoc = GL.GetUniformLocation(shader.ID, "view");
            GL.UniformMatrix4(viewLoc, false, ref view);

            // ..:: Projection ::..
            Matrix4 projection;
            projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45.0f), width / height, 0.1f, 100.0f);

            int projectionLoc = GL.GetUniformLocation(shader.ID, "projection");
            GL.UniformMatrix4(projectionLoc, false, ref projection);
        }

        public void time() {
            float currentFrame = (float)GLFW.GetTime();
            deltaTime = currentFrame - lastFrame;
            lastFrame = currentFrame;
        }

        /*
        public void zBuffer() {
            GL.Enable(EnableCap.DepthTest);

            GL.Enable(EnableCap.CullFace);
            GL.CullFace(CullFaceMode.Front);
        }
        */

        public void turn(float xo, float yo) {
            if(this.firstMouse) {
                this.lastX = xo;
                this.lastY = yo;
                this.firstMouse = false;
            }

            float xoffset = xo - this.lastX;
            float yoffset = yo - this.lastY;
            this.lastX = xo;
            this.lastY = yo;

            float sensitivity = 0.1f;
            xoffset *= sensitivity;
            yoffset *= sensitivity;

            this.xRot += xoffset;
            this.yRot -= yoffset;

            if(this.yRot < -90.0f) {
                this.yRot = -90.0f;
            }
            if(this.yRot > 90.0f) {
                this.yRot = 90.0f;
            }

            Vector3 direction;
            direction.X = (float)Math.Cos(MathHelper.DegreesToRadians(this.xRot)) * (float)Math.Cos(MathHelper.DegreesToRadians(this.yRot));
            direction.Y = (float)Math.Sin(MathHelper.DegreesToRadians(this.yRot));
            direction.Z = (float)Math.Sin(MathHelper.DegreesToRadians(this.xRot)) * (float)Math.Cos(MathHelper.DegreesToRadians(this.yRot));
            this.cameraFront = Vector3.Normalize(direction);
        }

        public void tick(KeyboardState input) {
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
    }
}
