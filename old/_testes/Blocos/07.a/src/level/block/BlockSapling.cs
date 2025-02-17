using OpenTK.Mathematics;

namespace RubyDung.src.level.block;

public class BlockSapling : Block {
    public BlockSapling(saplingType type) {
        this.type = type;
    }

    public enum saplingType {
        oak,
        jungle,
        spruce,
        birch
    }

    private saplingType type;

    protected override Vector2 getTexture(faceType face) {
        if(type == saplingType.birch) {
            return new Vector2(15, 4);
        }
        if(type == saplingType.spruce) {
            return new Vector2(15, 3);
        }
        if(type == saplingType.jungle) {
            return new Vector2(14, 1);
        }
        else {
            return new Vector2(15, 0);
        }
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
