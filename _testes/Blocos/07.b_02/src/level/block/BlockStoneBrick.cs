using OpenTK.Mathematics;

namespace RubyDung.src.level.block;

public class BlockStoneBrick : Block {
    public BlockStoneBrick() {
        
    }

    protected override Vector2 getTexture(string face) {
        if(type == "default") {
            return new Vector2(6, 3);
        }
        if(type == "mossy") {
            return new Vector2(4, 6);
        }
        if(type == "cracked") {
            return new Vector2(5, 6);
        }
        if(type == "chiseled") {
            return new Vector2(5, 13);
        }

        return base.getTexture(face);
    }
}
