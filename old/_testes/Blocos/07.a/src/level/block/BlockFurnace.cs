using OpenTK.Mathematics;

namespace RubyDung.src.level.block;

public class BlockFurnace : Block {
    public BlockFurnace(furnaceType type) {
        this.type = type;
    }

    public enum furnaceType {
        idle,
        active
    }

    private furnaceType type;

    protected override Vector2 getTexture(faceType face) {
        if(face == faceType.negativeX || 
           face == faceType.positiveX || 
           face == faceType.negativeZ
        ) {
            return new Vector2(13, 2);
        }
        if(face == faceType.positiveZ) {
            if(type == furnaceType.active) {
                return new Vector2(13, 3);
            }
            else {
                return new Vector2(12, 2);
            }
        }
        else {
            return new Vector2(14, 3);
        }
    }
}
