namespace RubyDung;

public class GrassTile : Tile {
    public GrassTile(int id) : base(id) {
        tex = 3;
    }

    protected override int GetTexture(int face) {
        if(face == 3) {
            return 0;
        }
        else {
            return face == 2 ? 2 : 3;
        }
    }
}
