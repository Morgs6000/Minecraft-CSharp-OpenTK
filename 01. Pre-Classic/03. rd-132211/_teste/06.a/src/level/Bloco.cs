using OpenTK.Mathematics;

namespace RubyDung.src.level {
    public class Bloco : Tile {
        public Bloco() {
            
        }

        protected Vector2 getTexture(int face) {
            if(face == 0) {
                return new Vector2(0, 1);
            }
            if(face == 1) {
                return new Vector2(2, 1);
            }
            if(face == 2) {
                return new Vector2(1, 2);
            }
            if(face == 3) {
                return new Vector2(1, 0);
            }
            if(face == 4) {
                return new Vector2(1, 1);
            }
            if(face == 5) {
                return new Vector2(2, 2);
            }
            else {
                return new Vector2(0, 0);
            }
        }
    }
}
