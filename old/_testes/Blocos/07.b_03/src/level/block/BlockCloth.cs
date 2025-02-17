using OpenTK.Mathematics;

namespace RubyDung.src.level.block;

public class BlockCloth : Block {
    public BlockCloth() {
        
    }

    protected override Vector2 getTexture(string face) {
        if(type == "white") {
            return new Vector2(0, 4);
        }
        if(type == "orange") {
            return new Vector2(2, 13);
        }
        if(type == "magenta") {
            return new Vector2(2, 12);
        }
        if(type == "light_blue") {
            return new Vector2(2, 11);
        }
        if(type == "yellow") {
            return new Vector2(2, 10);
        }
        if(type == "lime") {
            return new Vector2(2, 9);
        }
        if(type == "pink") {
            return new Vector2(2, 8);
        }
        if(type == "gray") {
            return new Vector2(2, 7);
        }
        if(type == "light_gray") {
            return new Vector2(1, 14);
        }
        if(type == "cyan") {
            return new Vector2(1, 13);
        }
        if(type == "purple") {
            return new Vector2(1, 12);
        }
        if(type == "blue") {
            return new Vector2(1, 11);
        }
        if(type == "brown") {
            return new Vector2(1, 10);
        }
        if(type == "green") {
            return new Vector2(1, 9);
        }
        if(type == "red") {
            return new Vector2(1, 8);
        }
        if(type == "black") {
            return new Vector2(1, 7);
        }

        return base.getTexture(face);
    }
}
