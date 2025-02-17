namespace RubyDung.src.level;

public class Chunk {
    public readonly Level level;

    public readonly int x0;
    public readonly int y0;
    public readonly int z0;

    public readonly int x1;
    public readonly int y1;
    public readonly int z1;

    private static Tesselator t = new Tesselator();

    public Chunk(Level level, int x0, int y0, int z0, int x1, int y1, int z1) {
        this.level = level;

        this.x0 = x0;
        this.y0 = y0;
        this.z0 = z0;

        this.x1 = x1;
        this.y1 = y1;
        this.z1 = z1;
    }

    public void rebuild(Shader shader) {
        for(int x = x0; x < x1; x++) {
            for(int y = y0; y < y1; y++) {
                for(int z = z0; z < z1; z++) {
                    Tile.rock.render(t, level, x, y, z);
                }
            }
        }

        t.flush(shader);
    }

    public void Use() {
        t.Use();
    }
}
