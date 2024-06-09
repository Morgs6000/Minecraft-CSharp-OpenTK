using OpenTK.Mathematics;

namespace RubyDung.src.level.block;

public class BlockLever : Block {
    public BlockLever() {
        
    }

    protected override void renderFace(Tesselator t, int x, int y, int z, string face) {
        this.renderBase(t, x, y, z, face);
        this.renderLever(t, x, y, z, face);
    }

    private void renderBase(Tesselator t, int x, int y, int z, string face) {
        float x0 = x + 0.0f + 0.3125f;
        float y0 = y + 0.0f;
        float z0 = z + 0.0f + 0.25f;

        float x1 = x + 1.0f - 0.3125f;
        float y1 = y + 1.0f - 0.8125f;
        float z1 = z + 1.0f - 0.25f;

        Vector2 tex = new Vector2(0, 1);
        Vector3 color = this.getColor(face);

        float col = 16.0f;
        float row = 16.0f;

        float u0 = tex.X / col;
        float u1 = u0 + 1.0f / col;
        float v0 = (row - 1.0f - tex.Y) / row;
        float v1 = v0 + 1.0f / row;

        float uPixel = ((1.0f / col) / col);
        float vPixel = ((1.0f / row) / row);

        // ..:: Negative X ::..
        t.vertex(x0, y0, z0);
        t.vertex(x0, y1, z0);
        t.vertex(x0, y1, z1);
        t.vertex(x0, y0, z1);

        t.triangle();

        t.tex(u0 + (uPixel * 4.0f), v0);
        t.tex(u0 + (uPixel * 4.0f), v1 - (vPixel * 13.0f));
        t.tex(u1 - (uPixel * 4.0f), v1 - (vPixel * 13.0f));
        t.tex(u1 - (uPixel * 4.0f), v0);

        t.color(color.X, color.Y, color.Z);

        // ..:: Positive X ::..
        t.vertex(x1, y0, z1);
        t.vertex(x1, y1, z1);
        t.vertex(x1, y1, z0);
        t.vertex(x1, y0, z0);

        t.triangle();

        t.tex(u0 + (uPixel * 4.0f), v0);
        t.tex(u0 + (uPixel * 4.0f), v1 - (vPixel * 13.0f));
        t.tex(u1 - (uPixel * 4.0f), v1 - (vPixel * 13.0f));
        t.tex(u1 - (uPixel * 4.0f), v0);

        t.color(color.X, color.Y, color.Z);

        // ..:: Negative Y ::..
        if(face == "y0") {
            t.vertex(x0, y0, z0);
            t.vertex(x0, y0, z1);
            t.vertex(x1, y0, z1);
            t.vertex(x1, y0, z0);

            t.triangle();

            t.tex(u0 + (uPixel * 5.0f), v0 + (uPixel * 4.0f));
            t.tex(u0 + (uPixel * 5.0f), v1 - (vPixel * 4.0f));
            t.tex(u1 - (uPixel * 5.0f), v1 - (vPixel * 4.0f));
            t.tex(u1 - (uPixel * 5.0f), v0 + (uPixel * 4.0f));

            t.color(color.X, color.Y, color.Z);
        }

        // ..:: Positive Y ::..
        t.vertex(x0, y1, z1);
        t.vertex(x0, y1, z0);
        t.vertex(x1, y1, z0);
        t.vertex(x1, y1, z1);

        t.triangle();

        t.tex(u0 + (uPixel * 5.0f), v0 + (uPixel * 4.0f));
        t.tex(u0 + (uPixel * 5.0f), v1 - (vPixel * 4.0f));
        t.tex(u1 - (uPixel * 5.0f), v1 - (vPixel * 4.0f));
        t.tex(u1 - (uPixel * 5.0f), v0 + (uPixel * 4.0f));

        t.color(color.X, color.Y, color.Z);

        // ..:: Negative Z ::..
        t.vertex(x1, y0, z0);
        t.vertex(x1, y1, z0);
        t.vertex(x0, y1, z0);
        t.vertex(x0, y0, z0);

        t.triangle();

        t.tex(u0 + (uPixel * 5.0f), v0);
        t.tex(u0 + (uPixel * 5.0f), v1 - (vPixel * 13.0f));
        t.tex(u1 - (uPixel * 5.0f), v1 - (vPixel * 13.0f));
        t.tex(u1 - (uPixel * 5.0f), v0);

        t.color(color.X, color.Y, color.Z);

        // ..:: Positive Z ::..
        t.vertex(x0, y0, z1);
        t.vertex(x0, y1, z1);
        t.vertex(x1, y1, z1);
        t.vertex(x1, y0, z1);

        t.triangle();

        t.tex(u0 + (uPixel * 5.0f), v0);
        t.tex(u0 + (uPixel * 5.0f), v1 - (vPixel * 13.0f));
        t.tex(u1 - (uPixel * 5.0f), v1 - (vPixel * 13.0f));
        t.tex(u1 - (uPixel * 5.0f), v0);

        t.color(color.X, color.Y, color.Z);
    }

