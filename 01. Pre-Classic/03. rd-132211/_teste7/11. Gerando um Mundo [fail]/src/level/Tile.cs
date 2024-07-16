namespace RubyDung.src.level;

public class Tile {
    public static Tile rock = new Tile(0);
    public static Tile grass = new Tile(1);

    private int tex;

    private Tile(int tex) {
        this.tex = tex;
    }

    public void render(Tesselator t, Level level, int x, int y, int z) {
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

        // ..:: x0 ::..
        if(!level.isSolidTile(x - 1, y, z)) {
            t.vertex(x0, y0, z0); // bottom left  // 0
            t.vertex(x0, y0, z1); // bottom right // 1
            t.vertex(x0, y1, z1); // top right    // 2
            t.vertex(x0, y1, z0); // top left     // 3

            // primeiro Triângulo
            t.indice(0); // bottom left 
            t.indice(1); // bottom right
            t.indice(2); // top right 

            // segundo Triângulo
            t.indice(0); // bottom left 
            t.indice(2); // top right
            t.indice(3); // top left

            t.tex(u0, v0); // bottom left 
            t.tex(u1, v0); // bottom right
            t.tex(u1, v1); // top right
            t.tex(u0, v1); // top left
        }

        // ..:: x1 ::..
        if(!level.isSolidTile(x + 1, y, z)) {
            t.vertex(x1, y0, z1); // bottom left  // 0
            t.vertex(x1, y0, z0); // bottom right // 1
            t.vertex(x1, y1, z0); // top right    // 2
            t.vertex(x1, y1, z1); // top left     // 3

            // primeiro Triângulo
            t.indice(0); // bottom left 
            t.indice(1); // bottom right
            t.indice(2); // top right 

            // segundo Triângulo
            t.indice(0); // bottom left 
            t.indice(2); // top right
            t.indice(3); // top left

            t.tex(u0, v0); // bottom left 
            t.tex(u1, v0); // bottom right
            t.tex(u1, v1); // top right
            t.tex(u0, v1); // top left
        }

        // ..:: y0 ::..
        if(!level.isSolidTile(x, y - 1, z)) {
            t.vertex(x0, y0, z0); // bottom left  // 0
            t.vertex(x1, y0, z0); // bottom right // 1
            t.vertex(x1, y0, z1); // top right    // 2
            t.vertex(x0, y0, z1); // top left     // 3

            // primeiro Triângulo
            t.indice(0); // bottom left 
            t.indice(1); // bottom right
            t.indice(2); // top right 

            // segundo Triângulo
            t.indice(0); // bottom left 
            t.indice(2); // top right
            t.indice(3); // top left

            t.tex(u0, v0); // bottom left 
            t.tex(u1, v0); // bottom right
            t.tex(u1, v1); // top right
            t.tex(u0, v1); // top left
        }

        // ..:: y1 ::..
        if(!level.isSolidTile(x, y + 1, z)) {
            t.vertex(x0, y1, z1); // bottom left  // 0
            t.vertex(x1, y1, z1); // bottom right // 1
            t.vertex(x1, y1, z0); // top right    // 2
            t.vertex(x0, y1, z0); // top left     // 3

            // primeiro Triângulo
            t.indice(0); // bottom left 
            t.indice(1); // bottom right
            t.indice(2); // top right 

            // segundo Triângulo
            t.indice(0); // bottom left 
            t.indice(2); // top right
            t.indice(3); // top left

            t.tex(u0, v0); // bottom left 
            t.tex(u1, v0); // bottom right
            t.tex(u1, v1); // top right
            t.tex(u0, v1); // top left
        }

        // ..:: z1 ::..
        if(!level.isSolidTile(x, y, z - 1)) {
            t.vertex(x1, y0, z0); // bottom left  // 0
            t.vertex(x0, y0, z0); // bottom right // 1
            t.vertex(x0, y1, z0); // top right    // 2
            t.vertex(x1, y1, z0); // top left     // 3

            // primeiro Triângulo
            t.indice(0); // bottom left 
            t.indice(1); // bottom right
            t.indice(2); // top right 

            // segundo Triângulo
            t.indice(0); // bottom left 
            t.indice(2); // top right
            t.indice(3); // top left

            t.tex(u0, v0); // bottom left 
            t.tex(u1, v0); // bottom right
            t.tex(u1, v1); // top right
            t.tex(u0, v1); // top left
        }

        // ..:: z1 ::..
        if(!level.isSolidTile(x, y, z + 1)) {
            t.vertex(x0, y0, z1); // bottom left  // 0
            t.vertex(x1, y0, z1); // bottom right // 1
            t.vertex(x1, y1, z1); // top right    // 2
            t.vertex(x0, y1, z1); // top left     // 3

            // primeiro Triângulo
            t.indice(0); // bottom left 
            t.indice(1); // bottom right
            t.indice(2); // top right 

            // segundo Triângulo
            t.indice(0); // bottom left 
            t.indice(2); // top right
            t.indice(3); // top left

            t.tex(u0, v0); // bottom left 
            t.tex(u1, v0); // bottom right
            t.tex(u1, v1); // top right
            t.tex(u0, v1); // top left
        }
    }
}
