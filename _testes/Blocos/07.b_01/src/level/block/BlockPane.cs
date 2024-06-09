using OpenTK.Mathematics;

namespace RubyDung.src.level.block;

public class BlockPane : Block {
    public BlockPane() {
        
    }

    protected override void renderFace(Tesselator t, int x, int y, int z, string face) {
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

        float uPixel = ((1.0f / col) / col);
        float vPixel = ((1.0f / row) / row);

        if(face == "y0") {
            // ..:: Negative Y ::..
            t.vertex(x0, y0, z0 + 0.4375f);
            t.vertex(x0, y0, z1 - 0.4375f);
            t.vertex(x1, y0, z1 - 0.4375f);
            t.vertex(x1, y0, z0 + 0.4375f);

            t.triangle();

            t.tex(u0 + (uPixel * 2.0f), v1);
            t.tex(u1 - (uPixel * 12.0f), v1);
            t.tex(u1 - (uPixel * 12.0f), v0);
            t.tex(u0 + (uPixel * 2.0f), v0);

            t.color(color.X, color.Y, color.Z);

            // ..:: Positive Y ::..
            t.vertex(x0, y0, z1 - 0.4375f);
            t.vertex(x0, y0, z0 + 0.4375f);
            t.vertex(x1, y0, z0 + 0.4375f);
            t.vertex(x1, y0, z1 - 0.4375f);

            t.triangle();

            t.tex(u0 + (uPixel * 2.0f), v1);
            t.tex(u1 - (uPixel * 12.0f), v1);
            t.tex(u1 - (uPixel * 12.0f), v0);
            t.tex(u0 + (uPixel * 2.0f), v0);

            t.color(color.X, color.Y, color.Z);
        }
        if(face == "y1") {
            // ..:: Negative Y ::..
            t.vertex(x0, y1, z0 + 0.4375f);
            t.vertex(x0, y1, z1 - 0.4375f);
            t.vertex(x1, y1, z1 - 0.4375f);
            t.vertex(x1, y1, z0 + 0.4375f);

            t.triangle();

            t.tex(u0 + (uPixel * 2.0f), v1);
            t.tex(u1 - (uPixel * 12.0f), v1);
            t.tex(u1 - (uPixel * 12.0f), v0);
            t.tex(u0 + (uPixel * 2.0f), v0);

            t.color(color.X, color.Y, color.Z);

            // ..:: Positive Y ::..
            t.vertex(x0, y1, z1 - 0.4375f);
            t.vertex(x0, y1, z0 + 0.4375f);
            t.vertex(x1, y1, z0 + 0.4375f);
            t.vertex(x1, y1, z1 - 0.4375f);

            t.triangle();

            t.tex(u0 + (uPixel * 2.0f), v1);
            t.tex(u1 - (uPixel * 12.0f), v1);
            t.tex(u1 - (uPixel * 12.0f), v0);
            t.tex(u0 + (uPixel * 2.0f), v0);

            t.color(color.X, color.Y, color.Z);
        }

        // ..:: Negative Z ::..
        t.vertex(x1, y0, z0 + 0.5f);
        t.vertex(x1, y1, z0 + 0.5f);
        t.vertex(x0, y1, z0 + 0.5f);
        t.vertex(x0, y0, z0 + 0.5f);

        t.triangle();

        t.tex(u0, v0);
        t.tex(u0, v1);
        t.tex(u1, v1);
        t.tex(u1, v0);

        t.color(color.X, color.Y, color.Z);

        // ..:: Positive Z ::..
        t.vertex(x0, y0, z1 - 0.5f);
        t.vertex(x0, y1, z1 - 0.5f);
        t.vertex(x1, y1, z1 - 0.5f);
        t.vertex(x1, y0, z1 - 0.5f);

        t.triangle();

        t.tex(u0, v0);
        t.tex(u0, v1);
        t.tex(u1, v1);
        t.tex(u1, v0);

        t.color(color.X, color.Y, color.Z);
    }
}
