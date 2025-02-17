namespace RubyDung.src.level;

internal class LevelRenderer {
    private Level level;

    private Chunk[] chunks;

    private int xChunks;
    private int yChunks;
    private int zChunks;

    public LevelRenderer(Level level, Shader shader) {
        this.level = level;

        xChunks = level.width / 16;
        yChunks = level.depth / 16;
        zChunks = level.height / 16;

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
                    chunks[(x + y * xChunks) * zChunks + z].rebuild(shader);
                }
            }
        }
    }

    public void Render() {
        for(int i = 0; i < this.chunks.Length; i++) {
            this.chunks[i].Render();
        }
    }
}
