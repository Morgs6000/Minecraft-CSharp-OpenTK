using OpenTK.Mathematics;

namespace RubyDung.src.level.block;

public class BlockHalfSlab : Block {
    public BlockHalfSlab() {
        
    }

    protected override Vector2 getTexture(int face) {
        if(face == 2 || face == 3) {
            return new Vector2(6, 0);
        }
        else {
            return new Vector2(5, 0);
        }
    }
}
