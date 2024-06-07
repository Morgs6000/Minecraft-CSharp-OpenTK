using OpenTK.Mathematics;

namespace RubyDung.src.level.block;

public class BlockDispenser : Block {
    public BlockDispenser() {
        
    }

    protected override Vector2 getTexture(int face) {
        if(face == 0 || face == 1 || face == 4) {
            return new Vector2(13, 2);
        }
        if(face == 5) {
            return new Vector2(14, 2);
        }
        else {
            return new Vector2(14, 3);
        }
    }
}
