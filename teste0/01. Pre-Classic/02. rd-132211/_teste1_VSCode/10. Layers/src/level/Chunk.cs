namespace RubyDung;

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

    public void OnLoad() {
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

    public void OnRenderFrame() {
        t.OnRenderFrame();
    }
}