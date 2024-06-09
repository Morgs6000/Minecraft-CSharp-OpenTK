using OpenTK.Mathematics;

namespace RubyDung.src.level.block;

public class BlockDispenser : BlockContainer {
    public BlockDispenser() {
    }

    protected override Vector2 getTexture(string face) {
        if(face == "x0" || face == "x1" || face == "z0") {
            return new Vector2(13, 2);
        }
        if(face == "z1") {
            return new Vector2(14, 2);
        }
        else {
            return new Vector2(14, 3);
        }
    }
}
