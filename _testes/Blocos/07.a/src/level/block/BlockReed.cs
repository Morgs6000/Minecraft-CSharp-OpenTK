using OpenTK.Mathematics;

namespace RubyDung.src.level.block;

public class BlockReed : Block {
    public BlockReed() {
        this.setTexture(9, 4);
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

        // ..:: ::..
        t.vertex(x0, y0, z0);
        t.vertex(x0, y1, z0);
        t.vertex(x1, y1, z1);
        t.vertex(x1, y0, z1);

        t.triangle();
        t.tex(tex.X, tex.Y);
        t.color(color.X, color.Y, color.Z);

        // ..:: ::..
        t.vertex(x1, y0, z0);
        t.vertex(x1, y1, z0);
        t.vertex(x0, y1, z1);
        t.vertex(x0, y0, z1);

        t.triangle();
        t.tex(tex.X, tex.Y);
        t.color(color.X, color.Y, color.Z);

        // ..:: ::..
        t.vertex(x1, y0, z1);
        t.vertex(x1, y1, z1);
        t.vertex(x0, y1, z0);
        t.vertex(x0, y0, z0);

        t.triangle();
        t.tex(tex.X, tex.Y);
        t.color(color.X, color.Y, color.Z);

        // ..:: ::..
        t.vertex(x0, y0, z1);
        t.vertex(x0, y1, z1);
        t.vertex(x1, y1, z0);
        t.vertex(x1, y0, z0);

        t.triangle();
        t.tex(tex.X, tex.Y);
        t.color(color.X, color.Y, color.Z);
    }
}
