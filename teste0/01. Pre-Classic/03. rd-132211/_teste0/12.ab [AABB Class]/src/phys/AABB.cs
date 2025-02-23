using OpenTK.Mathematics;
using RubyDung.src.level;

namespace RubyDung.src.phys;

public class AABB {
    // Dimensões do jogador (AABB)
    private float playerWidth = 0.3f;  // Largura do jogador
    private float playerHeight = 0.9f; // Altura do jogador

    private Vector3 eye;

    // Limites do jogador (AABB)
    private Vector3 playerMin; // Canto mínimo do jogador
    private Vector3 playerMax; // Canto máximo do jogador

    private Vector3 blockMin; // Canto mínimo do bloco
    private Vector3 blockMax; // Canto máximo do bloco

    // Calcula a profundidade da colisão em cada eixo
    private float overlapX;
    private float overlapY;
    private float overlapZ;

    public AABB() {

    }

    public void CheckCollision() {
        
    }

    public void Function1() {
        // Limites do jogador (AABB)
        playerMin = eye - new Vector3(playerWidth, playerHeight, playerWidth); // Canto mínimo do jogador
        playerMax = eye + new Vector3(playerWidth, playerHeight, playerWidth); // Canto máximo do jogador
    }

    public void Function2() {
        // Verifica colisão com blocos próximos ao jogador
        for(int x = (int)playerMin.X; x <= (int)playerMax.X; x++) {
            for(int y = (int)playerMin.Y; y <= (int)playerMax.Y; y++) {
                for(int z = (int)playerMin.Z; z <= (int)playerMax.Z; z++) {
                    if(level.IsTile(x, y, z)) {
                        blockMin = new Vector3(x, y, z); // Canto mínimo do bloco
                        blockMax = new Vector3(x + 1, y + 1, z + 1); // Canto máximo do bloco

                        // Verifica se há sobreposição entre o jogador e o bloco
                        bool collisionX = playerMax.X > blockMin.X && playerMin.X < blockMax.X;
                        bool collisionY = playerMax.Y > blockMin.Y && playerMin.Y < blockMax.Y;
                        bool collisionZ = playerMax.Z > blockMin.Z && playerMin.Z < blockMax.Z;

                        // Se houver colisão em todos os eixos, então há uma colisão real
                        if(collisionX && collisionY && collisionZ) {
                            Console.WriteLine($"Colisão detectada com bloco em: {x}, {y}, {z}");

                            // Calcula a profundidade da colisão em cada eixo
                            overlapX = Math.Min(playerMax.X - blockMin.X, blockMax.X - playerMin.X);
                            overlapY = Math.Min(playerMax.Y - blockMin.Y, blockMax.Y - playerMin.Y);
                            overlapZ = Math.Min(playerMax.Z - blockMin.Z, blockMax.Z - playerMin.Z);

                            // Determina o eixo com a menor sobreposição (eixo principal da colisão)
                            ClipXCollide();
                            ClipYCollide();
                            ClipZCollide();
                        }
                    }
                }
            }
        }
    }

    public void ClipXCollide() {
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
    }

    public void ClipYCollide() {
        if(overlapY < overlapX && overlapY < overlapZ) {
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
    }

    public void ClipZCollide() {
        if(overlapZ < overlapX && overlapZ < overlapY) {
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
