using OpenTK.Graphics.OpenGL4;
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

    private Crosshair crosshair;
    private Highlight highlight;

    private bool wireframe = false;

    public Game(GameWindowSettings gws, NativeWindowSettings nws) : base(gws, nws) {
        CenterWindow();
    }

    protected override void OnLoad() {
        base.OnLoad();

        GL.ClearColor(0.5f, 0.8f, 1.0f, 0.0f);

        shader = new Shader("src/shaders/shader_vertex.glsl", "src/shaders/shader_fragment.glsl");
        texture = new Texture("src/textures/terrain.png");

        // level = new Level(256, 64, 256);
        level = new Level(16, 16, 16);
        levelRenderer = new LevelRenderer(shader, level);
        levelRenderer.OnLoad();

        player = new Player(level);
        player.OnLoad(this);

        GL.Enable(EnableCap.DepthTest);
        GL.Enable(EnableCap.CullFace);

        crosshair = new Crosshair();
        highlight = new Highlight(level, player);
    }

    protected override void OnUpdateFrame(FrameEventArgs args) {
        base.OnUpdateFrame(args);

        if(KeyboardState.IsKeyPressed(Keys.Escape)) {
            Close();
        }

        if(KeyboardState.IsKeyDown(Keys.F3)) {
            if(KeyboardState.IsKeyPressed(Keys.W)) {
                Wireframe();
            }
        }
        else {
            player.OnUpdateFrame(this);        
            highlight.OnUpdateFrame();    
        }
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
        view *= player.GetLookAt();
        shader.SetMatrix4("view", view);

        Matrix4 projection = Matrix4.Identity;
        projection *= player.GetCreatePerspectiveFieldOfView(ClientSize);
        shader.SetMatrix4("projection", projection); 

        highlight.OnRenderFrame(ClientSize);       

        //GL.Disable(EnableCap.DepthTest);
        crosshair.OnRenderFrame();
        //GL.Enable(EnableCap.DepthTest);

        SwapBuffers();
    }

    protected override void OnFramebufferResize(FramebufferResizeEventArgs e) {
        base.OnFramebufferResize(e);

        GL.Viewport(0, 0, ClientSize.X, ClientSize.Y);
    }

    private void Wireframe() {
        wireframe = !wireframe;
        shader.SetBool("wireframe", wireframe);
        GL.PolygonMode(TriangleFace.FrontAndBack, wireframe ? PolygonMode.Line : PolygonMode.Fill);
        Console.WriteLine($"Wireframe: {(wireframe ? "ON" : "OFF")}");
    }
}

public class Highlight {
    private Shader shader;
    private Level level;
    private Player player;
    private Tesselator t;

    public Highlight(Level level, Player player) {
        //this.shader = shader;
        shader = new Shader("src/shaders/highlight_vertex.glsl", "src/shaders/highlight_fragment.glsl");

        this.level = level;
        this.player = player;

        t = new Tesselator(shader);
    }

    public Vector3? Raycast(Player player, Level level, float maxDistance) {
        Vector3 rayOrigin = player.position;
        Vector3 rayDirection = player.direction;

        float step = 0.1f;
        Vector3 currentPosition = rayOrigin;

        for(float distance = 0; distance < maxDistance; distance += step) {
            currentPosition += rayDirection * step;

            int x = (int)Math.Floor(currentPosition.X);
            int y = (int)Math.Floor(currentPosition.Y);
            int z = (int)Math.Floor(currentPosition.Z);

            //Console.WriteLine($"Raycast: Checking block at ({x}, {y}, {z})");

            if(level.IsTile(x, y, z)) {
                //Console.WriteLine($"Raycast: Block found at ({x}, {y}, {z})");
                return new Vector3(x, y, z);
            }
        }

        //Console.WriteLine("Raycast: No block found within range.");
        return null;
    }

    public void OnUpdateFrame() {
        A();
    }

