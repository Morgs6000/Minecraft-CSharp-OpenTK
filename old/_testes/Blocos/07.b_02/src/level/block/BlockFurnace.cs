using OpenTK.Mathematics;

namespace RubyDung.src.level.block;

public class BlockFurnace : BlockContainer {
    private bool isActive;

    public BlockFurnace(bool par2) {
        this.isActive = par2;
    }

    protected override Vector2 getTexture(string face) {
        if(face == "y0" || face == "y1") {
            return new Vector2(14, 3);
        }
        if(face == "z1") {
            if(!isActive) {
                return new Vector2(12, 2);
            }
            else {
                return new Vector2(13, 3);
            }
        }
        else {
            return new Vector2(13, 2);
        }

        return base.getTexture(face);
    }
}
