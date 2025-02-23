namespace RubyDung.src.level;

public class Tile {
    public static Tile tile = new Tile();

    public void Load(Tesselator t) {
        float x0 = 0.0f;
        float y0 = 0.0f;
        float z0 = 0.0f;

        float x1 = 1.0f;
        float y1 = 1.0f;
        float z1 = 1.0f;

        float u0 = (float)0 / 16.0f;
        float v0 = (16.0f - 1.0f) / 16.0f;

        float u1 = u0 + (1.0f / 16.0f);
        float v1 = v0 + (1.0f / 16.0f);

        // x0
        t.Vertex(x0, y0, z0);
        t.Vertex(x0, y0, z1);
        t.Vertex(x0, y1, z1);
        t.Vertex(x0, y1, z0);

        t.Indice();

        t.Tex(u0, v0);
        t.Tex(u1, v0);
        t.Tex(u1, v1);
        t.Tex(u0, v1);

        // x1
        t.Vertex(x1, y0, z1);
        t.Vertex(x1, y0, z0);
        t.Vertex(x1, y1, z0);
        t.Vertex(x1, y1, z1);

        t.Indice();

        t.Tex(u0, v0);
        t.Tex(u1, v0);
        t.Tex(u1, v1);
        t.Tex(u0, v1);

        // y0
        t.Vertex(x0, y0, z0);
        t.Vertex(x1, y0, z0);
        t.Vertex(x1, y0, z1);
        t.Vertex(x0, y0, z1);

        t.Indice();

        t.Tex(u0, v0);
        t.Tex(u1, v0);
        t.Tex(u1, v1);
        t.Tex(u0, v1);

        // y1
        t.Vertex(x0, y1, z1);
        t.Vertex(x1, y1, z1);
        t.Vertex(x1, y1, z0);
        t.Vertex(x0, y1, z0);

        t.Indice();

        t.Tex(u0, v0);
        t.Tex(u1, v0);
        t.Tex(u1, v1);
        t.Tex(u0, v1);

        // z0
        t.Vertex(x1, y0, z0);
        t.Vertex(x0, y0, z0);
        t.Vertex(x0, y1, z0);
        t.Vertex(x1, y1, z0);

        t.Indice();

        t.Tex(u0, v0);
        t.Tex(u1, v0);
        t.Tex(u1, v1);
        t.Tex(u0, v1);

        // z1
        t.Vertex(x0, y0, z1);
        t.Vertex(x1, y0, z1);
        t.Vertex(x1, y1, z1);
        t.Vertex(x0, y1, z1);

        t.Indice();

        t.Tex(u0, v0);
        t.Tex(u1, v0);
        t.Tex(u1, v1);
        t.Tex(u0, v1);
    }
}
