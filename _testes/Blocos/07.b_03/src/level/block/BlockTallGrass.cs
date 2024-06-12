using OpenTK.Mathematics;

namespace RubyDung.src.level.block;

public class BlockTallGrass : BlockFlower {
    public BlockTallGrass() {
        
    }

    protected override Vector3 getColor(string face) {
        if(type == "tall_grass") {
            return ColorConverter.HexToVector3("48b518");
        }
        if(type == "fern") {
            return ColorConverter.HexToVector3("48b518");
        }

        return base.getColor(face);
    }
}
