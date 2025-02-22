namespace RubyDung.src.level;

public class Tile {
    public static Tile tile = new Tile(0);

    private int tex = 0;

    private Tile(int tex) {
        this.tex = tex;
    }

    public void Load(Tesselator tesselator) {
        float u0 = (float)tex / 16.0f;
        float u1 = u0 + (1.0f / 16.0f);
        float v0 = (16.0f - 1.0f) / 16.0f;
        float v1 = v0 + (1.0f / 16.0f);

        tesselator.Vertex(-0.5f, -0.5f,  0.0f);
        tesselator.Vertex( 0.5f, -0.5f,  0.0f);
        tesselator.Vertex( 0.5f,  0.5f,  0.0f);
        tesselator.Vertex(-0.5f,  0.5f,  0.0f);

        tesselator.Indice();

        tesselator.Tex(u0, v0);
        tesselator.Tex(u1, v0);
        tesselator.Tex(u1, v1);
        tesselator.Tex(u0, v1);
    }
}
