namespace RubyDung.src.level;

public class Chunk {
    private Tesselator t = new Tesselator();

    public Chunk() {
        Tile.tile.render(t);
        this.t.flush();
    }

    public void render() {
        this.t.bind();
    }
}
