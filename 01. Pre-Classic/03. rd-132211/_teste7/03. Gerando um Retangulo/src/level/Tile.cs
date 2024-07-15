namespace RubyDung.src.level;

public class Tile {
    public static Tile triangle = new Tile();

    public void render(Tesselator t) {
        float x0 = -0.5f;
        float y0 = -0.5f;

        float x1 = 0.5f;
        float y1 = 0.5f;

        /*
        t.vertex(x0, y0); // bottom left  // 0
        t.vertex(x1, y0); // bottom right // 1
        t.vertex(x1, y1); // top right    // 2

        t.vertex(x0, y0); // bottom left  // 0
        t.vertex(x1, y1); // top right    // 2
        t.vertex(x0, y1); // top left     // 3
        */

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
    }
}
