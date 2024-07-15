namespace RubyDung.src.level;

public class Tile {
    public static Tile triangle = new Tile();

    public void render(Tesselator t) {
        t.vertex(-0.5f, -0.5f); // left
        t.vertex( 0.5f, -0.5f); // right
        t.vertex( 0.0f,  0.5f); // top
    }
}
