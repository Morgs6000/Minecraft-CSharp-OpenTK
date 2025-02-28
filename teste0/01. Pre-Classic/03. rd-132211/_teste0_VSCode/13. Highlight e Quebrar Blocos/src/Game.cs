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
    private AABB aabb;
    private Raycast raycast;

    private bool wireframe = false;

    // Construtor da classe Game
    public Game(GameWindowSettings gws, NativeWindowSettings nws) : base(gws, nws) {
        CenterWindow();
    }

    // Método chamado quando a janela é carregada
    protected override void OnLoad() {
        base.OnLoad();

        // Define a cor de fundo da tela
        GL.ClearColor(0.5f, 0.8f, 1.0f, 0.0f);

        // Carrega os shaders e a textura
        shader = new Shader("src/shaders/shader_vertex.glsl", "src/shaders/shader_fragment.glsl");
        texture = new Texture("src/textures/terrain.png");

        // Cria o nível e o renderizador do nível
        level = new Level(256, 64, 256);
        levelRenderer = new LevelRenderer(shader, level);
        levelRenderer.OnLoad();

        // Cria o jogador
        player = new Player(level);
        player.OnLoad(this);

        // Habilita o teste de profundidade e o culling de faces
        GL.Enable(EnableCap.DepthTest);
        GL.Enable(EnableCap.CullFace);

        // Cria o sistema de colisão (AABB)
        aabb = new AABB(player, level);
        raycast = new Raycast(shader, player, level, levelRenderer);
    }

    // Método chamado a cada frame para atualizar a lógica do jogo
    protected override void OnUpdateFrame(FrameEventArgs args) {
        base.OnUpdateFrame(args);

        // Fecha o jogo se a tecla ESC for pressionada
        if(KeyboardState.IsKeyPressed(Keys.Escape)) {
            Close();
        }

        // Alterna o modo wireframe se F3 + W forem pressionados
        if(KeyboardState.IsKeyDown(Keys.F3)) {
            if(KeyboardState.IsKeyPressed(Keys.W)) {
                Wireframe();
            }
        }
        else {
            // Atualiza a lógica do jogador
            player.OnUpdateFrame(this);
        }

        // Verifica colisões
        aabb.CheckCollision();
        raycast.CheckRaycast(this);
    }

    // Método chamado a cada frame para renderizar a cena
    protected override void OnRenderFrame(FrameEventArgs args) {
        base.OnRenderFrame(args);

        // Limpa o buffer de cor e profundidade
        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

        // Renderiza o shader e a textura
        shader.OnRenderFrame();
        texture.OnRenderFrame();

        // Renderiza o nível
        levelRenderer.OnRenderFrame();

        // Define a matriz de modelo, visão e projeção
        Matrix4 model = Matrix4.Identity;
        shader.SetMatrix4("model", model);

        Matrix4 view = Matrix4.Identity;
        view *= Matrix4.CreateTranslation(0.0f, ((player.height / 2) - 1.62f), 0.0f);
        view *= player.GetLookAt();
        shader.SetMatrix4("view", view);

        Matrix4 projection = Matrix4.Identity;
        projection *= player.GetCreatePerspectiveFieldOfView(ClientSize);
        shader.SetMatrix4("projection", projection);
        
        raycast.Render(shader);

        // Troca os buffers para exibir a cena renderizada
        SwapBuffers();
    }

    // Método chamado quando o tamanho da janela é alterado
    protected override void OnFramebufferResize(FramebufferResizeEventArgs e) {
        base.OnFramebufferResize(e);

        // Ajusta o viewport para o novo tamanho da janela
        GL.Viewport(0, 0, ClientSize.X, ClientSize.Y);
    }

    // Alterna entre o modo wireframe e o modo de preenchimento
    private void Wireframe() {
        wireframe = !wireframe;
        shader.SetBool("wireframe", wireframe);
        GL.PolygonMode(TriangleFace.FrontAndBack, wireframe ? PolygonMode.Line : PolygonMode.Fill);
        Console.WriteLine($"Wireframe: {(wireframe ? "ON" : "OFF")}");
    }
}

public class Raycast {
    private Player player;
    private Level level;
    private LevelRenderer levelRenderer;
    private Tesselator t;

    private Vector3 origin;
    private Vector3 direction;
    private Vector3 blockPos;

    public Raycast(Shader shader, Player player, Level level, LevelRenderer levelRenderer) {
        t = new Tesselator(shader);
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
            SetHighlight((int)blockPos.X, (int)blockPos.Y, (int)blockPos.Z);

            // Se o botão esquerdo do mouse for pressionado, remove o bloco
            if(window.MouseState.IsButtonPressed(MouseButton.Left)) {
                SetBlock((int)blockPos.X, (int)blockPos.Y, (int)blockPos.Z, 0);
            }
        }
        else {
            t.Init();
        }
    }

    private void SetHighlight(int x, int y, int z) {
        //GL.DepthFunc(DepthFunction.Always);
        GL.DepthFunc(DepthFunction.Lequal);

        GL.Enable(EnableCap.Blend);
        GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);

        t.Init();
        for(int i = 0; i < 6; i++) {
            Tile.rock.RenderFace(t, x, y, z, i);
        }
        t.OnLoad();

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

        t.OnRenderFrame();
    }
}

