namespace RubyDung;

public class Tile {
    public static readonly Tile[] tiles = new Tile[256];
    public static readonly Tile rock = new Tile(1, 1);
    public static readonly Tile grass = new Tile(2, 0);
    public static readonly Tile dirt = new Tile(3, 2);
    public static readonly Tile stoneBrick = new Tile(4, 16);
    public static readonly Tile wood = new Tile(5, 4);

    public int tex;
    public readonly int id;

    private Tile(int id) {
        tiles[id] = this;
        this.id = id;
    }

    protected Tile(int id, int tex) : this(id) {
        this.tex = tex;
    }
    
    public void OnLoad(Tesselator t, Level level, int x, int y, int z) {
        float x0 = (float)x + 0.0f;
        float y0 = (float)y + 0.0f;
        float z0 = (float)z + 0.0f;

        float x1 = (float)x + 1.0f;
        float y1 = (float)y + 1.0f;
        float z1 = (float)z + 1.0f;

        float u0 = (float)(tex % 16) / 16.0f;
        float v0 = (16.0f - 1.0f - tex / 16) / 16.0f;

        float u1 = u0 + (1.0f / 16.0f);
        float v1 = v0 + (1.0f / 16.0f);
        
        float cx = 0.6f;
        float cy = 1.0f;
        float cz = 0.8f;

        float br;

        // x0
        if(!level.IsSolidTile(x - 1, y, z)) {
            br = level.GetBrightness(x - 1, y, z) * cx;
            if(br == cx) {
                t.Vertex(x0, y0, z0);
                t.Vertex(x0, y0, z1);
                t.Vertex(x0, y1, z1);
                t.Vertex(x0, y1, z0);
                t.Indice();
                t.Tex(u0, v0);
                t.Tex(u1, v0);
                t.Tex(u1, v1);
                t.Tex(u0, v1);
                t.Color(br, br, br);
            }
        }

        // x1
        if(!level.IsSolidTile(x + 1, y, z)) {
            br = level.GetBrightness(x + 1, y, z) * cx;
            if(br == cx) {
                t.Vertex(x1, y0, z1);
                t.Vertex(x1, y0, z0);
                t.Vertex(x1, y1, z0);
                t.Vertex(x1, y1, z1);
                t.Indice();
                t.Tex(u0, v0);
                t.Tex(u1, v0);
                t.Tex(u1, v1);
                t.Tex(u0, v1);
                t.Color(br, br, br);
            }
        }

        // y0
        if(!level.IsSolidTile(x, y - 1, z)) {
            br = level.GetBrightness(x, y - 1, z) * cy;
            if(br == cy) {
                t.Vertex(x0, y0, z0);
                t.Vertex(x1, y0, z0);
                t.Vertex(x1, y0, z1);
                t.Vertex(x0, y0, z1);
                t.Indice();
                t.Tex(u0, v0);
                t.Tex(u1, v0);
                t.Tex(u1, v1);
                t.Tex(u0, v1);
                t.Color(br, br, br);
            }
        }

        // y1
        if(!level.IsSolidTile(x, y + 1, z)) {
            br = level.GetBrightness(x, y, z) * cy;
            if(br == cy) {
                t.Vertex(x0, y1, z1);
                t.Vertex(x1, y1, z1);
                t.Vertex(x1, y1, z0);
                t.Vertex(x0, y1, z0);
                t.Indice();
                t.Tex(u0, v0);
                t.Tex(u1, v0);
                t.Tex(u1, v1);
                t.Tex(u0, v1);
                t.Color(br, br, br);
            }
        }

        // z0
        if(!level.IsSolidTile(x, y, z - 1)) {
            br = level.GetBrightness(x, y, z - 1) * cz;
            if(br == cz) {
                t.Vertex(x1, y0, z0);
                t.Vertex(x0, y0, z0);
                t.Vertex(x0, y1, z0);
                t.Vertex(x1, y1, z0);
                t.Indice();
                t.Tex(u0, v0);
                t.Tex(u1, v0);
                t.Tex(u1, v1);
                t.Tex(u0, v1);
                t.Color(br, br, br);
            }
        }

        // z1
        if(!level.IsSolidTile(x, y, z + 1)) {
            br = level.GetBrightness(x, y, z + 1) * cz;
            if(br == cz) {
                t.Vertex(x0, y0, z1);
                t.Vertex(x1, y0, z1);
                t.Vertex(x1, y1, z1);
                t.Vertex(x0, y1, z1);
                t.Indice();
                t.Tex(u0, v0);
                t.Tex(u1, v0);
                t.Tex(u1, v1);
                t.Tex(u0, v1);
                t.Color(br, br, br);
            }
        }
    }

    public bool IsSolid() {
        return true;
    }
}