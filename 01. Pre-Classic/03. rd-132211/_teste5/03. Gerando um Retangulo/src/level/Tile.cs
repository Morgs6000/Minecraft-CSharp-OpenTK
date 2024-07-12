namespace RubyDung.src.level;

public class Tile
{
    public static Tile tile = new Tile();

    public void render(Tesselator t)
    {
        float x0 = -0.5f;
        float y0 = -0.5f;

        float x1 = 0.5f;
        float y1 = 0.5f;

        t.vertex(x0, y0);
        t.vertex(x1, y0);
        t.vertex(x1, y1);
        t.vertex(x0, y1);
    }
}
