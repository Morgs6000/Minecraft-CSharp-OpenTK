using OpenTK.Mathematics;

namespace RubyDung.src.level.block;

public class BlockTallGrass : Block {
    public BlockTallGrass(Vector2 tex) {
        this.tex = tex;
    }

    protected override void renderFace(Tesselator t, int x, int y, int z, int face) {
        float x0 = x + 0.0f;
        float y0 = y + 0.0f;
        float z0 = z + 0.0f;

        float x1 = x + 1.0f;
        float y1 = y + 1.0f;
        float z1 = z + 1.0f;

        t.vertex(x0, y0, z0);
        t.vertex(x0, y1, z0);
        t.vertex(x1, y1, z1);
        t.vertex(x1, y0, z1);

        t.triangle();
        t.tex(tex.X, tex.Y);

        t.vertex(x1, y0, z0);
        t.vertex(x1, y1, z0);
        t.vertex(x0, y1, z1);
        t.vertex(x0, y0, z1);

        t.triangle();
        t.tex(tex.X, tex.Y);

        t.vertex(x1, y0, z1);
        t.vertex(x1, y1, z1);
        t.vertex(x0, y1, z0);
        t.vertex(x0, y0, z0);

        t.triangle();
        t.tex(tex.X, tex.Y);

        t.vertex(x0, y0, z1);
        t.vertex(x0, y1, z1);
        t.vertex(x1, y1, z0);
        t.vertex(x1, y0, z0);

        t.triangle();
        t.tex(tex.X, tex.Y);
    }
}
