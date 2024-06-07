using OpenTK.Mathematics;

namespace RubyDung.src.level.block;

public class BlockFurnace : Block {
    public Vector2 tex_front;

    public BlockFurnace(Vector2 tex) {
        this.tex_front = tex;
    }

    protected override Vector2 getTexture(int face) {
        if(face == 0 || face == 1 || face == 4) {
            return new Vector2(13, 2);
        }
        if(face == 5) {
            return this.tex_front;
        }
        else {
            return new Vector2(14, 3);
        }
    }
}
