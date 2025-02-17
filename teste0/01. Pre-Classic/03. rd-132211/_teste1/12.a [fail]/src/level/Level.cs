using RubyDung.src.phys;

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

        for(int x = 0; x < w; x++) {
            for(int y = 0; y < h; y++) {
                for(int z = 0; z < d; z++) {
                    int i = (y * depth + z) * width + x;
                    blocks[i] = (byte)(y <= h * 2 / 3 ? 1 : 0);
                }
            }
        }
    }

    public bool IsTile(int x, int y, int z) {
        if(x >= 0 && y >= 0 && z >= 0 && x < width && y < height && z < depth) {
            return blocks[(y * depth + z) * width + x] == 1;
        }
        else {
            return false;
        }
    }

    public bool IsSolidTile(int x, int y, int z) {
        return IsTile(x, y, z);
    }

    public List<AABB> GetCubes(AABB aabb) {
        List<AABB> aabbs = new List<AABB>();
        int x0 = (int)aabb.x0;
        int x1 = (int)(aabb.x1 + 1.0f);
        int y0 = (int)aabb.y0;
        int y1 = (int)(aabb.y1 + 1.0f);
        int z0 = (int)aabb.z0;
        int z1 = (int)(aabb.z1 + 1.0f);

        if(x0 < 0) {
            x0 = 0;
        }

        if(y0 < 0) {
            y0 = 0;
        }

        if(z0 < 0) {
            z0 = 0;
        }

        if(x1 > this.width) {
            x1 = this.width;
        }

        if(y1 > this.depth) {
            y1 = this.depth;
        }

        if(z1 > this.height) {
            z1 = this.height;
        }

        for(int x = x0; x < x1; ++x) {
            for(int y = y0; y < y1; ++y) {
                for(int z = z0; z < z1; ++z) {
                    if(this.IsSolidTile(x, y, z)) {
                        aabbs.Add(new AABB(x, y, z, x + 1, y + 1, z + 1));
                    }
                }
            }
        }

        return aabbs;
    }
}