// Classe para lidar com colisões AABB (Axis-Aligned Bounding Box)
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

    // Verifica colisões entre o jogador e os blocos do nível
    public void CheckCollision() {
        PlayerPos();
        GetCubes();
    }

    // Calcula a posição do jogador em relação ao mundo
    private void PlayerPos() {
        x0 = player.position.X - (player.widht / 2);
        y0 = player.position.Y - (player.height / 2);
        z0 = player.position.Z - (player.widht / 2);

        x1 = player.position.X + (player.widht / 2);
        y1 = player.position.Y + (player.height / 2);
        z1 = player.position.Z + (player.widht / 2);
    }

    // Obtém os blocos que podem estar colidindo com o jogador
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

    // Resolve a colisão entre o jogador e o bloco
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

    // Verifica se há interseção entre o jogador e o bloco
    private bool Intersects() {
        bool collisionX = x1 > blockPos.X && x0 < (blockPos.X + 1);
        bool collisionY = y1 > blockPos.Y && y0 < (blockPos.Y + 1);
        bool collisionZ = z1 > blockPos.Z && z0 < (blockPos.Z + 1);

        return collisionX && collisionY && collisionZ;
    }

    // Resolve a colisão no eixo X
    private void ClipXCollide() {
        if(x0 < blockPos.X && x1 > blockPos.X) {
            player.position.X = blockPos.X - (player.widht / 2);
        }
        if(x0 < (blockPos.X + 1) && x1 > (blockPos.X + 1)) {
            player.position.X = (blockPos.X + 1) + (player.widht / 2);
        }
    }

    // Resolve a colisão no eixo Y
    private void ClipYCollide() {
        if(y0 < blockPos.Y && y1 > blockPos.Y) {
            player.position.Y = blockPos.Y - (player.height / 2);
        }
        if(y0 < (blockPos.Y + 1) && y1 > (blockPos.Y + 1)) {
            player.position.Y = (blockPos.Y + 1) + (player.height / 2);

            player.onGround = true;
        }
    }

    // Resolve a colisão no eixo Z
    private void ClipZCollide() {
        if(z0 < blockPos.Z && z1 > blockPos.Z) {
            player.position.Z = blockPos.Z - (player.widht / 2);
        }
        if(z0 < (blockPos.Z + 1) && z1 > (blockPos.Z + 1)) {
            player.position.Z = (blockPos.Z + 1) + (player.widht / 2);
        }
    }
}

// Classe responsável por renderizar o nível
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

        // Divide o nível em chunks para renderização eficiente
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

    // Carrega todos os chunks
    public void OnLoad() {
        for(int i = 0; i < chunks.Length; i++) {
            chunks[i].OnLoad();
        }
    }

    // Renderiza todos os chunks
    public void OnRenderFrame() {
        for(int i = 0; i < chunks.Length; i++) {
            chunks[i].OnRenderFrame();
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

    // Método para recarregar uma chunk específica
    public void ChunkReload(int chunkX, int chunkY, int chunkZ) {
        int index = (chunkX + chunkY * xChunks) * zChunks + chunkZ;

        if(index >= 0 && index < chunks.Length) {
            chunks[index].OnLoad(); // Recarrega a chunk
        }
    }
}

// Classe que representa o nível do jogo
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

        // Preenche o nível com blocos (1 para sólido, 0 para ar)
        for(int x = 0; x < w; x++) {
            for(int y = 0; y < h; y++) {
                for(int z = 0; z < d; z++) {
                    int i = (y * depth + z) * width + x;
                    blocks[i] = (byte)(y <= h * 2 / 3 ? 1 : 0);
                }
            }
        }
    }

    // Verifica se há um bloco na posição (x, y, z)
    public bool IsTile(int x, int y, int z) {
        if(x >= 0 && y >= 0 && z >= 0 && x < width && y < height && z < depth) {
            return blocks[(y * depth + z) * width + x] == 1;
        }
        else {
            return false;
        }
    }

    // Verifica se o bloco na posição (x, y, z) é sólido
    public bool IsSolidTile(int x, int y, int z) {
        return IsTile(x, y, z);
    }

    // Obtém o brilho do bloco na posição (x, y, z)
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

    // Método para definir o estado de um bloco
    public void SetTile(int x, int y, int z, byte id) {
        if(x >= 0 && y >= 0 && z >= 0 && x < width && y < height && z < depth) {
            blocks[(y * depth + z) * width + x] = id;
        }
    }
}

// Classe que representa um chunk do nível
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

    // Carrega os blocos do chunk
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

    // Renderiza o chunk
    public void OnRenderFrame() {
        t.OnRenderFrame();
    }
}

// Classe que representa o jogador
public class Player {
    private Level level;

