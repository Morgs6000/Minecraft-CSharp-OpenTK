namespace RubyDung.src.level;

public class PerlinNoiseFilter {
    Random random = new Random();
    int seed;
    int levels;
    int fuzz;

    public PerlinNoiseFilter(int levels) {
        this.seed = random.Next();
        this.levels = 0;
        this.fuzz = 16;
        this.levels = levels;
    }

    public int[] Read(int width, int depth) {
        Random random = new Random();
        int[] tmp = new int[width * depth];
        int level = this.levels;
        int step = width >> level;

        int val;
        int ss;
        for(val = 0; val < depth; val += step) {
            for(ss = 0; ss < width; ss += step) {
                tmp[ss + val * width] = (random.Next(256) - 128) * fuzz;
            }
        }

        for(step = width >> level; step > 1; step /= 2) {
            val = 256 * (step << level);
            ss = step / 2;

            int z;
            int x;
            int c;
            int r;
            int d;
            int mu;
            int ml;
            for(z = 0; z < depth; z += step) {
                for(x = 0; x < width; x += step) {
                    c = tmp[(x + 0) % width + (z + 0) % depth * width];
                    r = tmp[(x + step) % width + (z + 0) % depth * width];
                    d = tmp[(x + 0) % width + (z + step) % depth * width];
                    mu = tmp[(x + step) % width + (z + step) % depth * width];
                    ml = (c + d + r + mu) / 4 + random.Next(val * 2) - val;
                    tmp[x + ss + (z + ss) * width] = ml;
                }
            }

            for(z = 0; z < depth; z += step) {
                for(x = 0; x < width; x += step) {
                    c = tmp[x + z * width];
                    r = tmp[(x + step) % width + z * width];
                    d = tmp[x + (z + step) % width * width];
                    mu = tmp[(x + ss & width - 1) + (z + ss - step & depth - 1) * width];
                    ml = tmp[(x + ss - step & width - 1) + (z + ss & depth - 1) * width];
                    int m = tmp[(x + ss) % width + (z + ss) % depth * width];
                    int u = (c + r + m + mu) / 4 + random.Next(val * 2) - val;
                    int l = (c + d + m + ml) / 4 + random.Next(val * 2) - val;
                    tmp[x + ss + z * width] = u;
                    tmp[x + (z + ss) * width] = l;
                }
            }
        }

        int[] result = new int[width * depth];

        for(val = 0; val < depth; ++val) {
            for(ss = 0; ss < width; ++ss) {
                result[ss + val * width] = tmp[ss % width + val % depth * width] / 512 + 128;
            }
        }

        return result;
    }
}
