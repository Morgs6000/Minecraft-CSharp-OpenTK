namespace RubyDung;

public class Tile {
    public static Tile tile = new Tile();
    
    public void OnLoad(Tesselator t) {
        float u0 = 0.0f;
        float v0 = (16.0f - 1.0f) / 16.0f;

        float u1 = u0 + (1.0f / 16.0f);
        float v1 = v0 + (1.0f / 16.0f);

        t.Vertex(-0.5f, -0.5f);
        t.Vertex( 0.5f, -0.5f);
        t.Vertex( 0.5f,  0.5f);
        t.Vertex(-0.5f,  0.5f);
        t.Indice();
        t.Tex(u0, v0);
        t.Tex(u1, v0);
        t.Tex(u1, v1);
        t.Tex(u0, v1);
    }
}