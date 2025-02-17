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

        int[] heightmap1 = (new PerlinNoiseFilter(0)).Read(w, d);
        int[] heightmap2 = (new PerlinNoiseFilter(0)).Read(w, d);
        int[] cf = (new PerlinNoiseFilter(1)).Read(w, d);
        int[] rockMap = (new PerlinNoiseFilter(1)).Read(w, d);

        for(int x = 0; x < w; x++) {
            for(int y = 0; y < h; y++) {
                for(int z = 0; z < d; z++) {
                    int dh1 = heightmap1[x + z * width];
                    int dh2 = heightmap2[x + z * width];
                    int cfh = cf[x + z * width];
                    if(cfh < 128) {
                        dh2 = dh1;
                    }

                    int dh = dh1;
                    if(dh2 > dh) {
                        dh = dh2;
                    }

                    dh = dh / 8 + h / 3;
                    int rh = rockMap[x + z * width] / 8 + h / 3;
                    if(rh > dh - 2) {
                        rh = dh - 2;
                    }

                    int i = (y * depth + z) * width + x;
                    int id = 0;

                    if(y == dh) {
                        id = Tile.grass.id;
                    }
                    if(y < dh) {
                        id = Tile.dirt.id;
                    }
                    if(y <= rh) {
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
