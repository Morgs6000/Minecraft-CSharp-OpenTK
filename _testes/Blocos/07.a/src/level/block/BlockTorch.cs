using OpenTK.Mathematics;

namespace RubyDung.src.level.block;

public class BlockTorch : Block {
    public BlockTorch() {
        this.setTexture(0, 5);
    }

    protected override void renderFace(Tesselator t, int x, int y, int z, faceType face) {
        float x0 = x + 0.0f + ((1.0f / 16.0f) * 7.0f);
        float y0 = y + 0.0f;
        float z0 = z + 0.0f + ((1.0f / 16.0f) * 7.0f);

        float x1 = x + 1.0f - ((1.0f / 16.0f) * 7.0f);
        float y1 = y + 1.0f - ((1.0f / 16.0f) * 6.0f);
        float z1 = z + 1.0f - ((1.0f / 16.0f) * 7.0f);

        float col = 16.0f;
        float row = 16.0f;

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

        t.tex2(u0 + (pixel * 7.0f), v0);
        t.tex2(u0 + (pixel * 7.0f), v1 - (pixel * 6.0f));
        t.tex2(u1 - (pixel * 7.0f), v1 - (pixel * 6.0f));
        t.tex2(u1 - (pixel * 7.0f), v0);

        // ..:: Positive X ::..
        t.vertex(x1, y0, z1);
        t.vertex(x1, y1, z1);
        t.vertex(x1, y1, z0);
        t.vertex(x1, y0, z0);

        t.triangle();

        t.tex2(u0 + (pixel * 7.0f), v0);
        t.tex2(u0 + (pixel * 7.0f), v1 - (pixel * 6.0f));
        t.tex2(u1 - (pixel * 7.0f), v1 - (pixel * 6.0f));
        t.tex2(u1 - (pixel * 7.0f), v0);

        // ..:: Negative Y ::..
        t.vertex(x0, y0, z0);
        t.vertex(x0, y0, z1);
        t.vertex(x1, y0, z1);
        t.vertex(x1, y0, z0);

        t.triangle();

        t.tex2(u0 + (pixel * 7.0f), v0);
        t.tex2(u0 + (pixel * 7.0f), v1 - (pixel * 14.0f));
        t.tex2(u1 - (pixel * 7.0f), v1 - (pixel * 14.0f));
        t.tex2(u1 - (pixel * 7.0f), v0);

        // ..:: Positive Y ::..
        t.vertex(x0, y1, z1);
        t.vertex(x0, y1, z0);
        t.vertex(x1, y1, z0);
        t.vertex(x1, y1, z1);

        t.triangle();

        t.tex2(u0 + (pixel * 7.0f), v0 + (pixel * 8.0f));
        t.tex2(u0 + (pixel * 7.0f), v1 - (pixel * 6.0f));
        t.tex2(u1 - (pixel * 7.0f), v1 - (pixel * 6.0f));
        t.tex2(u1 - (pixel * 7.0f), v0 + (pixel * 8.0f));

        // ..:: Negative Z ::..
        t.vertex(x1, y0, z0);
        t.vertex(x1, y1, z0);
        t.vertex(x0, y1, z0);
        t.vertex(x0, y0, z0);

        t.triangle();

        t.tex2(u0 + (pixel * 7.0f), v0);
        t.tex2(u0 + (pixel * 7.0f), v1 - (pixel * 6.0f));
        t.tex2(u1 - (pixel * 7.0f), v1 - (pixel * 6.0f));
        t.tex2(u1 - (pixel * 7.0f), v0);

        // ..:: Positive Z ::..
        t.vertex(x0, y0, z1);
        t.vertex(x0, y1, z1);
        t.vertex(x1, y1, z1);
        t.vertex(x1, y0, z1);

        t.triangle();

        t.tex2(u0 + (pixel * 7.0f), v0);
        t.tex2(u0 + (pixel * 7.0f), v1 - (pixel * 6.0f));
        t.tex2(u1 - (pixel * 7.0f), v1 - (pixel * 6.0f));
        t.tex2(u1 - (pixel * 7.0f), v0);
    }
}
