using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using RubyDung.src.level;
using static System.Reflection.Metadata.BlobBuilder;

namespace RubyDung.src;

public class Window : GameWindow {
    private int _width;
    private int _height;

    private Shader shader;
    private Texture texture;
    private LevelRenderer levelRenderer;

    private bool wireframeMode = false;
    private bool movementMode = false;

    private Vector3i blockPos = new Vector3i(0, 0, 0);

    public Window(GameWindowSettings gws, NativeWindowSettings nws) : base(gws, nws) {
        _width = ClientSize.X;
        _height = ClientSize.Y;

        CenterWindow();
    }

    protected override void OnLoad() {
        base.OnLoad();

        GL.ClearColor(0.5f, 0.8f, 1.0f, 0.0f);

        shader = new Shader("../../../src/shaders/Vertex.glsl", "../../../src/shaders/Fragment.glsl");
        texture = new Texture("../../../src/textures/terrain.png");

        LoadLevel(16, 16, 16);
        LoadChunk(0, 0, 0, 16, 16, 16);

        GL.Enable(EnableCap.DepthTest);
        GL.Enable(EnableCap.CullFace);

        CursorState = movementMode ? CursorState.Normal : CursorState.Grabbed;

        if(movementMode) {
            MouseCallback();
        }
    }

    protected override void OnUpdateFrame(FrameEventArgs args) {
        base.OnUpdateFrame(args);

        if(KeyboardState.IsKeyDown(Keys.Escape)) {
            Close();
        }

        if(!KeyboardState.IsKeyDown(Keys.F3)) {
            if(!movementMode) {
                ProcessInput(args);
                MouseCallback();

                CheckCollision();
            }
            else {
                MouseProcessInput();
            }
        }
        else {
            if(KeyboardState.IsKeyPressed(Keys.W)) {
                Wireframe();
            }
            if(KeyboardState.IsKeyPressed(Keys.M)) {
                MovementMode();
            }
        }
    }

    protected override void OnRenderFrame(FrameEventArgs args) {
        base.OnRenderFrame(args);

        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

        shader.Render();
        texture.Render();

        RenderChunk();

        Matrix4 view = Matrix4.Identity;
        view *= Matrix4.CreateTranslation(0.0f, 0.0f, 0.0f);
        view *= Matrix4.LookAt(eye, eye + target, up);
        shader.SetMatrix4("view", view);

        Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(fov), (float)_width / (float)_height, 0.05f, 1000.0f);
        shader.SetMatrix4("projection", projection);

