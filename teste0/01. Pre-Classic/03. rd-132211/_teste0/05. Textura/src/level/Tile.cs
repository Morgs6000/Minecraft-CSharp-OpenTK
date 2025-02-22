namespace RubyDung.src.level;

public class Tile {
    public static Tile tile = new Tile();

    public void Load(Tesselator tesselator) {
        tesselator.Vertex(-0.5f, -0.5f,  0.0f);
        tesselator.Vertex( 0.5f, -0.5f,  0.0f);
        tesselator.Vertex( 0.5f,  0.5f,  0.0f);
        tesselator.Vertex(-0.5f,  0.5f,  0.0f);

        tesselator.Indice();

        tesselator.Tex(0.0f, 0.0f);
        tesselator.Tex(1.0f, 0.0f);
        tesselator.Tex(1.0f, 1.0f);
        tesselator.Tex(0.0f, 1.0f);
    }
}
