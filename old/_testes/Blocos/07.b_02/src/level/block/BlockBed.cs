﻿using OpenTK.Mathematics;

namespace RubyDung.src.level.block;

public class BlockBed : BlockDirectional {
    public BlockBed() {
        this.setTexture(7, 8);
    }

    protected override Vector2 getTexture(string face) {
        if(face == "y1") {
            return new Vector2(6, 8);
        }
        if(face == "y0") {
            return new Vector2(4, 0);
        }
        if(face == "z0") {
            return new Vector2(8, 9);
        }
        if(face == "z1") {
            return new Vector2(5, 9);
        }
        else {
            return new Vector2(6, 9);
        }
    }

    public override void renderFace(Tesselator t, int x, int y, int z, string face) {
        float x0 = x + 0.0f;
        float y0 = y + 0.0f;
        float z0 = z + 0.0f - 1.0f;

        float x1 = x + 1.0f;
        float y1 = y + 1.0f - 0.4375f;
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

        // ..:: Negative X ::..
        if(face == "x0") {
            t.vertex(x0, y0, z0);
            t.vertex(x0, y1, z0);
            t.vertex(x0, y1, z1);
            t.vertex(x0, y0, z1);

            t.triangle();

            t.tex(u1 + 0.0625f, v0);
            t.tex(u1 + 0.0625f, v1 - (vPixel * 7.0f));
            t.tex(u0, v1 - (vPixel * 7.0f));
            t.tex(u0, v0);

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
            t.tex(u0, v1 - (vPixel * 7.0f));
            t.tex(u1 + 0.0625f, v1 - (vPixel * 7.0f));
            t.tex(u1 + 0.0625f, v0);

            t.color(color.X, color.Y, color.Z);
        }

        // ..:: Negative Y ::..
        if(face == "y0") {
            t.vertex(x0, y0 + 0.1875f, z0);
            t.vertex(x0, y0 + 0.1875f, z1);
            t.vertex(x1, y0 + 0.1875f, z1);
            t.vertex(x1, y0 + 0.1875f, z0);

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

            t.tex(u0, v1);
            t.tex(u1 + 0.0625f, v1);
            t.tex(u1 + 0.0625f, v0);
            t.tex(u0, v0);

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
            t.tex(u0, v1 - (vPixel * 7.0f));
            t.tex(u1, v1 - (vPixel * 7.0f));
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
            t.tex(u0, v1 - (vPixel * 7.0f));
            t.tex(u1, v1 - (vPixel * 7.0f));
            t.tex(u1, v0);

            t.color(color.X, color.Y, color.Z);
        }
    }
}
