using OpenTK.Mathematics;

namespace RubyDung.src.level.block;

public class BlockBeacon : BlockContainer {
    public BlockBeacon() {

    }

    protected override void renderFace(Tesselator t, int x, int y, int z, string face) {
        this.renderGlass(t, x, y, z, face);
        this.renderObsidian(t, x, y, z, face);
        this.renderBeacon(t, x, y, z, face);
    }

    private void renderGlass(Tesselator t, int x, int y, int z, string face) {
        float x0 = x + 0.0f;
        float y0 = y + 0.0f;
        float z0 = z + 0.0f;

        float x1 = x + 1.0f;
        float y1 = y + 1.0f;
        float z1 = z + 1.0f;

        Vector2 tex = new Vector2(1, 3);
        Vector3 color = this.getColor(face);

        float col = 16.0f;
        float row = 16.0f;

        float u0 = tex.X / col;
        float u1 = u0 + 1.0f / col;
        float v0 = (row - 1.0f - tex.Y) / row;
        float v1 = v0 + 1.0f / row;

        // ..:: Negative X ::..
        if(face == "x0") {
            t.vertex(x0, y0, z0);
            t.vertex(x0, y1, z0);
            t.vertex(x0, y1, z1);
            t.vertex(x0, y0, z1);

            t.triangle();

            t.tex(u0, v0);
            t.tex(u0, v1);
            t.tex(u1, v1);
            t.tex(u1, v0);

            t.color(color.X, color.Y, color.Z);
        }

        // ..:: Positive X ::..
        if(face == "x1") {
            t.vertex(x1, y0, z1);
            t.vertex(x1, y1, z1);
            t.vertex(x1, y1, z0);
            t.vertex(x1, y0, z0);

            t.triangle();

            t.tex(u0, v0);
            t.tex(u0, v1);
            t.tex(u1, v1);
            t.tex(u1, v0);

            t.color(color.X, color.Y, color.Z);
        }

        // ..:: Negative Y ::..
        if(face == "y0") {
            t.vertex(x0, y0, z0);
            t.vertex(x0, y0, z1);
            t.vertex(x1, y0, z1);
            t.vertex(x1, y0, z0);

            t.triangle();

            t.tex(u0, v0);
            t.tex(u0, v1);
            t.tex(u1, v1);
            t.tex(u1, v0);

            t.color(color.X, color.Y, color.Z);
        }

        // ..:: Positive Y ::..
        if(face == "y1") {
            t.vertex(x0, y1, z1);
            t.vertex(x0, y1, z0);
            t.vertex(x1, y1, z0);
            t.vertex(x1, y1, z1);

            t.triangle();

            t.tex(u0, v0);
            t.tex(u0, v1);
            t.tex(u1, v1);
            t.tex(u1, v0);

            t.color(color.X, color.Y, color.Z);
        }

        // ..:: Negative Z ::..
        if(face == "z0") {
            t.vertex(x1, y0, z0);
            t.vertex(x1, y1, z0);
            t.vertex(x0, y1, z0);
            t.vertex(x0, y0, z0);

            t.triangle();

            t.tex(u0, v0);
            t.tex(u0, v1);
            t.tex(u1, v1);
            t.tex(u1, v0);

            t.color(color.X, color.Y, color.Z);
        }

        // ..:: Positive Z ::..
        if(face == "z1") {
            t.vertex(x0, y0, z1);
            t.vertex(x0, y1, z1);
            t.vertex(x1, y1, z1);
            t.vertex(x1, y0, z1);

            t.triangle();

            t.tex(u0, v0);
            t.tex(u0, v1);
            t.tex(u1, v1);
            t.tex(u1, v0);

            t.color(color.X, color.Y, color.Z);
        }
    }

