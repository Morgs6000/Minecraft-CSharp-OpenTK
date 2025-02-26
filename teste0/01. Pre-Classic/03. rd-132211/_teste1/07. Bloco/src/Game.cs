using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace RubyDung.src;

public class Game : GameWindow {
    private Shader shader;
    private Texture texture;
    private Tesselator t;
    private Player player;

    public Game(GameWindowSettings gws, NativeWindowSettings nws) : base(gws, nws) {
        CenterWindow();
    }

    protected override void OnLoad() {
        base.OnLoad();

        GL.ClearColor(0.5f, 0.8f, 1.0f, 0.0f);

        shader = new Shader("shader_vertex.glsl", "shader_fragment.glsl");
        texture = new Texture("../../../src/textures/terrain.png");

        t = new Tesselator(shader);
        Tile.tile.OnLoad(t);
        t.OnLoad();

        player = new Player();
        player.OnLoad(this);

        GL.Enable(EnableCap.DepthTest);
        GL.Enable(EnableCap.CullFace);
    }

    protected override void OnUpdateFrame(FrameEventArgs args) {
        base.OnUpdateFrame(args);

        //if(KeyboardState.IsKeyPressed(Keys.Escape)) {
        //    Close();
        //}

        if(KeyboardState.IsKeyDown(Keys.F3)) {
            if(KeyboardState.IsKeyPressed(Keys.W)) {
                Wireframe();
            }
        }

        player.OnUpdateFrame(this);
    }

    protected override void OnRenderFrame(FrameEventArgs args) {
        base.OnRenderFrame(args);

        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

        shader.OnRenderFrame();
        texture.OnRenderFrame();

        t.OnRenderFrame();

        Matrix4 model = Matrix4.Identity;
        shader.SetMatrix4("model", model);

        Matrix4 view = Matrix4.Identity;
        view *= player.LookAt();
        shader.SetMatrix4("view", view);

        Matrix4 projection = Matrix4.Identity;
        projection *= player.CreatePerspectiveFieldOfView(ClientSize);
        shader.SetMatrix4("projection", projection);

        SwapBuffers();
    }

    protected override void OnFramebufferResize(FramebufferResizeEventArgs e) {
        base.OnFramebufferResize(e);

        GL.Viewport(0, 0, ClientSize.X, ClientSize.Y);
    }

    /* ..:: Wireframe ::.. */
    private bool wireframe = false;

    private void Wireframe() {
        wireframe = !wireframe;
        shader.SetBool("wireframe", wireframe);
        GL.PolygonMode(TriangleFace.FrontAndBack, wireframe ? PolygonMode.Line : PolygonMode.Fill);
        Console.WriteLine($"Wireframe: {(wireframe ? "ON" : "OFF")}");
    }
}

public class Player() {
    /* ..:: OnLoad ::.. */
    public void OnLoad(GameWindow window) {
        window.CursorState = movement ? CursorState.Grabbed : CursorState.Normal;

        if(!movement) {
            MouseCallback();
        }
    }

    /* ..:: OnUpdateFrame ::.. */
    public void OnUpdateFrame(GameWindow window) {
        this.keyboardState = window.KeyboardState;
        this.mouseState = window.MouseState;

        if(keyboardState.IsKeyPressed(Keys.Escape)) {
            Pause(window);
        }

        if(keyboardState.IsKeyDown(Keys.F3)) {
            if(keyboardState.IsKeyPressed(Keys.M)) {
                Movement(window);
            }
        }
        else {
            if(!pause) {
                if(movement) {
                    ProcessInput();
                    MouseCallback();
                }
                else {
                    MouseProcessInput();
                }
            }
        }
    }

    /* ..:: Camera ::.. */
    //private Vector3 position = Vector3.Zero;
    private Vector3 position = new Vector3(0.0f, 0.0f, 3.0f);

    private Vector3 horizontal = Vector3.UnitX;
    private Vector3 vertical = Vector3.UnitY;
    private Vector3 direction = Vector3.UnitZ;

    public Matrix4 LookAt() {
        Vector3 eye = position;
        Vector3 target = direction;
        Vector3 up = vertical;

        return Matrix4.LookAt(eye, eye + target, up);
    }

    public Matrix4 CreatePerspectiveFieldOfView(Vector2i clientSize) {
        float fovy = MathHelper.DegreesToRadians(70.0f);
        float aspect = (float)clientSize.X / (float)clientSize.Y;
        float depthNear = 0.05f;
        float depthFar = 1000.0f;

        return Matrix4.CreatePerspectiveFieldOfView(fovy, aspect, depthNear, depthFar);
    }

    /* ..:: Teclado ::.. */
    private float deltaTime = 0.0f;
    private float lastFrame = 0.0f;

    private float walking = 4.317f;

    private KeyboardState keyboardState;

