namespace RubyDung.src.level;

public class Tile {
    public static Tile tile = new Tile();

    private string textureName = "Clay Basalt";

    public void SetTexture(string name) {
        textureName = name;
    }

    public void render(Tesselator t, TextureAtlas atlas) {
        float x0 = -0.5f;
        float y0 = -0.5f;

        float x1 = 0.5f;
        float y1 = 0.5f;

        var (u0, v0, u1, v1) = atlas.GetTextureCoordinates(textureName);

        t.tex(u0, v0);
        t.vertex(x0, y0);
        t.tex(u1, v0);
        t.vertex(x1, y0);
        t.tex(u1, v1);
        t.vertex(x1, y1);
        t.tex(u0, v1);
        t.vertex(x0, y1);
    }
}