        SwapBuffers();
    }

    protected override void OnFramebufferResize(FramebufferResizeEventArgs e) {
        base.OnFramebufferResize(e);

        _width = e.Width;
        _height = e.Height;

        GL.Viewport(0, 0, e.Width, e.Height);
    }

    private void Wireframe() {
        wireframeMode = !wireframeMode;

        shader.GetBool("wireframeMode", wireframeMode);

        GL.PolygonMode(TriangleFace.FrontAndBack, wireframeMode ? PolygonMode.Line : PolygonMode.Fill);

        Console.WriteLine($"O modo Wireframe {(wireframeMode ? "está ligado." : "está desligado.")}");
    }

    private void MovementMode() {
        movementMode = !movementMode;

        CursorState = movementMode ? CursorState.Normal : CursorState.Grabbed;

        if(movementMode) {
            MousePosition = new Vector2(width / 2, height / 2);
        }

        Console.WriteLine($"Modo de Movimentação {(movementMode ? "com o teclado e mouse" : "com o mouse")}");
    }

    /* ..:: Chunk ::.. */
    private void LoadChunk(int x0, int y0, int z0, int x1, int y1, int z1) {
        for(int x = x0; x < x1; x++) {
            for(int y = y0; y < y1; y++) {
                for(int z = z0; z < z1; z++) {
                    //LoadTile(0, 0, 0);
                    //LoadTile(blockPos.X, blockPos.Y, blockPos.Z);
                    LoadTile(x, y, z);
                }
            }
        }

        LoadTesselator();
    }

    private void RenderChunk() {
        RenderTesselator();
    }

    /* ..:: Level ::.. */
    public int width;
    public int height;
    public int depth;

    private byte[] blocks;

    private void LoadLevel(int w, int h, int d) {
        width = w;
        height = h;
        depth = d;

        blocks = new byte[w * h * d];

        for(int x = 0; x < w; x++) {
            for(int y = 0; y < h; y++) {
                for(int z = 0; z < d; z++) {
                    int i = (y * depth + z) * width + x;
                    blocks[i] = 1;
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

    /* ..:: AABB ::.. */

    private void CheckCollision() {
        // Dimensões do jogador (AABB)
        float playerWidth = 0.3f;  // Largura do jogador
        float playerHeight = 0.9f; // Altura do jogador

        // Limites do jogador (AABB)
        Vector3 playerMin = eye - new Vector3(playerWidth, playerHeight, playerWidth); // Canto mínimo do jogador
        Vector3 playerMax = eye + new Vector3(playerWidth, playerHeight, playerWidth); // Canto máximo do jogador

        // Verifica colisão com blocos próximos ao jogador
        for(int x = (int)playerMin.X; x <= (int)playerMax.X; x++) {
            for(int y = (int)playerMin.Y; y <= (int)playerMax.Y; y++) {
                for(int z = (int)playerMin.Z; z <= (int)playerMax.Z; z++) {
                    if(IsTile(x, y, z)) {
                        Vector3 blockMin = new Vector3(x, y, z); // Canto mínimo do bloco
                        Vector3 blockMax = new Vector3(x + 1, y + 1, z + 1); // Canto máximo do bloco

                        // Verifica se há sobreposição entre o jogador e o bloco
                        bool collisionX = playerMax.X > blockMin.X && playerMin.X < blockMax.X;
                        bool collisionY = playerMax.Y > blockMin.Y && playerMin.Y < blockMax.Y;
                        bool collisionZ = playerMax.Z > blockMin.Z && playerMin.Z < blockMax.Z;

                        // Se houver colisão em todos os eixos, então há uma colisão real
                        if(collisionX && collisionY && collisionZ) {
                            Console.WriteLine($"Colisão detectada com bloco em: {x}, {y}, {z}");

                            // Calcula a profundidade da colisão em cada eixo
                            float overlapX = Math.Min(playerMax.X - blockMin.X, blockMax.X - playerMin.X);
                            float overlapY = Math.Min(playerMax.Y - blockMin.Y, blockMax.Y - playerMin.Y);
                            float overlapZ = Math.Min(playerMax.Z - blockMin.Z, blockMax.Z - playerMin.Z);

                            // Determina o eixo com a menor sobreposição (eixo principal da colisão)
                            if(overlapX < overlapY && overlapX < overlapZ) {
                                // Colisão no eixo X
                                if(playerMax.X > blockMin.X && playerMin.X < blockMin.X) {
                                    Console.WriteLine("Colisão com a face X0 (esquerda) do bloco.");
                                    eye.X = blockMin.X - playerWidth; // Resposta à colisão
                                }
                                else if(playerMin.X < blockMax.X && playerMax.X > blockMax.X) {
                                    Console.WriteLine("Colisão com a face X1 (direita) do bloco.");
                                    eye.X = blockMax.X + playerWidth; // Resposta à colisão
                                }
                            }
                            else if(overlapY < overlapX && overlapY < overlapZ) {
                                // Colisão no eixo Y
                                if(playerMax.Y > blockMin.Y && playerMin.Y < blockMin.Y) {
                                    Console.WriteLine("Colisão com a face Y0 (inferior) do bloco.");
                                    eye.Y = blockMin.Y - playerHeight; // Resposta à colisão
                                }
                                else if(playerMin.Y < blockMax.Y && playerMax.Y > blockMax.Y) {
                                    Console.WriteLine("Colisão com a face Y1 (superior) do bloco.");
                                    eye.Y = blockMax.Y + playerHeight; // Resposta à colisão
                                }
                            }
                            else {
                                // Colisão no eixo Z
                                if(playerMax.Z > blockMin.Z && playerMin.Z < blockMin.Z) {
                                    Console.WriteLine("Colisão com a face Z0 (frontal) do bloco.");
                                    eye.Z = blockMin.Z - playerWidth; // Resposta à colisão
                                }
                                else if(playerMin.Z < blockMax.Z && playerMax.Z > blockMax.Z) {
                                    Console.WriteLine("Colisão com a face Z1 (traseira) do bloco.");
                                    eye.Z = blockMax.Z + playerWidth; // Resposta à colisão
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    /* ..:: Player ::.. */

    private Vector3 eye = new Vector3(0.0f, 0.0f, 32.0f);
    private Vector3 target = new Vector3(0.0f, 0.0f, 1.0f);
    private Vector3 up = new Vector3(0.0f, 1.0f, 0.0f);

    private Vector2 lastPos;

    private float pitch;        //xRot
    private float yaw = -90.0f; //yRot

    private bool firstMouse = true;

    private float fov = 60.0f;

    private void ProcessInput(FrameEventArgs args) {
        float speed = 1.5f;

        float x = 0.0f;
        float y = 0.0f;
        float z = 0.0f;

        if(KeyboardState.IsKeyDown(Keys.W)) {
            z++;
        }
        if(KeyboardState.IsKeyDown(Keys.S)) {
            z--;
        }
        if(KeyboardState.IsKeyDown(Keys.A)) {
            x--;
        }
        if(KeyboardState.IsKeyDown(Keys.D)) {
            x++;
        }

        if(KeyboardState.IsKeyDown(Keys.Space)) {
            y++;
        }
        if(KeyboardState.IsKeyDown(Keys.LeftShift)) {
            y--;
        }

        eye += x * Vector3.Normalize(Vector3.Cross(target, up)) * speed * (float)args.Time;
        eye += y * up * speed * (float)args.Time;
        eye += z * Vector3.Normalize(new Vector3(target.X, 0.0f, target.Z)) * speed * (float)args.Time;
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

    private void MouseProcessInput() {
        float scrollSensitivity = 2.0f;
        float dragSensitivity = 0.2f;

        // Movimento para frente e para trás com o scroll do mouse
        float scrollDelta = MouseState.ScrollDelta.Y;
        eye += target * scrollDelta * scrollSensitivity;

        // Movimento para a esquerda, direita, cima e baixo arrastando o mouse com o botão esquerdo pressionado
        if(MouseState.IsButtonDown(MouseButton.Left) || MouseState.IsButtonDown(MouseButton.Middle)) {
            float deltaX = MouseState.X - lastPos.X;
            float deltaY = MouseState.Y - lastPos.Y;

            eye -= Vector3.Normalize(Vector3.Cross(target, up)) * deltaX * dragSensitivity;
            eye += up * deltaY * dragSensitivity;
        }

        // Girar a câmera arrastando o mouse com o botão direito pressionado
        if(MouseState.IsButtonDown(MouseButton.Right)) {
            MouseCallback();
        }
        else {
            firstMouse = true;
        }

        lastPos = new Vector2(MouseState.X, MouseState.Y);
    }

    /* ..:: Tesselator ::.. */

    private List<float> vertexBuffer = new List<float>();
    private List<int> indiceBuffer = new List<int>();
    private List<float> texCoordBuffer = new List<float>();

    private int vertices = 0;

    private int VertexArrayObject;
    private int VertexBufferObject;
    private int ElementBufferObject;
    private int TextureBufferObject;

    public void LoadTesselator() {
        VertexArrayObject = GL.GenVertexArray();
        GL.BindVertexArray(VertexArrayObject);

        VertexBufferObject = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);
        GL.BufferData(BufferTarget.ArrayBuffer, vertexBuffer.Count * sizeof(float), vertexBuffer.ToArray(), BufferUsageHint.StaticDraw);

        GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 0, 0);
        GL.EnableVertexAttribArray(0);

        ElementBufferObject = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);
        GL.BufferData(BufferTarget.ElementArrayBuffer, indiceBuffer.Count * sizeof(int), indiceBuffer.ToArray(), BufferUsageHint.StaticDraw);

        TextureBufferObject = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ArrayBuffer, TextureBufferObject);
        GL.BufferData(BufferTarget.ArrayBuffer, texCoordBuffer.Count * sizeof(float), texCoordBuffer.ToArray(), BufferUsageHint.StaticDraw);

        GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 0, 0);
        GL.EnableVertexAttribArray(1);
    }

    public void RenderTesselator() {
        GL.BindVertexArray(VertexArrayObject);
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
        texCoordBuffer.Add(u);
        texCoordBuffer.Add(v);
    }

    /* ..:: Tile ::.. */

    public void LoadTile(int x, int y, int z) {
        float x0 = (float)x + 0.0f;
        float y0 = (float)y + 0.0f;
        float z0 = (float)z + 0.0f;

        float x1 = (float)x + 1.0f;
        float y1 = (float)y + 1.0f;
        float z1 = (float)z + 1.0f;

        float u0 = (float)0 / 16.0f;
        float u1 = u0 + (1.0f / 16.0f);
        float v0 = (16.0f - 1.0f) / 16.0f;
        float v1 = v0 + (1.0f / 16.0f);

        //x0
        if(!IsSolidTile(x - 1, y, z)) {
            Vertex(x0, y0, z0);
            Vertex(x0, y0, z1);
            Vertex(x0, y1, z1);
            Vertex(x0, y1, z0);

            Indice();

            Tex(u0, v0);
            Tex(u1, v0);
            Tex(u1, v1);
            Tex(u0, v1);
        }

        //x1
        if(!IsSolidTile(x + 1, y, z)) {
            Vertex(x1, y0, z1);
            Vertex(x1, y0, z0);
            Vertex(x1, y1, z0);
            Vertex(x1, y1, z1);

            Indice();

            Tex(u0, v0);
            Tex(u1, v0);
            Tex(u1, v1);
            Tex(u0, v1);
        }

        //y0
        if(!IsSolidTile(x, y - 1, z)) {
            Vertex(x0, y0, z0);
            Vertex(x1, y0, z0);
            Vertex(x1, y0, z1);
            Vertex(x0, y0, z1);

            Indice();

            Tex(u0, v0);
            Tex(u1, v0);
            Tex(u1, v1);
            Tex(u0, v1);
        }

        //y1
        if(!IsSolidTile(x, y + 1, z)) {
            Vertex(x0, y1, z1);
            Vertex(x1, y1, z1);
            Vertex(x1, y1, z0);
            Vertex(x0, y1, z0);

            Indice();

            Tex(u0, v0);
            Tex(u1, v0);
            Tex(u1, v1);
            Tex(u0, v1);
        }

        //z0
        if(!IsSolidTile(x, y, z - 1)) {
            Vertex(x1, y0, z0);
            Vertex(x0, y0, z0);
            Vertex(x0, y1, z0);
            Vertex(x1, y1, z0);

            Indice();

            Tex(u0, v0);
            Tex(u1, v0);
            Tex(u1, v1);
            Tex(u0, v1);
        }

        //z1
        if(!IsSolidTile(x, y, z + 1)) {
            Vertex(x0, y0, z1);
            Vertex(x1, y0, z1);
            Vertex(x1, y1, z1);
            Vertex(x0, y1, z1);

            Indice();

            Tex(u0, v0);
            Tex(u1, v0);
            Tex(u1, v1);
            Tex(u0, v1);
        }
    }
}
