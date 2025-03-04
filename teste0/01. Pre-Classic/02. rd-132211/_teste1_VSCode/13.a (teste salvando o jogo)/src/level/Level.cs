using System.IO.Compression;

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
                    int i = (y * depth + z) * width + x;
                    blocks[i] = (byte)(y <= h * 2 / 3 ? 1 : 0);
                }
            }
        }

        Load();
    }

    public void Load() {
        try {
            BinaryReader dis = new BinaryReader(new GZipStream(new FileStream("level.dat", FileMode.Open), CompressionMode.Decompress));
            dis.Read(blocks, 0, blocks.Length);
            dis.Close();
        }
        catch(Exception e) {
            Console.WriteLine(e.StackTrace);
        }
    }

    public void Save() {
        try {
            BinaryWriter dos = new BinaryWriter(new GZipStream(new FileStream("level.dat", FileMode.Create), CompressionMode.Compress));
            dos.Write(blocks);
            dos.Close();
        }
        catch(Exception e) {
            Console.WriteLine(e.StackTrace);
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
            blocks[(y * depth + z) * width + x] = id;
        }
    }
}
