using OpenTK.Mathematics;

namespace RubyDung.src.level.block;

public class BlockWorkbench : Block {
    public BlockWorkbench() {
        
    }

    protected override Vector2 getTexture(faceType face) {
        if(face == faceType.negativeX ||
           face == faceType.negativeZ
        ) {
            return new Vector2(12, 3);
        }
        if(face == faceType.positiveX ||
           face == faceType.positiveZ
        ) {
            return new Vector2(11, 3);
        }
        if(face == faceType.positiveY) {
            return new Vector2(11, 2);
        }
        else {
            return new Vector2(4, 0);
        }
    }
}
