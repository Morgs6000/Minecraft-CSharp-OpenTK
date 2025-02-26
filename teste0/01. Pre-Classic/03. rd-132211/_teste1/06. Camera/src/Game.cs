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
        player.OnLoad(CursorState);
    }

    protected override void OnUpdateFrame(FrameEventArgs args) {
        base.OnUpdateFrame(args);

        if(KeyboardState.IsKeyPressed(Keys.Escape)) {
            //Close();
            Pause();
        }

        if(KeyboardState.IsKeyDown(Keys.F3)) {
            if(KeyboardState.IsKeyPressed(Keys.W)) {
                Wireframe();
            }
            if(KeyboardState.IsKeyPressed(Keys.M)) {
                player.Movement(this);
            }
        }
        else {
            if(!pause) {
                player.OnUpdateFrame(KeyboardState, MouseState);
            }
        }
    }

    protected override void OnRenderFrame(FrameEventArgs args) {
        base.OnRenderFrame(args);

        GL.Clear(ClearBufferMask.ColorBufferBit);

        shader.OnRenderFrame();
        texture.OnRenderFrame();
        t.OnRenderFrame();

        Matrix4 model = Matrix4.Identity;
        shader.SetMatrix4("model", model);

        Matrix4 view = Matrix4.Identity;
        view *= player.GetLookAt();
        shader.SetMatrix4("view", view);

        SetupCamera();

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

    /* ..:: Camera ::.. */
    private void SetupCamera() {
        float fovy = MathHelper.DegreesToRadians(70.0f);
        float aspect = (float)ClientSize.X / (float)ClientSize.Y;
        float depthNear = 0.05f;
        float depthFar = 1000.0f;

        Matrix4 projection = Matrix4.Identity;
        projection *= Matrix4.CreatePerspectiveFieldOfView(fovy, aspect, depthNear, depthFar);
        shader.SetMatrix4("projection", projection);
    }

    /* ..:: Pause ::.. */
    private bool pause = false;

    private void Pause() {
        pause = !pause;
        CursorState = pause ? CursorState.Normal : CursorState.Grabbed;
        MousePosition = pause ? new Vector2(ClientSize.X / 2, ClientSize.Y / 2) : player.lastPos;
        Console.WriteLine($"Pause: {(pause ? "true" : "false")}");
    }
}

public class Player() {
    private CursorState cursorState;

    public void OnLoad(CursorState cursorState) {
        this.cursorState = cursorState;

        cursorState = CursorState.Grabbed;
    }

    public void OnUpdateFrame(KeyboardState keyboard, MouseState mouseState) {
        this.keyboardState = keyboard;
        this.mouseState = mouseState;

        if(movement) {
            ProcessInput();
            MouseCallback();
        }
        else {
            MouseProcessInput();
        }
    }

    /* ..:: Camera ::.. */
    //private Vector3 position = Vector3.Zero;
    private Vector3 position = new Vector3(0.0f, 0.0f, 3.0f);
    private Vector3 horizontal = Vector3.UnitX;
    private Vector3 vertical = Vector3.UnitY;
    private Vector3 direction = Vector3.UnitZ;

    public Matrix4 GetLookAt() {
        Vector3 eye = position;
        Vector3 target = direction;
        Vector3 up = vertical;

        return Matrix4.LookAt(eye, eye + target, up);
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
    //private float roll;         //zRot

    private bool firstMouse = true;

    public void MouseCallback() {
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

        direction.X = (float)Math.Cos(MathHelper.DegreesToRadians(pitch)) * (float)Math.Cos(MathHelper.DegreesToRadians(yaw));
        direction.Y = (float)Math.Sin(MathHelper.DegreesToRadians(pitch));
        direction.Z = (float)Math.Cos(MathHelper.DegreesToRadians(pitch)) * (float)Math.Sin(MathHelper.DegreesToRadians(yaw));
        direction = Vector3.Normalize(direction);
    }

    /* ..:: Arrasto ::.. */
    private bool movement = true;

    public void Movement(GameWindow window) {
        movement = !movement;
        cursorState = movement ? CursorState.Grabbed : CursorState.Normal;

        var mousePosition = window.MousePosition;
        Vector2i clientSize = window.ClientSize;
        mousePosition = movement ? lastPos : new Vector2(clientSize.X / 2, clientSize.Y / 2);

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
}

public class Tile() {
    public static Tile tile = new Tile();

    public void OnLoad(Tesselator t) {
        float u0 = (float)0 / 16.0f;
        float v0 = (16.0f - 1.0f) / 16.0f;

        float u1 = u0 + (1.0f / 16.0f);
        float v1 = v0 + (1.0f / 16.0f);

        t.Vertex(-0.5f, -0.5f,  0.0f);
        t.Vertex( 0.5f, -0.5f,  0.0f);
        t.Vertex( 0.5f,  0.5f,  0.0f);
        t.Vertex(-0.5f,  0.5f,  0.0f);

        t.Indice();

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
        indiceBuffer.Add(0);
        indiceBuffer.Add(1);
        indiceBuffer.Add(2);

        indiceBuffer.Add(0);
        indiceBuffer.Add(2);
        indiceBuffer.Add(3);
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
