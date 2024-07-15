using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace RubyDung.src;

public class Matrix {
    private int width;
    private int height;

    private Shader shader;

    public Matrix(int widht, int height, Shader shader) {
        this.width = widht;
        this.height = height;
        this.shader = shader;

        this.matrixProjection();
        this.matrixView();
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

        view *= this.LookAt();

        //view *= Matrix4.CreateTranslation(0.0f, 0.0f, -10.0f);
        //view *= Matrix4.CreateTranslation(0.0f, 0.0f, 0.0f);
        view *= Matrix4.CreateTranslation(0.0f, 0.0f, 8.0f);

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

    private Matrix4 LookAt() {
        float radius = 10.0f;
        float camX = (float)(Math.Sin(GLFW.GetTime()) * radius);
        float camZ = (float)(Math.Cos(GLFW.GetTime()) * radius);

        //Vector3 eye = new Vector3(1.0f, 1.0f, 1.0f);
        Vector3 eye = new Vector3(camX, 4.0f, camZ);
        Vector3 target = new Vector3(0.0f, 0.0f, 0.0f);
        Vector3 up = new Vector3(0.0f, 1.0f, 0.0f);

        return Matrix4.LookAt(eye, target, up);
    }
}
