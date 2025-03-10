﻿namespace RubyDung.src.level;

public class Tile {
    public static Tile tile = new Tile();

    private int tex = 0;

    public void render(Tesselator t, int x, int y, int z) {
        float x0 = (float)x + 0.0f;
        float y0 = (float)y + 0.0f;
        float z0 = (float)z + 0.0f;

        float x1 = (float)x + 1.0f;
        float y1 = (float)y + 1.0f;
        float z1 = (float)z + 1.0f;

        float u0 = (float)this.tex / 16.0f;
        float v0 = (16.0f - 1.0f) / 16.0f;

        float u1 = u0 + (1.0f / 16.0f);
        float v1 = v0 + (1.0f / 16.0f);

        // x0
        t.tex(u0, v0);
        t.vertex(x0, y0, z0);
        t.tex(u1, v0);
        t.vertex(x0, y0, z1);
        t.tex(u1, v1);
        t.vertex(x0, y1, z1);
        t.tex(u0, v1);
        t.vertex(x0, y1, z0);

        // x1
        t.tex(u0, v0);
        t.vertex(x1, y0, z1);
        t.tex(u1, v0);
        t.vertex(x1, y0, z0);
        t.tex(u1, v1);
        t.vertex(x1, y1, z0);
        t.tex(u0, v1);
        t.vertex(x1, y1, z1);

        // y0
        t.tex(u0, v0);
        t.vertex(x0, y0, z0);
        t.tex(u1, v0);
        t.vertex(x1, y0, z0);
        t.tex(u1, v1);
        t.vertex(x1, y0, z1);
        t.tex(u0, v1);
        t.vertex(x0, y0, z1);

        // y1
        t.tex(u0, v0);
        t.vertex(x0, y1, z1);
        t.tex(u1, v0);
        t.vertex(x1, y1, z1);
        t.tex(u1, v1);
        t.vertex(x1, y1, z0);
        t.tex(u0, v1);
        t.vertex(x0, y1, z0);

        // z0
        t.tex(u0, v0);
        t.vertex(x1, y0, z0);
        t.tex(u1, v0);
        t.vertex(x0, y0, z0);
        t.tex(u1, v1);
        t.vertex(x0, y1, z0);
        t.tex(u0, v1);
        t.vertex(x1, y1, z0);

        // z1
        t.tex(u0, v0);
        t.vertex(x0, y0, z1);
        t.tex(u1, v0);
        t.vertex(x1, y0, z1);
        t.tex(u1, v1);
        t.vertex(x1, y1, z1);
        t.tex(u0, v1);
        t.vertex(x0, y1, z1);
    }
}