    private void renderObsidian(Tesselator t, int x, int y, int z, string face) {
        float x0 = x + 0.0f + 0.125f;
        float y0 = y + 0.0f;
        float z0 = z + 0.0f + 0.125f;

        float x1 = x + 1.0f - 0.125f;
        float y1 = y + 1.0f - 0.8125f;
        float z1 = z + 1.0f - 0.125f;

        Vector2 tex = new Vector2(5, 2);
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

        t.tex(u0 + (uPixel * 2.0f), v0);
        t.tex(u0 + (uPixel * 2.0f), v1 - (vPixel * 13.0f));
        t.tex(u1 - (uPixel * 2.0f), v1 - (vPixel * 13.0f));
        t.tex(u1 - (uPixel * 2.0f), v0);

        t.color(color.X, color.Y, color.Z);

        // ..:: Positive X ::..
        t.vertex(x1, y0, z1);
        t.vertex(x1, y1, z1);
        t.vertex(x1, y1, z0);
        t.vertex(x1, y0, z0);

        t.triangle();

        t.tex(u0 + (uPixel * 2.0f), v0);
        t.tex(u0 + (uPixel * 2.0f), v1 - (vPixel * 13.0f));
        t.tex(u1 - (uPixel * 2.0f), v1 - (vPixel * 13.0f));
        t.tex(u1 - (uPixel * 2.0f), v0);

        t.color(color.X, color.Y, color.Z);

        // ..:: Negative Y ::..
        if(face == "y0") {
            t.vertex(x0, y0, z0);
            t.vertex(x0, y0, z1);
            t.vertex(x1, y0, z1);
            t.vertex(x1, y0, z0);

            t.triangle();

            t.tex(u0, v0);
            t.tex(u0, v1);
            t.tex(u1, v1);
            t.tex(u1, v0);

            t.color(color.X, color.Y, color.Z);
        }

        // ..:: Positive Y ::..
        t.vertex(x0, y1, z1);
        t.vertex(x0, y1, z0);
        t.vertex(x1, y1, z0);
        t.vertex(x1, y1, z1);

        t.triangle();

        t.tex(u0, v0);
        t.tex(u0, v1);
        t.tex(u1, v1);
        t.tex(u1, v0);

        t.color(color.X, color.Y, color.Z);

        // ..:: Negative Z ::..
        t.vertex(x1, y0, z0);
        t.vertex(x1, y1, z0);
        t.vertex(x0, y1, z0);
        t.vertex(x0, y0, z0);

        t.triangle();

        t.tex(u0 + (uPixel * 2.0f), v0);
        t.tex(u0 + (uPixel * 2.0f), v1 - (vPixel * 13.0f));
        t.tex(u1 - (uPixel * 2.0f), v1 - (vPixel * 13.0f));
        t.tex(u1 - (uPixel * 2.0f), v0);

        t.color(color.X, color.Y, color.Z);

        // ..:: Positive Z ::..
        t.vertex(x0, y0, z1);
        t.vertex(x0, y1, z1);
        t.vertex(x1, y1, z1);
        t.vertex(x1, y0, z1);

        t.triangle();

        t.tex(u0 + (uPixel * 2.0f), v0);
        t.tex(u0 + (uPixel * 2.0f), v1 - (vPixel * 13.0f));
        t.tex(u1 - (uPixel * 2.0f), v1 - (vPixel * 13.0f));
        t.tex(u1 - (uPixel * 2.0f), v0);

        t.color(color.X, color.Y, color.Z);
    }

    private void renderBeacon(Tesselator t, int x, int y, int z, string face) {
        float x0 = x + 0.0f + 0.1875f;
        float y0 = y + 0.0f + 0.1875f;
        float z0 = z + 0.0f + 0.1875f;

        float x1 = x + 1.0f - 0.1875f;
        float y1 = y + 1.0f - 0.1875f;
        float z1 = z + 1.0f - 0.1875f;

        Vector2 tex = new Vector2(9, 2);
        Vector3 color = this.getColor(face);

        float col = 16.0f;
        float row = 16.0f;

        float u0 = tex.X / col;
        float u1 = u0 + 1.0f / col;
        float v0 = (row - 1.0f - tex.Y) / row;
        float v1 = v0 + 1.0f / row;

        float uPixel = ((1.0f / col) / col);
        float vPixel = ((1.0f / row) / row);

        u0 += (uPixel * 3.0f);
        u1 -= (uPixel * 3.0f);
        v0 += (vPixel * 3.0f);
        v1 -= (vPixel * 3.0f);

        // ..:: Negative X ::..
        t.vertex(x0, y0, z0);
        t.vertex(x0, y1, z0);
        t.vertex(x0, y1, z1);
        t.vertex(x0, y0, z1);

        t.triangle();

        t.tex(u0, v0);
        t.tex(u0, v1);
        t.tex(u1, v1);
        t.tex(u1, v0);

        t.color(color.X, color.Y, color.Z);

        // ..:: Positive X ::..
        t.vertex(x1, y0, z1);
        t.vertex(x1, y1, z1);
        t.vertex(x1, y1, z0);
        t.vertex(x1, y0, z0);

        t.triangle();

        t.tex(u0, v0);
        t.tex(u0, v1);
        t.tex(u1, v1);
        t.tex(u1, v0);

        t.color(color.X, color.Y, color.Z);

        // ..:: Negative Y ::..
        t.vertex(x0, y0, z0);
        t.vertex(x0, y0, z1);
        t.vertex(x1, y0, z1);
        t.vertex(x1, y0, z0);

        t.triangle();

        t.tex(u0, v0);
        t.tex(u0, v1);
        t.tex(u1, v1);
        t.tex(u1, v0);

        t.color(color.X, color.Y, color.Z);

        // ..:: Positive Y ::..
        t.vertex(x0, y1, z1);
        t.vertex(x0, y1, z0);
        t.vertex(x1, y1, z0);
        t.vertex(x1, y1, z1);

        t.triangle();

        t.tex(u0, v0);
        t.tex(u0, v1);
        t.tex(u1, v1);
        t.tex(u1, v0);

        t.color(color.X, color.Y, color.Z);

        // ..:: Negative Z ::..
        t.vertex(x1, y0, z0);
        t.vertex(x1, y1, z0);
        t.vertex(x0, y1, z0);
        t.vertex(x0, y0, z0);

        t.triangle();

        t.tex(u0, v0);
        t.tex(u0, v1);
        t.tex(u1, v1);
        t.tex(u1, v0);

        t.color(color.X, color.Y, color.Z);

        // ..:: Positive Z ::..
        t.vertex(x0, y0, z1);
        t.vertex(x0, y1, z1);
        t.vertex(x1, y1, z1);
        t.vertex(x1, y0, z1);

        t.triangle();

        t.tex(u0, v0);
        t.tex(u0, v1);
        t.tex(u1, v1);
        t.tex(u1, v0);

        t.color(color.X, color.Y, color.Z);
    }
}
