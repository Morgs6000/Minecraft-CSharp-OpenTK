namespace RubyDung.src.level;

public class Chunk {
    public Level level;

    public int x0 = 0;
    public int y0 = 0;
    public int z0 = 0;

    public int x1 = 16;
    public int y1 = 16;
    public int z1 = 16;

    private Texture texture;
    private Tesselator t = new Tesselator();

    public Chunk(Level level) {
        this.level = level;
    }

    public void rebuild() {
        for(int x = this.x0; x < this.x1; x++) {
            for(int y = this.y0; y < this.y1; y++) {
                for(int z = this.z0; z < this.z1; z++) {
                    Tile.tile.render(this.t, this.level, x, y, z);
                }
            }
        }

        this.t.flush();
    }

    public void render() {
        this.t.bind();
    }
}
