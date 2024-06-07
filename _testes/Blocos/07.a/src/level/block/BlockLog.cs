using OpenTK.Mathematics;

namespace RubyDung.src.level.block;

public class BlockLog : Block {
    public BlockLog(logType type) {
        this.type = type;
    }

    public enum logType {
        oak,
        spruce,
        birch,
        jungle
    }

    private logType type;

    protected override Vector2 getTexture(int face) {
        if(face == 3 || face == 2) {
            return new Vector2(5, 1);
        }

        if(type == logType.jungle) {
            return new Vector2(9, 9);
        }
        if(type == logType.birch) {
            return new Vector2(5, 7);
        }
        if(type == logType.spruce) {
            return new Vector2(4, 7);
        }
        else {
            return new Vector2(4, 1);
        }
    }
}
