using OpenTK.Mathematics;

namespace RubyDung.src.level.block;

public class BlockLog : Block {
    public Vector2 tex_top;
    public Vector2 tex_side;

    public BlockLog(Vector2 tex_top, Vector2 tex_side) {
        this.tex_top = tex_top;
        this.tex_side = tex_side;
    }

    protected override Vector2 getTexture(int face) {
        if(face == 3 || face == 2) {
            return tex_top;
        }
        else {
            return tex_side;
        }
    }
}
