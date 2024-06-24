using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace RubyDung.src.level;

public class Chunk {
    public Level level;

    public int x0;
    public int y0;
    public int z0;

    public int x1;
    public int y1;
    public int z1;

    private Tesselator t = new Tesselator();

    public void loadChunk() {
        this.level = new Level(16, 16, 16);
        this.load(0, 0, 0, 16, 16, 16);
    }

    public void load(int x0, int y0, int z0, int x1, int y1, int z1) {
        for(int x = x0; x < x1; x++) {
            for(int y = y0; y < y1; y++) {
                for(int z = z0; z < z1; z++) {
                    if(this.level.isTile(x, y, z)) {
                        bool tex = y != this.level.height * 2 / 3;

                        if(tex) {
                            Tile.rock.render(this.t, this.level, x, y, z);
                        }
                        else {
                            Tile.grass.render(this.t, this.level, x, y, z);
                        }
                    }
                }
            }
        }

        this.t.flush();
    }

    public void bind() {
        this.t.bind();
    }
}

