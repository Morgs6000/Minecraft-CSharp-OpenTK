using OpenTK.Mathematics;

namespace RubyDung;

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