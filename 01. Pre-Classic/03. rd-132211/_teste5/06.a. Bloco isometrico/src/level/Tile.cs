namespace RubyDung.src.level;

public class Tile {
    public static Tile tile = new Tile();

    private int tex = 0;

    public void render(Tesselator t) {
        this.renderFace(t, "x0");
        this.renderFace(t, "x1");
        this.renderFace(t, "y0");
        this.renderFace(t, "y1");
        this.renderFace(t, "z0");
        this.renderFace(t, "z1");
    }

    private int getTexture(string face) {
        if(face == "x0") {
            return 16;
        }
        if(face == "x1") {
            return 17;
        }
        if(face == "y0") {
            return 18;
        }
        if(face == "y1") {
            return 19;
        }
        if(face == "z0") {
            return 20;
        }
        if(face == "z1") {
            return 21;
        }

        return 0;
    }

    private void renderFace(Tesselator t, string face) {
        //float x0 = -0.5f;
        //float y0 = -0.5f;
        //float z0 = -0.5f;

        //float x1 = 0.5f;
        //float y1 = 0.5f;
        //float z1 = 0.5f;

        float x0 = -0.5f * 16.0f;
        float y0 = -0.5f * 16.0f;
        float z0 = -0.5f * 16.0f;

        float x1 = 0.5f * 16.0f;
        float y1 = 0.5f * 16.0f;
        float z1 = 0.5f * 16.0f;

        //float x0 = 0.0f * 16.0f;
        //float y0 = 0.0f * 16.0f;
        //float z0 = 0.0f * 16.0f;

        //float x1 = 1.0f * 16.0f;
        //float y1 = 1.0f * 16.0f;
        //float z1 = 1.0f * 16.0f;

        int tex = this.getTexture(face);

        float u0 = (float)(tex % 16) / 16.0f;
        float v0 = ((16.0f - 1.0f) - (float)(tex / 16)) / 16.0f;

        float u1 = u0 + (1.0f / 16.0f);
        float v1 = v0 + (1.0f / 16.0f);

        if(face == "x0") {
            t.tex(u0, v0);
            t.vertex(x0, y0, z0);
            t.tex(u1, v0);
            t.vertex(x0, y0, z1);
            t.tex(u1, v1);
            t.vertex(x0, y1, z1);
            t.tex(u0, v1);
            t.vertex(x0, y1, z0);
        }
        if(face == "x1") {
            t.tex(u0, v0);
            t.vertex(x1, y0, z1);
            t.tex(u1, v0);
            t.vertex(x1, y0, z0);
            t.tex(u1, v1);
            t.vertex(x1, y1, z0);
            t.tex(u0, v1);
            t.vertex(x1, y1, z1);
        }
        if(face == "y0") {
            t.tex(u0, v0);
            t.vertex(x0, y0, z0);
            t.tex(u1, v0);
            t.vertex(x1, y0, z0);
            t.tex(u1, v1);
            t.vertex(x1, y0, z1);
            t.tex(u0, v1);
            t.vertex(x0, y0, z1);
        }
        if(face == "y1") {
            t.tex(u0, v0);
            t.vertex(x0, y1, z1);
            t.tex(u1, v0);
            t.vertex(x1, y1, z1);
            t.tex(u1, v1);
            t.vertex(x1, y1, z0);
            t.tex(u0, v1);
            t.vertex(x0, y1, z0);
        }
        if(face == "z0") {
            t.tex(u0, v0);
            t.vertex(x1, y0, z0);
            t.tex(u1, v0);
            t.vertex(x0, y0, z0);
            t.tex(u1, v1);
            t.vertex(x0, y1, z0);
            t.tex(u0, v1);
            t.vertex(x1, y1, z0);
        }
        if(face == "z1") {
            t.tex(u0, v0);
            t.vertex(x0, y0, z1);
            t.tex(u1, v0);
            t.vertex(x1, y0, z1);
            t.tex(u1, v1);
            t.vertex(x1, y1, z1);
            t.tex(u0, v1);
            t.vertex(x0, y1, z1);
        }
    }
}
