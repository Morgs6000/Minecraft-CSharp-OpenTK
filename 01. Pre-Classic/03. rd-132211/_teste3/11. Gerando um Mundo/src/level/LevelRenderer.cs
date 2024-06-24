using OpenTK.Windowing.GraphicsLibraryFramework;

namespace RubyDung.src.level;

public class LevelRenderer {
    private Level level;
    private Chunk[] chunks;

    private int xChunks;
    private int yChunks;
    private int zChunks;

    public LevelRenderer(Level level) {
        this.level = level;

        this.xChunks = level.width / 16;
        this.yChunks = level.height / 16;
        this.zChunks = level.depth / 16;

        this.chunks = new Chunk[this.xChunks * this.yChunks * this.zChunks];

        for(int x = 0; x < this.xChunks; ++x) {
            for(int y = 0; y < this.yChunks; ++y) {
                for(int z = 0; z < this.zChunks; ++z) {
                    int x0 = x * 16;
                    int y0 = y * 16;
                    int z0 = z * 16;

                    int x1 = (x + 1) * 16;
                    int y1 = (y + 1) * 16;
                    int z1 = (z + 1) * 16;

                    this.chunks[(x + y * this.xChunks) * this.zChunks + z] = new Chunk(level, x0, y0, z0, x1, y1, z1);
                }
            }
        }
    }

    public void render() {
        for(int i = 0; i < this.chunks.Length; i++) {
            this.chunks[i].render();
        }
    }

    public void render_tesseletor(KeyboardState input, Shader shader) {
        for(int i = 0; i < this.chunks.Length; i++) {
            this.chunks[i].render_tesseletor(input, shader);
        }
    }
}
