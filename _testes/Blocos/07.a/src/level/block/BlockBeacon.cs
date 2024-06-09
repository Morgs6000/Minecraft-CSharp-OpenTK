using OpenTK.Mathematics;
using static System.Net.Mime.MediaTypeNames;

namespace RubyDung.src.level.block;

public class BlockBeacon : Block {
    public BlockBeacon() {
        //this.tex = new Vector2(5, 2);
    }

    protected override void renderFace(Tesselator t, int x, int y, int z, faceType face) {
        this.renderGlass(t, x, y, z, face);
        this.renderObsidian(t, x, y, z);
        this.renderBeacon(t, x, y, z);
    }

    public void renderGlass(Tesselator t, int x, int y, int z, faceType face) {
        float x0 = x + 0.0f;
        float y0 = y + 0.0f;
        float z0 = z + 0.0f;

        float x1 = x + 1.0f;
        float y1 = y + 1.0f;
        float z1 = z + 1.0f;

        Vector2 tex = new Vector2(1, 3);

        // ..:: Negative X ::..
        if(face == faceType.negativeX) {
            t.vertex(x0, y0, z0);
            t.vertex(x0, y1, z0);
            t.vertex(x0, y1, z1);
            t.vertex(x0, y0, z1);

            t.triangle();
            t.tex(tex.X, tex.Y);
        }

        // ..:: Positive X ::..
        if(face == faceType.positiveX) {
            t.vertex(x1, y0, z1);
            t.vertex(x1, y1, z1);
            t.vertex(x1, y1, z0);
            t.vertex(x1, y0, z0);

            t.triangle();
            t.tex(tex.X, tex.Y);
        }

        // ..:: Negative Y ::..
        if(face == faceType.negativeY) {
            t.vertex(x0, y0, z0);
            t.vertex(x0, y0, z1);
            t.vertex(x1, y0, z1);
            t.vertex(x1, y0, z0);

            t.triangle();
            t.tex(tex.X, tex.Y);
        }

        // ..:: Positive Y ::..
        if(face == faceType.positiveY) {
            t.vertex(x0, y1, z1);
            t.vertex(x0, y1, z0);
            t.vertex(x1, y1, z0);
            t.vertex(x1, y1, z1);

            t.triangle();
            t.tex(tex.X, tex.Y);
        }

        // ..:: Negative Z ::..
        if(face == faceType.negativeZ) {
            t.vertex(x1, y0, z0);
            t.vertex(x1, y1, z0);
            t.vertex(x0, y1, z0);
            t.vertex(x0, y0, z0);

            t.triangle();
            t.tex(tex.X, tex.Y);
        }

        // ..:: Positive Z ::..
        if(face == faceType.positiveZ) {
            t.vertex(x0, y0, z1);
            t.vertex(x0, y1, z1);
            t.vertex(x1, y1, z1);
            t.vertex(x1, y0, z1);

            t.triangle();
            t.tex(tex.X, tex.Y);
        }
    }

    public void renderObsidian(Tesselator t, int x, int y, int z) {
        float x0 = x + 0.0f + ((1.0f / 16.0f) * 2.0f);
        float y0 = y + 0.0f;
        float z0 = z + 0.0f + ((1.0f / 16.0f) * 2.0f);

        float x1 = x + 1.0f - ((1.0f / 16.0f) * 2.0f);
        float y1 = y + 1.0f - ((1.0f / 16.0f) * 13.0f);
        float z1 = z + 1.0f - ((1.0f / 16.0f) * 2.0f);

        float col = 16.0f;
        float row = 16.0f;

        Vector2 tex = new Vector2(5, 2);

        float u0 = tex.X / col;
        float u1 = u0 + 1.0f / col;
        float v0 = (row - 1.0f - tex.Y) / row;
        float v1 = v0 + 1.0f / row;

        float pixel = ((1.0f / 16.0f) / 16.0f);

        // ..:: Negative X ::..
        t.vertex(x0, y0, z0);
        t.vertex(x0, y1, z0);
        t.vertex(x0, y1, z1);
        t.vertex(x0, y0, z1);

        t.triangle();

        t.tex2(u0 + (pixel * 2.0f), v0);
        t.tex2(u0 + (pixel * 2.0f), v1 - (pixel * 13.0f));
        t.tex2(u1 - (pixel * 2.0f), v1 - (pixel * 13.0f));
        t.tex2(u1 - (pixel * 2.0f), v0);

        // ..:: Positive X ::..
        t.vertex(x1, y0, z1);
        t.vertex(x1, y1, z1);
        t.vertex(x1, y1, z0);
        t.vertex(x1, y0, z0);

        t.triangle();

        t.tex2(u0 + (pixel * 2.0f), v0);
        t.tex2(u0 + (pixel * 2.0f), v1 - (pixel * 13.0f));
        t.tex2(u1 - (pixel * 2.0f), v1 - (pixel * 13.0f));
        t.tex2(u1 - (pixel * 2.0f), v0);

        // ..:: Negative Y ::..
        t.vertex(x0, y0, z0);
        t.vertex(x0, y0, z1);
        t.vertex(x1, y0, z1);
        t.vertex(x1, y0, z0);

        t.triangle();

        t.tex2(u0 + (pixel * 2.0f), v0 + (pixel * 2.0f));
        t.tex2(u0 + (pixel * 2.0f), v1 - (pixel * 2.0f));
        t.tex2(u1 - (pixel * 2.0f), v1 - (pixel * 2.0f));
        t.tex2(u1 - (pixel * 2.0f), v0 + (pixel * 2.0f));

        // ..:: Positive Y ::..
        t.vertex(x0, y1, z1);
        t.vertex(x0, y1, z0);
        t.vertex(x1, y1, z0);
        t.vertex(x1, y1, z1);

        t.triangle();

        t.tex2(u0 + (pixel * 2.0f), v0 + (pixel * 2.0f));
        t.tex2(u0 + (pixel * 2.0f), v1 - (pixel * 2.0f));
        t.tex2(u1 - (pixel * 2.0f), v1 - (pixel * 2.0f));
        t.tex2(u1 - (pixel * 2.0f), v0 + (pixel * 2.0f));

        // ..:: Negative Z ::..
        t.vertex(x1, y0, z0);
        t.vertex(x1, y1, z0);
        t.vertex(x0, y1, z0);
        t.vertex(x0, y0, z0);

        t.triangle();

        t.tex2(u0 + (pixel * 2.0f), v0);
        t.tex2(u0 + (pixel * 2.0f), v1 - (pixel * 13.0f));
        t.tex2(u1 - (pixel * 2.0f), v1 - (pixel * 13.0f));
        t.tex2(u1 - (pixel * 2.0f), v0);

        // ..:: Positive Z ::..
        t.vertex(x0, y0, z1);
        t.vertex(x0, y1, z1);
        t.vertex(x1, y1, z1);
        t.vertex(x1, y0, z1);

        t.triangle();

        t.tex2(u0 + (pixel * 2.0f), v0);
        t.tex2(u0 + (pixel * 2.0f), v1 - (pixel * 13.0f));
        t.tex2(u1 - (pixel * 2.0f), v1 - (pixel * 13.0f));
        t.tex2(u1 - (pixel * 2.0f), v0);
    }

