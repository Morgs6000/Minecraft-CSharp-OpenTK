using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System.Reflection.Emit;

namespace RubyDung.src;

public class Game : GameWindow {
    private Shader shader;
    private Texture texture;
    private Level level;
    private LevelRenderer levelRenderer;
    private Player player;
    private AABB aabb;
    private Raycast raycast;

    public Game(GameWindowSettings gws, NativeWindowSettings nws) : base(gws, nws) {
        CenterWindow();
    }

    protected override void OnLoad() {
        base.OnLoad();

        GL.ClearColor(0.5f, 0.8f, 1.0f, 0.0f);

        shader = new Shader("../../../src/shaders/Vertex.glsl", "../../../src/shaders/Fragment.glsl");

        texture = new Texture("../../../src/textures/terrain.png");

        level = new Level(256, 64, 256);
        levelRenderer = new LevelRenderer(level);
        levelRenderer.Load();

        player = new Player(level);

        CursorState = movement ? CursorState.Grabbed : CursorState.Normal;

        if(!movement) {
            player.MouseCallback(this);
        }

        GL.Enable(EnableCap.DepthTest);
        GL.Enable(EnableCap.CullFace);

        aabb = new AABB(player, level);
        raycast = new Raycast(player, level, levelRenderer);
    }

    protected override void OnFramebufferResize(FramebufferResizeEventArgs e) {
        base.OnFramebufferResize(e);

        GL.Viewport(0, 0, e.Width, e.Height);
    }

    protected override void OnUpdateFrame(FrameEventArgs args) {
        base.OnUpdateFrame(args);

        if(KeyboardState.IsKeyDown(Keys.Escape)) {
            Close();
        }

        if(!KeyboardState.IsKeyDown(Keys.F3)) {
            if(movement) {
                player.ProcessInput(this);
                player.MouseCallback(this);

                aabb.CheckCollision();
                raycast.CheckRaycast(this);
            }
            else {
                player.MouseProcessInput(this);
            }
        }
        else {
            if(KeyboardState.IsKeyPressed(Keys.W)) {
                Wireframe();
            }
            if(KeyboardState.IsKeyPressed(Keys.M)) {
                Movement();
            }
        }
    }

    protected override void OnRenderFrame(FrameEventArgs args) {
        base.OnRenderFrame(args);

        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

        shader.Render();

        texture.Render();

        levelRenderer.Render(shader);

        player.Render(shader);
        SetupCamera();

        raycast.Render(shader);

        SwapBuffers();
    }

    protected override void OnUnload() {
        base.OnUnload();

        levelRenderer.Unload();
    }

    private void SetupCamera() {
        float fovy = MathHelper.DegreesToRadians(70.0f);
        float aspect = (float)ClientSize.X / (float)ClientSize.Y;
        float depthNear = 0.05f;
        float depthFar = 1000.0f;

        Matrix4 projection = Matrix4.Identity;
        projection *= Matrix4.CreatePerspectiveFieldOfView(fovy, aspect, depthNear, depthFar);
        shader.SetMatrix4("projection", projection);
    }

    /* ..:: Wireframe ::.. */

    private bool wireframe = false;

    private void Wireframe() {
        wireframe = !wireframe;

        shader.SetBool("wireframe", wireframe);

        GL.PolygonMode(TriangleFace.FrontAndBack, wireframe ? PolygonMode.Line : PolygonMode.Fill);

        Console.WriteLine($"Wireframe: {(wireframe ? "ON" : "OFF")}");
    }

    /* ..:: Movement ::.. */

    private bool movement = true;

    private void Movement() {
        movement = !movement;

        CursorState = movement ? CursorState.Grabbed : CursorState.Normal;

        if(!movement) {
            MousePosition = new Vector2(ClientSize.X / 2, ClientSize.Y / 2);
        }

        Console.WriteLine($"Modo de Movimentação {(movement ? "com o teclado e mouse" : "com o mouse")}");
    }
}

public class Raycast {
    private Player player;
    private Level level;
    private LevelRenderer levelRenderer;
    private Tesselator t = new Tesselator();

