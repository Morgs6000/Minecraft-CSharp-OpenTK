using OpenTK.Mathematics;

namespace RubyDung.src.level.block;

public class BlockBookshelf : Block {
    public BlockBookshelf() {

    }

    protected override Vector2 getTexture(string face) {
        if(face == "y0" || face == "y1") {
            return new Vector2(4, 0);
        }
        else {
            return new Vector2(3, 2);
        }
    }
}
