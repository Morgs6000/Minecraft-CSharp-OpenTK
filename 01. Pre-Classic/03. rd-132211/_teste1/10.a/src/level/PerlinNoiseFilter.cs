namespace RubyDung.src.level {
    public class PerlinNoiseFilter {
        private Random random = new Random();
        private int seed;
        private int levels;
        private int fuzz;

        public PerlinNoiseFilter(int levels) {
            this.seed = this.random.Next();
            this.levels = 0;
            this.fuzz = 16;
            this.levels = levels;
        }

        public int[] read(int width, int height) {
            Random random = new Random();
            int[] tmp = new int[width * height];
            int level = this.levels;
            int step = width >> level;

            for(int val = 0; val < height; val += step) {
                for(int ss = 0; ss < width; ss += step) {
                    tmp[ss + val * width] = (random.Next(256) - 128) * this.fuzz;
                }
            }

            for(step = width >> level; step > 1; step /= 2) {
                int val = 256 * (step << level);
                int ss = step / 2;

                for(int y = 0; y < height; y += step) {
                    for(int x = 0; x < width; x += step) {
                        int c = tmp[(x + 0) % width + (y + 0) % height * width];
                        int r = tmp[(x + step) % width + (y + 0) % height * width];
                        int d = tmp[(x + 0) % width + (y + step) % height * width];
                        int mu = tmp[(x + step) % width + (y + step) % height * width];
                        int ml = (c + d + r + mu) / 4 + random.Next(val * 2) - val;
                        tmp[x + ss + (y + ss) * width] = ml;
                    }
                }

                for(int y = 0; y < height; y += step) {
                    for(int x = 0; x < width; x += step) {
                        int c = tmp[x + y * width];
                        int r = tmp[(x + step) % width + y * width];
                        int d = tmp[x + (y + step) % width * width];
                        int mu = tmp[(x + ss & width - 1) + (y + ss - step & height - 1) * width];
                        int ml = tmp[(x + ss - step & width - 1) + (y + ss & height - 1) * width];
                        int m = tmp[(x + ss) % width + (y + ss) % height * width];
                        int u = (c + r + m + mu) / 4 + random.Next(val * 2) - val;
                        int l = (c + d + m + ml) / 4 + random.Next(val * 2) - val;
                        tmp[x + ss + y * width] = u;
                        tmp[x + (y + ss) * width] = l;
                    }
                }
            }

            int[] result = new int[width * height];

            for(int val = 0; val < height; ++val) {
                for(int ss = 0; ss < width; ++ss) {
                    result[ss + val * width] = tmp[ss % width + val % height * width] / 512 + 128;
                }
            }

            return result;
        }
    }
}