    private Vector3 origin;
    private Vector3 direction;
    private Vector3 blockPos;

    public Raycast(Player player, Level level, LevelRenderer levelRenderer) {
        this.player = player;
        this.level = level;
        this.levelRenderer = levelRenderer;
    }

    // Método para verificar colisões ao longo de um raio
    public void CheckRaycast(GameWindow window) {
        origin = player.position;
        direction = player.direction;

        // Verifica se o jogador está olhando para um bloco
        if(Cast()) {
            SetHighlight((int)blockPos.X, (int)blockPos.Y, (int)blockPos.Z, intersectFace);

            // Se o botão esquerdo do mouse for pressionado, remove o bloco
            if(window.MouseState.IsButtonPressed(MouseButton.Left)) {
                SetBlock((int)blockPos.X, (int)blockPos.Y, (int)blockPos.Z, 0);
            }
        }
        else {
            t.Init();
        }
    }

    private void SetHighlight(int x, int y, int z, int face) {
        //GL.DepthFunc(DepthFunction.Always);
        GL.DepthFunc(DepthFunction.Lequal);

        GL.Enable(EnableCap.Blend);
        GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);

        t.Init();
        //Tile.rock.RenderBlock(t, x, y, z);
        Tile.rock.RenderFace(t, x, y, z, face);
        t.Load();

