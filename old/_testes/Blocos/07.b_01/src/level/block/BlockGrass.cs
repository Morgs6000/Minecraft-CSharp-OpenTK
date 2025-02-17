using OpenTK.Mathematics;
using System.Security.AccessControl;

namespace RubyDung.src.level.block;

public class BlockGrass : Block {
    public BlockGrass() {
        
    }

    protected override Vector2 getTexture(string face) {
        if(face == "y1") {
            return new Vector2(0, 0);
        }
        else {
            return face == "y0" ? new Vector2(2, 0) : new Vector2(3, 0);
        }
    }

    protected override Vector3 getColor(string face) {
        return face == "y1" ? new Vector3(0.0f, 1.0f, 0.0f) : new Vector3(1.0f, 1.0f, 1.0f);
    }
}
