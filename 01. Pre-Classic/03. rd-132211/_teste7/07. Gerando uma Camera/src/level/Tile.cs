namespace RubyDung.src.level;

public class Tile {
    public static Tile triangle = new Tile();

    private int tex = 0;

    public void render(Tesselator t) {
        float x0 = -0.5f;
        float y0 = -0.5f;
        float z0 = -0.5f;

        float x1 = 0.5f;
        float y1 = 0.5f;
        float z1 = 0.5f;

        float u0 = (float)this.tex / 16.0f;
        float v0 = (16.0f - 1.0f) / 16.0f;

        float u1 = u0 + (1.0f / 16.0f);
        float v1 = v0 + (1.0f / 16.0f);

        // ..:: x0 ::..
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

        // ..:: x1 ::..
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

        // ..:: y0 ::..
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

        // ..:: y1 ::..
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

        // ..:: z1 ::..
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

        // ..:: z1 ::..
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
