namespace RubyDung.src.level {
    internal class Tile {
        public void render(Tesselator t) {
            t.vertex(-0.5f, -0.5f, 0.0f);
            t.vertex(-0.5f,  0.5f, 0.0f);
            t.vertex( 0.5f,  0.5f, 0.0f);
            t.vertex( 0.5f, -0.5f, 0.0f);

            t.triangle();
        }
    }
}
