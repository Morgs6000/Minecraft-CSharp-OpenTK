using OpenTK.Mathematics;

namespace RubyDung.src.level.block;

public class BlockFlower : Block {
    public BlockFlower(flowerType type) {
        this.type = type;
    }

    public enum flowerType {
        rose,
        dandelion,
        red_mushroom,
        brown_mushroom
    }

    private flowerType type;

    protected override Vector2 getTexture(int face) {
        if(type == flowerType.brown_mushroom) {
            return new Vector2(13, 1);
        }
        if(type == flowerType.red_mushroom) {
            return new Vector2(12, 1);
        }
        if(type == flowerType.dandelion) {
            return new Vector2(13, 0);
        }
        else {
            return new Vector2(12, 0);
        }
    }

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
