namespace RubyDung.src.level {
    public class Level {
        public int width;
        public int height;
        public int depth;
        private byte[] blocks;

        public Level(int w, int h, int d) {
            this.width = w;
            this.height = h;
            this.depth = d;

            int[] heightmap1 = (new PerlinNoiseFilter(0)).read(w, h);
            int[] heightmap2 = (new PerlinNoiseFilter(0)).read(w, h);
            int[] cf = (new PerlinNoiseFilter(1)).read(w, h);
            int[] rockMap = (new PerlinNoiseFilter(1)).read(w, h);

            this.blocks = new byte[w * h * d];

            for(int x = 0; x < w; ++x) {
                for(int y = 0; y < h; ++y) {
                    for(int z = 0; z < d; ++z) {
                        int dh1 = heightmap1[x + z * this.width];
                        int dh2 = heightmap2[x + z * this.width];
                        int cfh = cf[x + z * this.width];
                        if(cfh < 128) {
                            dh2 = dh1;
                        }

                        int dh = dh1;
                        if(dh2 > dh) {
                            dh = dh2;
                        }

                        dh = dh / 8 + d / 3;
                        int rh = rockMap[x + z * this.width] / 8 + d / 3;
                        if(rh > dh - 2) {
                            rh = dh - 2;
                        }

                        int i = (y * this.height + z) * this.width + x;
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
            return x >= 0 && y >= 0 && z >= 0 && x < this.width && y < this.depth && z < this.height ? this.blocks[(y * this.height + z) * this.width + x] : 0;
        }

        public bool isSolidTile(int x, int y, int z) {
            return this.isTile(x, y, z);
        }
    }
}
