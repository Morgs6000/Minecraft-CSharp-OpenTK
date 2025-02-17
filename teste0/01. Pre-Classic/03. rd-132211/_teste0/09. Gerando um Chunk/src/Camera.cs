using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace RubyDung.src;

public class Camera {
    public bool movement_mode = true;

    //private KeyboardState keyboardState;

    private Vector3 eye = new Vector3(0.0f, 0.0f, -3.0f);
    private Vector3 target = new Vector3(0.0f, 0.0f, 1.0f);
    private Vector3 up = new Vector3(0.0f, 1.0f, 0.0f);

    private Vector2 lastPos;

    private float pitch;        // xRot
    private float yaw;          // yRot

    public bool _firstMouse = true;

    public bool firstMouse {
        get => _firstMouse;
        set {
            if(_firstMouse != value) { // Verifica se o valor está mudando
                _firstMouse = value;
                Console.WriteLine($"firstMouse = {value}");
            }
        }
    }

    private float fov = 60.0f;

    private bool rightMouseButtonPressed = false;
    private bool leftMouseButtonPressed = false;

    public Camera() {
        
    }

    public void ProcessInput(FrameEventArgs args, KeyboardState keyboardState) {
        float speed = 4.317f;

        if(keyboardState.IsKeyDown(Keys.W)) {
            eye += Vector3.Normalize(new Vector3(target.X, 0.0f, target.Z)) * speed * (float)args.Time; // Forward 
        }
        if(keyboardState.IsKeyDown(Keys.S)) {
            eye -= Vector3.Normalize(new Vector3(target.X, 0.0f, target.Z)) * speed * (float)args.Time; // Backwards 
        }
        if(keyboardState.IsKeyDown(Keys.A)) {
            eye -= Vector3.Normalize(Vector3.Cross(target, up)) * speed * (float)args.Time; // Left
        }
        if(keyboardState.IsKeyDown(Keys.D)) {
            eye += Vector3.Normalize(Vector3.Cross(target, up)) * speed * (float)args.Time; // Right
        }

        if(keyboardState.IsKeyDown(Keys.Space)) {
            eye += up * speed * (float)args.Time; // Up 
        }
        if(keyboardState.IsKeyDown(Keys.LeftShift)) {
            eye -= up * speed * (float)args.Time; // Down 
        }
    }

    public void MouseProcessInput(MouseState mouseState) {
        float scrollOffset = mouseState.ScrollDelta.Y;
        float scrollSensitivity = 2.0f;

        if(scrollOffset != 0) {
            eye += target * scrollOffset * scrollSensitivity;
        }

        if(mouseState.IsButtonDown(MouseButton.Right)) {
            if(!rightMouseButtonPressed) {
                rightMouseButtonPressed = true;
                firstMouse = true;
            }

            MouseCallback(mouseState);
        }
        else {
            rightMouseButtonPressed = false;
        }

        if(mouseState.IsButtonDown(MouseButton.Left) || mouseState.IsButtonDown(MouseButton.Middle)) {
            if(!leftMouseButtonPressed) {
                leftMouseButtonPressed = true;
                firstMouse = true;
            }

            CameraCallback(mouseState);
        }
        else {
            leftMouseButtonPressed = false;
        }
    }

    public void MouseCallback(MouseState mouseState) {
        float sensitivity = 0.2f;

        if(firstMouse) {
            lastPos = new Vector2(mouseState.X, mouseState.Y);
            firstMouse = false;
        }
        else {
            float deltaX = mouseState.X - lastPos.X;
            float deltaY = mouseState.Y - lastPos.Y;
            lastPos = new Vector2(mouseState.X, mouseState.Y);

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

    public void CameraCallback(MouseState mouseState) {
        float sensitivity = 0.2f;

        if(firstMouse) {
            lastPos = new Vector2(mouseState.X, mouseState.Y);
            firstMouse = false;
        }
        else {
            float deltaX = mouseState.X - lastPos.X;
            float deltaY = mouseState.Y - lastPos.Y;
            lastPos = new Vector2(mouseState.X, mouseState.Y);

            Vector3 right = Vector3.Normalize(Vector3.Cross(target, this.up));
            Vector3 up = Vector3.Normalize(Vector3.Cross(right, target));

            eye -= right * deltaX * sensitivity;
            eye += up * deltaY * sensitivity;
        }
    }

    public void Use(Shader shader, float width, float height) {
        Matrix4 view = Matrix4.Identity;
        //view *= Matrix4.CreateRotationY(MathHelper.DegreesToRadians(-45.0f));
        //view *= Matrix4.CreateRotationY(MathHelper.DegreesToRadians((float)GLFW.GetTime() * 100));
        //view *= Matrix4.CreateRotationX(MathHelper.DegreesToRadians(30.0f));
        view *= Matrix4.CreateTranslation(0.0f, 0.0f, -10.0f);
        view *= Matrix4.LookAt(eye, eye + target, up);
        shader.SetMatrix4("view", view);

        Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(fov), width / height, 0.05f, 1000.0f);
        shader.SetMatrix4("projection", projection);
    }
}
