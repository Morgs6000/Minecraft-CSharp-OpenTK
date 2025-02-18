namespace RubyDung.src.level;

public class Tile {
    public static Tile tile = new Tile(0);

    private int tex = 0;

    private Tile(int tex) {
        this.tex = tex;
    }

    public void Load(Tesselator tesselator, int x, int y, int z) {
        float x0 = (float)x + 0.0f;
        float y0 = (float)y + 0.0f;
        float z0 = (float)z + 0.0f;

        float x1 = (float)x + 1.0f;
        float y1 = (float)y + 1.0f;
        float z1 = (float)z + 1.0f;

        float u0 = (float)tex / 16.0f;
        float u1 = u0 + (1.0f / 16.0f);
        float v0 = (16.0f - 1.0f) / 16.0f;
        float v1 = v0 + (1.0f / 16.0f);

        //x0
        tesselator.Vertex(x0, y0, z0);
        tesselator.Vertex(x0, y0, z1);
        tesselator.Vertex(x0, y1, z1);
        tesselator.Vertex(x0, y1, z0);

        tesselator.Indice();

        tesselator.Tex(u0, v0);
        tesselator.Tex(u1, v0);
        tesselator.Tex(u1, v1);
        tesselator.Tex(u0, v1);

        //x1
        tesselator.Vertex(x1, y0, z1);
        tesselator.Vertex(x1, y0, z0);
        tesselator.Vertex(x1, y1, z0);
        tesselator.Vertex(x1, y1, z1);

        tesselator.Indice();

        tesselator.Tex(u0, v0);
        tesselator.Tex(u1, v0);
        tesselator.Tex(u1, v1);
        tesselator.Tex(u0, v1);

        //y0
        tesselator.Vertex(x0, y0, z0);
        tesselator.Vertex(x1, y0, z0);
        tesselator.Vertex(x1, y0, z1);
        tesselator.Vertex(x0, y0, z1);

        tesselator.Indice();

        tesselator.Tex(u0, v0);
        tesselator.Tex(u1, v0);
        tesselator.Tex(u1, v1);
        tesselator.Tex(u0, v1);

        //y1
        tesselator.Vertex(x0, y1, z1);
        tesselator.Vertex(x1, y1, z1);
        tesselator.Vertex(x1, y1, z0);
        tesselator.Vertex(x0, y1, z0);

        tesselator.Indice();

        tesselator.Tex(u0, v0);
        tesselator.Tex(u1, v0);
        tesselator.Tex(u1, v1);
        tesselator.Tex(u0, v1);

        //z0
        tesselator.Vertex(x1, y0, z0);
        tesselator.Vertex(x0, y0, z0);
        tesselator.Vertex(x0, y1, z0);
        tesselator.Vertex(x1, y1, z0);

        tesselator.Indice();

        tesselator.Tex(u0, v0);
        tesselator.Tex(u1, v0);
        tesselator.Tex(u1, v1);
        tesselator.Tex(u0, v1);

        //z1
        tesselator.Vertex(x0, y0, z1);
        tesselator.Vertex(x1, y0, z1);
        tesselator.Vertex(x1, y1, z1);
        tesselator.Vertex(x0, y1, z1);

        tesselator.Indice();

        tesselator.Tex(u0, v0);
        tesselator.Tex(u1, v0);
        tesselator.Tex(u1, v1);
        tesselator.Tex(u0, v1);
    }
}
