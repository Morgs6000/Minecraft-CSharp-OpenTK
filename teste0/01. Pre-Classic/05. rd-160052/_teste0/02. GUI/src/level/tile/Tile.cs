namespace RubyDung.src.level.tile;

public class Tile {
    public static readonly Tile[] tiles = new Tile[256];

    public static readonly Tile empty = null;
    public static readonly Tile rock = new Tile(1, 1);
    public static readonly Tile grass = new GrassTile(2);
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
        //x0
        if(!level.IsSolidTile(x - 1, y, z)) {
            RenderFace(tesselator, x, y, z, 0);
        }

        //x1
        if(!level.IsSolidTile(x + 1, y, z)) {
            RenderFace(tesselator, x, y, z, 1);
        }

        //y0
        if(!level.IsSolidTile(x, y - 1, z)) {
            RenderFace(tesselator, x, y, z, 2);
        }

        //y1
        if(!level.IsSolidTile(x, y + 1, z)) {
            RenderFace(tesselator, x, y, z, 3);
        }

        //z0
        if(!level.IsSolidTile(x, y, z - 1)) {
            RenderFace(tesselator, x, y, z, 4);
        }

        //z1
        if(!level.IsSolidTile(x, y, z + 1)) {
            RenderFace(tesselator, x, y, z, 5);
        }
    }

    protected virtual int GetTexture(int face) {
        return tex;
    }

    public void RenderFace(Tesselator tesselator, int x, int y, int z, int face) {
        float x0 = x + 0.0f;
        float y0 = y + 0.0f;
        float z0 = z + 0.0f;

        float x1 = x + 1.0f;
        float y1 = y + 1.0f;
        float z1 = z + 1.0f;

        int tex = GetTexture(face);

        float u0 = tex % 16 / 16.0f;
        float v0 = (float)(16.0f - 1.0f - tex / 16) / 16.0f;

        float u1 = u0 + 1.0f / 16.0f;
        float v1 = v0 + 1.0f / 16.0f;

        //x0
        if(face == 0) {
            tesselator.VertexUV(x0, y0, z0, u0, v0);
            tesselator.VertexUV(x0, y0, z1, u1, v0);
            tesselator.VertexUV(x0, y1, z1, u1, v1);
            tesselator.VertexUV(x0, y1, z0, u0, v1);

            tesselator.Indice();
        }

        //x1
        if(face == 1) {
            tesselator.VertexUV(x1, y0, z1, u0, v0);
            tesselator.VertexUV(x1, y0, z0, u1, v0);
            tesselator.VertexUV(x1, y1, z0, u1, v1);
            tesselator.VertexUV(x1, y1, z1, u0, v1);

            tesselator.Indice();
        }

        //y0
        if(face == 2) {
            tesselator.VertexUV(x0, y0, z0, u0, v0);
            tesselator.VertexUV(x1, y0, z0, u1, v0);
            tesselator.VertexUV(x1, y0, z1, u1, v1);
            tesselator.VertexUV(x0, y0, z1, u0, v1);

            tesselator.Indice();
        }

        //y1
        if(face == 3) {
            tesselator.VertexUV(x0, y1, z1, u0, v0);
            tesselator.VertexUV(x1, y1, z1, u1, v0);
            tesselator.VertexUV(x1, y1, z0, u1, v1);
            tesselator.VertexUV(x0, y1, z0, u0, v1);

            tesselator.Indice();
        }

        //z0
        if(face == 4) {
            tesselator.VertexUV(x1, y0, z0, u0, v0);
            tesselator.VertexUV(x0, y0, z0, u1, v0);
            tesselator.VertexUV(x0, y1, z0, u1, v1);
            tesselator.VertexUV(x1, y1, z0, u0, v1);

            tesselator.Indice();
        }

        //z1
        if(face == 5) {
            tesselator.VertexUV(x0, y0, z1, u0, v0);
            tesselator.VertexUV(x1, y0, z1, u1, v0);
            tesselator.VertexUV(x1, y1, z1, u1, v1);
            tesselator.VertexUV(x0, y1, z1, u0, v1);

            tesselator.Indice();
        }
    }

    public bool IsSolid() {
        return true;
    }
}
