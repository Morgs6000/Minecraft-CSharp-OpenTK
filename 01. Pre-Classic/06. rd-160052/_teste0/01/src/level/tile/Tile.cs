namespace RubyDung.src.level.tile;

public class Tile {
    //public static Tile[] tiles = new Tile[256];
    public static List<Tile> tiles = new List<Tile>();

    //public static Tile empty = null;
    public static Tile rock = new Tile(1, 1);
    public static Tile grass = new GrassTile(2);
    public static Tile dirt = new Tile(3, 2);
    public static Tile stoneBrick = new Tile(4, 16);
    public static Tile wood = new Tile(5, 4);

    public int tex;

    public int id;

    protected Tile(int id) {
        //tiles[id] = this;

        this.id = id;
        tiles.Add(this);
    }

    protected Tile(int id, int tex) {
        //tiles[id] = this;
        tiles.Add(this);

        this.id = id;
        this.tex = tex;
    }

    public void render(Tesselator t, Level level, int x, int y, int z) {
        if(!level.isSolidTile(x - 1, y, z)) {
            this.renderFace(t, x, y, z, 0);
        }
        if(!level.isSolidTile(x + 1, y, z)) {
            this.renderFace(t, x, y, z, 1);
        }
        if(!level.isSolidTile(x, y - 1, z)) {
            this.renderFace(t, x, y, z, 2);
        }
        if(!level.isSolidTile(x, y + 1, z)) {
            this.renderFace(t, x, y, z, 3);
        }
        if(!level.isSolidTile(x, y, z - 1)) {
            this.renderFace(t, x, y, z, 4);
        }
        if(!level.isSolidTile(x, y, z + 1)) {
            this.renderFace(t, x, y, z, 5);
        }
    }

    protected virtual int getTexture(int face) {
        return this.tex;
    }

    public void renderFace(Tesselator t, int x, int y, int z, int face) {
        float x0 = x + 0.0f;
        float y0 = y + 0.0f;
        float z0 = z + 0.0f;

        float x1 = x + 1.0f;
        float y1 = y + 1.0f;
        float z1 = z + 1.0f;

        int tex = this.getTexture(face);

        float u0 = tex % 16 / 16.0f;
        float v0 = (16.0f - 1.0f - tex / 16) / 16.0f;

        float u1 = u0 + 1.0f / 16.0f;
        float v1 = v0 + 1.0f / 16.0f;

        // x0
        if(face == 0) {
            t.vertexUV(x0, y0, z0, u0, v0);
            t.vertexUV(x0, y0, z1, u1, v0);
            t.vertexUV(x0, y1, z1, u1, v1);
            t.vertexUV(x0, y1, z0, u0, v1);
        }

        // x1
        if(face == 1) {
            t.vertexUV(x1, y0, z1, u0, v0);
            t.vertexUV(x1, y0, z0, u1, v0);
            t.vertexUV(x1, y1, z0, u1, v1);
            t.vertexUV(x1, y1, z1, u0, v1);
        }

        // y0
        if(face == 2) {
            t.vertexUV(x0, y0, z0, u0, v0);
            t.vertexUV(x1, y0, z0, u1, v0);
            t.vertexUV(x1, y0, z1, u1, v1);
            t.vertexUV(x0, y0, z1, u0, v1);
        }

        // y1
        if(face == 3) {
            t.vertexUV(x0, y1, z1, u0, v0);
            t.vertexUV(x1, y1, z1, u1, v0);
            t.vertexUV(x1, y1, z0, u1, v1);
            t.vertexUV(x0, y1, z0, u0, v1);
        }

        // z0
        if(face == 4) {
            t.vertexUV(x1, y0, z0, u0, v0);
            t.vertexUV(x0, y0, z0, u1, v0);
            t.vertexUV(x0, y1, z0, u1, v1);
            t.vertexUV(x1, y1, z0, u0, v1);
        }

        // z1
        if(face == 5) {
            t.vertexUV(x0, y0, z1, u0, v0);
            t.vertexUV(x1, y0, z1, u1, v0);
            t.vertexUV(x1, y1, z1, u1, v1);
            t.vertexUV(x0, y1, z1, u0, v1);
        }
    }
}
