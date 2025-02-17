using OpenTK.Mathematics;

namespace RubyDung.src.level.block;

public class BlockSapling : BlockFlower {    
    public BlockSapling() {
        
    }

    protected override Vector2 getTexture(string face) {
        if(type == "oak") {
            return new Vector2(15, 0);
        }
        if(type == "spruce") {
            return new Vector2(15, 3);
        }
        if(type == "birch") {
            return new Vector2(15, 4);
        }
        if(type == "jungle") {
            return new Vector2(14, 1);
        }

        return base.getTexture(face);
    }
}
