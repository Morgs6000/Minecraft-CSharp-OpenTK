using RubyDung.src.level.tile;

namespace RubyDung.src.level;

public class Level {
    public readonly int width;
    public readonly int height;
    public readonly int depth;

    private byte[] blocks;

    public Level(int w, int h, int d) {
        width = w;
        height = h;
        depth = d;

        blocks = new byte[w * h * d];

        bool mapLoaded = false;

        if(!mapLoaded) {
            GenerateMap();
        }
    }

    private void GenerateMap() {
        int w = width;
        int h = height;
        int d = depth;

        for(int x = 0; x < w; x++) {
            for(int y = 0; y < h; y++) {
                for(int z = 0; z < d; z++) {
                    int i = (y * depth + z) * width + x;
                    int id = 0;

                    if(y == height * 2 / 3) {
                        id = Tile.grass.id;
                    }
                    if(y < height * 2 / 3) {
                        id = Tile.dirt.id;
                    }
                    if(y < (height * 2 / 3) - 5) {
                        id = Tile.rock.id;
                    }

                    blocks[i] = (byte)id;
                }
            }
        }
    }

    public int GetTile(int x, int y, int z) {
        return x >= 0 && y >= 0 && z >= 0 && x < width && y < height && z < depth ? blocks[(y * depth + z) * width + x] : 0;
    }

    public bool IsSolidTile(int x, int y, int z) {
        Tile tile = Tile.tiles[GetTile(x, y, z)];
        return tile == null ? false : tile.IsSolid();
    }
}