    private void renderLever(Tesselator t, int x, int y, int z, string face) {
        float x0 = x + 0.0f + 0.4375f;
        float y0 = y + 0.0f;
        float z0 = z + 0.0f + 0.4375f;

        float x1 = x + 1.0f - 0.4375f;
        float y1 = y + 1.0f - 0.375f;
        float z1 = z + 1.0f - 0.4375f;

        Vector2 tex = new Vector2(0, 6);
        Vector3 color = this.getColor(face);

        float col = 16.0f;
        float row = 16.0f;

        float u0 = tex.X / col;
        float u1 = u0 + 1.0f / col;
        float v0 = (row - 1.0f - tex.Y) / row;
        float v1 = v0 + 1.0f / row;

        float uPixel = ((1.0f / col) / col);
        float vPixel = ((1.0f / row) / row);

        // ..:: Negative X ::..
        t.vertex(x0, y0, z0);
        t.vertex(x0, y1, z0);
        t.vertex(x0, y1, z1);
        t.vertex(x0, y0, z1);

        t.triangle();

        t.tex(u0 + (uPixel * 7.0f), v0);
        t.tex(u0 + (uPixel * 7.0f), v1 - (vPixel * 6.0f));
        t.tex(u1 - (uPixel * 7.0f), v1 - (vPixel * 6.0f));
        t.tex(u1 - (uPixel * 7.0f), v0);

        t.color(color.X, color.Y, color.Z);

        // ..:: Positive X ::..
        t.vertex(x1, y0, z1);
        t.vertex(x1, y1, z1);
        t.vertex(x1, y1, z0);
        t.vertex(x1, y0, z0);

        t.triangle();

        t.tex(u0 + (uPixel * 7.0f), v0);
        t.tex(u0 + (uPixel * 7.0f), v1 - (vPixel * 6.0f));
        t.tex(u1 - (uPixel * 7.0f), v1 - (vPixel * 6.0f));
        t.tex(u1 - (uPixel * 7.0f), v0);

        t.color(color.X, color.Y, color.Z);

        // ..:: Negative Y ::..
        if(face == "y0") {
            t.vertex(x0, y0, z0);
            t.vertex(x0, y0, z1);
            t.vertex(x1, y0, z1);
            t.vertex(x1, y0, z0);

            t.triangle();

            t.tex(u0 + (uPixel * 7.0f), v0);
            t.tex(u0 + (uPixel * 7.0f), v1 - (vPixel * 14.0f));
            t.tex(u1 - (uPixel * 7.0f), v1 - (vPixel * 14.0f));
            t.tex(u1 - (uPixel * 7.0f), v0);

            t.color(color.X, color.Y, color.Z);
        }

        // ..:: Positive Y ::..
        t.vertex(x0, y1, z1);
        t.vertex(x0, y1, z0);
        t.vertex(x1, y1, z0);
        t.vertex(x1, y1, z1);

        t.triangle();

        t.tex(u0 + (uPixel * 7.0f), v0 + (uPixel * 8.0f));
        t.tex(u0 + (uPixel * 7.0f), v1 - (vPixel * 6.0f));
        t.tex(u1 - (uPixel * 7.0f), v1 - (vPixel * 6.0f));
        t.tex(u1 - (uPixel * 7.0f), v0 + (uPixel * 8.0f));

        t.color(color.X, color.Y, color.Z);

        // ..:: Negative Z ::..
        t.vertex(x1, y0, z0);
        t.vertex(x1, y1, z0);
        t.vertex(x0, y1, z0);
        t.vertex(x0, y0, z0);

        t.triangle();

        t.tex(u0 + (uPixel * 7.0f), v0);
        t.tex(u0 + (uPixel * 7.0f), v1 - (vPixel * 6.0f));
        t.tex(u1 - (uPixel * 7.0f), v1 - (vPixel * 6.0f));
        t.tex(u1 - (uPixel * 7.0f), v0);

        t.color(color.X, color.Y, color.Z);

        // ..:: Positive Z ::..
        t.vertex(x0, y0, z1);
        t.vertex(x0, y1, z1);
        t.vertex(x1, y1, z1);
        t.vertex(x1, y0, z1);

        t.triangle();

        t.tex(u0 + (uPixel * 7.0f), v0);
        t.tex(u0 + (uPixel * 7.0f), v1 - (vPixel * 6.0f));
        t.tex(u1 - (uPixel * 7.0f), v1 - (vPixel * 6.0f));
        t.tex(u1 - (uPixel * 7.0f), v0);

        t.color(color.X, color.Y, color.Z);
    }
}
