using OpenTK.Mathematics;
using System.Security.AccessControl;

namespace RubyDung.src.level.block;

public class BlockGrass : Block {
    public BlockGrass() {
        
    }

    protected override Vector2 getTexture(string face) {
        if(type == "grass") {
            if(face == "y1") {
                return new Vector2(0, 0);
            }
            else {
                return face == "y0" ? new Vector2(2, 0) : new Vector2(3, 0);
            }
        }
        if(type == "snow") {
            if(face == "y1") {
                return new Vector2(2, 4);
            }
            else {
                return face == "y0" ? new Vector2(2, 0) : new Vector2(4, 4);
            }
        }

        return base.getTexture(face);
    }

    protected override Vector3 getColor(string face) {
        if(type == "grass") {
            return face == "y1" ? ColorConverter.HexToVector3("7cbd6b") : new Vector3(1.0f, 1.0f, 1.0f);
        }

        return base.getColor(face);
    }
}
