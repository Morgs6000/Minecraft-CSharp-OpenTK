namespace RubyDung.src.level;

public class Tile {
    public static readonly Tile[] tiles = new Tile[256];

    public static readonly Tile empty = null;
    public static readonly Tile rock = new Tile(1, 1);
    public static readonly Tile grass = new Tile(2, 3);
    public static readonly Tile dirt = new Tile(3, 2);
    public static readonly Tile stoneBrick = new Tile(4, 16);
    public static readonly Tile wood = new Tile(5, 4);

    public int tex;
    public readonly int id;

    protected Tile(int id) {
        tiles[id] = this;
        this.id = id;
    }

    protected Tile(int id, int tex) : this(id) {
        this.tex = tex;
    }

    public void Load(Tesselator tesselator, Level level, int x, int y, int z) {
        if(!level.IsSolidTile(x, y, z + 1)) {
            RenderFace(tesselator, x, y, z, 5);
        }
    }

    protected virtual int GetTexture(int face) {
        return tex;
    }

    public void RenderFace(Tesselator tesselator, int x, int y, int z, int face) {
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

        if(face == 5) {
            tesselator.Vertex(x0, y0, z0);
            tesselator.Vertex(x1, y0, z0);
            tesselator.Vertex(x1, y1, z0);
            tesselator.Vertex(x0, y1, z0);

            tesselator.Indice();

            tesselator.Tex(u0, v0);
            tesselator.Tex(u1, v0);
            tesselator.Tex(u1, v1);
            tesselator.Tex(u0, v1);
        }
    }

    public bool IsSolid() {
        return true;
    }
}
