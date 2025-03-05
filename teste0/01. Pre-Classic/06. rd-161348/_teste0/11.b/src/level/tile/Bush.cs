namespace RubyDung;

public class Bush : Tile {
    public Bush(int id) : base(id) {
        tex = 15;
    }

    public override void OnLoad(Tesselator t, Level level, int x, int y, int z) {
        int tex = GetTexture(15);

        float u0 = (float)(tex % 16) / 16.0f;
        float v0 = (16.0f - 1.0f - tex / 16) / 16.0f;

        float u1 = u0 + (1.0f / 16.0f);
        float v1 = v0 + (1.0f / 16.0f);
        
        int rots = 2;
        
        for(int r = 0; r < rots; r++) {
            float xa = (float)(Math.Sin((double)r * Math.PI / (double)rots + 0.7853981633974483) * 0.5f);
            float za = (float)(Math.Cos((double)r * Math.PI / (double)rots + 0.7853981633974483) * 0.5f);

            float x0 = (float)x + 0.5f - xa;
            float y0 = (float)y + 0.0f;
            float z0 = (float)z + 0.5f - za;

            float x1 = (float)x + 0.5f + xa;
            float y1 = (float)y + 1.0f;
            float z1 = (float)z + 0.5f + za;

            t.Vertex(x1, y0, z1);
            t.Vertex(x0, y0, z0);
            t.Vertex(x0, y1, z0);
            t.Vertex(x1, y1, z1);
            t.Indice();
            t.Tex(u1, v0);
            t.Tex(u0, v0);
            t.Tex(u0, v1);
            t.Tex(u1, v1);
            t.Color(1.0f, 1.0f, 1.0f);

            t.Vertex(x0, y0, z0);
            t.Vertex(x1, y0, z1);
            t.Vertex(x1, y1, z1);
            t.Vertex(x0, y1, z0);
            t.Indice();
            t.Tex(u0, v0);
            t.Tex(u1, v0);
            t.Tex(u1, v1);
            t.Tex(u0, v1);
            t.Color(1.0f, 1.0f, 1.0f);
        }
    }

    public override bool IsSolid() {
        return false;
    }
}