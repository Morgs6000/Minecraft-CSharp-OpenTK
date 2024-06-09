namespace RubyDung.src.level.block;

public class BlockCrops : BlockFlower {
    public BlockCrops() {
        
    }

    protected override void renderFace(Tesselator t, int x, int y, int z, string face) {
        float x0 = x + 0.0f;
        float y0 = y + 0.0f;
        float z0 = z + 0.0f;

        float x1 = x + 1.0f;
        float y1 = y + 1.0f;
        float z1 = z + 1.0f;

        float col = 16.0f;
        float row = 16.0f;

        float u0 = tex.X / col;
        float u1 = u0 + 1.0f / col;
        float v0 = (row - 1.0f - tex.Y) / row;
        float v1 = v0 + 1.0f / row;

        // ..:: Negative X ::..
        t.vertex(x0 + 0.25f, y0, z0);
        t.vertex(x0 + 0.25f, y1, z0);
        t.vertex(x0 + 0.25f, y1, z1);
        t.vertex(x0 + 0.25f, y0, z1);

        t.triangle();

        t.tex(u0, v0);
        t.tex(u0, v1);
        t.tex(u1, v1);
        t.tex(u1, v0);

        t.color(color.X, color.Y, color.Z);

        // ..:: Positive X ::..
        t.vertex(x1 - 0.75f, y0, z1);
        t.vertex(x1 - 0.75f, y1, z1);
        t.vertex(x1 - 0.75f, y1, z0);
        t.vertex(x1 - 0.75f, y0, z0);

        t.triangle();

        t.tex(u0, v0);
        t.tex(u0, v1);
        t.tex(u1, v1);
        t.tex(u1, v0);

        t.color(color.X, color.Y, color.Z);

        // ..:: Negative X ::..
        t.vertex(x0 + 0.75f, y0, z0);
        t.vertex(x0 + 0.75f, y1, z0);
        t.vertex(x0 + 0.75f, y1, z1);
        t.vertex(x0 + 0.75f, y0, z1);

        t.triangle();

        t.tex(u0, v0);
        t.tex(u0, v1);
        t.tex(u1, v1);
        t.tex(u1, v0);

        t.color(color.X, color.Y, color.Z);

        // ..:: Positive X ::..
        t.vertex(x1 - 0.25f, y0, z1);
        t.vertex(x1 - 0.25f, y1, z1);
        t.vertex(x1 - 0.25f, y1, z0);
        t.vertex(x1 - 0.25f, y0, z0);

        t.triangle();

        t.tex(u0, v0);
        t.tex(u0, v1);
        t.tex(u1, v1);
        t.tex(u1, v0);

        t.color(color.X, color.Y, color.Z);

        // ..:: Negative Z ::..
        t.vertex(x1, y0, z0 + 0.25f);
        t.vertex(x1, y1, z0 + 0.25f);
        t.vertex(x0, y1, z0 + 0.25f);
        t.vertex(x0, y0, z0 + 0.25f);

        t.triangle();

        t.tex(u0, v0);
        t.tex(u0, v1);
        t.tex(u1, v1);
        t.tex(u1, v0);

        t.color(color.X, color.Y, color.Z);

        // ..:: Positive Z ::..
        t.vertex(x0, y0, z1 - 0.75f);
        t.vertex(x0, y1, z1 - 0.75f);
        t.vertex(x1, y1, z1 - 0.75f);
        t.vertex(x1, y0, z1 - 0.75f);

        t.triangle();

        t.tex(u0, v0);
        t.tex(u0, v1);
        t.tex(u1, v1);
        t.tex(u1, v0);

        t.color(color.X, color.Y, color.Z);

        // ..:: Negative Z ::..
        t.vertex(x1, y0, z0 + 0.75f);
        t.vertex(x1, y1, z0 + 0.75f);
        t.vertex(x0, y1, z0 + 0.75f);
        t.vertex(x0, y0, z0 + 0.75f);

        t.triangle();

        t.tex(u0, v0);
        t.tex(u0, v1);
        t.tex(u1, v1);
        t.tex(u1, v0);

        t.color(color.X, color.Y, color.Z);

        // ..:: Positive Z ::..
        t.vertex(x0, y0, z1 - 0.25f);
        t.vertex(x0, y1, z1 - 0.25f);
        t.vertex(x1, y1, z1 - 0.25f);
        t.vertex(x1, y0, z1 - 0.25f);

        t.triangle();

        t.tex(u0, v0);
        t.tex(u0, v1);
        t.tex(u1, v1);
        t.tex(u1, v0);

        t.color(color.X, color.Y, color.Z);
    }
}
