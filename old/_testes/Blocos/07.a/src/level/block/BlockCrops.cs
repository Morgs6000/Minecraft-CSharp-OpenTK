using OpenTK.Mathematics;

namespace RubyDung.src.level.block;
public class BlockCrops : Block {
    public BlockCrops(cropType type) {
        this.type = type;
    }

    public enum cropType {
        wheat_0,
        wheat_1,
        wheat_2,
        wheat_3,
        wheat_4,
        wheat_5,
        wheat_6,
        wheat_7,
        crops_0,
        crops_1,
        crops_2,
        carrot_3,
        potato_3,
        nether_wart_0,
        nether_wart_1,
        nether_wart_2
    }

    private cropType type;

    protected override Vector2 getTexture(faceType face) {
        if(type == cropType.nether_wart_2) {
            return new Vector2(4, 14);
        }
        if(type == cropType.nether_wart_1) {
            return new Vector2(3, 14);
        }
        if(type == cropType.nether_wart_0) {
            return new Vector2(2, 14);
        }
        if(type == cropType.potato_3) {
            return new Vector2(12, 12);
        }
        if(type == cropType.carrot_3) {
            return new Vector2(11, 12);
        }
        if(type == cropType.crops_2) {
            return new Vector2(10, 12);
        }
        if(type == cropType.crops_1) {
            return new Vector2(9, 12);
        }
        if(type == cropType.crops_0) {
            return new Vector2(8, 12);
        }
        if(type == cropType.wheat_7) {
            return new Vector2(15, 5);
        }
        if(type == cropType.wheat_6) {
            return new Vector2(14, 5);
        }
        if(type == cropType.wheat_5) {
            return new Vector2(13, 5);
        }
        if(type == cropType.wheat_4) {
            return new Vector2(12, 5);
        }
        if(type == cropType.wheat_3) {
            return new Vector2(11, 5);
        }
        if(type == cropType.wheat_2) {
            return new Vector2(10, 5);
        }
        if(type == cropType.wheat_1) {
            return new Vector2(9, 5);
        }
        else {
            return new Vector2(8, 5);
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

        float pixel = (1.0f / 16.0f);

        // ..:: Negative X ::..
        t.vertex(x0 + (pixel * 4.0f), y0, z0);
        t.vertex(x0 + (pixel * 4.0f), y1, z0);
        t.vertex(x0 + (pixel * 4.0f), y1, z1);
        t.vertex(x0 + (pixel * 4.0f), y0, z1);

        t.triangle();
        t.tex(tex.X, tex.Y);
        t.color(color.X, color.Y, color.Z);

        // ..:: Positive X ::..
        t.vertex(x1 - (pixel * 12.0f), y0, z1);
        t.vertex(x1 - (pixel * 12.0f), y1, z1);
        t.vertex(x1 - (pixel * 12.0f), y1, z0);
        t.vertex(x1 - (pixel * 12.0f), y0, z0);

        t.triangle();
        t.tex(tex.X, tex.Y);
        t.color(color.X, color.Y, color.Z);

        // ..:: Negative X ::..
        t.vertex(x0 + (pixel * 12.0f), y0, z0);
        t.vertex(x0 + (pixel * 12.0f), y1, z0);
        t.vertex(x0 + (pixel * 12.0f), y1, z1);
        t.vertex(x0 + (pixel * 12.0f), y0, z1);

        t.triangle();
        t.tex(tex.X, tex.Y);
        t.color(color.X, color.Y, color.Z);

        // ..:: Positive X ::..
        t.vertex(x1 - (pixel * 4.0f), y0, z1);
        t.vertex(x1 - (pixel * 4.0f), y1, z1);
        t.vertex(x1 - (pixel * 4.0f), y1, z0);
        t.vertex(x1 - (pixel * 4.0f), y0, z0);

        t.triangle();
        t.tex(tex.X, tex.Y);
        t.color(color.X, color.Y, color.Z);

        // ..:: Negative Z ::..
        t.vertex(x1, y0, z0 + (pixel * 4.0f));
        t.vertex(x1, y1, z0 + (pixel * 4.0f));
        t.vertex(x0, y1, z0 + (pixel * 4.0f));
        t.vertex(x0, y0, z0 + (pixel * 4.0f));

        t.triangle();
        t.tex(tex.X, tex.Y);
        t.color(color.X, color.Y, color.Z);

        // ..:: Positive Z ::..
        t.vertex(x0, y0, z1 - (pixel * 12.0f));
        t.vertex(x0, y1, z1 - (pixel * 12.0f));
        t.vertex(x1, y1, z1 - (pixel * 12.0f));
        t.vertex(x1, y0, z1 - (pixel * 12.0f));

        t.triangle();
        t.tex(tex.X, tex.Y);
        t.color(color.X, color.Y, color.Z);

        // ..:: Negative Z ::..
        t.vertex(x1, y0, z0 + (pixel * 12.0f));
        t.vertex(x1, y1, z0 + (pixel * 12.0f));
        t.vertex(x0, y1, z0 + (pixel * 12.0f));
        t.vertex(x0, y0, z0 + (pixel * 12.0f));

        t.triangle();
        t.tex(tex.X, tex.Y);
        t.color(color.X, color.Y, color.Z);

        // ..:: Positive Z ::..
        t.vertex(x0, y0, z1 - (pixel * 4.0f));
        t.vertex(x0, y1, z1 - (pixel * 4.0f));
        t.vertex(x1, y1, z1 - (pixel * 4.0f));
        t.vertex(x1, y0, z1 - (pixel * 4.0f));

        t.triangle();
        t.tex(tex.X, tex.Y);
        t.color(color.X, color.Y, color.Z);
    }
}
