using OpenTK.Mathematics;

namespace RubyDung.src.level.block;

public class BlockPumpkin : BlockDirectional {
    public BlockPumpkin() {
        
    }

    protected override Vector2 getTexture(string face) {
        if(face == "z1") {
            return this.tex;
        }
        if(face == "y0" || face == "y1") {
            return new Vector2(6, 6);
        }
        else {
            return new Vector2(6, 7);
        }
    }
}
