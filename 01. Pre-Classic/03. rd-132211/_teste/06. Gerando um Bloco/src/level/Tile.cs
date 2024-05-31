using OpenTK.Mathematics;

namespace RubyDung.src.level {
    internal class Tile {
        public static Tile tile = new Bloco();

        public int texX = 0;
        public int texY = 0;

        private int col = 1;
        private int row = 1;

        /*
        protected Tile() {
            
        }

        protected Tile(int texX, int texY) {
            this.texX = texX;
            this.texY = texY;
        }
        */

        public void render(Tesselator t) {
            this.renderFace(t, 0);
            this.renderFace(t, 1);
            this.renderFace(t, 2);
            this.renderFace(t, 3);
            this.renderFace(t, 4);
            this.renderFace(t, 5);
        }

        protected Vector2 getTexture(int face) {
            return new Vector2(this.texX, this.texY);
        }

        public void renderFace(Tesselator t, int face) {
            float x0 = -0.5f;
            float y0 = -0.5f;
            float z0 = -0.5f;

            float x1 = 0.5f;
            float y1 = 0.5f;
            float z1 = 0.5f;

            int texX = (int)this.getTexture(face).X;
            int texY = (int)this.getTexture(face).Y;

            float u0 = texX / col;
            float u1 = u0 + (1.0f / col);
            float v0 = ((row - 1.0f) - texY) / row;
            float v1 = v0 + (1.0f / row);

            // ..:: Positive X ::..
            if(face == 0) {
                t.vertex(x1, y1, z0);
                t.vertex(x1, y1, z1);
                t.vertex(x1, y0, z1);
                t.vertex(x1, y0, z0);

                t.triangle();

                t.tex(u0, v0);
                t.tex(u0, v1);
                t.tex(u1, v1);
                t.tex(u1, v0);

                Console.WriteLine(texX + ", " + texY);
            }

            // ..:: Negative X ::..
            if(face == 1) {
                t.vertex(x0, y1, z1);
                t.vertex(x0, y1, z0);
                t.vertex(x0, y0, z0);
                t.vertex(x0, y0, z1);

                t.triangle();

                t.tex(u0, v0);
                t.tex(u0, v1);
                t.tex(u1, v1);
                t.tex(u1, v0);

                Console.WriteLine(texX + ", " + texY);
            }

            // ..:: Positive Y ::..
            if(face == 2) {
                t.vertex(x1, y1, z0);
                t.vertex(x0, y1, z0);
                t.vertex(x0, y1, z1);
                t.vertex(x1, y1, z1);

                t.triangle();

                t.tex(u0, v0);
                t.tex(u0, v1);
                t.tex(u1, v1);
                t.tex(u1, v0);

                Console.WriteLine(texX + ", " + texY);
            }

            // ..:: Negative Y ::..
            if(face == 3) {
                t.vertex(x1, y0, z1);
                t.vertex(x0, y0, z1);
                t.vertex(x0, y0, z0);
                t.vertex(x1, y0, z0);

                t.triangle();

                t.tex(u0, v0);
                t.tex(u0, v1);
                t.tex(u1, v1);
                t.tex(u1, v0);

                Console.WriteLine(texX + ", " + texY);
            }

            // ..:: Positive Z ::..
            if(face == 4) {
                t.vertex(x1, y1, z1);
                t.vertex(x0, y1, z1);
                t.vertex(x0, y0, z1);
                t.vertex(x1, y0, z1);

                t.triangle();

                t.tex(u0, v0);
                t.tex(u0, v1);
                t.tex(u1, v1);
                t.tex(u1, v0);

                Console.WriteLine(texX + ", " + texY);
            }

            // ..:: Negative Z ::..
            if(face == 5) {
                t.vertex(x0, y1, z0);
                t.vertex(x1, y1, z0);
                t.vertex(x1, y0, z0);
                t.vertex(x0, y0, z0);

                t.triangle();

                t.tex(u0, v0);
                t.tex(u0, v1);
                t.tex(u1, v1);
                t.tex(u1, v0);

                Console.WriteLine(texX + ", " + texY);
            }
        }
    }
}