    public void ProcessInput() {
        float currentFrame = (float)GLFW.GetTime();
        deltaTime = currentFrame - lastFrame;
        lastFrame = currentFrame;

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

        if(keyboardState.IsKeyDown(Keys.Space)) {
            y++;
        }
        if(keyboardState.IsKeyDown(Keys.LeftShift)) {
            y--;
        }

        position += x * Vector3.Normalize(Vector3.Cross(direction, vertical)) * speed;
        position += y * vertical * speed;
        position += z * Vector3.Normalize(new Vector3(direction.X, 0.0f, direction.Z)) * speed;
    }

    /* ..:: Mouse ::.. */
    public Vector2 lastPos;

    private MouseState mouseState;

    private float pitch;        //xRot
    private float yaw = -90.0f; //yRot
    private float roll;         //zRot

    private bool firstMouse = true;

    private float sensitivity = 0.2f;

    public void MouseCallback() {
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

        direction.X = (float)Math.Cos(MathHelper.DegreesToRadians(pitch)) * (float)Math.Cos(MathHelper.DegreesToRadians(yaw));
        direction.Y = (float)Math.Sin(MathHelper.DegreesToRadians(pitch));
        direction.Z = (float)Math.Cos(MathHelper.DegreesToRadians(pitch)) * (float)Math.Sin(MathHelper.DegreesToRadians(yaw));
        direction = Vector3.Normalize(direction);
    }

    /* ..:: Arrasto ::.. */
    private bool movement = false;

    public void Movement(GameWindow window) {
        movement = !movement;
        window.CursorState = movement ? CursorState.Grabbed : CursorState.Normal;
        window.MousePosition = movement ? lastPos : new Vector2(window.ClientSize.X / 2, window.ClientSize.Y / 2);
        Console.WriteLine($"Modo de Movimentação {(movement ? "com o teclado e mouse" : "com o mouse")}");
    }

    public void MouseProcessInput() {
        float scrollSensitivity = 2.0f;
        float dragSensitivity = 0.2f;

        // Movimento para frente e para trás com o scroll do mouse
        float scrollDelta = mouseState.ScrollDelta.Y;
        position += direction * scrollDelta * scrollSensitivity;

        // Movimento para a esquerda, direita, cima e baixo arrastando o mouse com o botão esquerdo pressionado
        if(mouseState.IsButtonDown(MouseButton.Left) || mouseState.IsButtonDown(MouseButton.Middle)) {
            float deltaX = mouseState.X - lastPos.X;
            float deltaY = mouseState.Y - lastPos.Y;

            position -= Vector3.Normalize(Vector3.Cross(direction, vertical)) * deltaX * dragSensitivity;
            position += vertical * deltaY * dragSensitivity;
        }

        // Girar a câmera arrastando o mouse com o botão direito pressionado
        if(mouseState.IsButtonDown(MouseButton.Right)) {
            MouseCallback();
        }
        else {
            firstMouse = true;
        }

        lastPos = new Vector2(mouseState.X, mouseState.Y);
    }

    /* ..:: Pause ::.. */
    private bool pause = false;

    private void Pause(GameWindow window) {
        pause = !pause;
        window.CursorState = pause ? CursorState.Normal : CursorState.Grabbed;
        window.MousePosition = pause ? new Vector2(window.ClientSize.X / 2, window.ClientSize.Y / 2) : lastPos;
        Console.WriteLine($"{(pause ? "PAUSE" : "RETURN")}");
    }
}

public class Tile() {
    public static Tile tile = new Tile();

    public void OnLoad(Tesselator t) {
        float x0 = 0.0f;
        float y0 = 0.0f;
        float z0 = 0.0f;

        float x1 = 1.0f;
        float y1 = 1.0f;
        float z1 = 1.0f;

        float u0 = (float)0 / 16.0f;
        float v0 = (16.0f - 1.0f) / 16.0f;

        float u1 = u0 + (1.0f / 16.0f);
        float v1 = v0 + (1.0f / 16.0f);

        // x0
        t.Indice();
        t.Vertex(x0, y0, z0);
        t.Vertex(x0, y0, z1);
        t.Vertex(x0, y1, z1);
        t.Vertex(x0, y1, z0);
        t.Tex(u0, v0);
        t.Tex(u1, v0);
        t.Tex(u1, v1);
        t.Tex(u0, v1);

        // x1
        t.Indice();
        t.Vertex(x1, y0, z1);
        t.Vertex(x1, y0, z0);
        t.Vertex(x1, y1, z0);
        t.Vertex(x1, y1, z1);
        t.Tex(u0, v0);
        t.Tex(u1, v0);
        t.Tex(u1, v1);
        t.Tex(u0, v1);

        // y0
        t.Indice();
        t.Vertex(x0, y0, z0);
        t.Vertex(x1, y0, z0);
        t.Vertex(x1, y0, z1);
        t.Vertex(x0, y0, z1);
        t.Tex(u0, v0);
        t.Tex(u1, v0);
        t.Tex(u1, v1);
        t.Tex(u0, v1);

        // y1
        t.Indice();
        t.Vertex(x0, y1, z1);
        t.Vertex(x1, y1, z1);
        t.Vertex(x1, y1, z0);
        t.Vertex(x0, y1, z0);
        t.Tex(u0, v0);
        t.Tex(u1, v0);
        t.Tex(u1, v1);
        t.Tex(u0, v1);

        // z0
        t.Indice();
        t.Vertex(x1, y0, z0);
        t.Vertex(x0, y0, z0);
        t.Vertex(x0, y1, z0);
        t.Vertex(x1, y1, z0);
        t.Tex(u0, v0);
        t.Tex(u1, v0);
        t.Tex(u1, v1);
        t.Tex(u0, v1);

        // z1
        t.Indice();
        t.Vertex(x0, y0, z1);
        t.Vertex(x1, y0, z1);
        t.Vertex(x1, y1, z1);
        t.Vertex(x0, y1, z1);
        t.Tex(u0, v0);
        t.Tex(u1, v0);
        t.Tex(u1, v1);
        t.Tex(u0, v1);
    }
}

