namespace RubyDung.src.level;

public class Level {
    public int width;
    public int height;
    public int depth;

    private byte[] blocks;

    public Level(int w, int h, int d) {
        this.width = w;
        this.height = h;
        this.depth = d;

        this.blocks = new byte[w * h * d];

        for(int x = 0; x < w; x++) {
            for(int y = 0; y < h; y++) {
                for(int z = 0; z < d; z++) {
                    int i = (y * this.depth + z) * this.width + x;

                    this.blocks[i] = 1;
                }
            }
        }
    }

    public bool isTile(int x, int y, int z) {
        if(x >= 0 && y >= 0 && z >= 0 && x < this.width && y < this.height && z < this.depth) {
            return this.blocks[(y * this.depth + z) * this.width + x] == 1;
        }
        else {
            return false;
        }
    }

    public bool isSolidTile(int x, int y, int z) {
        return this.isTile(x, y, z);
    }
}
