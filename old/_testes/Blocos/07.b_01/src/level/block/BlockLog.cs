using OpenTK.Mathematics;

namespace RubyDung.src.level.block;

public class BlockLog : Block {
    public BlockLog() {
        
    }

    protected override Vector2 getTexture(string face) {
        if(face == "y0" || face == "y1") {
            return new Vector2(5, 1);
        }
        else {
            return this.tex;
        }
    }
}
