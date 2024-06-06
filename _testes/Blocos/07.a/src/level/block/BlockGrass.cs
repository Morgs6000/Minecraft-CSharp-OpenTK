using OpenTK.Mathematics;

namespace RubyDung.src.level.block;

public class BlockGrass : Block {
    public BlockGrass() {
        
    }

    protected override Vector2 getTexture(int face) {
        if(face == 3) {
            return new Vector2(0, 0);
        }
        else {
            return face == 2 ? new Vector2(2, 0) : new Vector2(3, 0);
        }
    }
}
