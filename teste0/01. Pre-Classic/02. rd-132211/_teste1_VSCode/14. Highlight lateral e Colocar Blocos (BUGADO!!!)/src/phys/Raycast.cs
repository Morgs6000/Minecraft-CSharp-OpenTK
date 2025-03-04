using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace RubyDung;

public class Raycast {
    private Player player;
    private Level level;
    private LevelRenderer levelRenderer;
    private Tesselator t;

    private Vector3 origin;
    private Vector3 direction;
    private Vector3 blockPos;
    
    private Vector3 hitPos;
    private Vector3 hitNormal;

    private int intersectFace = -1;

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
            SetHighlight((int)blockPos.X, (int)blockPos.Y, (int)blockPos.Z, intersectFace);

            // Se o botão esquerdo do mouse for pressionado, remove o bloco
            if(window.MouseState.IsButtonPressed(MouseButton.Left)) {
                SetBlock((int)blockPos.X, (int)blockPos.Y, (int)blockPos.Z, 0);
            }

            // Se o botão direito do mouse for pressionado, coloca um bloco
            if(window.MouseState.IsButtonPressed(MouseButton.Right)) {
                Place();
            }
        }
        else {
            t.Init();
        }
    }

    private void Place() {
        // Calcula a posição do novo bloco
        Vector3 newBlockPos = new Vector3(
            blockPos.X + (int)hitNormal.X,
            blockPos.Y + (int)hitNormal.Y,
            blockPos.Z + (int)hitNormal.Z
        );

        // Verifica se a posição do novo bloco está dentro dos limites do mundo
        if(newBlockPos.X >= 0 && newBlockPos.X < level.width &&
           newBlockPos.Y >= 0 && newBlockPos.Y < level.height &&
           newBlockPos.Z >= 0 && newBlockPos.Z < level.depth
        ) {
            SetBlock((int)newBlockPos.X, (int)newBlockPos.Y, (int)newBlockPos.Z, 1);
        }
        else {
            Console.WriteLine("Posição do novo bloco fora dos limites do mundo.");
        }
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

    private void SetHighlight(int x, int y, int z, int face) {
        //GL.DepthFunc(DepthFunction.Always);
        GL.DepthFunc(DepthFunction.Lequal);

        GL.Enable(EnableCap.Blend);
        GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);

        t.Init();
        Tile.rock.RenderFace(t, x, y, z, face);
        t.OnLoad();

        //GL.DepthFunc(DepthFunction.Less);
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

                // Calcula a normal da face colidida
                hitNormal = CalculateHitNormal(currentPosition);
                //Console.WriteLine(hitNormal);

                // Define a posição da colisão
                hitPos = currentPosition;

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

    private Vector3 CalculateHitNormal(Vector3 hitPosition) {
        // Posição do bloco colidido
        Vector3 blockPosition = new Vector3(
            (int)Math.Floor(hitPosition.X),
            (int)Math.Floor(hitPosition.Y),
            (int)Math.Floor(hitPosition.Z)
        );

        // Vetor do centro do bloco para o ponto de colisão
        Vector3 toHit = hitPosition - blockPosition;

        float epsilon = 0.01f; // Ajuste conforme necessário

        // Determina a face colidida com base na direção do raio
        if(toHit.X < epsilon) {
            Console.WriteLine($"Posição do Bloco: {blockPos}; Face: x0 (esquerda)");
            return -Vector3.UnitX; // Face esquerda
        }
        if(toHit.X > 1.0f - epsilon) {
            Console.WriteLine($"Posição do Bloco: {blockPos}; Face: x1 (direita)");
            return Vector3.UnitX; // Face direita
        }
        if(toHit.Y < epsilon) {
            Console.WriteLine($"Posição do Bloco: {blockPos}; Face: y0 (inferior)");
            return -Vector3.UnitY; // Face inferior
        }
        if(toHit.Y > 1.0f - epsilon) {
            Console.WriteLine($"Posição do Bloco: {blockPos}; Face: y1 (superior)");
            return Vector3.UnitY; // Face superior
        }
        if(toHit.Z < epsilon) {
            Console.WriteLine($"Posição do Bloco: {blockPos}; Face: z0 (traseira)");
            return -Vector3.UnitZ; // Face traseira
        }
        if(toHit.Z > 1.0f - epsilon) {
            Console.WriteLine($"Posição do Bloco: {blockPos}; Face: z1 (frontal)");
            return Vector3.UnitZ; // Face frontal
        }

        return Vector3.Zero; // Caso padrão (não deve acontecer)
    }

    public void Render(Shader shader) {
        //float alpha = (float)Math.Sin((double)Environment.TickCount / 100.0f) * 0.2f + 0.4f;
        float alpha = (float)Math.Sin(GLFW.GetTime() * 10.0) * 0.2f + 0.4f;
        shader.SetColor("color0", 1.0f, 1.0f, 1.0f, alpha);

        t.OnRenderFrame();
    }
}
