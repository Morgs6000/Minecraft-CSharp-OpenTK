namespace RubyDung.src.level {
    public class Tile {
        public static Tile tile = new Tile();

        public void render(Tesselator t) {
            t.vertex(-0.5f, -0.5f,  0.0f); //Bottom-left vertex
            t.vertex( 0.0f,  0.5f,  0.0f); //Top vertex
            t.vertex( 0.5f, -0.5f,  0.0f); //Bottom-right vertex
        }
    }
}
