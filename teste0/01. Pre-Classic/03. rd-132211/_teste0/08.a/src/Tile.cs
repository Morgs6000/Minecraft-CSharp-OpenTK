﻿namespace RubyDung.src;

public class Tile {
    public static Tile rock = new Tile(0);
    public static Tile grass = new Tile(1);

    private int tex = 0;

    private Tile(int tex) {
        this.tex = tex;
    }

    public void render(Tesselator t, int x, int y, int z) {
        float x0 = (float)x + -0.5f;
        float y0 = (float)y + -0.5f;
        float z0 = (float)z + -0.5f;

        float x1 = (float)x + 0.5f;
        float y1 = (float)y + 0.5f;
        float z1 = (float)z + 0.5f;

        float u0 = (float)tex / 16.0f;
        float v0 = (16.0f - 1.0f) / 16.0f;

        float u1 = u0 + (1.0f / 16.0f);
        float v1 = v0 + (1.0f / 16.0f);

        // x0
        t.vertex(x0, y0, z0);
        t.vertex(x0, y0, z1);
        t.vertex(x0, y1, z1);
        t.vertex(x0, y1, z0);

        t.indice();
        t.tex(u0, u1, v0, v1);

        // x1
        t.vertex(x1, y0, z1);
        t.vertex(x1, y0, z0);
        t.vertex(x1, y1, z0);
        t.vertex(x1, y1, z1);

        t.indice();
        t.tex(u0, u1, v0, v1);

        // y0
        t.vertex(x0, y0, z0);
        t.vertex(x1, y0, z0);
        t.vertex(x1, y0, z1);
        t.vertex(x0, y0, z1);

        t.indice();
        t.tex(u0, u1, v0, v1);

        // y1
        t.vertex(x0, y1, z1);
        t.vertex(x1, y1, z1);
        t.vertex(x1, y1, z0);
        t.vertex(x0, y1, z0);

        t.indice();
        t.tex(u0, u1, v0, v1);

        // z0
        t.vertex(x1, y0, z0);
        t.vertex(x0, y0, z0);
        t.vertex(x0, y1, z0);
        t.vertex(x1, y1, z0);

        t.indice();
        t.tex(u0, u1, v0, v1);

        // z1
        t.vertex(x0, y0, z1);
        t.vertex(x1, y0, z1);
        t.vertex(x1, y1, z1);
        t.vertex(x0, y1, z1);

        t.indice();
        t.tex(u0, u1, v0, v1);
    }
}
