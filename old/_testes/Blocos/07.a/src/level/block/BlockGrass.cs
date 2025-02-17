using OpenTK.Mathematics;

namespace RubyDung.src.level.block;

public class BlockGrass : Block {
    public BlockGrass(grassType type) {
        this.type = type;
    }

    public enum grassType {
        grass,
        snow
    }

    private grassType type;

    protected override Vector2 getTexture(faceType face) {
        if(type == grassType.snow) {
                if(face == faceType.positiveY) {
                return new Vector2(2, 4);
            }
            else {
                return face == faceType.negativeY ? new Vector2(2, 0) : new Vector2(4, 4);
            }
        }
        else {
            if(face == faceType.positiveY) {
                return new Vector2(0, 0);
            }
            else {
                return face == faceType.negativeY ? new Vector2(2, 0) : new Vector2(3, 0);
            }
        }
    }

    protected override Vector3 getColor(faceType face) {
        if(type == grassType.snow) {
                return new Vector3(1.0f, 1.0f, 1.0f);
        }
        else {
            if(face == faceType.positiveY) {
                return new Vector3(0.0f, 1.0f, 0.0f);
            }
            else {
                return new Vector3(1.0f, 1.0f, 1.0f);
            }
        }
    }
}
