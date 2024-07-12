namespace RubyDung.src.level;

public class Chunk {
    private Shader shader;
    private Tesselator t = new Tesselator();

    public Chunk() {
        this.shader = new Shader("shader.vert", "shader.frag");

        Tile.tile.render(t);
        this.t.flush();
    }

    public void render() {
        this.shader.use();
        this.t.bind();
    }
}
