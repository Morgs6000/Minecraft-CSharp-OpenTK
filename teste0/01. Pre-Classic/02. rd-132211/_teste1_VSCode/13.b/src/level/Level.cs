namespace RubyDung;

public class Level {
    public readonly int width;
    public readonly int height;
    public readonly int depth;

    private byte[] blocks;
    private int[] lightDepths;

    public Level(int w, int h, int d) {
        width = w;
        height = h;
        depth = d;

        blocks = new byte[w * h * d];
        lightDepths = new int[w * d];

        for(int x = 0; x < w; x++) {
            for(int y = 0; y < h; y++) {
                for(int z = 0; z < d; z++) {
                    // int i = (y * depth + z) * width + x;
                    int i = (x + y * width) * depth + z;
                    blocks[i] = (byte)(y <= h * 2 / 3 ? 1 : 0);
                }
            }
        }
    }

    public bool IsTile(int x, int y, int z) {
        if(x >= 0 && y >= 0 && z >= 0 && x < width && y < height && z < depth) {
            // return blocks[(y * depth + z) * width + x] == 1;
            return blocks[(x + y * width) * depth + z] == 1;
        }
        else {
            return false;
        }
    }

    public bool IsSolidTile(int x, int y, int z) {
        return IsTile(x, y, z);
    }

    public float GetBrightness(int x, int y, int z) {
        float dark = 0.8f;
        float light = 1.0f;

        if(x >= 0 && y >= 0 && z >= 0 && x < width && y < height && z < depth) {
            return y < lightDepths[x + z * this.width] ? dark : light;
        }
        else {
            return light;
        }
    }

    public void SetTile(int x, int y, int z, byte id) {
        if(x >= 0 && y >= 0 && z >= 0 && x < width && y < height && z < depth) {
            // blocks[(y * depth + z) * width + x] = id;
            blocks[(x + y * width) * depth + z] = id;
        }
    }
}
