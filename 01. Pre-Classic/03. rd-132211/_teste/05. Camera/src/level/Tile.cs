namespace RubyDung.src.level {
    internal class Tile {
        private int texX = 0;
        private int texY = 0;
        private int col = 16;
        private int row = 16;

        public void render(Tesselator t) {
            float u0 = texX / col;
            float u1 = u0 + (1.0f / col);
            float v0 = ((row - 1.0f) - texY) / row;
            float v1 = v0 + (1.0f / row);

            t.vertex(-0.5f, -0.5f,  0.0f);
            t.vertex(-0.5f,  0.5f,  0.0f);
            t.vertex( 0.5f,  0.5f,  0.0f);
            t.vertex( 0.5f, -0.5f,  0.0f);

            t.triangle();

            t.tex(u0, v0);
            t.tex(u0, v1);
            t.tex(u1, v1);
            t.tex(u1, v0);
        }
    }
}
