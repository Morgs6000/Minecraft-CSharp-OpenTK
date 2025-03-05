using System.Numerics;

namespace RubyDung;

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
        //int index = (chunkY * zChunks + chunkZ) * xChunks + chunkX;

        if(index >= 0 && index < chunks.Length) {
            chunks[index].OnLoad(); // Recarrega a chunk
        }
    }
}
