namespace RubyDung.src.level;

public class LevelRenderer {
    private Chunk[] chunk;

    public LevelRenderer() {
        chunk = new Chunk[1];

        chunk[0] = new Chunk(0, 0, 0, 16, 16, 16);
    }

    public void Load() {
        chunk[0].Load();
    }

    public void Render() {
        chunk[0].Render();
    }
}
