namespace RubyDung.src.level;

public class LevelRenderer {
    private Chunk chunk = new Chunk();

    public LevelRenderer() {
        chunk.Load();
    }

    public void Render() {
        chunk.Render();
    }
}