    public float widht = 0.6f;
    public float height = 1.8f;
    
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

    // Configura o estado do cursor quando o jogador é carregado
    public void OnLoad(GameWindow window) {
        window.CursorState = CursorState.Grabbed;
    }

    // Atualiza a lógica do jogador a cada frame
    public void OnUpdateFrame(GameWindow window) {
        KeyboardState keyboardState = window.KeyboardState;
        MouseState mouseState = window.MouseState;

        Time();
        ProcessInput(keyboardState);
        Gravity(keyboardState);
        MouseCallBack(mouseState);

        if(keyboardState.IsKeyPressed(Keys.R)) {
            ResetPos();
        }

        Console.WriteLine($"Posição do Jogador: {position.ToString("F0")}; onGound: {onGround}");
    }

    // Reseta a posição do jogador para uma posição aleatória
    private void ResetPos() {
        Random random = new Random();

        float x = (float)random.NextDouble() * (float)level.width;
        float y = (float)(level.height + 10);
        float z = (float)random.NextDouble() * (float)level.depth;

        SetPos(x, y, z);
    }

    // Define a posição do jogador
    private void SetPos(float x, float y, float z) {
        position = new Vector3(x, y, z);
    }

    // Calcula o tempo decorrido desde o último frame
    private void Time() {
        float currentFrame = (float)GLFW.GetTime();
        deltaTime = currentFrame - lastFrame;
        lastFrame = currentFrame;
    }

    // Processa as entradas do teclado para mover o jogador
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
        //*/

        position += x * speed * Vector3.Normalize(Vector3.Cross(direction, vertical));
        position += y * speed * vertical;
        position += z * speed * Vector3.Normalize(new Vector3(direction.X, 0.0f, direction.Z));
    }

    // Aplica a gravidade ao jogador
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

    // Atualiza a direção da câmera com base no movimento do mouse
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

            // Limita o ângulo de pitch para evitar inversões
            if(pitch < -89.0f) {
                pitch = -89.0f;
            }
            if(pitch > 89.0f) {
                pitch = 89.0f;
            }
        }

        // Atualiza a direção da câmera
        direction.X = (float)Math.Cos(MathHelper.DegreesToRadians(pitch)) * (float)Math.Cos(MathHelper.DegreesToRadians(yaw));
        direction.Y = (float)Math.Sin(MathHelper.DegreesToRadians(pitch));
        direction.Z = (float)Math.Cos(MathHelper.DegreesToRadians(pitch)) * (float)Math.Sin(MathHelper.DegreesToRadians(yaw));
        direction   = Vector3.Normalize(direction);
    }

    // Retorna a matriz de visão (LookAt) da câmera
    public Matrix4 GetLookAt() {
        Vector3 eye    = position;
        Vector3 target = direction;
        Vector3 up     = vertical;

        return Matrix4.LookAt(eye, eye + target, up);
    }

    // Retorna a matriz de projeção da câmera
    public Matrix4 GetCreatePerspectiveFieldOfView (Vector2i clientSize) {
        float fovy      = MathHelper.DegreesToRadians(70.0f);
        float aspect    = (float)clientSize.X / (float)clientSize.Y;
        float depthNear = 0.05f;
        float depthFar  = 1000.0f;

        return Matrix4.CreatePerspectiveFieldOfView(fovy, aspect, depthNear, depthFar);
    }
}

// Classe que representa um bloco no nível
public class Tile {
    public static Tile rock = new Tile(0);
    public static Tile grass = new Tile(1);

    private int tex = 0;

    private Tile(int tex) {
        this.tex = tex;
    }
    
    // Carrega o bloco no tesselator
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

        // Renderiza cada face do bloco se ela estiver visível
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
}

// Classe responsável por tesselar (gerar vértices, índices, texturas e cores) para renderização
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

    // Carrega os buffers na GPU
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

    // Renderiza o conteúdo tesselado
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

    // Adiciona um vértice ao buffer
    public void Vertex(float x, float y, float z) {
        vertexBuffer.Add(x);
        vertexBuffer.Add(y);
        vertexBuffer.Add(z);
    }

    // Adiciona índices para formar triângulos
    public void Indice() {
        // primeiro triangulo
        indiceBuffer.Add(0 + vertices);
        indiceBuffer.Add(1 + vertices);
        indiceBuffer.Add(2 + vertices);

        // segundo triangulo
        indiceBuffer.Add(0 + vertices);
        indiceBuffer.Add(2 + vertices);
        indiceBuffer.Add(3 + vertices);

        vertices += 4;
    }

    // Adiciona coordenadas de textura ao buffer
    public void Tex(float u, float v) {
        hasTexture = true;

        texCoordBuffer.Add(u);
        texCoordBuffer.Add(v);
    }

    // Adiciona cores ao buffer
    public void Color(float r, float g, float b) {
        hasColor = true;

        for(int i = 0; i < 4; i++) {
            colorBuffer.Add(r);
            colorBuffer.Add(g);
            colorBuffer.Add(b);
        }
    }
}