using OpenTK.Mathematics;

namespace RubyDung.src.level.block;

public class BlockLeaves : BlockLeavesBase {
    public BlockLeaves() {
        
    }

    protected override Vector2 getTexture(string face) {
        if(type == "oak") {
            return new Vector2(4, 3);
        }
        if(type == "oak_opaque") {
            return new Vector2(5, 3);
        }
        if(type == "spruce") {
            return new Vector2(4, 8);
        }
        if(type == "spruce_opaque") {
            return new Vector2(5, 8);
        }
        if(type == "birch") {
            return new Vector2(4, 3);
        }
        if(type == "birch_opaque") {
            return new Vector2(5, 3);
        }
        if(type == "jungle") {
            return new Vector2(4, 12);
        }
        if(type == "jungle_opaque") {
            return new Vector2(5, 12);
        }

        return base.getTexture(face);
    }

    protected override Vector3 getColor(string face) {
        if(type == "oak") {
            return ColorConverter.HexToVector3("48b518");
        }
        if(type == "oak_opaque") {
            return ColorConverter.HexToVector3("48b518");
        }
        if(type == "spruce") {
            return ColorConverter.HexToVector3("619961");
        }
        if(type == "spruce_opaque") {
            return ColorConverter.HexToVector3("619961");
        }
        if(type == "birch") {
            return ColorConverter.HexToVector3("80A755");
        }
        if(type == "birch_opaque") {
            return ColorConverter.HexToVector3("80A755");
        }
        if(type == "jungle") {
            return ColorConverter.HexToVector3("48b518");
        }
        if(type == "jungle_opaque") {
            return ColorConverter.HexToVector3("48b518");
        }

        return base.getColor(face);
    }
}
