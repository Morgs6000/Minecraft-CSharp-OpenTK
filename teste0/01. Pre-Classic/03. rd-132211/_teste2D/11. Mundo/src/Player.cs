using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using RubyDung.src.level;

namespace RubyDung.src;

public class Player {
    private Level level;

    public Vector3 eye = new Vector3(0.0f, 0.0f, 0.0f);
    private Vector3 target = new Vector3(1.0f, 0.0f, 0.0f);
    private Vector3 up = new Vector3(0.0f, 1.0f, 0.0f);

    public Player(Level level) {
        this.level = level;

        ResetPos();
    }

    private void ResetPos() {
        Random random = new Random();

        float x = (float)random.NextDouble() * -(float)level.width * 48.0f;
        float y = -(float)(level.height + 10) * 48.0f;
        //float z = (float)random.NextDouble() * (float)level.depth;

        SetPos(x, y, 0);
    }

    private void SetPos(float x, float y, float z) {
        //this.x = x;
        //this.y = y;
        //this.z = z;

        this.eye = new Vector3(x, y, z);
    }

    public void ProcessInput(GameWindow window, FrameEventArgs args) {
        //float speed = 4.317f;
        float speed = 0.5f;

        float x = 0.0f;
        float y = 0.0f;
        float z = 0.0f;

        if(window.KeyboardState.IsKeyDown(Keys.W)) {
            y--;
        }
        if(window.KeyboardState.IsKeyDown(Keys.S)) {
            y++;
        }
        if(window.KeyboardState.IsKeyDown(Keys.A)) {
            x++;
        }
        if(window.KeyboardState.IsKeyDown(Keys.D)) {
            x--;
        }

        eye += x * target * speed;
        eye += y * up * speed;

        if(window.KeyboardState.IsKeyPressed(Keys.R)) {
            ResetPos();
        }
    }
}
