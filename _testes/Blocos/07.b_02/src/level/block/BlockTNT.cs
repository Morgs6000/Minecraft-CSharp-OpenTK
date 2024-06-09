using OpenTK.Mathematics;

namespace RubyDung.src.level.block;

public class BlockTNT : Block {
    public BlockTNT() {
    }

    protected override Vector2 getTexture(string face) {
        if(face == "y1") {
            return new Vector2(9, 0);
        }
        if(face == "y0") {
            return new Vector2(10, 0);
        }
        else {
            return new Vector2(8, 0);
        }
    }
}
