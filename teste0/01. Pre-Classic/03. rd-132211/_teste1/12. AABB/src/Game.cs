﻿using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace RubyDung.src;

public class Game : GameWindow {
    private Shader shader;
    private Texture texture;
    private Level level;
    private LevelRenderer levelRenderer;
    private Player player;
    private AABB aabb;

    public Game(GameWindowSettings gws, NativeWindowSettings nws) : base(gws, nws) {
        CenterWindow();
    }

    protected override void OnLoad() {
        base.OnLoad();

        GL.ClearColor(0.5f, 0.8f, 1.0f, 0.0f);

        shader = new Shader("shader_vertex.glsl", "shader_fragment.glsl");
        texture = new Texture("../../../src/textures/terrain.png");

        level = new Level(256, 64, 256);
        levelRenderer = new LevelRenderer(shader, level);
        levelRenderer.OnLoad();

        player = new Player(level);
        player.OnLoad(this);

        GL.Enable(EnableCap.DepthTest);
        GL.Enable(EnableCap.CullFace);

        aabb = new AABB(player, level);
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
        aabb.CheckCollision();
    }

    protected override void OnRenderFrame(FrameEventArgs args) {
        base.OnRenderFrame(args);

        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

        shader.OnRenderFrame();
        texture.OnRenderFrame();

        levelRenderer.OnRenderFrame();

        Matrix4 model = Matrix4.Identity;
        shader.SetMatrix4("model", model);

        Matrix4 view = Matrix4.Identity;
        view *= Matrix4.CreateTranslation(0.0f, ((player.height / 2) - 1.62f), 0.0f);
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

public class AABB {
    private Player player;
    private Level level;

    private float x0;
    private float y0;
    private float z0;

    private float x1;
    private float y1;
    private float z1;

    private Vector3 blockPos;

    public AABB(Player player, Level level) {
        this.player = player;
        this.level = level;
    }

    public void CheckCollision() {
        PlayerPos();
        GetCubes();
    }

    private void PlayerPos() {
        x0 = player.position.X - (player.widht / 2);
        y0 = player.position.Y - (player.height / 2);
        z0 = player.position.Z - (player.widht / 2);

        x1 = player.position.X + (player.widht / 2);
        y1 = player.position.Y + (player.height / 2);
        z1 = player.position.Z + (player.widht / 2);
    }

    private void GetCubes() {
        for(int x = (int)x0; x <= (int)x1; x++) {
            for(int y = (int)y0; y <= (int)y1; y++) {
                for(int z = (int)z0; z <= (int)z1; z++) {
                    if(level.IsSolidTile(x, y, z)) {
                        blockPos = new Vector3(x, y, z);

                        ClipCollide();
                    }
                }
            }
        }
    }

    private void ClipCollide() {
        if(Intersects()) {
            float overlapX = Math.Min(x1 - blockPos.X, (blockPos.X + 1) - x0);
            float overlapY = Math.Min(y1 - blockPos.Y, (blockPos.Y + 1) - y0);
            float overlapZ = Math.Min(z1 - blockPos.Z, (blockPos.Z + 1) - z0);

            if(overlapX < overlapY && overlapX < overlapZ) {
                ClipXCollide();
            }
            if(overlapY < overlapX && overlapY < overlapZ) {
                ClipYCollide();
            }
            if(overlapZ < overlapX && overlapZ < overlapY) {
                ClipZCollide();
            }
        }
    }

    private bool Intersects() {
        bool collisionX = x1 > blockPos.X && x0 < (blockPos.X + 1);
        bool collisionY = y1 > blockPos.Y && y0 < (blockPos.Y + 1);
        bool collisionZ = z1 > blockPos.Z && z0 < (blockPos.Z + 1);

        return collisionX && collisionY && collisionZ;
    }

    private void ClipXCollide() {
        if(x0 < blockPos.X && x1 > blockPos.X) {
            player.position.X = blockPos.X - (player.widht / 2);
        }
        if(x0 < (blockPos.X + 1) && x1 > (blockPos.X + 1)) {
            player.position.X = (blockPos.X + 1) + (player.widht / 2);
        }
    }

    private void ClipYCollide() {
        if(y0 < blockPos.Y && y1 > blockPos.Y) {
            player.position.Y = blockPos.Y - (player.height / 2);
        }
        if(y0 < (blockPos.Y + 1) && y1 > (blockPos.Y + 1)) {
            player.position.Y = (blockPos.Y + 1) + (player.height / 2);

            player.onGround = true;
        }
    }

    private void ClipZCollide() {
        if(z0 < blockPos.Z && z1 > blockPos.Z) {
            player.position.Z = blockPos.Z - (player.widht / 2);
        }
        if(z0 < (blockPos.Z + 1) && z1 > (blockPos.Z + 1)) {
            player.position.Z = (blockPos.Z + 1) + (player.widht / 2);
        }
    }
}

public class LevelRenderer {
    private Level level;

    private int xChunks;
    private int yChunks;
    private int zChunks;

    private Chunk[] chunks;

    public LevelRenderer(Shader shader, Level level) {
        this.level = level;

        xChunks = level.width / 16;
        yChunks = level.height / 16;
        zChunks = level.depth / 16;

        chunks = new Chunk[xChunks * yChunks * zChunks];

        for(int x = 0; x < xChunks; x++) {
            for(int y = 0; y < yChunks; y++) {
                for(int z = 0; z < zChunks; z++) {
                    int x0 = x * 16;
                    int y0 = y * 16;
                    int z0 = z * 16;

                    int x1 = (x + 1) * 16;
                    int y1 = (y + 1) * 16;
                    int z1 = (z + 1) * 16;

                    chunks[(x + y * xChunks) * zChunks + z] = new Chunk(shader, level, x0, y0, z0, x1, y1, z1);
                }
            }
        }
    }

    public void OnLoad() {
        for(int i = 0; i < chunks.Length; i++) {
            chunks[i].OnLoad();
        }
    }

    public void OnRenderFrame() {
        for(int i = 0; i < chunks.Length; i++) {
            chunks[i].OnRenderFrame();
        }
    }
}

public class Level {
    public readonly int width;
    public readonly int height;
    public readonly int depth;

    private byte[] blocks;
    private int[] lightDepths;

    public Level(int w, int h, int d) {
        width = w;
        height = h;
        depth = d;

        blocks = new byte[w * h * d];
        lightDepths = new int[w * d];

        for(int x = 0; x < w; x++) {
            for(int y = 0; y < h; y++) {
                for(int z = 0; z < d; z++) {
                    int i = (y * depth + z) * width + x;
                    blocks[i] = (byte)(y <= h * 2 / 3 ? 1 : 0);
                }
            }
        }
    }

    public bool IsTile(int x, int y, int z) {
        if(x >= 0 && y >= 0 && z >= 0 && x < width && y < height && z < depth) {
            return blocks[(y * depth + z) * width + x] == 1;
        }
        else {
            return false;
        }
    }

    public bool IsSolidTile(int x, int y, int z) {
        return IsTile(x, y, z);
    }

    public float GetBrightness(int x, int y, int z) {
        float dark = 0.8f;
        float light = 1.0f;

        if(x >= 0 && y >= 0 && z >= 0 && x < width && y < height && z < depth) {
            return y < lightDepths[x + z * this.width] ? dark : light;
        }
        else {
            return light;
        }
    }
}

public class Chunk {
    public readonly Level level;

    public readonly int x0;
    public readonly int y0;
    public readonly int z0;

    public readonly int x1;
    public readonly int y1;
    public readonly int z1;

    private Tesselator t;

    public Chunk(Shader shader, Level level, int x0, int y0, int z0, int x1, int y1, int z1) {
        t = new Tesselator(shader);
        this.level = level;

        this.x0 = x0;
        this.y0 = y0;
        this.z0 = z0;

        this.x1 = x1;
        this.y1 = y1;
        this.z1 = z1;
    }

    public void OnLoad() {
        for(int x = x0; x < x1; x++) {
            for(int y = y0; y < y1; y++) {
                for(int z = z0; z < z1; z++) {
                    if(this.level.IsTile(x, y, z)) {
                        bool tex = y != level.height * 2 / 3;

                        if(!tex) {
                            Tile.rock.OnLoad(t, level, x, y, z);
                        }
                        else {
                            Tile.grass.OnLoad(t, level, x, y, z);
                        }
                    }
                }
            }
        }

        t.OnLoad();
    }

    public void OnRenderFrame() {
        t.OnRenderFrame();
    }
}

public class Player {
    private Level level;

    public float widht = 0.6f;
    public float height = 1.8f;

    public Player(Level level) {
        this.level = level;

        ResetPos();
    }

    private void ResetPos() {
        Random random = new Random();

        float x = (float)random.NextDouble() * (float)level.width;
        float y = (float)(level.height + 10);
        float z = (float)random.NextDouble() * (float)level.depth;

        SetPos(x, y, z);

        onGround = false; // isso não era para estar aqui
    }

    private void SetPos(float x, float y, float z) {
        position = new Vector3(x, y, z);
    }

    /* ..:: OnLoad ::.. */
    public void OnLoad(GameWindow window) {
        MouseState mouseState = window.MouseState;

        window.CursorState = movement ? CursorState.Grabbed : CursorState.Normal;

        if(!movement) {
            MouseCallback(mouseState);
        }
    }

    /* ..:: OnUpdateFrame ::.. */
    public void OnUpdateFrame(GameWindow window) {
        KeyboardState keyboardState = window.KeyboardState;
        MouseState mouseState = window.MouseState;

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
                    Time();
                    ProcessInput(keyboardState);
                    Jump(keyboardState);
                    MouseCallback(mouseState);

                    Console.WriteLine($"Posição do Jogador: {position.ToString("F0")}");
                }
                else {
                    MouseProcessInput(mouseState);
                }
            }
        }
    }

    /* ..:: Camera ::.. */
    public Vector3 position = Vector3.Zero;

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

    /* ..:: Time ::.. */
    private float deltaTime = 0.0f;
    private float lastFrame = 0.0f;

    private void Time() {
        float currentFrame = (float)GLFW.GetTime();
        deltaTime = currentFrame - lastFrame;
        lastFrame = currentFrame;
    }

    /* ..:: Teclado ::.. */
    private float walking = 4.317f;

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

        position += x * Vector3.Normalize(Vector3.Cross(direction, vertical)) * speed;
        position += y * vertical * speed;
        position += z * Vector3.Normalize(new Vector3(direction.X, 0.0f, direction.Z)) * speed;

        if(keyboardState.IsKeyPressed(Keys.R)) {
            ResetPos();
        }
    }

    /* ..:: Pulo ::.. */
    private float falling = -77.71f;
    private float jumping = 1.2522f;

    public bool onGround = false;

    private Vector3 velocity;

    private void Jump(KeyboardState keyboardState) {
        if(keyboardState.IsKeyDown(Keys.Space) && onGround) {
            onGround = false;

            velocity.Y = MathF.Sqrt(jumping * -2.0f * falling);
        }

        velocity.Y += falling * deltaTime;
        position += Vector3.UnitY * velocity.Y * deltaTime;

        if(onGround && velocity.Y < 0) {
            velocity.Y = -2.0f;
        }
    }

    /* ..:: Mouse ::.. */
    private Vector2 lastPos;

    private float pitch;        //xRot
    private float yaw = -90.0f; //yRot
    private float roll;         //zRot

    private bool firstMouse = true;

    private float sensitivity = 0.2f;

    private void MouseCallback(MouseState mouseState) {
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

    private void Movement(GameWindow window) {
        movement = !movement;
        window.CursorState = movement ? CursorState.Grabbed : CursorState.Normal;
        window.MousePosition = movement ? lastPos : new Vector2(window.ClientSize.X / 2, window.ClientSize.Y / 2);
        Console.WriteLine($"Modo de Movimentação {(movement ? "com o teclado e mouse" : "com o mouse")}");
    }

    private void MouseProcessInput(MouseState mouseState) {
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
            MouseCallback(mouseState);
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

public class Tile {
    public static Tile rock = new Tile(0);
    public static Tile grass = new Tile(1);

    private int tex = 0;

    private Tile(int tex) {
        this.tex = tex;
    }

    public void OnLoad(Tesselator t, Level level, int x, int y, int z) {
        float x0 = (float)x + 0.0f;
        float y0 = (float)y + 0.0f;
        float z0 = (float)z + 0.0f;

        float x1 = (float)x + 1.0f;
        float y1 = (float)y + 1.0f;
        float z1 = (float)z + 1.0f;

        float u0 = (float)tex / 16.0f;
        float v0 = (16.0f - 1.0f) / 16.0f;

        float u1 = u0 + (1.0f / 16.0f);
        float v1 = v0 + (1.0f / 16.0f);

        float c1 = 1.0f;
        float c2 = 0.8f;
        float c3 = 0.6f;

        float br;

        // x0
        if(!level.IsSolidTile(x - 1, y, z)) {
            br = level.GetBrightness(x - 1, y, z) * c3;
            if(br == c3) {
                t.Vertex(x0, y0, z0);
                t.Vertex(x0, y0, z1);
                t.Vertex(x0, y1, z1);
                t.Vertex(x0, y1, z0);
                t.Indice();
                t.Tex(u0, v0);
                t.Tex(u1, v0);
                t.Tex(u1, v1);
                t.Tex(u0, v1);
                t.Color(br, br, br);
            }
        }

        // x1
        if(!level.IsSolidTile(x + 1, y, z)) {
            br = level.GetBrightness(x + 1, y, z) * c3;
            if(br == c3) {
                t.Vertex(x1, y0, z1);
                t.Vertex(x1, y0, z0);
                t.Vertex(x1, y1, z0);
                t.Vertex(x1, y1, z1);
                t.Indice();
                t.Tex(u0, v0);
                t.Tex(u1, v0);
                t.Tex(u1, v1);
                t.Tex(u0, v1);
                t.Color(br, br, br);
            }
        }

        // y0
        if(!level.IsSolidTile(x, y - 1, z)) {
            br = level.GetBrightness(x, y - 1, z) * c1;
            if(br == c1) {
                t.Vertex(x0, y0, z0);
                t.Vertex(x1, y0, z0);
                t.Vertex(x1, y0, z1);
                t.Vertex(x0, y0, z1);
                t.Indice();
                t.Tex(u0, v0);
                t.Tex(u1, v0);
                t.Tex(u1, v1);
                t.Tex(u0, v1);
                t.Color(br, br, br);
            }
        }

        // y1
        if(!level.IsSolidTile(x, y + 1, z)) {
            br = level.GetBrightness(x, y, z) * c1;
            if(br == c1) {
                t.Vertex(x0, y1, z1);
                t.Vertex(x1, y1, z1);
                t.Vertex(x1, y1, z0);
                t.Vertex(x0, y1, z0);
                t.Indice();
                t.Tex(u0, v0);
                t.Tex(u1, v0);
                t.Tex(u1, v1);
                t.Tex(u0, v1);
                t.Color(br, br, br);
            }
        }

        // z0
        if(!level.IsSolidTile(x, y, z - 1)) {
            br = level.GetBrightness(x, y, z - 1) * c2;
            if(br == c2) {
                t.Vertex(x1, y0, z0);
                t.Vertex(x0, y0, z0);
                t.Vertex(x0, y1, z0);
                t.Vertex(x1, y1, z0);
                t.Indice();
                t.Tex(u0, v0);
                t.Tex(u1, v0);
                t.Tex(u1, v1);
                t.Tex(u0, v1);
                t.Color(br, br, br);
            }
        }

        // z1
        if(!level.IsSolidTile(x, y, z + 1)) {
            br = level.GetBrightness(x, y, z + 1) * c2;
            if(br == c2) {
                t.Vertex(x0, y0, z1);
                t.Vertex(x1, y0, z1);
                t.Vertex(x1, y1, z1);
                t.Vertex(x0, y1, z1);
                t.Indice();
                t.Tex(u0, v0);
                t.Tex(u1, v0);
                t.Tex(u1, v1);
                t.Tex(u0, v1);
                t.Color(br, br, br);
            }
        }
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
    }

    public void Indice() {
        indiceBuffer.Add(0 + vertices);
        indiceBuffer.Add(1 + vertices);
        indiceBuffer.Add(2 + vertices);

        indiceBuffer.Add(0 + vertices);
        indiceBuffer.Add(2 + vertices);
        indiceBuffer.Add(3 + vertices);

        vertices += 4;
    }

    public void Tex(float u, float v) {
        hasTexture = true;

        texCoordBuffer.Add(u);
        texCoordBuffer.Add(v);
    }

    public void Color(float r, float g, float b) {
        hasColor = true;

        for(int i = 0; i < 4; i++) {
            colorBuffer.Add(r);
            colorBuffer.Add(g);
            colorBuffer.Add(b);
        }
    }
}
