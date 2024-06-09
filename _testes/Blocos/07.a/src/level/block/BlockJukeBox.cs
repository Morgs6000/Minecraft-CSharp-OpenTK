using OpenTK.Mathematics;

namespace RubyDung.src.level.block;

public class BlockJukeBox : Block {
    public BlockJukeBox() {
        
    }

    protected override Vector2 getTexture(faceType face) {
        if(face == faceType.positiveY) {
            return new Vector2(11, 4);
        }
        else {
            return new Vector2(10, 4);
        }
    }
}
