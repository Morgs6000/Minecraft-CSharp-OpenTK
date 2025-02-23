﻿using OpenTK.Mathematics;
using RubyDung.src.level;

namespace RubyDung.src.phys;

public class AABB {
    // Dimensões do jogador (AABB)
    private float playerWidth = 0.3f;  // Largura do jogador
    private float playerHeight = 0.9f; // Altura do jogador

    // Limites do jogador (AABB)
    private Vector3 playerMin; // Canto mínimo do jogador
    private Vector3 playerMax; // Canto máximo do jogador

    private Vector3 blockMin; // Canto mínimo do bloco
    private Vector3 blockMax; // Canto máximo do bloco

    // Calcula a profundidade da colisão em cada eixo
    private float overlapX;
    private float overlapY;
    private float overlapZ;

    private Player player;
    private Level level;

    public AABB(Player player, Level level) {
        this.player = player;
        this.level = level;
    }

    public void CheckCollision() {
        // Limites do jogador (AABB)
        playerMin = player.GetEye() - new Vector3(playerWidth, playerHeight, playerWidth); // Canto mínimo do jogador
        playerMax = player.GetEye() + new Vector3(playerWidth, playerHeight, playerWidth); // Canto máximo do jogador

        // Verifica colisão com blocos próximos ao jogador
        for(int x = (int)playerMin.X; x <= (int)playerMax.X; x++) {
            for(int y = (int)playerMin.Y; y <= (int)playerMax.Y; y++) {
                for(int z = (int)playerMin.Z; z <= (int)playerMax.Z; z++) {
                    if(level.IsSolidTile(x, y, z)) {
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

    private void ClipXCollide() {
        if(overlapX < overlapY && overlapX < overlapZ) {
            // Colisão no eixo X
            if(playerMax.X > blockMin.X && playerMin.X < blockMin.X) {
                Console.WriteLine("Colisão com a face X0 (esquerda) do bloco.");
                player.SetEyeX(blockMin.X - playerWidth); // Resposta à colisão
                //player.SetEye(Vector3.UnitX * (blockMin.X - playerWidth)); // Resposta à colisão
            }
            else if(playerMin.X < blockMax.X && playerMax.X > blockMax.X) {
                Console.WriteLine("Colisão com a face X1 (direita) do bloco.");
                player.SetEyeX(blockMax.X + playerWidth); // Resposta à colisão
                //player.SetEye(Vector3.UnitX * (blockMax.X + playerWidth)); // Resposta à colisão
            }
        }
    }

    private void ClipYCollide() {
        if(overlapY < overlapX && overlapY < overlapZ) {
            // Colisão no eixo Y
            if(playerMax.Y > blockMin.Y && playerMin.Y < blockMin.Y) {
                Console.WriteLine("Colisão com a face Y0 (inferior) do bloco.");
                player.SetEyeY(blockMin.Y - playerHeight); // Resposta à colisão
                //player.SetEye(Vector3.UnitY * (blockMin.Y - playerHeight)); // Resposta à colisão
            }
            else if(playerMin.Y < blockMax.Y && playerMax.Y > blockMax.Y) {
                Console.WriteLine("Colisão com a face Y1 (superior) do bloco.");
                player.SetEyeY(blockMax.Y + playerHeight); // Resposta à colisão
                //player.SetEye(Vector3.UnitY * (blockMax.Y + playerHeight)); // Resposta à colisão
            }
        }
    }

    private void ClipZCollide() {
        if(overlapZ < overlapX && overlapZ < overlapY) {
            // Colisão no eixo Z
            if(playerMax.Z > blockMin.Z && playerMin.Z < blockMin.Z) {
                Console.WriteLine("Colisão com a face Z0 (frontal) do bloco.");
                player.SetEyeZ(blockMin.Z - playerWidth); // Resposta à colisão
                //player.SetEye(Vector3.UnitZ * (blockMin.Z - playerWidth)); // Resposta à colisão
            }
            else if(playerMin.Z < blockMax.Z && playerMax.Z > blockMax.Z) {
                Console.WriteLine("Colisão com a face Z1 (traseira) do bloco.");
                player.SetEyeZ(blockMax.Z + playerWidth); // Resposta à colisão
                //player.SetEye(Vector3.UnitZ * (blockMax.Z + playerWidth)); // Resposta à colisão
            }
        }
    }
}
