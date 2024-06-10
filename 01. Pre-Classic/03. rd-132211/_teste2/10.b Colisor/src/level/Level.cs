using RubyDung.src.phys;
using System.Collections;

namespace RubyDung.src.level;

public class Level {
    public int width;
    public int height;
    public int depth;
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

    public bool isTile(int x, int y, int z) {
        if(x >= 0 && y >= 0 && z >= 0 && x < width && y < height && z < depth) {
            return blocks[(y * depth + z) * width + x] == 1;
        }
        else {
            return false;
        }
    }

    public bool isSolidTile(int x, int y, int z) {
        return isTile(x, y, z);
    }

    public List<AABB> getCubes(AABB aABB) {
        List<AABB> aABBs = new List<AABB>();
        int x0 = (int)aABB.x0;
        int x1 = (int)(aABB.x1 + 1.0F);
        int y0 = (int)aABB.y0;
        int y1 = (int)(aABB.y1 + 1.0F);
        int z0 = (int)aABB.z0;
        int z1 = (int)(aABB.z1 + 1.0F);
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
                    if(this.isSolidTile(x, y, z)) {
                        aABBs.Add(new AABB((float)x, (float)y, (float)z, (float)(x + 1), (float)(y + 1), (float)(z + 1)));
                    }
                }
            }
        }

        return aABBs;
    }
}
