using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;
using StbImageSharp;

namespace ConsoleApp1.src;

public class Cube {
    public void OnLoad() {
        this.shader = new Shader();

        this.t = new Tesselator();
        this.t.init();

        Tile.rock.render(this.t);
        //Tile.tiles[1].render(this.t);

        this.t.flush();

        this.texture = new Texture();
    }

    public void OnRenderFrame(int width, int height) {
        this.shader.use();

        this.cordinate_systems(width, height);

        this.texture.bind();

        this.t.render();
    }

    // ..:: Shader ::..
    public Shader shader;

    // ..:: Tesselator ::..
    public Tesselator t;

    // ..:: Texture ::..
    private Texture texture;

    // ..:: Tile ::..
    

    // ..:: cordinate_systems ::..
    private int width;
    private int height;

    public void cordinate_systems(int width, int height) {
        this.width = width;
        this.height = height;

        this.projection();
        this.view();
    }

    private void projection() {
        Matrix4 projection = Matrix4.Identity;

        projection *= this.createPerspectiveFieldOfView();
        //projection *= this.createPerspectiveOffCenter();
        //projection *= this.createOrthographic();
        //projection *= this.createOrthographicOffCenter();

        this.shader.setMat4("projection", projection);
    }

    private void view() {
        Matrix4 view = Matrix4.Identity;

        view *= Matrix4.CreateFromAxisAngle(new Vector3(0.5f, 1.0f, 0.0f), (float)GLFW.GetTime());
        view *= Matrix4.CreateTranslation(0.0f, 0.0f, -3.0f);
        //view *= Matrix4.CreateTranslation(0.0f, 0.0f, -10.0f);

        //view *= Matrix4.CreateScale(16.0f);

        this.shader.setMat4("view", view);
    }

    private Matrix4 createPerspectiveFieldOfView() {
        float fovy = MathHelper.DegreesToRadians(60.0f);
        float aspect = (float)this.width / (float)this.height;
        float depthNear = 0.3f;
        float depthFar = 1000.0f;

        return Matrix4.CreatePerspectiveFieldOfView(fovy, aspect, depthNear, depthFar);
    }

    // Não sei como funciona
    private Matrix4 createPerspectiveOffCenter() {
        float left = 0.0f;
        float right = (float)this.width;
        float bottom = 0.0f;
        float top = (float)this.height;
        float depthNear = 0.3f;
        float depthFar = 1000.0f;

        return Matrix4.CreatePerspectiveOffCenter(left, right, bottom, top, depthNear, depthFar);
    }

    private Matrix4 createOrthographic() {
        float width = (float)this.width;
        float height = (float)this.height;
        float depthNear = 0.3f;
        float depthFar = 1000.0f;

        return Matrix4.CreateOrthographic(width, height, depthNear, depthFar);
    }

    private Matrix4 createOrthographicOffCenter() {
        float left = 0.0f;
        float right = (float)this.width;
        float bottom = 0.0f;
        float top = (float)this.height;
        float depthNear = 0.3f;
        float depthFar = 1000.0f;

        return Matrix4.CreateOrthographicOffCenter(left, right, bottom, top, depthNear, depthFar);
    }
}
