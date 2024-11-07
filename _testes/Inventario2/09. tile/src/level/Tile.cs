namespace RubyDung.src.level;

public class Tile {
    public static Tile tile = new Tile();

    private int tex = 0;

    public void render(Tesselator t) {
        float x0 = -0.5f;
        float y0 = -0.5f;

        float x1 = 0.5f;
        float y1 = 0.5f;

        float u0 = (float)this.tex / 16.0f;
        float v0 = (16.0f - 1.0f) / 16.0f;

        float u1 = u0 + (1.0f / 16.0f);
        float v1 = v0 + (1.0f / 16.0f);

        t.tex(u0, v0);
        t.vertex(x0, y0);
        t.tex(u1, v0);
        t.vertex(x1, y0);
        t.tex(u1, v1);
        t.vertex(x1, y1);
        t.tex(u0, v1);
        t.vertex(x0, y1);
    }
}
