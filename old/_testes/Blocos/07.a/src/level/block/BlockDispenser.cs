using OpenTK.Mathematics;

namespace RubyDung.src.level.block;

public class BlockDispenser : Block {
    public BlockDispenser() {
        
    }

    protected override Vector2 getTexture(faceType face) {
        if(face == faceType.negativeX || 
           face == faceType.positiveX || 
           face == faceType.negativeZ
        ) {
            return new Vector2(13, 2);
        }
        if(face == faceType.positiveZ) {
            return new Vector2(14, 2);
        }
        else {
            return new Vector2(14, 3);
        }
    }
}
