using OpenTK.Mathematics;

namespace RubyDung.src.level.block;

public class BlockHalfSlab : Block {
    public BlockHalfSlab() {
    }

    protected override Vector2 getTexture(string face) {
        if(face == "y0" || face == "y1") {
            return new Vector2(6, 0);
        }
        else {
            return new Vector2(5, 0);
        }
    }
}