        //GL.DepthFunc(DepthFunction.Less);
    }

    private void SetBlock(int x, int y, int z, byte id) {
        level.SetTile(x, y, z, id);

        // Calcula a chunk que contém o bloco
        int chunkX = x / 16;
        int chunkY = y / 16;
        int chunkZ = z / 16;

        // Recarrega apenas a chunk afetada
        levelRenderer.ChunkReloadNeighbors(chunkX, chunkY, chunkZ);

        Console.WriteLine($"Bloco removido: ({x}, {y}, {z})");
        Console.WriteLine($"Chunk recarregada: ({chunkX}, {chunkY}, {chunkZ})");
    }

    /*
    private bool Cast() {
        // Normaliza a direção do raio
        direction = Vector3.Normalize(direction);

        // Posição atual ao longo do raio
        Vector3 currentPosition = origin;

        // Tamanho do passo (ajuste conforme necessário)
        float stepSize = 0.1f;

        // Distância máxima do raio
        float maxDistance = 10.0f;

        // Itera ao longo do raio
        for(float distance = 0; distance < maxDistance; distance += stepSize) {
            // Atualiza a posição atual
            currentPosition += direction * stepSize;

            // Verifica se a posição atual colide com um bloco sólido
            if(IsSolidBlock(currentPosition)) {
                // Printa a posição do bloco no console
                //Console.WriteLine($"Bloco colidido: ({blockPos})");

                return true; // Sai do método após encontrar uma colisão
            }
        }

        //Console.WriteLine("Nenhum bloco colidido.");
        blockPos = Vector3.Zero;
        return false;
    }
    */

    private int intersectFace = -1;

    private bool Cast() {
        direction = Vector3.Normalize(direction);
        Vector3 currentPosition = origin;
        float stepSize = 0.1f;
        float maxDistance = 10.0f;

        for(float distance = 0; distance < maxDistance; distance += stepSize) {
            currentPosition += direction * stepSize;

            if(IsSolidBlock(currentPosition)) {
                // Determina qual face foi intersectada
                Vector3 delta = currentPosition - blockPos;
                float epsilon = 0.0001f;

                if(Math.Abs(delta.X) < epsilon) {
                    intersectFace = 0; // Face X-
                    return true;
                }
                if(Math.Abs(delta.X - 1) < epsilon) {
                    intersectFace = 1; // Face X+
                    return true;
                }
                if(Math.Abs(delta.Y) < epsilon) {
                    intersectFace = 2; // Face Y-
                    return true;
                }
                if(Math.Abs(delta.Y - 1) < epsilon) {
                    intersectFace = 3; // Face Y+
                    return true;
                }
                if(Math.Abs(delta.Z) < epsilon) {
                    intersectFace = 4; // Face Z-
                    return true;
                }
                if(Math.Abs(delta.Z - 1) < epsilon) {
                    intersectFace = 5; // Face Z+
                    return true;
                }

                return true; // Colisão detectada, mas a face não foi determinada
            }
        }

        blockPos = Vector3.Zero;
        return false; // Nenhum bloco foi intersectado
    }

    // Verifica se há um bloco sólido na posição dada
    private bool IsSolidBlock(Vector3 position) {
        blockPos = new Vector3(
            (int)Math.Floor(position.X),
            (int)Math.Floor(position.Y),
            (int)Math.Floor(position.Z)
        );

        return level.IsSolidTile((int)blockPos.X, (int)blockPos.Y, (int)blockPos.Z);
    }

    public void Render(Shader shader) {
        //float alpha = (float)Math.Sin((double)Environment.TickCount / 100.0f) * 0.2f + 0.4f;
        float alpha = (float)Math.Sin(GLFW.GetTime() * 10.0) * 0.2f + 0.4f;
        shader.SetColor("color0", 1.0f, 1.0f, 1.0f, alpha);

        t.Render(shader);
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
    private Vector3 playerPos;

    public AABB(Player player, Level level) {
        this.player = player;
        this.level = level;
    }

    public void CheckCollision() {
        PlayerPos();
        GetCubes();
    }

    private void PlayerPos() {
        // Limites do jogador (AABB)
        x0 = player.position.X - player.width;
        y0 = player.position.Y - player.height;
        z0 = player.position.Z - player.width;

        x1 = player.position.X + player.width;
        y1 = player.position.Y + player.height;
        z1 = player.position.Z + player.width;
    }

    private void GetCubes() {
        // Verifica colisão com blocos próximos ao jogador
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

    private bool Intersects() {
        // Verifica se há sobreposição entre o jogador e o bloco
        bool collisionX = x1 > blockPos.X && x0 < (blockPos.X + 1);
        bool collisionY = y1 > blockPos.Y && y0 < (blockPos.Y + 1);
        bool collisionZ = z1 > blockPos.Z && z0 < (blockPos.Z + 1);

        return collisionX && collisionY && collisionZ;
    }

    private void ClipCollide() {
        // Se houver colisão em todos os eixos, então há uma colisão real
        if(Intersects()) {
            //Console.WriteLine($"Posição do jogador: {player.position.ToString("F1")}");
            //Console.WriteLine($"Colisão detectada com bloco em: {blockPos}");

            // Calcula a profundidade da colisão em cada eixo
            float overlapX = Math.Min(x1 - blockPos.X, (blockPos.X + 1) - x0);
            float overlapY = Math.Min(y1 - blockPos.Y, (blockPos.Y + 1) - y0);
            float overlapZ = Math.Min(z1 - blockPos.Z, (blockPos.Z + 1) - z0);

            // Usa uma variável local para armazenar a nova posição do jogador
            playerPos = player.position;

            // Determina o eixo com a menor sobreposição (eixo principal da colisão)
            if(overlapX < overlapY && overlapX < overlapZ) {
                ClipXCollide();
            }
            if(overlapY < overlapX && overlapY < overlapZ) {
                ClipYCollide();
            }
            if(overlapZ < overlapX && overlapZ < overlapY) {
                ClipZCollide();
            }

            // Atribui a nova posição ao jogador
            player.position = playerPos;
        }
    }

    private void ClipXCollide() {
        // Colisão no eixo X
        if(x1 > blockPos.X && x0 < blockPos.X) {
            //Console.WriteLine("Colisão com a face x0 (esquerda) do bloco.");
            playerPos.X = blockPos.X - player.width;
        }
        else if(x0 < (blockPos.X + 1) && x1 > (blockPos.X + 1)) {
            //Console.WriteLine("Colisão com a face x1 (direita) do bloco.");
            playerPos.X = (blockPos.X + 1) + player.width;
        }
    }

    private void ClipYCollide() {
        // Colisão no eixo Y
        if(y1 > blockPos.Y && y0 < blockPos.Y) {
            //Console.WriteLine("Colisão com a face y0 (inferior) do bloco.");
            playerPos.Y = blockPos.Y - player.height;

            //player.velocity.Y = 0.0f; // pro jogador não atravessar o teto?
        }
        else if(y0 < (blockPos.Y + 1) && y1 > (blockPos.Y + 1)) {
            //Console.WriteLine("Colisão com a face y1 (superior) do bloco.");
            playerPos.Y = (blockPos.Y + 1) + player.height;

            //player.velocity.Y = 0.0f;
            player.onGround = true;
        }
    }

    private void ClipZCollide() {
        // Colisão no eixo Z
        if(z1 > blockPos.Z && z0 < blockPos.Z) {
            //Console.WriteLine("Colisão com a face z0 (traseira) do bloco.");
            playerPos.Z = blockPos.Z - player.width;
        }
        else if(z0 < (blockPos.Z + 1) && z1 > (blockPos.Z + 1)) {
            //Console.WriteLine("Colisão com a face z1 (frontal) do bloco.");
            playerPos.Z = (blockPos.Z + 1) + player.width;
        }
    }
}

public class LevelRenderer {
    private Level level;

    private int xChunks;
    private int yChunks;
    private int zChunks;

    private Chunk[] chunks;

    public LevelRenderer(Level level) {
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

                    chunks[(x + y * xChunks) * zChunks + z] = new Chunk(level, x0, y0, z0, x1, y1, z1);
                }
            }
        }
    }

    public void Load() {
        for(int i = 0; i < chunks.Length; i++) {
            chunks[i].Load();
        }
    }

    public void Render(Shader shader) {
        for(int i = 0; i < chunks.Length; i++) {
            chunks[i].Render(shader);
        }
    }

    public void Unload() {
        for(int i = 0; i < chunks.Length; i++) {
            chunks[i].Unload();
        }
    }

    // Método para recarregar uma chunk específica
    public void ChunkReload(int chunkX, int chunkY, int chunkZ) {
        int index = (chunkX + chunkY * xChunks) * zChunks + chunkZ;

        if(index >= 0 && index < chunks.Length) {
            chunks[index].Load(); // Recarrega a chunk
        }
    }

    // Método para recarregar a chunk e suas vizinhas, se necessário
    public void ChunkReloadNeighbors(int chunkX, int chunkY, int chunkZ) {
        // Recarrega a chunk atual
        ChunkReload(chunkX, chunkY, chunkZ);

        // Verifica se o bloco está na borda da chunk e recarrega as chunks vizinhas
        if(chunkX > 0) {
            ChunkReload(chunkX - 1, chunkY, chunkZ); // Chunk à esquerda (X-1)
        }
        if(chunkX < xChunks - 1) {
            ChunkReload(chunkX + 1, chunkY, chunkZ); // Chunk à direita (X+1)
        }
        if(chunkY > 0) {
            ChunkReload(chunkX, chunkY - 1, chunkZ); // Chunk abaixo (Y-1)
        }
        if(chunkY < yChunks - 1) {
            ChunkReload(chunkX, chunkY + 1, chunkZ); // Chunk acima (Y+1)
        }
        if(chunkZ > 0) {
            ChunkReload(chunkX, chunkY, chunkZ - 1); // Chunk atrás (Z-1)
        }
        if(chunkZ < zChunks - 1) {
            ChunkReload(chunkX, chunkY, chunkZ + 1); // Chunk à frente (Z+1)
        }
    }
}

