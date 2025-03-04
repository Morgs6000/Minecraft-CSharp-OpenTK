using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace RubyDung.src;

public class Camera {
    public Vector3 eye = new Vector3(0.0f, 0.0f, -3.0f);
    public Vector3 target = new Vector3(0.0f, 0.0f, 1.0f);
    public Vector3 up = new Vector3(0.0f, 1.0f, 0.0f);

    private Vector2 lastPos;

    private float pitch;        //xRot
    private float yaw = -90.0f; //yRot

    private bool firstMouse = true;

    private float fov = 60.0f;

    public void ProcessInput(GameWindow window, FrameEventArgs args) {
        float speed = 1.5f;

        float x = 0.0f;
        float y = 0.0f;
        float z = 0.0f;

        if(window.KeyboardState.IsKeyDown(Keys.W)) {
            //z++;
            y++;
        }
        if(window.KeyboardState.IsKeyDown(Keys.S)) {
            //z--;
            y--;
        }
        if(window.KeyboardState.IsKeyDown(Keys.A)) {
            x--;
        }
        if(window.KeyboardState.IsKeyDown(Keys.D)) {
            x++;
        }

        //if(window.KeyboardState.IsKeyDown(Keys.Space)) {
        //    y++;
        //}
        //if(window.KeyboardState.IsKeyDown(Keys.LeftShift)) {
        //    y--;
        //}

        eye += x * Vector3.Normalize(Vector3.Cross(target, up)) * speed * (float)args.Time;
        eye += y * up * speed * (float)args.Time;
        eye += z * Vector3.Normalize(new Vector3(target.X, 0.0f, target.Z)) * speed * (float)args.Time;
    }
}

