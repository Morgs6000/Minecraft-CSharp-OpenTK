using OpenTK.Mathematics;

namespace RubyDung.src.level.block;

public class BlockWorkbench : Block {
    public BlockWorkbench() {
    }

    protected override Vector2 getTexture(string face) {
        if(face == "x0" || face == "z0") {
            return new Vector2(12, 3);
        }
        if(face == "x1" || face == "z1") {
            return new Vector2(11, 3);
        }
        if(face == "y1") {
            return new Vector2(11, 2);
        }
        else {
            return new Vector2(4, 0);
        }
    }
}