    public void renderBeacon(Tesselator t, int x, int y, int z) {
        float x0 = x + 0.0f + ((1.0f / 16.0f) * 3.0f);
        float y0 = y + 0.0f + ((1.0f / 16.0f) * 3.0f);
        float z0 = z + 0.0f + ((1.0f / 16.0f) * 3.0f);

        float x1 = x + 1.0f - ((1.0f / 16.0f) * 3.0f);
        float y1 = y + 1.0f - ((1.0f / 16.0f) * 3.0f);
        float z1 = z + 1.0f - ((1.0f / 16.0f) * 3.0f);

        float col = 16.0f;
        float row = 16.0f;

        Vector2 tex = new Vector2(9, 2);

        float u0 = tex.X / col;
        float u1 = u0 + 1.0f / col;
        float v0 = (row - 1.0f - tex.Y) / row;
        float v1 = v0 + 1.0f / row;

        u0 += (((1.0f / 16.0f) / 16.0f) * 3.0f);
        u1 -= (((1.0f / 16.0f) / 16.0f) * 3.0f);
        v0 += (((1.0f / 16.0f) / 16.0f) * 3.0f);
        v1 -= (((1.0f / 16.0f) / 16.0f) * 3.0f);

        // ..:: Negative X ::..
        t.vertex(x0, y0, z0);
        t.vertex(x0, y1, z0);
        t.vertex(x0, y1, z1);
        t.vertex(x0, y0, z1);

        t.triangle();

        t.tex2(u0, v0);
        t.tex2(u0, v1);
        t.tex2(u1, v1);
        t.tex2(u1, v0);

        // ..:: Positive X ::..
        t.vertex(x1, y0, z1);
        t.vertex(x1, y1, z1);
        t.vertex(x1, y1, z0);
        t.vertex(x1, y0, z0);

        t.triangle();

        t.tex2(u0, v0);
        t.tex2(u0, v1);
        t.tex2(u1, v1);
        t.tex2(u1, v0);

        // ..:: Negative Y ::..
        t.vertex(x0, y0, z0);
        t.vertex(x0, y0, z1);
        t.vertex(x1, y0, z1);
        t.vertex(x1, y0, z0);

        t.triangle();

        t.tex2(u0, v0);
        t.tex2(u0, v1);
        t.tex2(u1, v1);
        t.tex2(u1, v0);

        // ..:: Positive Y ::..
        t.vertex(x0, y1, z1);
        t.vertex(x0, y1, z0);
        t.vertex(x1, y1, z0);
        t.vertex(x1, y1, z1);

        t.triangle();

        t.tex2(u0, v0);
        t.tex2(u0, v1);
        t.tex2(u1, v1);
        t.tex2(u1, v0);

        // ..:: Negative Z ::..
        t.vertex(x1, y0, z0);
        t.vertex(x1, y1, z0);
        t.vertex(x0, y1, z0);
        t.vertex(x0, y0, z0);

        t.triangle();

        t.tex2(u0, v0);
        t.tex2(u0, v1);
        t.tex2(u1, v1);
        t.tex2(u1, v0);

        // ..:: Positive Z ::..
        t.vertex(x0, y0, z1);
        t.vertex(x0, y1, z1);
        t.vertex(x1, y1, z1);
        t.vertex(x1, y0, z1);

        t.triangle();

        t.tex2(u0, v0);
        t.tex2(u0, v1);
        t.tex2(u1, v1);
        t.tex2(u1, v0);
    }
}
