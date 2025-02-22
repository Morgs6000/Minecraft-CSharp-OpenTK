namespace RubyDung.src.level;

public class Chunk {
    private Tesselator tesselator = new Tesselator();

    public void Load() {
        Tile.tile.Load(tesselator);
        tesselator.Load();
    }

    public void Render() {
        tesselator.Render();
    }
}
