using OpenTK.Mathematics;

namespace RubyDung.src.level.block;

public class BlockMelon : Block {
    public BlockMelon() {
        
    }

    protected override Vector2 getTexture(string face) {
        if(face == "y0" || face == "y1") {
            return new Vector2(9, 8);
        }
        else {
            return new Vector2(8, 8);
        }
    }
}
