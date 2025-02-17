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
            if(type == "oak") {
                return new Vector2(4, 1);
            }
            if(type == "spruce") {
                return new Vector2(4, 7);
            }
            if(type == "birch") {
                return new Vector2(5, 7);
            }
            if(type == "jungle") {
                return new Vector2(9, 9);
            }
        }

        return base.getTexture(face);
    }
}