    public void A() {
        Vector3? highlightedBlock = Raycast(player, level, 5.0f);

        if(highlightedBlock.HasValue) {
            int x = (int)highlightedBlock.Value.X;
            int y = (int)highlightedBlock.Value.Y;
            int z = (int)highlightedBlock.Value.Z;

            B(x, y, z);
        }
    }

    public void B(int x, int y, int z) {
        shader.OnRenderFrame();

        //GL.Disable(EnableCap.DepthTest);
        GL.DepthFunc(DepthFunction.Lequal);

        GL.Enable(EnableCap.Blend);
        GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);

        //float alpha = (float)Math.Sin((double)Environment.TickCount / 100.0f) * 0.2f + 0.4f;
        float alpha = (float)Math.Sin(GLFW.GetTime() * 10.0) * 0.2f + 0.4f;
        // shader.SetColorRGBA("color0", 1.0f, 1.0f, 1.0f, alpha);
        shader.SetColorRGBA("color0", 1.0f, 0.0f, 0.0f, 1.0f);
        
        t.Init();
        for(int i = 0; i < 6; i++) {
            Tile.rock.RenderFace(t, x, y, z, i);
        }
        t.OnLoad();

        //GL.Enable(EnableCap.DepthTest);
    }

    public void OnRenderFrame(Vector2i clientSize) {
        shader.OnRenderFrame();

        t.OnRenderFrame();

        Matrix4 model = Matrix4.Identity;
        shader.SetMatrix4("model", model);

        Matrix4 view = Matrix4.Identity;
        view *= player.GetLookAt();
        shader.SetMatrix4("view", view);

        Matrix4 projection = Matrix4.Identity;
        projection *= player.GetCreatePerspectiveFieldOfView(clientSize);
        shader.SetMatrix4("projection", projection);
    }
}

public class Crosshair {
    private Shader shader;
    private int vao;
    private int vbo;

    public Crosshair() {
        shader = new Shader("src/shaders/crosshair_vertex.glsl", "src/shaders/crosshair_fragment.glsl");

        float[] vertices = {
            // Linha horizontal
            -0.02f,  0.0f,
             0.02f,  0.0f,
            // Linha vertical
             0.0f, -0.02f,
             0.0f,  0.02f
        };

        // Gerar e configurar o VAO e VBO
        vao = GL.GenVertexArray();
        vbo = GL.GenBuffer();

        GL.BindVertexArray(vao);

        GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
        GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

        GL.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, 2 * sizeof(float), 0);
        GL.EnableVertexAttribArray(0);

        GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        GL.BindVertexArray(0);
    }

    public void OnRenderFrame() {
        shader.OnRenderFrame();
        shader.SetColorRGB("color", 1.0f, 1.0f, 1.0f); // Cor branca

        GL.BindVertexArray(vao);
        GL.DrawArrays(PrimitiveType.Lines, 0, 4);
        GL.BindVertexArray(0);
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

    public void SetTile(int x, int y, int z, int type) {
        if(x >= 0 && y >= 0 && z >= 0 && x < width && y < height && z < depth) {
            blocks[(y * depth + z) * width + x] = (byte)type;
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
        t.Init();

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
    
    // Variaveis da Camera
    public Vector3 position   = Vector3.Zero;

    private Vector3 horizontal = Vector3.UnitX;
    private Vector3 vertical   = Vector3.UnitY;
    public Vector3 direction  = Vector3.UnitZ;

    // Variaveis de Tempo
    private float deltaTime = 0.0f;
    private float lastFrame = 0.0f;

    // Variaveis do Movimento
    private float walking = 4.317f;

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
        MouseCallBack(mouseState);

        if(keyboardState.IsKeyPressed(Keys.R)) {
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

        if(keyboardState.IsKeyDown(Keys.Space)) {
            y++;
        }
        if(keyboardState.IsKeyDown(Keys.LeftShift)) {
            y--;
        }

        position += x * speed * Vector3.Normalize(Vector3.Cross(direction, vertical));
        position += y * speed * vertical;
        position += z * speed * Vector3.Normalize(new Vector3(direction.X, 0.0f, direction.Z));
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

        //Console.WriteLine($"Camera Direction: ({direction.X}, {direction.Y}, {direction.Z})");
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
                t.Color(br, br, br);
                t.Tex(u0, v0);
                t.Vertex(x0, y0, z0);
                t.Tex(u1, v0);
                t.Vertex(x0, y0, z1);
                t.Tex(u1, v1);
                t.Vertex(x0, y1, z1);
                t.Tex(u0, v1);
                t.Vertex(x0, y1, z0);
            }
        }

        // x1
        if(!level.IsSolidTile(x + 1, y, z)) {
            br = level.GetBrightness(x + 1, y, z) * c3;
            if(br == c3) {
                t.Color(br, br, br);
                t.Tex(u0, v0);
                t.Vertex(x1, y0, z1);
                t.Tex(u1, v0);
                t.Vertex(x1, y0, z0);
                t.Tex(u1, v1);
                t.Vertex(x1, y1, z0);
                t.Tex(u0, v1);
                t.Vertex(x1, y1, z1);
            }
        }

        // y0
        if(!level.IsSolidTile(x, y - 1, z)) {
            br = level.GetBrightness(x, y - 1, z) * c1;
            if(br == c1) {
                t.Color(br, br, br);
                t.Tex(u0, v0);
                t.Vertex(x0, y0, z0);
                t.Tex(u1, v0);
                t.Vertex(x1, y0, z0);
                t.Tex(u1, v1);
                t.Vertex(x1, y0, z1);
                t.Tex(u0, v1);
                t.Vertex(x0, y0, z1);
            }
        }

        // y1
        if(!level.IsSolidTile(x, y + 1, z)) {
            br = level.GetBrightness(x, y, z) * c1;
            if(br == c1) {
                t.Color(br, br, br);
                t.Tex(u0, v0);
                t.Vertex(x0, y1, z1);
                t.Tex(u1, v0);
                t.Vertex(x1, y1, z1);
                t.Tex(u1, v1);
                t.Vertex(x1, y1, z0);
                t.Tex(u0, v1);
                t.Vertex(x0, y1, z0);
            }
        }

        // z0
        if(!level.IsSolidTile(x, y, z - 1)) {
            br = level.GetBrightness(x, y, z - 1) * c2;
            if(br == c2) {
                t.Color(br, br, br);
                t.Tex(u0, v0);
                t.Vertex(x1, y0, z0);
                t.Tex(u1, v0);
                t.Vertex(x0, y0, z0);
                t.Tex(u1, v1);
                t.Vertex(x0, y1, z0);
                t.Tex(u0, v1);
                t.Vertex(x1, y1, z0);
            }
        }

        // z1
        if(!level.IsSolidTile(x, y, z + 1)) {
            br = level.GetBrightness(x, y, z + 1) * c2;
            if(br == c2) {
                t.Color(br, br, br);
                t.Tex(u0, v0);
                t.Vertex(x0, y0, z1);
                t.Tex(u1, v0);
                t.Vertex(x1, y0, z1);
                t.Tex(u1, v1);
                t.Vertex(x1, y1, z1);
                t.Tex(u0, v1);
                t.Vertex(x0, y1, z1);
            }
        }
    }

    public void RenderFace(Tesselator t, int x, int y, int z, int face) {
        float x0 = (float)x + 0.0f;
        float y0 = (float)y + 0.0f;
        float z0 = (float)z + 0.0f;

        float x1 = (float)x + 1.0f;
        float y1 = (float)y + 1.0f;
        float z1 = (float)z + 1.0f;

        // x0
        if(face == 0) {
            t.Vertex(x0, y0, z0);
            t.Vertex(x0, y0, z1);
            t.Vertex(x0, y1, z1);
            t.Vertex(x0, y1, z0);
        }

        // x1
        if(face == 1) {
            t.Vertex(x1, y0, z1);
            t.Vertex(x1, y0, z0);
            t.Vertex(x1, y1, z0);
            t.Vertex(x1, y1, z1);
        }

        // y0
        if(face == 2) {
            t.Vertex(x0, y0, z0);
            t.Vertex(x1, y0, z0);
            t.Vertex(x1, y0, z1);
            t.Vertex(x0, y0, z1);
        }

        // y1
        if(face == 3) {
            t.Vertex(x0, y1, z1);
            t.Vertex(x1, y1, z1);
            t.Vertex(x1, y1, z0);
            t.Vertex(x0, y1, z0);
        }

        // z0
        if(face == 4) {
            t.Vertex(x1, y0, z0);
            t.Vertex(x0, y0, z0);
            t.Vertex(x0, y1, z0);
            t.Vertex(x1, y1, z0);
        }

        // z1
        if(face == 5) {
            t.Vertex(x0, y0, z1);
            t.Vertex(x1, y0, z1);
            t.Vertex(x1, y1, z1);
            t.Vertex(x0, y1, z1);
        }
    }
}

