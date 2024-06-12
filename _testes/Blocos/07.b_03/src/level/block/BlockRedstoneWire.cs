using OpenTK.Mathematics;

namespace RubyDung.src.level.block;

public class BlockRedstoneWire : Block {
    public BlockRedstoneWire() {
        this.setColor(1.0f, 0.0f, 0.0f);
    }

    protected override Vector3 getColor(string face) {
        if(type == "0") {
            return ColorConverter.HexToVector3("4B0000");
        }
        if(type == "1") {
            return ColorConverter.HexToVector3("6F0000");
        }
        if(type == "2") {
            return ColorConverter.HexToVector3("790000");
        }
        if(type == "3") {
            return ColorConverter.HexToVector3("820000");
        }
        if(type == "4") {
            return ColorConverter.HexToVector3("8C0000");
        }
        if(type == "5") {
            return ColorConverter.HexToVector3("970000");
        }
        if(type == "6") {
            return ColorConverter.HexToVector3("A10000");
        }
        if(type == "7") {
            return ColorConverter.HexToVector3("AB0000");
        }
        if(type == "8") {
            return ColorConverter.HexToVector3("B50000");
        }
        if(type == "9") {
            return ColorConverter.HexToVector3("BF0000");
        }
        if(type == "10") {
            return ColorConverter.HexToVector3("CA0000");
        }
        if(type == "11") {
            return ColorConverter.HexToVector3("D30000");
        }
        if(type == "12") {
            return ColorConverter.HexToVector3("DD0000");
        }
        if(type == "13") {
            return ColorConverter.HexToVector3("E70600");
        }
        if(type == "14") {
            return ColorConverter.HexToVector3("F11B00");
        }
        if(type == "15") {
            return ColorConverter.HexToVector3("FC3100");
        }

        return base.getColor(face);
    }

    public override void renderFace(Tesselator t, int x, int y, int z, string face) {
        float x0 = x + 0.0f;
        float y0 = y + 0.0f;
        float z0 = z + 0.0f;

        float x1 = x + 1.0f;
        float y1 = y + 1.0f;
        float z1 = z + 1.0f;

        Vector2 tex = this.getTexture(face);
        Vector3 color = this.getColor(face);

        float col = 16.0f;
        float row = 16.0f;

        float u0 = tex.X / col;
        float u1 = u0 + 1.0f / col;
        float v0 = (row - 1.0f - tex.Y) / row;
        float v1 = v0 + 1.0f / row;

        // ..:: Positive Y ::..
        t.vertex(x0, y0, z1);
        t.vertex(x0, y0, z0);
        t.vertex(x1, y0, z0);
        t.vertex(x1, y0, z1);

        t.triangle();

        t.tex(u0, v0);
        t.tex(u0, v1);
        t.tex(u1, v1);
        t.tex(u1, v0);

        t.color(color.X, color.Y, color.Z);
    }
}
