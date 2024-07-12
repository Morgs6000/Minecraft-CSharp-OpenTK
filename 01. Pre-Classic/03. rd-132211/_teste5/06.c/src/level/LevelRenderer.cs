namespace RubyDung.src.level;

public class LevelRenderer {
    private Chunk chunk;

    public LevelRenderer() {
        this.chunk = new Chunk();
    }

    public void render() {
        this.chunk.render();
    }
}
