namespace ConsoleApp1.src;

public class Tile {
    public static Tile[] tiles = new Tile[256];

    public static Tile rock = new Tile(1, 1);

    private int tex = 0;

    public int id;

    protected Tile(int id) {
        tiles[id] = this;
        this.id = id;
    }

    protected Tile(int id, int tex) {
        this.tex = tex;
    }

    public void render(Tesselator t) {
        float x0 = -0.5f;
        float y0 = -0.5f;
        float z0 = -0.5f;

        float x1 = 0.5f;
        float y1 = 0.5f;
        float z1 = 0.5f;

        float u0 = (float)this.tex / 16.0f;
        float v0 = (16.0f - 1.0f) / 16.0f;

        float u1 = u0 + (1.0f / 16.0f);
        float v1 = v0 + (1.0f / 16.0f);

        // x0
        t.tex(u0, v0);
        t.vertex(x0, y0, z0);
        t.tex(u1, v0);
        t.vertex(x0, y0, z1);
        t.tex(u1, v1);
        t.vertex(x0, y1, z1);
        t.tex(u0, v1);
        t.vertex(x0, y1, z0);

        // x1
        t.tex(u0, v0);
        t.vertex(x1, y0, z1);
        t.tex(u1, v0);
        t.vertex(x1, y0, z0);
        t.tex(u1, v1);
        t.vertex(x1, y1, z0);
        t.tex(u0, v1);
        t.vertex(x1, y1, z1);

        // y0
        t.tex(u0, v0);
        t.vertex(x0, y0, z0);
        t.tex(u1, v0);
        t.vertex(x1, y0, z0);
        t.tex(u1, v1);
        t.vertex(x1, y0, z1);
        t.tex(u0, v1);
        t.vertex(x0, y0, z1);

        // y1
        t.tex(u0, v0);
        t.vertex(x0, y1, z1);
        t.tex(u1, v0);
        t.vertex(x1, y1, z1);
        t.tex(u1, v1);
        t.vertex(x1, y1, z0);
        t.tex(u0, v1);
        t.vertex(x0, y1, z0);

        // z0
        t.tex(u0, v0);
        t.vertex(x1, y0, z0);
        t.tex(u1, v0);
        t.vertex(x0, y0, z0);
        t.tex(u1, v1);
        t.vertex(x0, y1, z0);
        t.tex(u0, v1);
        t.vertex(x1, y1, z0);

        // z1
        t.tex(u0, v0);
        t.vertex(x0, y0, z1);
        t.tex(u1, v0);
        t.vertex(x1, y0, z1);
        t.tex(u1, v1);
        t.vertex(x1, y1, z1);
        t.tex(u0, v1);
        t.vertex(x0, y1, z1);
    }
}
