using OpenTK.Mathematics;

namespace RubyDung.src.level.block;

public class BlockCactus : Block {
    public BlockCactus() {
        this.setTexture(5, 4);
    }

    protected override void renderFace(Tesselator t, int x, int y, int z, faceType face) {
        float x0 = x + 0.0f;
        float y0 = y + 0.0f;
        float z0 = z + 0.0f;

        float x1 = x + 1.0f;
        float y1 = y + 1.0f;
        float z1 = z + 1.0f;

        Vector2 tex = this.getTexture(face);
        Vector3 color = this.getColor(face);

        // ..:: Negative X ::..
        t.vertex(x0 + ((1.0f / 16.0f) * 1.0f), y0, z0);
        t.vertex(x0 + ((1.0f / 16.0f) * 1.0f), y1, z0);
        t.vertex(x0 + ((1.0f / 16.0f) * 1.0f), y1, z1);
        t.vertex(x0 + ((1.0f / 16.0f) * 1.0f), y0, z1);

        t.triangle();
        t.tex(6, 4);
        t.color(color.X, color.Y, color.Z);

        // ..:: Positive X ::..
        t.vertex(x1 - ((1.0f / 16.0f) * 1.0f), y0, z1);
        t.vertex(x1 - ((1.0f / 16.0f) * 1.0f), y1, z1);
        t.vertex(x1 - ((1.0f / 16.0f) * 1.0f), y1, z0);
        t.vertex(x1 - ((1.0f / 16.0f) * 1.0f), y0, z0);

        t.triangle();
        t.tex(6, 4);
        t.color(color.X, color.Y, color.Z);

        // ..:: Negative Y ::..
        t.vertex(x0, y0, z0);
        t.vertex(x0, y0, z1);
        t.vertex(x1, y0, z1);
        t.vertex(x1, y0, z0);

        t.triangle();
        t.tex(7, 4);
        t.color(color.X, color.Y, color.Z);

        // ..:: Positive Y ::..
        t.vertex(x0, y1, z1);
        t.vertex(x0, y1, z0);
        t.vertex(x1, y1, z0);
        t.vertex(x1, y1, z1);

        t.triangle();
        t.tex(5, 4);
        t.color(color.X, color.Y, color.Z);

        // ..:: Negative Z ::..
        t.vertex(x1, y0, z0 + ((1.0f / 16.0f) * 1.0f));
        t.vertex(x1, y1, z0 + ((1.0f / 16.0f) * 1.0f));
        t.vertex(x0, y1, z0 + ((1.0f / 16.0f) * 1.0f));
        t.vertex(x0, y0, z0 + ((1.0f / 16.0f) * 1.0f));

        t.triangle();
        t.tex(6, 4);
        t.color(color.X, color.Y, color.Z);

        // ..:: Positive Z ::..
        t.vertex(x0, y0, z1 - ((1.0f / 16.0f) * 1.0f));
        t.vertex(x0, y1, z1 - ((1.0f / 16.0f) * 1.0f));
        t.vertex(x1, y1, z1 - ((1.0f / 16.0f) * 1.0f));
        t.vertex(x1, y0, z1 - ((1.0f / 16.0f) * 1.0f));

        t.triangle();
        t.tex(6, 4);
        t.color(color.X, color.Y, color.Z);
    }
}
