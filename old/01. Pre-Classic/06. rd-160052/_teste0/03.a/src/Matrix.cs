using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace RubyDung.src;

public class Matrix {
    private int width;
    private int height;

    private Shader shader;

    private Vector3 eye = new Vector3(0.0f, 0.0f, 3.0f);
    private Vector3 target = new Vector3(0.0f, 0.0f, -1.0f);
    private Vector3 up = new Vector3(0.0f, 1.0f, 0.0f);

    private float deltaTime = 0.0f;
    private float lastFrame = 0.0f;

    private static bool firstMouse = true;

    private float lastX;
    private float lastY;

    private float yaw = -90.0f;
    private float pitch;

    public void matrix(int width, int height, Shader shader) {
        this.width = width;
        this.height = height;

        this.shader = shader;

        this.matrixProjection();
        this.matrixView();

        this.time();

        //this.lastX = width / 2.0f;
        //this.lastY = height / 2.0f;
    }

    private void matrixProjection() {
        Matrix4 projection = Matrix4.Identity;

        projection *= this.CreatePerspectiveFieldOfView();
        //projection *= this.CreatePerspectiveOffCenter();
        //projection *= this.CreateOrthographic();
        //projection *= this.CreateOrthographicOffCenter();

        this.shader.setMat4("projection", projection);
    }

    private void matrixView() {
        Matrix4 view = Matrix4.Identity;

        view *= Matrix4.CreateRotationY(MathHelper.DegreesToRadians(180.0f));

        view *= Matrix4.CreateTranslation(0.0f, 0.0f, -10.0f);

        view *= Matrix4.LookAt(this.eye, this.eye + this.target, this.up);

        this.shader.setMat4("view", view);
    }

    private Matrix4 CreatePerspectiveFieldOfView() {
        float fovy = MathHelper.DegreesToRadians(60.0f);
        float aspect = (float)this.width / (float)this.height;
        float depthNear = 0.3f;
        float depthFar = 1000.0f;

        return Matrix4.CreatePerspectiveFieldOfView(fovy, aspect, depthNear, depthFar);
    }

    // Não sei como funciona
    private Matrix4 CreatePerspectiveOffCenter() {
        float left = 0.0f;
        float right = (float)this.width;
        float bottom = 0.0f;
        float top = (float)this.height;
        float depthNear = 0.3f;
        float depthFar = 1000.0f;

        return Matrix4.CreatePerspectiveOffCenter(left, right, bottom, top, depthNear, depthFar);
    }

    private Matrix4 CreateOrthographic() {
        float width = (float)this.width;
        float height = (float)this.height;
        float depthNear = 0.3f;
        float depthFar = 1000.0f;

        return Matrix4.CreateOrthographic(width, height, depthNear, depthFar);
    }

    private Matrix4 CreateOrthographicOffCenter() {
        float left = 0.0f;
        float right = (float)this.width;
        float bottom = 0.0f;
        float top = (float)this.height;
        float depthNear = 0.3f;
        float depthFar = 1000.0f;

        return Matrix4.CreateOrthographicOffCenter(left, right, bottom, top, depthNear, depthFar);
    }

    public void time() {
        float curretFrame = (float)GLFW.GetTime();

        this.deltaTime = curretFrame - this.lastFrame;
        this.lastFrame = curretFrame;
    }

    public void processInput(KeyboardState input) {
        float speed = 4.317f * this.deltaTime;

        float x = 0.0f;
        float y = 0.0f;
        float z = 0.0f;

        if(input.IsKeyDown(Keys.W)) {
            z++;
        }
        if(input.IsKeyDown(Keys.S)) {
            z--;
        }
        if(input.IsKeyDown(Keys.A)) {
            x++;
        }
        if(input.IsKeyDown(Keys.D)) {
            x--;
        }

        if(input.IsKeyDown(Keys.Space)) {
            y++;
        }
        if(input.IsKeyDown(Keys.LeftShift)) {
            y--;
        }

        this.eye += x * Vector3.Normalize(Vector3.Cross(-this.target, this.up)) * speed;
        this.eye += y * this.up * speed;
        this.eye += z * Vector3.Normalize(new Vector3(this.target.X, 0.0f, this.target.Z)) * speed;
    }

    public void mouse_callback(double xposIn, double yposIn) {
        float xpos = (float)(xposIn);
        float ypos = (float)(yposIn);

        if(firstMouse) {
            this.lastX = xpos;
            this.lastY = ypos;
            firstMouse = false;
        }

        float xoffset = xpos - this.lastX;
        float yoffset = this.lastY - ypos;

        this.lastX = xpos;
        this.lastY = ypos;

        float sensitivity = 0.1f;
        xoffset *= sensitivity;
        yoffset *= sensitivity;

        this.yaw += xoffset;
        this.pitch += yoffset;

        if(this.pitch > 89.0f) {
            this.pitch = 89.0f;
        }
        if(this.pitch < -89.0f) {
            this.pitch = -89.0f;
        }

        Vector3 front;
        front.X = MathF.Cos(MathHelper.DegreesToRadians(this.yaw)) * MathF.Cos(MathHelper.DegreesToRadians(this.pitch));
        front.Y = MathF.Sin(MathHelper.DegreesToRadians(this.pitch));
        front.Z = MathF.Sin(MathHelper.DegreesToRadians(this.yaw)) * MathF.Cos(MathHelper.DegreesToRadians(this.pitch));
        this.target = Vector3.Normalize(front);
    }
}
