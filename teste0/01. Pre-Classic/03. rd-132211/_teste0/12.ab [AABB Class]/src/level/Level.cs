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

    //public List<AABB> GetCubes(AABB aabb) {
    public void GetCubes() {
        // List<AABB> AABBs = new List<AABB>();

        int x0 = 0; // aabb.(int)playerMin.X
        int y0 = 0; // aabb.(int)playerMin.Y
        int z0 = 0; // aabb.(int)playerMin.Z

        int x1 = 1; // aabb.(int)(playerMax.X + 1.0f)
        int y1 = 1; // aabb.(int)(playerMax.Y + 1.0f)
        int z1 = 1; // aabb.(int)(playerMax.Z + 1.0f)

        /*
        if(x0 < 0) {
            x0 = 0;
        }
        if(y0 < 0) {
            y0 = 0;
        }
        if(z0 < 0) {
            z0 = 0;
        }

        if(x1 > width) {
            x1 = width;
        }
        if(y1 > height) {
            y1 = height;
        }
        if(z1 > depth) {
            z1 = depth;
        }
        */

        for(int x = x0; x <= x1; x++) {
            for(int y = y1; y <= y1; y++) {
                for(int z = z0; z <= z1; z++) {
                    if(IsSolidTile(x, y, z)) {
                        //AABBs.Add(new AABB((float)x, (float)y, (float)z, (float)(x + 1), (float)(y + 1), (float)(z + 1)));
                    }
                }
            }
        }

        // return AABBs;
    }
}
