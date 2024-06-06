using OpenTK.Mathematics;

namespace RubyDung.src.level.block;

public class BlockTNT : Block {
    public BlockTNT() {
        
    }

    protected override Vector2 getTexture(int face) {
        if(face == 3) {
            return new Vector2(9, 0);
        }
        if(face == 2) {
            return new Vector2(10, 0);
        }
        else {
            return new Vector2(8, 0);
        }
    }
}
