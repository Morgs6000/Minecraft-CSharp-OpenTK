using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace RubyDung.src;

public class Matrix {
    private int width;
    private int height;

    private Shader shader;

    private Vector3 position;

    public Matrix(int width, int height, Shader shader, Vector3 position) {
        this.width = width;
        this.height = height;

        this.shader = shader;

        this.position = position;

        this.matrixProjection();
        this.matrixView();
        this.matrixModel();
    }

    private void matrixProjection() {
        Matrix4 projection = Matrix4.Identity;

        projection *= CreatePerspectiveFieldOfView();
        //projection *= CreatePerspectiveOffCenter();
        //projection *= CreateOrthographic();
        //projection *= CreateOrthographicOffCenter();

        this.shader.setMatrix4("projection", projection);

        //Console.WriteLine("Matriz de Projeção:");
        //Console.WriteLine(projection);
    }

    private void matrixView() {
        Matrix4 view = Matrix4.Identity;

        //view *= Matrix4.CreateFromAxisAngle(new Vector3(0.5f, 1.0f, -3.0f), (float)GLFW.GetTime());

        view *= Matrix4.CreateRotationY(MathHelper.DegreesToRadians(180.0f));

        view *= this.LookAt();

        view *= Matrix4.CreateTranslation(0.0f, 0.0f, -10.0f);

        this.shader.setMatrix4("view", view);
    }

    private void matrixModel() {
        Matrix4 model = Matrix4.Identity;

        //model *= Matrix4.CreateTranslation(0.0f, 0.0f, 0.0f);
        model *= Matrix4.CreateTranslation(this.position);

        this.shader.setMatrix4("model", model);
    }

    private Matrix4 CreatePerspectiveFieldOfView() {
        float fovy = MathHelper.DegreesToRadians(60.0f);
        float aspect = (float)this.width / (float)this.height;
        float depthNear = 0.3f;
        float depthFar = 1000.0f;

        return Matrix4.CreatePerspectiveFieldOfView(fovy, aspect, depthNear, depthFar);
    }

    private Matrix4 CreatePerspectiveOffCenter() {
        //float left = 0.0f;
        //float right = (float)this.width;
        //float bottom = 0.0f;
        //float top = (float)this.height;

        //float left = -(float)this.width / 2.0f;
        //float right = (float)this.width / 2.0f;
        //float bottom = -(float)this.height / 2.0f;
        //float top = (float)this.height;

        float left = -1.0f;
        float right = 1.0f;
        float bottom = -1.0f;
        float top = 1.0f;

        float depthNear = 0.1f;
        float depthFar = 100.0f;

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
        //float radius = 10.0f;
        //float camX = (float)Math.Cos(GLFW.GetTime());
        //float camY = (float)Math.Sin(GLFW.GetTime());
        //float camZ = (float)Math.Sin(GLFW.GetTime());

        // Configura a matriz de visualização isométrica
        Vector3 eye = new Vector3(1.0f, 1.0f, 1.0f); // Posição da câmera
        Vector3 target = Vector3.Zero; // Onde a câmera está olhando
        Vector3 up = Vector3.UnitY; // Direção "para cima" da câmera

        return Matrix4.LookAt(eye, target, up);
    }
}
