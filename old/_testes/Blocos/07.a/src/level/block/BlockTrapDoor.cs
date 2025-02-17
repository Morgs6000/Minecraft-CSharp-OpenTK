using OpenTK.Compute.OpenCL;
using OpenTK.Mathematics;

namespace RubyDung.src.level.block;

public class BlockTrapDoor : Block {
    public BlockTrapDoor() {
        this.setTexture(4, 5);
    }

    protected override void renderFace(Tesselator t, int x, int y, int z, faceType face) {
        float x0 = x + 0.0f;
        float y0 = y + 0.0f + ((1.0f / 16.0f) * 13.0f);
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

        float pixel = ((1.0f / 16.0f) / 16.0f);

        // ..:: Negative X ::..
        if(face == faceType.negativeX) {
            t.vertex(x0, y0, z0);
            t.vertex(x0, y1, z0);
            t.vertex(x0, y1, z1);
            t.vertex(x0, y0, z1);

            t.triangle();
            t.tex(tex.X, tex.Y);

            t.tex2(u0, v0);
            t.tex2(u0, v1);
            t.tex2(u1, v1);
            t.tex2(u1, v0);
        }

        // ..:: Positive X ::..
        if(face == faceType.positiveX) {
            t.vertex(x1, y0, z1);
            t.vertex(x1, y1, z1);
            t.vertex(x1, y1, z0);
            t.vertex(x1, y0, z0);

            t.triangle();
            t.tex(tex.X, tex.Y);

            t.tex2(u0, v0);
            t.tex2(u0, v1);
            t.tex2(u1, v1);
            t.tex2(u1, v0);
        }

        // ..:: Negative Y ::..
        if(face == faceType.negativeY) {
            t.vertex(x0, y0, z0);
            t.vertex(x0, y0, z1);
            t.vertex(x1, y0, z1);
            t.vertex(x1, y0, z0);

            t.triangle();
            t.tex(tex.X, tex.Y);

            t.tex2(u0, v0);
            t.tex2(u0, v1);
            t.tex2(u1, v1);
            t.tex2(u1, v0);
        }

        // ..:: Positive Y ::..
        if(face == faceType.positiveY) {
            t.vertex(x0, y1, z1);
            t.vertex(x0, y1, z0);
            t.vertex(x1, y1, z0);
            t.vertex(x1, y1, z1);

            t.triangle();
            t.tex(tex.X, tex.Y);

            t.tex2(u0, v0);
            t.tex2(u0, v1);
            t.tex2(u1, v1);
            t.tex2(u1, v0);
        }

        // ..:: Negative Z ::..
        if(face == faceType.negativeZ) {
            t.vertex(x1, y0, z0);
            t.vertex(x1, y1, z0);
            t.vertex(x0, y1, z0);
            t.vertex(x0, y0, z0);

            t.triangle();
            t.tex(tex.X, tex.Y);

            t.tex2(u0, v0);
            t.tex2(u0, v1);
            t.tex2(u1, v1);
            t.tex2(u1, v0);
        }

        // ..:: Positive Z ::..
        if(face == faceType.positiveZ) {
            t.vertex(x0, y0, z1);
            t.vertex(x0, y1, z1);
            t.vertex(x1, y1, z1);
            t.vertex(x1, y0, z1);

            t.triangle();
            t.tex(tex.X, tex.Y);

            t.tex2(u0, v0 + (pixel * 13.0f));
            t.tex2(u0, v1);
            t.tex2(u1, v1);
            t.tex2(u1, v0 + (pixel * 13.0f));
        }
    }
}