public class Tesselator {
    private List<float> vertexBuffer = new List<float>();
    private List<int> indiceBuffer = new List<int>();
    private List<float> texCoordBuffer = new List<float>();
    private List<float> colorBuffer = new List<float>();

    private int vertices = 0;

    private bool hasTexture = false;
    private bool hasColor = false;

    private int vertexArrayObject;
    private int vertexBufferObject;
    private int elementBufferObject;
    private int textureBufferObject;
    private int colorBufferObject;

    private Shader shader;

    public Tesselator(Shader shader) {
        this.shader = shader;
    }

    public void OnLoad() {
        /* ..:: Vertex Array Object ::.. */
        vertexArrayObject = GL.GenVertexArray();
        GL.BindVertexArray(vertexArrayObject);

        /* ..:: Vertex Buffer Object ::.. */
        vertexBufferObject = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferObject);
        GL.BufferData(BufferTarget.ArrayBuffer, vertexBuffer.Count * sizeof(float), vertexBuffer.ToArray(), BufferUsageHint.StreamDraw);

        int aPos = shader.GetAttribLocation("aPos");
        GL.VertexAttribPointer(aPos, 3, VertexAttribPointerType.Float, false, 0, 0);
        GL.EnableVertexAttribArray(aPos);

        /* ..:: Element Buffer Object ::.. */
        elementBufferObject = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ElementArrayBuffer, elementBufferObject);
        GL.BufferData(BufferTarget.ElementArrayBuffer, indiceBuffer.Count * sizeof(int), indiceBuffer.ToArray(), BufferUsageHint.StreamDraw);

        /* ..:: Texture Buffer Object ::.. */
        if(hasTexture) {
            textureBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, textureBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, texCoordBuffer.Count * sizeof(float), texCoordBuffer.ToArray(), BufferUsageHint.StreamDraw);

            int aTexCoord = shader.GetAttribLocation("aTexCoord");
            GL.VertexAttribPointer(aTexCoord, 2, VertexAttribPointerType.Float, false, 0, 0);
            GL.EnableVertexAttribArray(aTexCoord);
        }

        /* ..:: Color Buffer Object ::.. */
        if(hasColor) {
            colorBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, colorBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, colorBuffer.Count * sizeof(float), colorBuffer.ToArray(), BufferUsageHint.StreamDraw);

            int aColor = shader.GetAttribLocation("aColor");
            GL.VertexAttribPointer(aColor, 3, VertexAttribPointerType.Float, false, 0, 0);
            GL.EnableVertexAttribArray(aColor);
        }
    }

    public void OnRenderFrame() {
        shader.SetBool("hasTexture", hasTexture);
        shader.SetBool("hasColor", hasColor);

        GL.BindVertexArray(vertexArrayObject);
        GL.DrawElements(PrimitiveType.Triangles, indiceBuffer.Count, DrawElementsType.UnsignedInt, 0);
    }

    public void Vertex(float x, float y, float z) {
        vertexBuffer.Add(x);
        vertexBuffer.Add(y);
        vertexBuffer.Add(z);

        vertices++;
    }

    public void Indice() {
        indiceBuffer.Add(0 + vertices);
        indiceBuffer.Add(1 + vertices);
        indiceBuffer.Add(2 + vertices);

        indiceBuffer.Add(0 + vertices);
        indiceBuffer.Add(2 + vertices);
        indiceBuffer.Add(3 + vertices);
    }

    public void Tex(float u, float v) {
        hasTexture = true;

        texCoordBuffer.Add(u);
        texCoordBuffer.Add(v);
    }

    public void Color(float r, float g, float b) {
        hasColor = true;

        for(int i = 0; i < vertices; i++) {
            colorBuffer.Add(r);
            colorBuffer.Add(g);
            colorBuffer.Add(b);
        }
    }
}
