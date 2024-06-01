using OpenTK.Mathematics;

namespace RubyDung.src.level {
    public class Bloco : Tile {
        public Bloco() {
            //this.texX = 0;
            //this.texY = 1;
        }

        protected override Vector2 getTexture(int face) {
            // ..:: Positive X ::..
            if(face == 0) {
                return new Vector2(2, 1);
            }
            // ..:: Negative X ::..
            if(face == 1) {
                return new Vector2(0, 1);
            }
            // ..:: Positive Y ::..
            if(face == 2) {
                return new Vector2(1, 0);
            }
            // ..:: Negative Y ::..
            if(face == 3) {
                return new Vector2(1, 2);
            }
            // ..:: Positive Z ::..
            if(face == 4) {
                return new Vector2(2, 2);
            }
            // ..:: Negative Z ::..
            if(face == 5) {
                return new Vector2(1, 1);
            }
            else {
                return new Vector2(0, 0);
            }
        }
    }
}
