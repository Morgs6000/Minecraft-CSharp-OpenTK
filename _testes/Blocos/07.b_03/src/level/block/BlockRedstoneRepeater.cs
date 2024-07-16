﻿using OpenTK.Mathematics;

namespace RubyDung.src.level.block;

public class BlockRedstoneRepeater : BlockDirectional {
    public BlockRedstoneRepeater() {
        
    }

    public override void renderFace(Tesselator t, int x, int y, int z, string face) {
        this.renderRepeater(t, x, y, z, face);
        this.renderTorch(t, x, y, z - 0.3125f, face);
        this.renderTorch(t, x, y, z - 0.0625f, face);
    }

    private void renderRepeater(Tesselator t, int x, int y, int z, string face) {
        float x0 = x + 0.0f;
        float y0 = y + 0.0f;
        float z0 = z + 0.0f;

        float x1 = x + 1.0f;
        float y1 = y + 1.0f - 0.875f;
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

            t.tex(u0, v0);
            t.tex(u0, v1 - (vPixel * 14.0f));
            t.tex(u1, v1 - (vPixel * 14.0f));
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
            t.tex(u0, v1 - (vPixel * 14.0f));
            t.tex(u1, v1 - (vPixel * 14.0f));
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
            t.tex(u0, v1 - (vPixel * 14.0f));
            t.tex(u1, v1 - (vPixel * 14.0f));
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
            t.tex(u0, v1 - (vPixel * 14.0f));
            t.tex(u1, v1 - (vPixel * 14.0f));
            t.tex(u1, v0);

            t.color(color.X, color.Y, color.Z);
        }
    }

    private void renderTorch(Tesselator t, float x, float y, float z, string face) {
        float x0 = x + 0.0f + 0.4375f;
        float y0 = y + 0.0f;
        float z0 = z + 0.0f + 0.4375f;

        float x1 = x + 1.0f - 0.4375f;
        float y1 = y + 1.0f - 0.375f;
        float z1 = z + 1.0f - 0.4375f;

        Vector2 tex = new Vector2(3, 6);
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
        t.vertex(x0, y0, z0 - 0.0625f);
        t.vertex(x0, y1 + 0.0625f, z0 - 0.0625f);
        t.vertex(x0, y1 + 0.0625f, z1 + 0.0625f);
        t.vertex(x0, y0, z1 + 0.0625f);

        t.triangle();

        t.tex(u0 + (uPixel * 6.0f), v0);
        t.tex(u0 + (uPixel * 6.0f), v1 - (vPixel * 5.0f));
        t.tex(u1 - (uPixel * 6.0f), v1 - (vPixel * 5.0f));
        t.tex(u1 - (uPixel * 6.0f), v0);

        t.color(color.X, color.Y, color.Z);

        // ..:: Positive X ::..
        t.vertex(x1, y0, z1 + 0.0625f);
        t.vertex(x1, y1 + 0.0625f, z1 + 0.0625f);
        t.vertex(x1, y1 + 0.0625f, z0 - 0.0625f);
        t.vertex(x1, y0, z0 - 0.0625f);

        t.triangle();

        t.tex(u0 + (uPixel * 6.0f), v0);
        t.tex(u0 + (uPixel * 6.0f), v1 - (vPixel * 5.0f));
        t.tex(u1 - (uPixel * 6.0f), v1 - (vPixel * 5.0f));
        t.tex(u1 - (uPixel * 6.0f), v0);

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
        t.vertex(x1 + 0.0625f, y0, z0);
        t.vertex(x1 + 0.0625f, y1 + 0.0625f, z0);
        t.vertex(x0 - 0.0625f, y1 + 0.0625f, z0);
        t.vertex(x0 - 0.0625f, y0, z0);

        t.triangle();

        t.tex(u0 + (uPixel * 6.0f), v0);
        t.tex(u0 + (uPixel * 6.0f), v1 - (vPixel * 5.0f));
        t.tex(u1 - (uPixel * 6.0f), v1 - (vPixel * 5.0f));
        t.tex(u1 - (uPixel * 6.0f), v0);

        t.color(color.X, color.Y, color.Z);

        // ..:: Positive Z ::..
        t.vertex(x0 - 0.0625f, y0, z1);
        t.vertex(x0 - 0.0625f, y1 + 0.0625f, z1);
        t.vertex(x1 + 0.0625f, y1 + 0.0625f, z1);
        t.vertex(x1 + 0.0625f, y0, z1);

        t.triangle();

        t.tex(u0 + (uPixel * 6.0f), v0);
        t.tex(u0 + (uPixel * 6.0f), v1 - (vPixel * 5.0f));
        t.tex(u1 - (uPixel * 6.0f), v1 - (vPixel * 5.0f));
        t.tex(u1 - (uPixel * 6.0f), v0);

        t.color(color.X, color.Y, color.Z);
    }
}