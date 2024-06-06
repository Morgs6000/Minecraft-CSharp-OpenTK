using OpenTK.Mathematics;

namespace RubyDung.src.level.block;

public class BlockBeacon : Block {
    public BlockBeacon() {

    }

    protected virtual void renderFace(Tesselator t, int x, int y, int z, int face) {
        this.renderGlass(t, x, y, z);
        this.renderObsidian(t, x, y, z);
        this.renderBeacon(t, x, y, z);
    }

    public void renderGlass(Tesselator t, int x, int y, int z) {
        float x0 = x + 0.0f;
        float y0 = y + 0.0f;
        float z0 = z + 0.0f;

        float x1 = x + 1.0f;
        float y1 = y + 1.0f;
        float z1 = z + 1.0f;

        Vector2 tex = new Vector2(1, 3);

        // ..:: Negative X ::..
        t.vertex(x0, y0, z0);
        t.vertex(x0, y1, z0);
        t.vertex(x0, y1, z1);
        t.vertex(x0, y0, z1);

        t.triangle();
        t.tex(tex.X, tex.Y);

        // ..:: Positive X ::..
        t.vertex(x1, y0, z1);
        t.vertex(x1, y1, z1);
        t.vertex(x1, y1, z0);
        t.vertex(x1, y0, z0);

        t.triangle();
        t.tex(tex.X, tex.Y);

        // ..:: Negative Y ::..
        t.vertex(x0, y0, z0);
        t.vertex(x0, y0, z1);
        t.vertex(x1, y0, z1);
        t.vertex(x1, y0, z0);

        t.triangle();
        t.tex(tex.X, tex.Y);

        // ..:: Positive Y ::..
        t.vertex(x0, y1, z1);
        t.vertex(x0, y1, z0);
        t.vertex(x1, y1, z0);
        t.vertex(x1, y1, z1);

        t.triangle();
        t.tex(tex.X, tex.Y);

        // ..:: Negative Z ::..
        t.vertex(x1, y0, z0);
        t.vertex(x1, y1, z0);
        t.vertex(x0, y1, z0);
        t.vertex(x0, y0, z0);

        t.triangle();
        t.tex(1, 3);

        // ..:: Positive Z ::..
        t.vertex(x0, y0, z1);
        t.vertex(x0, y1, z1);
        t.vertex(x1, y1, z1);
        t.vertex(x1, y0, z1);

        t.triangle();
        t.tex(tex.X, tex.Y);
    }

    public void renderObsidian(Tesselator t, int x, int y, int z) {
        float x0 = x + 0.0f + ((1.0f / 16.0f) * 2.0f);
        float y0 = y + 0.0f;
        float z0 = z + 0.0f + ((1.0f / 16.0f) * 2.0f);

        float x1 = x + 1.0f - ((1.0f / 16.0f) * 2.0f);
        float y1 = y + 1.0f - ((1.0f / 16.0f) * 13.0f);
        float z1 = z + 1.0f - ((1.0f / 16.0f) * 2.0f);

        Vector2 tex = new Vector2(5, 2);

        // ..:: Negative X ::..
        t.vertex(x0, y0, z0);
        t.vertex(x0, y1, z0);
        t.vertex(x0, y1, z1);
        t.vertex(x0, y0, z1);

        t.triangle();
        t.tex(tex.X, tex.Y);

        // ..:: Positive X ::..
        t.vertex(x1, y0, z1);
        t.vertex(x1, y1, z1);
        t.vertex(x1, y1, z0);
        t.vertex(x1, y0, z0);

        t.triangle();
        t.tex(tex.X, tex.Y);

        // ..:: Negative Y ::..
        t.vertex(x0, y0, z0);
        t.vertex(x0, y0, z1);
        t.vertex(x1, y0, z1);
        t.vertex(x1, y0, z0);

        t.triangle();
        t.tex(tex.X, tex.Y);

        // ..:: Positive Y ::..
        t.vertex(x0, y1, z1);
        t.vertex(x0, y1, z0);
        t.vertex(x1, y1, z0);
        t.vertex(x1, y1, z1);

        t.triangle();
        t.tex(tex.X, tex.Y);

        // ..:: Negative Z ::..
        t.vertex(x1, y0, z0);
        t.vertex(x1, y1, z0);
        t.vertex(x0, y1, z0);
        t.vertex(x0, y0, z0);

        t.triangle();
        t.tex(tex.X, tex.Y);

        // ..:: Positive Z ::..
        t.vertex(x0, y0, z1);
        t.vertex(x0, y1, z1);
        t.vertex(x1, y1, z1);
        t.vertex(x1, y0, z1);

        t.triangle();
        t.tex(tex.X, tex.Y);
    }

    public void renderBeacon(Tesselator t, int x, int y, int z) {
        float x0 = x + 0.0f + ((1.0f / 16.0f) * 3.0f);
        float y0 = y + 0.0f + ((1.0f / 16.0f) * 3.0f);
        float z0 = z + 0.0f + ((1.0f / 16.0f) * 3.0f);

        float x1 = x + 1.0f - ((1.0f / 16.0f) * 3.0f);
        float y1 = y + 1.0f - ((1.0f / 16.0f) * 3.0f);
        float z1 = z + 1.0f - ((1.0f / 16.0f) * 3.0f);

        Vector2 tex = new Vector2(9, 2);

        // ..:: Negative X ::..
        t.vertex(x0, y0, z0);
        t.vertex(x0, y1, z0);
        t.vertex(x0, y1, z1);
        t.vertex(x0, y0, z1);

        t.triangle();
        t.tex(tex.X, tex.Y);

        // ..:: Positive X ::..
        t.vertex(x1, y0, z1);
        t.vertex(x1, y1, z1);
        t.vertex(x1, y1, z0);
        t.vertex(x1, y0, z0);

        t.triangle();
        t.tex(tex.X, tex.Y);

        // ..:: Negative Y ::..
        t.vertex(x0, y0, z0);
        t.vertex(x0, y0, z1);
        t.vertex(x1, y0, z1);
        t.vertex(x1, y0, z0);

        t.triangle();
        t.tex(tex.X, tex.Y);

        // ..:: Positive Y ::..
        t.vertex(x0, y1, z1);
        t.vertex(x0, y1, z0);
        t.vertex(x1, y1, z0);
        t.vertex(x1, y1, z1);

        t.triangle();
        t.tex(tex.X, tex.Y);

        // ..:: Negative Z ::..
        t.vertex(x1, y0, z0);
        t.vertex(x1, y1, z0);
        t.vertex(x0, y1, z0);
        t.vertex(x0, y0, z0);

        t.triangle();
        t.tex(tex.X, tex.Y);

        // ..:: Positive Z ::..
        t.vertex(x0, y0, z1);
        t.vertex(x0, y1, z1);
        t.vertex(x1, y1, z1);
        t.vertex(x1, y0, z1);

        t.triangle();
        t.tex(tex.X, tex.Y);
    }
}
