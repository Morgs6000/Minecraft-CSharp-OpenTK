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

        /*
        t.vertex(x0, y0); // bottom left  // 0
        t.vertex(x1, y0); // bottom right // 1
        t.vertex(x1, y1); // top right    // 2
        t.vertex(x0, y1); // top left     // 3

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
        */

        /*
        // ..:: vertices ::..
        t.vertex(x0, y0, z1); // 0
        t.vertex(x1, y0, z1); // 1
        t.vertex(x1, y1, z1); // 2
        t.vertex(x0, y1, z1); // 3
        t.vertex(x1, y0, z0); // 4
        t.vertex(x0, y0, z0); // 5
        t.vertex(x0, y1, z0); // 6
        t.vertex(x1, y1, z0); // 7

        // ..:: indices ::..
        // x1
        // primeiro Triângulo
        t.indice(1); // bottom left 
        t.indice(4); // bottom right
        t.indice(7); // top right 

        // segundo Triângulo
        t.indice(1); // bottom left 
        t.indice(7); // top right
        t.indice(2); // top left

        // y1
        // primeiro Triângulo
        t.indice(3); // bottom left 
        t.indice(2); // bottom right
        t.indice(7); // top right 

        // segundo Triângulo
        t.indice(3); // bottom left 
        t.indice(7); // top right
        t.indice(6); // top left

        // z1
        // primeiro Triângulo
        t.indice(0); // bottom left 
        t.indice(1); // bottom right
        t.indice(2); // top right 

        // segundo Triângulo
        t.indice(0); // bottom left 
        t.indice(2); // top right
        t.indice(3); // top left

        // ..:: texCoords ::..
        t.tex(u0, v0); // 0
        t.tex(u1, v0); // 1
        t.tex(u1, v1); // 2
        t.tex(u0, v1); // 3

        t.tex(u0, v0); // 4
        t.tex(u1, v0); // 5
        t.tex(u1, v1); // 6
        t.tex(u0, v1); // 7
        */

        /*
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

        // ..:: y1 ::..
        t.vertex(x0, y1, z1); // bottom left  // 4
        t.vertex(x1, y1, z1); // bottom right // 5
        t.vertex(x1, y1, z0); // top right    // 6
        t.vertex(x0, y1, z0); // top left     // 7

        // primeiro Triângulo
        t.indice(4); // bottom left 
        t.indice(5); // bottom right
        t.indice(6); // top right 

        // segundo Triângulo
        t.indice(4); // bottom left 
        t.indice(6); // top right
        t.indice(7); // top left

        t.tex(u0, v0); // bottom left 
        t.tex(u1, v0); // bottom right
        t.tex(u1, v1); // top right
        t.tex(u0, v1); // top left

        // ..:: z1 ::..
        t.vertex(x0, y0, z1); // bottom left  // 8
        t.vertex(x1, y0, z1); // bottom right // 9
        t.vertex(x1, y1, z1); // top right    // 10
        t.vertex(x0, y1, z1); // top left     // 11

        // primeiro Triângulo
        t.indice(8); // bottom left 
        t.indice(9); // bottom right
        t.indice(10); // top right 

        // segundo Triângulo
        t.indice(8); // bottom left 
        t.indice(10); // top right
        t.indice(11); // top left

        t.tex(u0, v0); // bottom left 
        t.tex(u1, v0); // bottom right
        t.tex(u1, v1); // top right
        t.tex(u0, v1); // top left
        */

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
