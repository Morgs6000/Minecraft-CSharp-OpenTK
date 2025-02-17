namespace RubyDung.src.level;

public class Chunk {
    private Tesselator tesselator = new Tesselator();

    public void Load() {
        tesselator.Load();
    }

    public void Render() {
        tesselator.Render();
    }
}
