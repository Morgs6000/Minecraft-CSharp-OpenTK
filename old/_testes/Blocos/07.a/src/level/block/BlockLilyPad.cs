using OpenTK.Mathematics;

namespace RubyDung.src.level.block;

public class BlockLilyPad : Block {
    public BlockLilyPad() {
        this.setTexture(12, 4);
    }

    /*
    protected override Vector3 getColor(faceType face) {
        return new Vector3(0.0f, 1.0f, 0.0f);
    }
    */

    protected override void renderFace(Tesselator t, int x, int y, int z, faceType face) {
        float x0 = x + 0.0f;
        float y0 = y + 0.0f;
        float z0 = z + 0.0f;

        float x1 = x + 1.0f;
        float y1 = y + 0.0f;
        float z1 = z + 1.0f;

        Vector2 tex = this.getTexture(face);
        Vector3 color = this.getColor(face);

        // ..:: Negative Y ::..
        t.vertex(x1, y0, z1);
        t.vertex(x1, y0, z0);
        t.vertex(x0, y0, z0);
        t.vertex(x0, y0, z1);

        t.triangle();
        t.tex(tex.X, tex.Y);
        t.color(color.X, color.Y, color.Z);

        // ..:: Positive Y ::..
        t.vertex(x0, y1, z1);
        t.vertex(x0, y1, z0);
        t.vertex(x1, y1, z0);
        t.vertex(x1, y1, z1);

        t.triangle();
        t.tex(tex.X, tex.Y);
        t.color(color.X, color.Y, color.Z);
    }
}
