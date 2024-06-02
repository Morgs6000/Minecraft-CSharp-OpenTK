namespace RubyDung.src.level {
    public class Tile {
        public static Tile empty = null;
        public static Tile rock = new Tile(0);
        public static Tile grass = new Tile(1);

        private int tex = 0;

        private Tile(int tex) {
            this.tex = tex;
        }

        public void render(Tesselator t, Level level, int x, int y, int z) {
            float x0 = (float)x + 0.0f;
            float y0 = (float)y + 0.0f;
            float z0 = (float)z + 0.0f;

            float x1 = (float)x + 1.0f;
            float y1 = (float)y + 1.0f;
            float z1 = (float)z + 1.0f;

            float u0 = (float)this.tex / 16.0f;
            float u1 = u0 + (1.0f / 16.0f);
            float v0 = ((16.0f - 1.0f)) / 16.0f;
            float v1 = v0 + (1.0f / 16.0f);

            // ..:: Negative X ::..
            if(!level.isSolidTile(x - 1, y, z)) {
                t.vertex(x0, y0, z0);
                t.vertex(x0, y1, z0);
                t.vertex(x0, y1, z1);
                t.vertex(x0, y0, z1);

                t.triangle();
                t.tex(u0, u1, v0, v1);
            }

            // ..:: Positive X ::..
            if(!level.isSolidTile(x + 1, y, z)) {
                t.vertex(x1, y0, z1);
                t.vertex(x1, y1, z1);
                t.vertex(x1, y1, z0);
                t.vertex(x1, y0, z0);

                t.triangle();
                t.tex(u0, u1, v0, v1);
            }

            // ..:: Negative Y ::..
            if(!level.isSolidTile(x, y - 1, z)) {
                t.vertex(x0, y0, z0);
                t.vertex(x0, y0, z1);
                t.vertex(x1, y0, z1);
                t.vertex(x1, y0, z0);

                t.triangle();
                t.tex(u0, u1, v0, v1);
            }

            // ..:: Positive Y ::..
            if(!level.isSolidTile(x, y + 1, z)) {
                t.vertex(x0, y1, z1);
                t.vertex(x0, y1, z0);
                t.vertex(x1, y1, z0);
                t.vertex(x1, y1, z1);

                t.triangle();
                t.tex(u0, u1, v0, v1);
            }

            // ..:: Negative Z ::..
            if(!level.isSolidTile(x, y, z - 1)) {
                t.vertex(x1, y0, z0);
                t.vertex(x1, y1, z0);
                t.vertex(x0, y1, z0);
                t.vertex(x0, y0, z0);

                t.triangle();
                t.tex(u0, u1, v0, v1);
            }

            // ..:: Positive Z ::..
            if(!level.isSolidTile(x, y, z + 1)) {
                t.vertex(x0, y0, z1);
                t.vertex(x0, y1, z1);
                t.vertex(x1, y1, z1);
                t.vertex(x1, y0, z1);

                t.triangle();
                t.tex(u0, u1, v0, v1);
            }
        }
    }
}
