namespace RubyDung.src.level.tile;

public class GrassTile : Tile {
    public GrassTile(int id) : base(id) {
        this.tex = 3;
    }

    protected override int getTexture(int face) {
        if(face == 3) {
            return 0;
        }
        else {
            return face == 2 ? 2 : 3;
        }
    }
}
