using OpenTK.Mathematics;

namespace RubyDung.src.level;

public class Tile {
    public static Tile rock = new Tile(new Vector2(1, 0));
    public static Tile grass = new Tile(new Vector2(0, 0));

    private Vector2 tex;

    private Tile(Vector2 tex) {
        this.tex = tex;
    }

    public void render(Tesselator t, Level level, int x, int y, int z) {
        float x0 = (float)x + 0.0f;
        float y0 = (float)y + 0.0f;
        float z0 = (float)z + 0.0f;

        float x1 = (float)x + 1.0f;
        float y1 = (float)y + 1.0f;
        float z1 = (float)z + 1.0f;

        // ..:: Negative X ::..
        if(!level.isSolidTile(x - 1, y, z)) {
            t.vertex(x0, y0, z0);
            t.vertex(x0, y1, z0);
            t.vertex(x0, y1, z1);
            t.vertex(x0, y0, z1);

            t.triangle();
            t.tex(this.tex.X, this.tex.Y);
        }

        // ..:: Positive X ::..
        if(!level.isSolidTile(x + 1, y, z)) {
            t.vertex(x1, y0, z1);
            t.vertex(x1, y1, z1);
            t.vertex(x1, y1, z0);
            t.vertex(x1, y0, z0);

            t.triangle();
            t.tex(this.tex.X, this.tex.Y);
        }

        // ..:: Negative Y ::..
        if(!level.isSolidTile(x, y - 1, z)) {
            t.vertex(x0, y0, z0);
            t.vertex(x0, y0, z1);
            t.vertex(x1, y0, z1);
            t.vertex(x1, y0, z0);

            t.triangle();
            t.tex(this.tex.X, this.tex.Y);
        }

        // ..:: Positive Y ::..
        if(!level.isSolidTile(x, y + 1, z)) {
            t.vertex(x0, y1, z1);
            t.vertex(x0, y1, z0);
            t.vertex(x1, y1, z0);
            t.vertex(x1, y1, z1);

            t.triangle();
            t.tex(this.tex.X, this.tex.Y);
        }

        // ..:: Negative Z ::..
        if(!level.isSolidTile(x, y, z - 1)) {
            t.vertex(x1, y0, z0);
            t.vertex(x1, y1, z0);
            t.vertex(x0, y1, z0);
            t.vertex(x0, y0, z0);

            t.triangle();
            t.tex(this.tex.X, this.tex.Y);
        }

        // ..:: Positive Z ::..
        if(!level.isSolidTile(x, y, z + 1)) {
            t.vertex(x0, y0, z1);
            t.vertex(x0, y1, z1);
            t.vertex(x1, y1, z1);
            t.vertex(x1, y0, z1);

            t.triangle();
            t.tex(this.tex.X, this.tex.Y);
        }
    }
}
