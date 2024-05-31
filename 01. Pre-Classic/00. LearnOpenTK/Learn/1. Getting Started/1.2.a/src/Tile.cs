using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnOpenTK.src {
    internal class Tile {
        public Tile() {
        
        }

        public void Render(Tesselator t) {
            t.vertex(-0.5f, -0.5f, 0.0f); // bottom left
            t.vertex(-0.5f,  0.5f, 0.0f); // top left
            t.vertex( 0.5f,  0.5f, 0.0f); // top right
            t.vertex( 0.5f, -0.5f, 0.0f); // bottom right

            //t.triangle();
        }
    }    
}
