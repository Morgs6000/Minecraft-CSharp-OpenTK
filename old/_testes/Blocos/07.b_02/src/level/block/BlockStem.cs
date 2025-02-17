using OpenTK.Mathematics;

namespace RubyDung.src.level.block;

public class BlockStem : BlockFlower {
    public BlockStem() {
        
    }

    protected override Vector2 getTexture(string face) {
        if(type == "0") {
            return new Vector2(15, 6);
        }
        if(type == "1") {
            return new Vector2(15, 7);
        }

        return base.getTexture(face);
    }

    protected override Vector3 getColor(string face) {
        //if(type == "0") {
        //    return ColorConverter.HexToVector3("00FF00");
        //}
        //if(type == "1") {
        //    return ColorConverter.HexToVector3("20F704");
        //}
        //if(type == "2") {
        //    return ColorConverter.HexToVector3("40EF08");
        //}
        //if(type == "3") {
        //    return ColorConverter.HexToVector3("60E70C");
        //}
        //if(type == "4") {
        //    return ColorConverter.HexToVector3("80DF10");
        //}
        //if(type == "5") {
        //    return ColorConverter.HexToVector3("A0D714");
        //}
        //if(type == "6") {
        //    return ColorConverter.HexToVector3("C0CF18");
        //}
        //if(type == "7") {
            return ColorConverter.HexToVector3("E0C71C");
        //}

        //return base.getColor(face);
    }
}
