using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace RubyDung.src;

public class Inputs {
    private KeyboardState input;
    private GameWindow window;
    private Shader shader;

    private bool isWireframe;

    public Vector3 position;

    public Inputs(KeyboardState input, GameWindow window, Shader shader/*, Vector3 position*/) {
        this.input = input;
        this.window = window;
        this.shader = shader;
        //this.position = position;
    }

    public void processInput() {
        this.closeWindow();
        this.wireframeMode();
        this.playerInputs();

        this.time();
    }

    private void closeWindow() {
        if(input.IsKeyDown(Keys.Escape)) {
            this.window.Close();
        }
    }

    private void wireframeMode() {
        if(input.IsKeyDown(Keys.F3) && input.IsKeyPressed(Keys.W)) {
            this.isWireframe = !this.isWireframe;

            this.shader.setBool("isWireframe", this.isWireframe);

            GL.PolygonMode(MaterialFace.FrontAndBack, this.isWireframe ? PolygonMode.Line : PolygonMode.Fill);

            //Console.WriteLine(isWireframe);
        }
    }

    private float deltaTime = 0.0f;
    private float lastFrame = 0.0f;

    private void time() {
        float currentFrame = (float)GLFW.GetTime();
        this.deltaTime = currentFrame - this.lastFrame;
        this.lastFrame = currentFrame;
    }

    private float x = 0;
    private float y = 0;
    private float z = 0;

    private void playerInputs() {
        float speed = 4.317f * this.deltaTime;

        if(input.IsKeyPressed(Keys.W)) {
            z++;
        }
        if(input.IsKeyPressed(Keys.S)) {
            z--;
        }
        if(input.IsKeyPressed(Keys.A)) {
            x++;
        }
        if(input.IsKeyPressed(Keys.D)) {
            x--;
        }

        if(input.IsKeyPressed(Keys.Space)) {
            y++;
        }
        if(input.IsKeyPressed(Keys.LeftShift)) {
            y--;
        }

        this.position = new Vector3(x, y, z);
    }
}
