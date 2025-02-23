using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace RubyDung.src;

public class Player {
    private Vector3 eye = new Vector3(0.0f, 0.0f, 0.0f);
    private Vector3 target = new Vector3(0.0f, 0.0f, 1.0f);
    private Vector3 up = new Vector3(0.0f, 1.0f, 0.0f);

    private Vector2 lastPos;

    private float pitch;        //xRot
    private float yaw = -90.0f; //yRot

    private bool firstMouse = true;

    private float fov = 60.0f;

    public void ProcessInput(GameWindow window, FrameEventArgs args) {
        float speed = 4.317f;

        float x = 0.0f;
        float y = 0.0f;
        float z = 0.0f;

        if(window.KeyboardState.IsKeyDown(Keys.W)) {
            z++;
        }
        if(window.KeyboardState.IsKeyDown(Keys.S)) {
            z--;
        }
        if(window.KeyboardState.IsKeyDown(Keys.A)) {
            x--;
        }
        if(window.KeyboardState.IsKeyDown(Keys.D)) {
            x++;
        }

        if(window.KeyboardState.IsKeyDown(Keys.Space)) {
            y++;
        }
        if(window.KeyboardState.IsKeyDown(Keys.LeftShift)) {
            y--;
        }

        eye += x * Vector3.Normalize(Vector3.Cross(target, up)) * speed * (float)args.Time;
        eye += y * up * speed * (float)args.Time;
        eye += z * Vector3.Normalize(new Vector3(target.X, 0.0f, target.Z)) * speed * (float)args.Time;
    }

    public void MouseCallback(GameWindow window) {
        float sensitivity = 0.2f;

        if(firstMouse) {
            lastPos = new Vector2(window.MouseState.X, window.MouseState.Y);
            firstMouse = false;
        }
        else {
            float deltaX = window.MouseState.X - lastPos.X;
            float deltaY = window.MouseState.Y - lastPos.Y;
            lastPos = new Vector2(window.MouseState.X, window.MouseState.Y);

            yaw += deltaX * sensitivity;
            pitch -= deltaY * sensitivity;

            if(pitch > 89.0f) {
                pitch = 89.0f;
            }
            if(pitch < -89.0f) {
                pitch = -89.0f;
            }
        }

        target.X = (float)Math.Cos(MathHelper.DegreesToRadians(pitch)) * (float)Math.Cos(MathHelper.DegreesToRadians(yaw));
        target.Y = (float)Math.Sin(MathHelper.DegreesToRadians(pitch));
        target.Z = (float)Math.Cos(MathHelper.DegreesToRadians(pitch)) * (float)Math.Sin(MathHelper.DegreesToRadians(yaw));
        target = Vector3.Normalize(target);
    }

    public void Render(Shader shader, int width, int height) {
        Matrix4 view = Matrix4.Identity;
        //view *= Matrix4.CreateTranslation(0.0f, 0.0f, 0.0f);
        view *= Matrix4.LookAt(eye, eye + target, up);
        shader.SetMatrix4("view", view);

        Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(fov), (float)width / (float)height, 0.05f, 1000.0f);
        shader.SetMatrix4("projection", projection);
    }
}
