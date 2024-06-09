using OpenTK.Mathematics;

namespace RubyDung.src.level.block;

public class BlockWood : Block {
    public BlockWood() {
        
    }

    protected override Vector2 getTexture(string face) {
        if(type == "oak") {
            return new Vector2(4, 0);
        }
        if(type == "spruce") {
            return new Vector2(6, 12);
        }
        if(type == "birch") {
            return new Vector2(6, 13);
        }
        if(type == "jungle") {
            return new Vector2(7, 12);
        }

        return base.getTexture(face);
    }
}
