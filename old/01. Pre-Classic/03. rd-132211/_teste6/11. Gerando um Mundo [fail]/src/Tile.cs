namespace RubyDung.src;

public class Tile {
    public static Tile rock = new Tile(0);
    public static Tile grass = new Tile(1);

    private int tex = 0;

    private Tile(int tex) {
        this.tex = tex;
    }

    public void blockGen(Tesselator t, Level level, int x, int y, int z) {
        float x0 = (float)x + -0.5f;
        float y0 = (float)y + -0.5f;
        float z0 = (float)z + -0.5f;

        float x1 = (float)x + 0.5f;
        float y1 = (float)y + 0.5f;
        float z1 = (float)z + 0.5f;

        float u0 = (float)this.tex / 16;
        float v0 = (float)(16 - 1) / 16;

        float u1 = u0 + ((float)1 / 16);
        float v1 = v0 + ((float)1 / 16);

        // x0
        if(!level.isSolidTile(x - 1, y, z)) {
            t.addTexCoords(u0, v0);
            t.addVertices(x0, y0, z0); // Bottom-left vertex
            t.addTexCoords(u1, v0);
            t.addVertices(x0, y0, z1); // Bottom-right vertex
            t.addTexCoords(u1, v1);
            t.addVertices(x0, y1, z1); // Top-right vertex
            t.addTexCoords(u0, v1);
            t.addVertices(x0, y1, z0); // Top-left vertex

            t.addIndices();
        }

        // x1
        if(!level.isSolidTile(x + 1, y, z)) {
            t.addTexCoords(u0, v0);
            t.addVertices(x1, y0, z1); // Bottom-left vertex
            t.addTexCoords(u1, v0);
            t.addVertices(x1, y0, z0); // Bottom-right vertex
            t.addTexCoords(u1, v1);
            t.addVertices(x1, y1, z0); // Top-right vertex
            t.addTexCoords(u0, v1);
            t.addVertices(x1, y1, z1); // Top-left vertex

            t.addIndices();
        }

        // y0
        if(!level.isSolidTile(x, y - 1, z)) {
            t.addTexCoords(u0, v0);
            t.addVertices(x0, y0, z0); // Bottom-left vertex
            t.addTexCoords(u1, v0);
            t.addVertices(x1, y0, z0); // Bottom-right vertex
            t.addTexCoords(u1, v1);
            t.addVertices(x1, y0, z1); // Top-right vertex
            t.addTexCoords(u0, v1);
            t.addVertices(x0, y0, z1); // Top-left vertex

            t.addIndices();
        }

        // y1
        if(!level.isSolidTile(x, y + 1, z)) {
            t.addTexCoords(u0, v0);
            t.addVertices(x0, y1, z1); // Bottom-left vertex
            t.addTexCoords(u1, v0);
            t.addVertices(x1, y1, z1); // Bottom-right vertex
            t.addTexCoords(u1, v1);
            t.addVertices(x1, y1, z0); // Top-right vertex
            t.addTexCoords(u0, v1);
            t.addVertices(x0, y1, z0); // Top-left vertex

            t.addIndices();
        }

        // z0
        if(!level.isSolidTile(x, y, z - 1)) {
            t.addTexCoords(u0, v0);
            t.addVertices(x1, y0, z0); // Bottom-left vertex
            t.addTexCoords(u1, v0);
            t.addVertices(x0, y0, z0); // Bottom-right vertex
            t.addTexCoords(u1, v1);
            t.addVertices(x0, y1, z0); // Top-right vertex
            t.addTexCoords(u0, v1);
            t.addVertices(x1, y1, z0); // Top-left vertex

            t.addIndices();
        }

        // z1
        if(!level.isSolidTile(x, y, z + 1)) {
            t.addTexCoords(u0, v0);
            t.addVertices(x0, y0, z1); // Bottom-left vertex
            t.addTexCoords(u1, v0);
            t.addVertices(x1, y0, z1); // Bottom-right vertex
            t.addTexCoords(u1, v1);
            t.addVertices(x1, y1, z1); // Top-right vertex
            t.addTexCoords(u0, v1);
            t.addVertices(x0, y1, z1); // Top-left vertex

            t.addIndices();
        }
    }
}
