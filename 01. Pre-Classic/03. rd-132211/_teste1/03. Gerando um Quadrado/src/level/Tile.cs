namespace RubyDung.src.level {
    public class Tile {
        public static Tile tile = new Tile();

        public void render(Tesselator t) {
            t.vertex(-0.5f, -0.5f,  0.0f); // bottom left
            t.vertex(-0.5f,  0.5f,  0.0f); // top left 
            t.vertex( 0.5f,  0.5f,  0.0f); // top right
            t.vertex( 0.5f, -0.5f,  0.0f); // bottom right

            t.triangle();
        }
    }
}
