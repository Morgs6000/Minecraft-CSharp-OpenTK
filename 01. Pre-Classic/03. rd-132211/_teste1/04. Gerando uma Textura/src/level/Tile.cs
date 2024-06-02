namespace RubyDung.src.level {
    public class Tile {
        public static Tile tile = new Tile();

        private int tex = 0;

        public void render(Tesselator t) {
            float u0 = (float)this.tex / 16.0f;
            float u1 = u0 + (1.0f / 16.0f);
            float v0 = ((16.0f - 1.0f)) / 16.0f;
            float v1 = v0 + (1.0f / 16.0f);

            t.vertex(-0.5f, -0.5f,  0.0f); // bottom left
            t.vertex(-0.5f,  0.5f,  0.0f); // top left 
            t.vertex( 0.5f,  0.5f,  0.0f); // top right
            t.vertex( 0.5f, -0.5f,  0.0f); // bottom right

            t.triangle();
            t.tex(u0, u1, v0, v1);
        }
    }
}
