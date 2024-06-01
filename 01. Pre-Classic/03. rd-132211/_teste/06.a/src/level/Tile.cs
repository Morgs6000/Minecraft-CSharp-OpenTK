using OpenTK.Mathematics;

namespace RubyDung.src.level {
    public class Tile {
        public static Tile tile = new Bloco();

        public int texX = 0;
        public int texY = 0;

        //public Vector2 tex = new Vector2(0, 0);

        private float col = 3;
        private float row = 3;
        
        protected Tile() {
            
        }

        /*
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

        protected virtual Vector2 getTexture(int face) {
            return new Vector2(this.texX, this.texY);
            //return new Vector2(this.tex.X, this.tex.Y);
        }

        public void renderFace(Tesselator t, int face) {
            float x0 = -0.5f;
            float y0 = -0.5f;
            float z0 = -0.5f;

            float x1 = 0.5f;
            float y1 = 0.5f;
            float z1 = 0.5f;

            int texX = (int)this.getTexture(face).X;
            //int texX = 1;
            int texY = (int)this.getTexture(face).Y;
            //int texY = 1;

            float u0 = texX / this.col;
            float u1 = u0 + (1.0f / this.col);
            float v0 = ((this.row - 1.0f) - texY) / this.row;
            float v1 = v0 + (1.0f / this.row);
            /*
            float u0 = texX / (float)this.col;
            float u1 = u0 + (1.0f / (float)this.col);
            float v0 = (((float)this.row - 1.0f) - texY) / (float)this.row;
            float v1 = v0 + (1.0f / (float)this.row);
            */

            //Console.WriteLine(u0);
            //Console.WriteLine(u1);
            //Console.WriteLine(v0);
            //Console.WriteLine(v1);

            // ..:: Positive X ::..
            if(face == 0) {
                t.vertex(x1, y0, z1);
                t.vertex(x1, y1, z1);
                t.vertex(x1, y1, z0);
                t.vertex(x1, y0, z0);

                t.triangle();

                /*
                t.tex(u0, v0);
                t.tex(u0, v1);
                t.tex(u1, v1);
                t.tex(u1, v0);
                */
                t.tex(u0, u1, v0, v1);

                //Console.WriteLine("face: " + face + ", x: " + texX + ", y: " + texY);
            }

            // ..:: Negative X ::..
            if(face == 1) {
                t.vertex(x0, y0, z0);
                t.vertex(x0, y1, z0);
                t.vertex(x0, y1, z1);
                t.vertex(x0, y0, z1);

                t.triangle();

                /*
                t.tex(u0, v0);
                t.tex(u0, v1);
                t.tex(u1, v1);
                t.tex(u1, v0);
                */
                t.tex(u0, u1, v0, v1);

                //Console.WriteLine("face: " + face + ", x: " + texX + ", y: " + texY);
            }

            // ..:: Positive Y ::..
            if(face == 2) {
                t.vertex(x0, y1, z1);
                t.vertex(x0, y1, z0);
                t.vertex(x1, y1, z0);
                t.vertex(x1, y1, z1);

                t.triangle();

                /*
                t.tex(u0, v0);
                t.tex(u0, v1);
                t.tex(u1, v1);
                t.tex(u1, v0);
                */
                t.tex(u0, u1, v0, v1);

                //Console.WriteLine("face: " + face + ", x: " + texX + ", y: " + texY);
            }

            // ..:: Negative Y ::..
            if(face == 3) {
                t.vertex(x0, y0, z0);
                t.vertex(x0, y0, z1);
                t.vertex(x1, y0, z1);
                t.vertex(x1, y0, z0);

                t.triangle();

                /*
                t.tex(u0, v0);
                t.tex(u0, v1);
                t.tex(u1, v1);
                t.tex(u1, v0);
                */
                t.tex(u0, u1, v0, v1);

                //Console.WriteLine("face: " + face + ", x: " + texX + ", y: " + texY);
            }

            // ..:: Positive Z ::..
            if(face == 4) {
                t.vertex(x0, y0, z1);
                t.vertex(x0, y1, z1);
                t.vertex(x1, y1, z1);
                t.vertex(x1, y0, z1);

                t.triangle();

                /*
                t.tex(u0, v0);
                t.tex(u0, v1);
                t.tex(u1, v1);
                t.tex(u1, v0);
                */
                t.tex(u0, u1, v0, v1);

                //Console.WriteLine("face: " + face + ", x: " + texX + ", y: " + texY);
            }

            // ..:: Negative Z ::..
            if(face == 5) {
                t.vertex(x1, y0, z0);
                t.vertex(x1, y1, z0);
                t.vertex(x0, y1, z0);
                t.vertex(x0, y0, z0);

                t.triangle();

                /*
                t.tex(u0, v0);
                t.tex(u0, v1);
                t.tex(u1, v1);
                t.tex(u1, v0);
                */
                t.tex(u0, u1, v0, v1);

                //Console.WriteLine("face: " + face + ", x: " + texX + ", y: " + texY);
            }
        }
    }
}
