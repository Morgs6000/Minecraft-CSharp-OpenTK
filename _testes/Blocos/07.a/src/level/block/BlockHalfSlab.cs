using OpenTK.Mathematics;

namespace RubyDung.src.level.block;

public class BlockHalfSlab : Block {
    public BlockHalfSlab() {
        
    }

    protected override Vector2 getTexture(faceType face) {
        if(face == faceType.negativeY || 
           face == faceType.positiveY
        ) {
            return new Vector2(6, 0);
        }
        else {
            return new Vector2(5, 0);
        }
    }
}
