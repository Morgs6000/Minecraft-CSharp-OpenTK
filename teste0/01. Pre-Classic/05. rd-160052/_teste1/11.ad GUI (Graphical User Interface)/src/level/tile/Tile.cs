namespace RubyDung;

public class Tile {
    public static readonly Tile[] tiles = new Tile[256];
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
    
    public void OnLoad(Tesselator t, Level level, int x, int y, int z) {
        float cx = 0.6f;
        float cy = 1.0f;
        float cz = 0.8f;

        // x0
        if(!level.IsSolidTile(x - 1, y, z)) {
            RenderFace(t, x, y, z, 0);
            t.Color(cx, cx, cx);
        }

        // x1
        if(!level.IsSolidTile(x + 1, y, z)) {
            RenderFace(t, x, y, z, 1);
            t.Color(cx, cx, cx);
        }

        // y0
        if(!level.IsSolidTile(x, y - 1, z)) {
            RenderFace(t, x, y, z, 2);
            t.Color(cy, cy, cy);
        }

        // y1
        if(!level.IsSolidTile(x, y + 1, z)) {
            RenderFace(t, x, y, z, 3);
            t.Color(cy, cy, cy);
        }

        // z0
        if(!level.IsSolidTile(x, y, z - 1)) {
            RenderFace(t, x, y, z, 4);
            t.Color(cz, cz, cz);
        }

        // z1
        if(!level.IsSolidTile(x, y, z + 1)) {
            RenderFace(t, x, y, z, 5);
            t.Color(cz, cz, cz);
        }
    }

    protected virtual int GetTexture(int face) {
        return tex;
    }

    public void RenderFace(Tesselator t, int x, int y, int z, int face) {
        float x0 = (float)x + 0.0f;
        float y0 = (float)y + 0.0f;
        float z0 = (float)z + 0.0f;

        float x1 = (float)x + 1.0f;
        float y1 = (float)y + 1.0f;
        float z1 = (float)z + 1.0f;

        int tex = GetTexture(face);

        float u0 = (float)(tex % 16) / 16.0f;
        float v0 = (16.0f - 1.0f - tex / 16) / 16.0f;

        float u1 = u0 + (1.0f / 16.0f);
        float v1 = v0 + (1.0f / 16.0f);

        // x0
        if(face == 0) {
            t.Vertex(x0, y0, z0);
            t.Vertex(x0, y0, z1);
            t.Vertex(x0, y1, z1);
            t.Vertex(x0, y1, z0);
            t.Indice();
            t.Tex(u0, v0);
            t.Tex(u1, v0);
            t.Tex(u1, v1);
            t.Tex(u0, v1);
        }

        // x1
        if(face == 1) {
            t.Vertex(x1, y0, z1);
            t.Vertex(x1, y0, z0);
            t.Vertex(x1, y1, z0);
            t.Vertex(x1, y1, z1);
            t.Indice();
            t.Tex(u0, v0);
            t.Tex(u1, v0);
            t.Tex(u1, v1);
            t.Tex(u0, v1);
        }

        // y0
        if(face == 2) {
            t.Vertex(x0, y0, z0);
            t.Vertex(x1, y0, z0);
            t.Vertex(x1, y0, z1);
            t.Vertex(x0, y0, z1);
            t.Indice();
            t.Tex(u0, v0);
            t.Tex(u1, v0);
            t.Tex(u1, v1);
            t.Tex(u0, v1);
        }

        // y1
        if(face == 3) {
            t.Vertex(x0, y1, z1);
            t.Vertex(x1, y1, z1);
            t.Vertex(x1, y1, z0);
            t.Vertex(x0, y1, z0);
            t.Indice();
            t.Tex(u0, v0);
            t.Tex(u1, v0);
            t.Tex(u1, v1);
            t.Tex(u0, v1);
        }

        // z0
        if(face == 4) {
            t.Vertex(x1, y0, z0);
            t.Vertex(x0, y0, z0);
            t.Vertex(x0, y1, z0);
            t.Vertex(x1, y1, z0);
            t.Indice();
            t.Tex(u0, v0);
            t.Tex(u1, v0);
            t.Tex(u1, v1);
            t.Tex(u0, v1);
        }

        // z1
        if(face == 5) {
            t.Vertex(x0, y0, z1);
            t.Vertex(x1, y0, z1);
            t.Vertex(x1, y1, z1);
            t.Vertex(x0, y1, z1);
            t.Indice();
            t.Tex(u0, v0);
            t.Tex(u1, v0);
            t.Tex(u1, v1);
            t.Tex(u0, v1);
        }
    }

    public bool IsSolid() {
        return true;
    }
}