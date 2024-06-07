using OpenTK.Mathematics;

namespace RubyDung.src.level.block;

public class BlockWorkbench : Block {
    public BlockWorkbench() {
        
    }

    protected override Vector2 getTexture(int face) {
        if(face == 0) {
            return new Vector2(12, 3);
        }
        if(face == 1) {
            return new Vector2(11, 3);
        }
        if(face == 3) {
            return new Vector2(11, 2);
        }
        if(face == 4) {
            return new Vector2(12, 3);
        }
        if(face == 5) {
            return new Vector2(11, 3);
        }
        else {
            return new Vector2(4, 0);
        }
    }
}
