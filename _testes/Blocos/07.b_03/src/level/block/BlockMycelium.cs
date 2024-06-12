using OpenTK.Mathematics;

namespace RubyDung.src.level.block;

public class BlockMycelium : Block {
    public BlockMycelium() {

    }

    protected override Vector2 getTexture(string face) {
        if(face == "y1") {
            return new Vector2(14, 4);
        }
        else {
            return face == "y0" ? new Vector2(2, 0) : new Vector2(13, 4);
        }
    }
}
