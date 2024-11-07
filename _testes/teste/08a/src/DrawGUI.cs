using OpenTK.Mathematics;

namespace ConsoleApp1.src;

public class DrawGUI {
    private int paintTexture = 1;

    public void OnLoad() {
        this.shader = new Shader();
        this.texture = new Texture();
    }

    public void drawGui() {
        this.shader.use();

        this.setupOrthoCamera();

        Matrix4 view = Matrix4.Identity;
        view *= Matrix4.CreateTranslation(1.5f, -0.5f, -0.5f);

        //view *= Matrix4.LookAt(new Vector3(1.0f, 1.0f, 1.0f), new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 1.0f, 0.0f));

        //view *= Matrix4.CreateRotationY(MathHelper.DegreesToRadians(45.0f));
        view *= Matrix4.CreateFromAxisAngle(new Vector3(0.0f, 1.0f, 0.0f), MathHelper.DegreesToRadians(45.0f));

        //view *= Matrix4.CreateRotationX(MathHelper.DegreesToRadians(30.0f));
        view *= Matrix4.CreateFromAxisAngle(new Vector3(1.0f, 0.0f, 0.0f), MathHelper.DegreesToRadians(30.0f));

        view *= Matrix4.CreateScale(48.0f, 48.0f, 48.0f);
        view *= Matrix4.CreateTranslation((float)(this.width - 48), (float)(this.height - 48), 0.0f);

        view *= Matrix4.CreateTranslation(0.0f, 0.0f, -200.0f);

        this.shader.setMat4("view", view);

        this.texture.bind();

        Tesselator t = new Tesselator();
        t.init();

        Tile.rock.render(t);

        t.flush();
        t.render();
    }

    // ..:: Shader ::..
    public Shader shader;

    // ..:: Tesselator ::..
    public Tesselator t;

    // ..:: Texture ::..
    private Texture texture;

    private int width;
    private int height;

    private void setupOrthoCamera() {
        Matrix4 projection = Matrix4.Identity;
        projection *= Matrix4.CreateOrthographicOffCenter(0.0f, (float)this.width, 0.0f, (float)this.height, 100.0f, 300.0f);
        this.shader.setMat4("projection", projection);

        Matrix4 view = Matrix4.Identity;
        //view *= Matrix4.CreateTranslation(0.0f, 0.0f, -200.0f);
        this.shader.setMat4("view", view);
    }
}