public class Level {
    public readonly int width;
    public readonly int height;
    public readonly int depth;

    private byte[] blocks;

    public Level(int w, int h, int d) {
        width = w;
        height = h;
        depth = d;

        blocks = new byte[w * h * d];

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

    // Método para definir o estado de um bloco
    public void SetTile(int x, int y, int z, byte id) {
        if(x >= 0 && y >= 0 && z >= 0 && x < width && y < height && z < depth) {
            blocks[(y * depth + z) * width + x] = id;
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

    private Tesselator t = new Tesselator();

    public Chunk(Level level, int x0, int y0, int z0, int x1, int y1, int z1) {
        this.level = level;

        this.x0 = x0;
        this.y0 = y0;
        this.z0 = z0;

        this.x1 = x1;
        this.y1 = y1;
        this.z1 = z1;
    }

    public void Load() {
        t.Init();

        for(int x = x0; x < x1; x++) {
            for(int y = y0; y < y1; y++) {
                for(int z = z0; z < z1; z++) {
                    if(this.level.IsTile(x, y, z)) {
                        bool tex = y != level.height * 2 / 3;

                        if(!tex) {
                            Tile.rock.Load(t, level, x, y, z);
                        }
                        else {
                            Tile.grass.Load(t, level, x, y, z);
                        }
                    }
                }
            }
        }

        t.Load();
    }

    public void Render(Shader shader) {
        t.Render(shader);
    }

    public void Unload() {
        t.Init();
    }
}

public class Player {
    private Level level;

    private float walking = 4.317f;

    public float width = 0.3f;
    public float height = 0.9f;

    private float cameraHeight = 1.62f;

    private float deltaTime = 0.0f;
    private float lastFrame = 0.0f;

    // Variáveis para gravidade
    private float falling = -77.71f;
    private float jumping = 1.2522f;

    public Vector3 velocity;

    public bool onGround = false;

    // Variáveis para o mouse
    private Vector3 eye = new Vector3(0.0f, 0.0f, 0.0f);
    private Vector3 target = new Vector3(0.0f, 0.0f, 1.0f);
    private Vector3 up = new Vector3(0.0f, 1.0f, 0.0f);

    private Vector2 lastPos;

    private float pitch;        //xRot
    private float yaw = -90.0f; //yRot
    //private float roll;         //zRot

    private bool firstMouse = true;

    public Player(Level level) {
        this.level = level;

        ResetPos();
    }

    public Vector3 position {
        get {
            return eye;
        }
        set {
            eye = value;
        }
    }

    public Vector3 direction {
        get {
            return target;
        }
        set {
            target = value;
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

    public void ProcessInput(GameWindow window) {
        float currentFrame = (float)GLFW.GetTime();
        deltaTime = currentFrame - lastFrame;
        lastFrame = currentFrame;

        float speed = walking * deltaTime;

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

        /*
        if(window.KeyboardState.IsKeyDown(Keys.Space)) {
            y++;
        }
        if(window.KeyboardState.IsKeyDown(Keys.LeftShift)) {
            y--;
        }
        //*/

        //*
        if(window.KeyboardState.IsKeyDown(Keys.Space) && onGround) {
            onGround = false;

            velocity.Y = MathF.Sqrt(jumping * -2.0f * falling);
            //velocity.Y = jumping;
        }
        //if(!onGround) {
        velocity.Y += falling * deltaTime;
        position += Vector3.UnitY * velocity.Y * deltaTime;
        //Console.WriteLine($"Posição do jogador: {position.ToString("F0")}");

        if(onGround && velocity.Y < 0) {
            velocity.Y = -2.0f;
        }
        //}
        //*/

        position += x * Vector3.Normalize(Vector3.Cross(target, up)) * speed;
        //position += y * up * speed;
        //position += Vector3.UnitY * velocity.Y * (float)args.Time;
        position += z * Vector3.Normalize(new Vector3(target.X, 0.0f, target.Z)) * speed;

        if(window.KeyboardState.IsKeyPressed(Keys.R)) {
            onGround = false;
            ResetPos();
        }
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

    public void MouseProcessInput(GameWindow window) {
        float scrollSensitivity = 2.0f;
        float dragSensitivity = 0.2f;

        // Movimento para frente e para trás com o scroll do mouse
        float scrollDelta = window.MouseState.ScrollDelta.Y;
        eye += target * scrollDelta * scrollSensitivity;

        // Movimento para a esquerda, direita, cima e baixo arrastando o mouse com o botão esquerdo pressionado
        if(window.MouseState.IsButtonDown(MouseButton.Left) || window.MouseState.IsButtonDown(MouseButton.Middle)) {
            float deltaX = window.MouseState.X - lastPos.X;
            float deltaY = window.MouseState.Y - lastPos.Y;

            eye -= Vector3.Normalize(Vector3.Cross(target, up)) * deltaX * dragSensitivity;
            eye += up * deltaY * dragSensitivity;
        }

        // Girar a câmera arrastando o mouse com o botão direito pressionado
        if(window.MouseState.IsButtonDown(MouseButton.Right)) {
            MouseCallback(window);
        }
        else {
            firstMouse = true;
        }

        lastPos = new Vector2(window.MouseState.X, window.MouseState.Y);
    }

    public void Render(Shader shader) {
        Matrix4 view = Matrix4.Identity;
        view *= Matrix4.CreateTranslation(0.0f, height - cameraHeight, 0.0f);
        view *= Matrix4.LookAt(eye, eye + target, up);
        shader.SetMatrix4("view", view);
    }
}

public class Tile {
    public static Tile rock = new Tile(0);
    public static Tile grass = new Tile(1);

    private int tex = 0;

    private Tile(int tex) {
        this.tex = tex;
    }

    public void Load(Tesselator t, Level level, int x, int y, int z) {
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

        // x0
        if(!level.IsSolidTile(x - 1, y, z)) {
            t.Vertex(x0, y0, z0);
            t.Vertex(x0, y0, z1);
            t.Vertex(x0, y1, z1);
            t.Vertex(x0, y1, z0);

            t.Indice();

            t.Tex(u0, v0);
            t.Tex(u1, v0);
            t.Tex(u1, v1);
            t.Tex(u0, v1);
        }

        // x1
        if(!level.IsSolidTile(x + 1, y, z)) {
            t.Vertex(x1, y0, z1);
            t.Vertex(x1, y0, z0);
            t.Vertex(x1, y1, z0);
            t.Vertex(x1, y1, z1);

            t.Indice();

            t.Tex(u0, v0);
            t.Tex(u1, v0);
            t.Tex(u1, v1);
            t.Tex(u0, v1);
        }

        // y0
        if(!level.IsSolidTile(x, y - 1, z)) {
            t.Vertex(x0, y0, z0);
            t.Vertex(x1, y0, z0);
            t.Vertex(x1, y0, z1);
            t.Vertex(x0, y0, z1);

            t.Indice();

            t.Tex(u0, v0);
            t.Tex(u1, v0);
            t.Tex(u1, v1);
            t.Tex(u0, v1);
        }

        // y1
        if(!level.IsSolidTile(x, y + 1, z)) {
            t.Vertex(x0, y1, z1);
            t.Vertex(x1, y1, z1);
            t.Vertex(x1, y1, z0);
            t.Vertex(x0, y1, z0);

            t.Indice();

            t.Tex(u0, v0);
            t.Tex(u1, v0);
            t.Tex(u1, v1);
            t.Tex(u0, v1);
        }

        // z0
        if(!level.IsSolidTile(x, y, z - 1)) {
            t.Vertex(x1, y0, z0);
            t.Vertex(x0, y0, z0);
            t.Vertex(x0, y1, z0);
            t.Vertex(x1, y1, z0);

            t.Indice();

            t.Tex(u0, v0);
            t.Tex(u1, v0);
            t.Tex(u1, v1);
            t.Tex(u0, v1);
        }

        // z1
        if(!level.IsSolidTile(x, y, z + 1)) {
            t.Vertex(x0, y0, z1);
            t.Vertex(x1, y0, z1);
            t.Vertex(x1, y1, z1);
            t.Vertex(x0, y1, z1);

            t.Indice();

            t.Tex(u0, v0);
            t.Tex(u1, v0);
            t.Tex(u1, v1);
            t.Tex(u0, v1);
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

            t.Indice();
        }

        // x1
        if(face == 1) {
            t.Vertex(x1, y0, z1);
            t.Vertex(x1, y0, z0);
            t.Vertex(x1, y1, z0);
            t.Vertex(x1, y1, z1);

            t.Indice();
        }

        // y0
        if(face == 2) {
            t.Vertex(x0, y0, z0);
            t.Vertex(x1, y0, z0);
            t.Vertex(x1, y0, z1);
            t.Vertex(x0, y0, z1);

            t.Indice();
        }

        // y1
        if(face == 3) {
            t.Vertex(x0, y1, z1);
            t.Vertex(x1, y1, z1);
            t.Vertex(x1, y1, z0);
            t.Vertex(x0, y1, z0);

            t.Indice();
        }

        // z0
        if(face == 4) {
            t.Vertex(x1, y0, z0);
            t.Vertex(x0, y0, z0);
            t.Vertex(x0, y1, z0);
            t.Vertex(x1, y1, z0);

            t.Indice();
        }

        // z1
        if(face == 5) {
            t.Vertex(x0, y0, z1);
            t.Vertex(x1, y0, z1);
            t.Vertex(x1, y1, z1);
            t.Vertex(x0, y1, z1);

            t.Indice();
        }
    }

    public void RenderBlock(Tesselator t, int x, int y, int z) {
        float x0 = (float)x + 0.0f;
        float y0 = (float)y + 0.0f;
        float z0 = (float)z + 0.0f;

        float x1 = (float)x + 1.0f;
        float y1 = (float)y + 1.0f;
        float z1 = (float)z + 1.0f;

        // x0
        t.Vertex(x0, y0, z0);
        t.Vertex(x0, y0, z1);
        t.Vertex(x0, y1, z1);
        t.Vertex(x0, y1, z0);

        t.Indice();

        // x1
        t.Vertex(x1, y0, z1);
        t.Vertex(x1, y0, z0);
        t.Vertex(x1, y1, z0);
        t.Vertex(x1, y1, z1);

        t.Indice();

        // y0
        t.Vertex(x0, y0, z0);
        t.Vertex(x1, y0, z0);
        t.Vertex(x1, y0, z1);
        t.Vertex(x0, y0, z1);

        t.Indice();

        // y1
        t.Vertex(x0, y1, z1);
        t.Vertex(x1, y1, z1);
        t.Vertex(x1, y1, z0);
        t.Vertex(x0, y1, z0);

        t.Indice();

        // z0
        t.Vertex(x1, y0, z0);
        t.Vertex(x0, y0, z0);
        t.Vertex(x0, y1, z0);
        t.Vertex(x1, y1, z0);

        t.Indice();

        // z1
        t.Vertex(x0, y0, z1);
        t.Vertex(x1, y0, z1);
        t.Vertex(x1, y1, z1);
        t.Vertex(x0, y1, z1);

        t.Indice();
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

    public void Load() {
        /* ..:: Vertex Array Object ::.. */
        vertexArrayObject = GL.GenVertexArray();
        GL.BindVertexArray(vertexArrayObject);

        /* ..:: Vertex Buffer Object ::.. */
        vertexBufferObject = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferObject);
        GL.BufferData(BufferTarget.ArrayBuffer, vertexBuffer.Count * sizeof(float), vertexBuffer.ToArray(), BufferUsageHint.StreamDraw);

        GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 0, 0);
        GL.EnableVertexAttribArray(0);

        /* ..:: Element Buffer Object ::.. */
        elementBufferObject = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ElementArrayBuffer, elementBufferObject);
        GL.BufferData(BufferTarget.ElementArrayBuffer, indiceBuffer.Count * sizeof(int), indiceBuffer.ToArray(), BufferUsageHint.StreamDraw);

        /* ..:: Texture Buffer Object ::.. */
        if(hasTexture) {
            //Console.WriteLine($"hasTexture: {hasTexture}");

            textureBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, textureBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, texCoordBuffer.Count * sizeof(float), texCoordBuffer.ToArray(), BufferUsageHint.StreamDraw);

            GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 0, 0);
            GL.EnableVertexAttribArray(1);
        }

        /* ..:: Color Buffer Object ::.. */
        if(hasColor) {
            //Console.WriteLine($"hasColor: {hasColor}");

            colorBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, colorBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, colorBuffer.Count * sizeof(float), colorBuffer.ToArray(), BufferUsageHint.StreamDraw);

            GL.VertexAttribPointer(2, 3, VertexAttribPointerType.Float, false, 0, 0);
            GL.EnableVertexAttribArray(2);
        }
    }

    public void Render(Shader shader) {
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
