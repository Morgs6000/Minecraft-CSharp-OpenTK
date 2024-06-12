using OpenTK.Mathematics;

namespace RubyDung.src.level.block;

public class BlockPistonBase : Block {
    public BlockPistonBase() {
        
    }

    protected override Vector2 getTexture(string face) {
        if(face == "y1") {
            return this.tex;
        }
        if(face == "y0") {
            return new Vector2(13, 6);
        }
        else {
            return new Vector2(12, 6);
        }
    }
}
