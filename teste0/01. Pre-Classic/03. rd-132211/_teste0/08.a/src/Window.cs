using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace RubyDung.src;

public class Window : GameWindow {
    private int width;
    private int height;

    private Tesselator t = new Tesselator();
    private Shader shader;
    private Texture texture;

    private bool wireframe_mode = false;
    private bool movement_mode = false; // Dar um nome melhor para esta variavel

    private Vector3 eye = new Vector3(0.0f, 0.0f, -3.0f);
    private Vector3 target = new Vector3(0.0f, 0.0f, 1.0f);
    private Vector3 up = new Vector3(0.0f, 1.0f, 0.0f);

    private Vector2 lastPos;

    private float pitch;        // xRot
    private float yaw = -90.0f; // yRot

    private bool firstMouse = true;

    private float fov = 60.0f;

    private bool rightMouseButtonPressed = false;
    private bool leftMouseButtonPressed = false;

    public Window(GameWindowSettings gws, NativeWindowSettings nws) : base(gws, nws) {
        width = ClientSize.X;
        height = ClientSize.Y;

        CenterWindow();
    }

    protected override void OnUpdateFrame(FrameEventArgs args) {
        base.OnUpdateFrame(args);

        // Fechar janela
        if(KeyboardState.IsKeyDown(Keys.Escape)) {
            Close();
        }

        if(!KeyboardState.IsKeyDown(Keys.F3)) {
            // Movimentação
            if(!movement_mode) {
                float speed = 1.5f;

                if(KeyboardState.IsKeyDown(Keys.W)) {
                    eye += Vector3.Normalize(new Vector3(target.X, 0.0f, target.Z)) * speed * (float)args.Time; // Forward 
                }
                if(KeyboardState.IsKeyDown(Keys.S)) {
                    eye -= Vector3.Normalize(new Vector3(target.X, 0.0f, target.Z)) * speed * (float)args.Time; // Backwards 
                }
                if(KeyboardState.IsKeyDown(Keys.A)) {
                    eye -= Vector3.Normalize(Vector3.Cross(target, up)) * speed * (float)args.Time; // Left
                }
                if(KeyboardState.IsKeyDown(Keys.D)) {
                    eye += Vector3.Normalize(Vector3.Cross(target, up)) * speed * (float)args.Time; // Right
                }

                if(KeyboardState.IsKeyDown(Keys.Space)) {
                    eye += up * speed * (float)args.Time; // Up 
                }
                if(KeyboardState.IsKeyDown(Keys.LeftShift)) {
                    eye -= up * speed * (float)args.Time; // Down 
                }

                MouseCallback();
            }
            else {
                float scrollOffset = MouseState.ScrollDelta.Y;
                float scrollSensitivity = 2.0f;

                if(scrollOffset != 0) {
                    eye += target * scrollOffset * scrollSensitivity; 
                }

                if(MouseState.IsButtonDown(MouseButton.Right)) {
                    if(!rightMouseButtonPressed) {
                        rightMouseButtonPressed = true;
                        firstMouse = true;
                    }

                    MouseCallback();
                }
                else {
                    rightMouseButtonPressed = false;
                }

                if(MouseState.IsButtonDown(MouseButton.Left) || MouseState.IsButtonDown(MouseButton.Middle)) {
                    if(!leftMouseButtonPressed) {
                        leftMouseButtonPressed = true;
                        firstMouse = true;
                    }

                    MoveCamera();
                }
                else {
                    leftMouseButtonPressed = false;
                }
            }
        }
        else {
            // Modo Wireframe
            if(KeyboardState.IsKeyPressed(Keys.W)) {
                wireframe_mode = !wireframe_mode;

                shader.GetBool("wireframe_mode", wireframe_mode);

                GL.PolygonMode(MaterialFace.FrontAndBack, wireframe_mode ? PolygonMode.Line : PolygonMode.Fill);

                Console.WriteLine($"O modo Wireframe {(wireframe_mode ? "está ligado" : "está desligado")}");
            }
            // Modo de Movimentação
            if(KeyboardState.IsKeyPressed(Keys.M)) {
                movement_mode = !movement_mode;

                Console.WriteLine($"Modo de Movimentação {(wireframe_mode ? "com o teclado e mouse" : "com o mouse")}");
            }
        }
    }

    private void MouseCallback() {
        float sensitivity = 0.2f;

        if(firstMouse) {
            lastPos = new Vector2(MouseState.X, MouseState.Y);
            firstMouse = false;
        }
        else {
            float deltaX = MouseState.X - lastPos.X;
            float deltaY = MouseState.Y - lastPos.Y;
            lastPos = new Vector2(MouseState.X, MouseState.Y);

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

    private void MoveCamera() {
        float sensitivity = 0.2f;

        if(firstMouse) {
            lastPos = new Vector2(MouseState.X, MouseState.Y);
            firstMouse = false;
        }
        else {
            float deltaX = MouseState.X - lastPos.X;
            float deltaY = MouseState.Y - lastPos.Y;
            lastPos = new Vector2(MouseState.X, MouseState.Y);

            Vector3 right = Vector3.Normalize(Vector3.Cross(target, this.up));
            Vector3 up = Vector3.Normalize(Vector3.Cross(right, target));

            eye -= right * deltaX * sensitivity;
            eye += up * deltaY * sensitivity;
        }
    }

    protected override void OnLoad() {
        base.OnLoad();

        GL.ClearColor(0.5f, 0.8f, 1.0f, 0.0f);

        shader = new Shader("../../../src/shaders/Vertex.glsl", "../../../src/shaders/Fragment.glsl");
        shader.Use();

        texture = new Texture("../../../src/textures/terrain.png");
        texture.Use();

        Tile.rock.render(t, 0, 0, 0);
        t.flush(shader);

        GL.Enable(EnableCap.DepthTest);
        GL.Enable(EnableCap.CullFace);

        //CursorState = CursorState.Grabbed;
    }

    protected override void OnRenderFrame(FrameEventArgs args) {
        base.OnRenderFrame(args);

        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

        shader.Use();
        texture.Use();

        t.Use();

        Matrix4 view = Matrix4.Identity;
        //view *= Matrix4.CreateRotationY(MathHelper.DegreesToRadians(-45.0f));
        //view *= Matrix4.CreateRotationY(MathHelper.DegreesToRadians((float)GLFW.GetTime() * 100));
        //view *= Matrix4.CreateRotationX(MathHelper.DegreesToRadians(30.0f));
        view *= Matrix4.CreateTranslation(0.0f, 0.0f, -10.0f);
        view *= Matrix4.LookAt(eye, eye + target, up);
        shader.SetMatrix4("view", view);

        Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(fov), (float)width / (float)height, 0.05f, 1000.0f);
        shader.SetMatrix4("projection", projection);

        SwapBuffers();
    }

    protected override void OnFramebufferResize(FramebufferResizeEventArgs e) {
        base.OnFramebufferResize(e);

        width = e.Width;
        height = e.Height;

        GL.Viewport(0, 0, e.Width, e.Height);
    }
}
