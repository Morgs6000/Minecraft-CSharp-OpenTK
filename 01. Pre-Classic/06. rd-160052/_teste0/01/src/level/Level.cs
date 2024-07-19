using RubyDung.src.level.tile;

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

        bool mapLoaded = false;

        if(!mapLoaded) {
            this.generateMap();
        }
    }

    private void generateMap() {
        int w = this.width;
        int h = this.height;
        int d = this.depth;

        for(int x = 0; x < w; x++) {
            for(int y = 0; y < h; y++) {
                for(int z = 0; z < d; z++) {
                    int i = (y * this.depth + z) * this.width + x;
                    int id = 0;

                    if(y == h * 2 / 3) {
                        id = Tile.rock.id;
                    }
                    //if(y <= h * 2 / 3) {
                    //    id = Tile.rock.id;
                    //}

                    //this.blocks[i] = (byte)(y <= h * 2 / 3 ? 1 : 2);
                    //this.blocks[i] = (byte)5;
                    this.blocks[i] = (byte)id;
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

    public int getTile(int x, int y, int z) {
        return x >= 0 && y >= 0 && z >= 0 && x < this.width && y < this.height && z < this.depth ? this.blocks[(y * this.depth + z) * this.width + x] : 0;
    }

    public bool isSolidTile(int x, int y, int z) {
        return this.isTile(x, y, z);
    }
}
