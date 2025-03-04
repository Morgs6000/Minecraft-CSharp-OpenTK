using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace RubyDung;

public class Player {
    private Level level;

    public float widht = 0.6f;
    public float height = 1.8f;
    
    // Variaveis da Camera
    public Vector3 position    = Vector3.Zero;

    private Vector3 horizontal = Vector3.UnitX;
    private Vector3 vertical   = Vector3.UnitY;
    public Vector3 direction   = Vector3.UnitZ;

    // Variaveis de Tempo
    private float deltaTime = 0.0f;
    private float lastFrame = 0.0f;

    // Variaveis do Movimento
    private float walking = 4.317f;

    // Variaveis de Gravidade
    private float falling = -77.71f;
    private float jumping = 1.2522f;

    public bool onGround = false;

    private Vector3 velocity;

    // Variaveis do Mouse
    private Vector2 lastPos;

    private float pitch;        // xRot
    private float yaw = -90.0f; // yRot
    private float roll;         // zRot

    private bool fistMouse = true;

    private float sensitivity = 0.2f;

    public Player(Level level) {
        this.level = level;

        ResetPos();
    }

    public void OnLoad(GameWindow window) {
        window.CursorState = CursorState.Grabbed;
    }

    public void OnUpdateFrame(GameWindow window) {
        KeyboardState keyboardState = window.KeyboardState;
        MouseState mouseState = window.MouseState;

        Time();
        ProcessInput(keyboardState);
        Gravity(keyboardState);
        MouseCallBack(mouseState);

        if(keyboardState.IsKeyDown(Keys.R)) {
            ResetPos();
        }
    }

    private void ResetPos() {
        Random random = new Random();

        float x = (float)random.NextDouble() * (float)level.width;
        float y = (float)(level.height + 10);
        float z = (float)random.NextDouble() * (float)level.depth;

        SetPos(x, y, z);
    }

    private void SetPos(float x, float y, float z) {
        position = new Vector3(x, y, z);
    }

    private void Time() {
        float currentFrame = (float)GLFW.GetTime();
        deltaTime = currentFrame - lastFrame;
        lastFrame = currentFrame;
    }

    private void ProcessInput(KeyboardState keyboardState) {
        float speed = walking * deltaTime;

        float x = 0.0f;
        float y = 0.0f;
        float z = 0.0f;

        if(keyboardState.IsKeyDown(Keys.W)) {
            z++;
        }
        if(keyboardState.IsKeyDown(Keys.S)) {
            z--;
        }
        if(keyboardState.IsKeyDown(Keys.A)) {
            x--;
        }
        if(keyboardState.IsKeyDown(Keys.D)) {
            x++;
        }

        /*
        if(keyboardState.IsKeyDown(Keys.Space)) {
            y++;
        }
        if(keyboardState.IsKeyDown(Keys.LeftShift)) {
            y--;
        }
        */

        position += x * speed * Vector3.Normalize(Vector3.Cross(direction, vertical));
        position += y * speed * vertical;
        position += z * speed * Vector3.Normalize(new Vector3(direction.X, 0.0f, direction.Z));
    }

    public void Gravity(KeyboardState keyboardState) {
        if(onGround) {
            if(keyboardState.IsKeyDown(Keys.Space)) {
                onGround = false;
                
                // Aplica a força do pulo
                velocity.Y = MathF.Sqrt(jumping * -2.0f * falling);
            }
        }
        else {
            velocity.Y += falling * deltaTime; // Aplica a gravidade
            position += velocity * deltaTime;  // Atualiza a posição
        }
    }

    private void MouseCallBack(MouseState mouseState) {
        if(fistMouse) {
            lastPos = new Vector2(mouseState.X, mouseState.Y);

            fistMouse = false;
        }
        else {
            float deltaX = mouseState.X - lastPos.X;
            float deltaY = mouseState.Y - lastPos.Y;

            lastPos = new Vector2(mouseState.X, mouseState.Y);

            pitch -= deltaY * sensitivity;
            yaw   += deltaX * sensitivity;

            if(pitch < -89.0f) {
                pitch = -89.0f;
            }
            if(pitch > 89.0f) {
                pitch = 89.0f;
            }
        }

        direction.X = (float)Math.Cos(MathHelper.DegreesToRadians(pitch)) * (float)Math.Cos(MathHelper.DegreesToRadians(yaw));
        direction.Y = (float)Math.Sin(MathHelper.DegreesToRadians(pitch));
        direction.Z = (float)Math.Cos(MathHelper.DegreesToRadians(pitch)) * (float)Math.Sin(MathHelper.DegreesToRadians(yaw));
        direction   = Vector3.Normalize(direction);
    }

    public Matrix4 GetLookAt() {
        Vector3 eye    = position;
        Vector3 target = direction;
        Vector3 up     = vertical;

        return Matrix4.LookAt(eye, eye + target, up);
    }

    public Matrix4 GetCreatePerspectiveFieldOfView (Vector2i clientSize) {
        float fovy      = MathHelper.DegreesToRadians(70.0f);
        float aspect    = (float)clientSize.X / (float)clientSize.Y;
        float depthNear = 0.05f;
        float depthFar  = 1000.0f;

        return Matrix4.CreatePerspectiveFieldOfView(fovy, aspect, depthNear, depthFar);
    }
}