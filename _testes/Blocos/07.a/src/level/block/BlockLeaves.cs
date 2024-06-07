using OpenTK.Mathematics;

namespace RubyDung.src.level.block;

public class BlockLeaves : Block {
    public BlockLeaves(Vector2 tex) {
        this.tex = tex;
        //this.color = new Vector3(0.0f, 1.0f, 0.0f);
    }

    /*
    protected override Vector3 getColor(int face) {
        return new Vector3(0.0f, 1.0f, 0.0f);
    }
    */
}