public class Tesselator {
    private List<float> vertexBuffer = new List<float>();
    private List<int> indiceBuffer = new List<int>();
    private List<float> texCoordBuffer = new List<float>();
    private List<float> colorBuffer = new List<float>();

    private int vertices = 0;

    private float u;
    private float v;

    private float r;
    private float g;
    private float b;

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
        GL.BufferData(BufferTarget.ArrayBuffer, vertexBuffer.Count * sizeof(float), vertexBuffer.ToArray(), BufferUsageHint.StaticDraw);

        int aPos = shader.GetAttribLocation("aPos");
        GL.VertexAttribPointer(aPos, 3, VertexAttribPointerType.Float, false, 0, 0);
        GL.EnableVertexAttribArray(aPos);

        /* ..:: Element Buffer Object ::.. */
        elementBufferObject = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ElementArrayBuffer, elementBufferObject);
        GL.BufferData(BufferTarget.ElementArrayBuffer, indiceBuffer.Count * sizeof(int), indiceBuffer.ToArray(), BufferUsageHint.StaticDraw);

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

    private void Clear() {
        vertexBuffer.Clear();
        indiceBuffer.Clear();
        texCoordBuffer.Clear();
        colorBuffer.Clear();

        vertices = 0;

        GL.DeleteVertexArray(vertexArrayObject);
        GL.DeleteBuffer(vertexBufferObject);
        GL.DeleteBuffer(elementBufferObject);
        GL.DeleteBuffer(textureBufferObject);
        GL.DeleteBuffer(colorBufferObject);
    }

    public void Init() {
        Clear();

        hasTexture = false;
        hasColor = false;
    }

    public void Vertex(float x, float y, float z) {
        vertexBuffer.Add(x);
        vertexBuffer.Add(y);
        vertexBuffer.Add(z);

        if(hasTexture) {
            texCoordBuffer.Add(u);
            texCoordBuffer.Add(v);
        }

        if(hasColor) {
            colorBuffer.Add(r);
            colorBuffer.Add(g);
            colorBuffer.Add(b);
        }

        vertices++;

        if(vertices % 4 == 0) {
            int indices = vertices - 4;

            // primeiro triangulo
            indiceBuffer.Add(0 + indices);
            indiceBuffer.Add(1 + indices);
            indiceBuffer.Add(2 + indices);

            // segundo triangulo
            indiceBuffer.Add(0 + indices);
            indiceBuffer.Add(2 + indices);
            indiceBuffer.Add(3 + indices);
        }
    }

    public void Tex(float u, float v) {
        hasTexture = true;

        this.u = u;
        this.v = v;
    }

    public void Color(float r, float g, float b) {
        hasColor = true;

        this.r = r;
        this.g = g;
        this.b = b;
    }
}