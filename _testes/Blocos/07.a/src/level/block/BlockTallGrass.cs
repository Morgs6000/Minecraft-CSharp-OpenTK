using OpenTK.Mathematics;

namespace RubyDung.src.level.block;

public class BlockTallGrass : Block {
    public BlockTallGrass(grassType type) {
        this.type = type;
    }

    public enum grassType {
        tall_grass,
        dead_bush,
        fern
    }

    private grassType type;

    protected override Vector2 getTexture(int face) {
        if(type == grassType.fern) {
            return new Vector2(8, 3);
        }
        if(type == grassType.dead_bush) {
            return new Vector2(7, 3);
        }
        else {
            return new Vector2(7, 2);
        }
    }

    /*
    protected override Vector3 getColor(int face) {
        if(type == grassType.tall_grass || type == grassType.fern) {
            return new Vector3(0.0f, 1.0f, 0.0f);
        }
        else {
            return new Vector3(1.0f, 1.0f, 1.0f);
        }
    }
    */

    protected override void renderFace(Tesselator t, int x, int y, int z, int face) {
        float x0 = x + 0.0f;
        float y0 = y + 0.0f;
        float z0 = z + 0.0f;

        float x1 = x + 1.0f;
        float y1 = y + 1.0f;
        float z1 = z + 1.0f;

        Vector2 tex = this.getTexture(face);
        Vector3 color = this.getColor(face);

        if(face == 0) {
            t.vertex(x0, y0, z0);
            t.vertex(x0, y1, z0);
            t.vertex(x1, y1, z1);
            t.vertex(x1, y0, z1);

            t.triangle();
            t.tex(tex.X, tex.Y);
            t.color(color.X, color.Y, color.Z);
        }

        if(face == 1) {
            t.vertex(x1, y0, z0);
            t.vertex(x1, y1, z0);
            t.vertex(x0, y1, z1);
            t.vertex(x0, y0, z1);

            t.triangle();
            t.tex(tex.X, tex.Y);
            t.color(color.X, color.Y, color.Z);
        }

        if(face == 2) {
            t.vertex(x1, y0, z1);
            t.vertex(x1, y1, z1);
            t.vertex(x0, y1, z0);
            t.vertex(x0, y0, z0);

            t.triangle();
            t.tex(tex.X, tex.Y);
            t.color(color.X, color.Y, color.Z);
        }

        if(face == 3) {
            t.vertex(x0, y0, z1);
            t.vertex(x0, y1, z1);
            t.vertex(x1, y1, z0);
            t.vertex(x1, y0, z0);

            t.triangle();
            t.tex(tex.X, tex.Y);
            t.color(color.X, color.Y, color.Z);
        }
    }
}
