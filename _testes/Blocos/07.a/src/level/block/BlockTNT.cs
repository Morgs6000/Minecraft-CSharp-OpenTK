using OpenTK.Mathematics;

namespace RubyDung.src.level.block;

public class BlockTNT : Block {
    public BlockTNT() {
        
    }

    protected override Vector2 getTexture(faceType face) {
        if(face == faceType.positiveY) {
            return new Vector2(9, 0);
        }
        if(face == faceType.negativeY) {
            return new Vector2(10, 0);
        }
        else {
            return new Vector2(8, 0);
        }
    }
}
