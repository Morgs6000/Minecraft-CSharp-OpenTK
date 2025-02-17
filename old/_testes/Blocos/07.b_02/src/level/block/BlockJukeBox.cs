using OpenTK.Mathematics;

namespace RubyDung.src.level.block;

public class BlockJukeBox : BlockContainer {
    public BlockJukeBox() {

    }

    protected override Vector2 getTexture(string face) {
        if(face == "y1") {
            return new Vector2(11, 4);
        }
        else {
            return new Vector2(10, 4);
        }
    }
}
