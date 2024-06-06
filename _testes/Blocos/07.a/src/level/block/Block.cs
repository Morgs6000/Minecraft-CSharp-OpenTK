using OpenTK.Mathematics;

namespace RubyDung.src.level.block;

public class Block {
    public static Block grass = new BlockGrass();
    public static Block stone = new Block(new Vector2(1, 0));
    public static Block dirt = new Block(new Vector2(2, 0));
    public static Block planks = new Block(new Vector2(4, 0));
    public static Block stoneDoubleSlab = new BlockHalfSlab();
    public static Block stoneSlab = new Block(new Vector2(6, 0));
    public static Block brick = new Block(new Vector2(7, 0));
    public static Block tnt = new BlockTNT();
    public static Block web = new BlockWeb();
    public static Block plantRed = new BlockFlower(new Vector2(12, 0));
    public static Block plantYellow = new BlockFlower(new Vector2(13, 0));
    public static Block sapling = new BlockSapling(new Vector2(15, 0));

    public static Block cobblestone = new Block(new Vector2(0, 1));
    public static Block bedrock = new Block(new Vector2(1, 1));
    public static Block sand = new Block(new Vector2(2, 1));
    public static Block gravel = new Block(new Vector2(3, 1));
    public static Block wood = new BlockLog(new Vector2(5, 1), new Vector2(4, 1));
    public static Block blockSteel = new Block(new Vector2(6, 1));
    public static Block blockGold = new Block(new Vector2(7, 1));
    public static Block blockDiamond = new Block(new Vector2(8, 1));
    public static Block blockEmerald = new Block(new Vector2(9, 1));
    public static Block mushroomRed = new BlockFlower(new Vector2(12, 1));
    public static Block mushroomBrown = new BlockFlower(new Vector2(13, 1));
    public static Block sapling_jungle = new BlockSapling(new Vector2(14, 1));

    public static Block oreGold = new Block(new Vector2(0, 2));
    public static Block oreIron = new Block(new Vector2(1, 2));
    public static Block oreCoal = new Block(new Vector2(2, 2));
    public static Block bookShelf = new BlockBookshelf();
    public static Block cobblestoneMossy = new Block(new Vector2(4, 2));
    public static Block obsidian = new Block(new Vector2(5, 2));
    public static Block tallGrass = new BlockTallGrass(new Vector2(7, 2));
    public static Block beacon = new BlockBeacon();
    public static Block workbench = new Block(new Vector2(11, 2));
    public static Block stoneOvenIdle = new Block(new Vector2(12, 2));
    public static Block dispenser = new Block(new Vector2(14, 2));

    public static Block sponge = new Block(new Vector2(0, 3));
    public static Block glass = new Block(new Vector2(1, 3));
    public static Block oreDiamond = new Block(new Vector2(2, 3));
    public static Block oreRedstone = new Block(new Vector2(3, 3));
    public static Block leaves = new Block(new Vector2(4, 3));
    public static Block leaves_opaque = new Block(new Vector2(5, 3));
    public static Block stoneBrick = new Block(new Vector2(6, 3));
    public static Block deadBush = new BlockTallGrass(new Vector2(7, 3));
    public static Block fern = new BlockTallGrass(new Vector2(8, 3));
    public static Block stoneOvenActive = new Block(new Vector2(13, 3));
    public static Block sapling_spruce = new BlockSapling(new Vector2(15, 3));

    public static Block cloth = new Block(new Vector2(0, 4));
    public static Block mobSpawner = new Block(new Vector2(1, 4));
    public static Block blockSnow = new Block(new Vector2(2, 4));
    public static Block ice = new Block(new Vector2(3, 4));
    public static Block grass_snow = new Block(new Vector2(4, 4));
    public static Block cactus = new Block(new Vector2(5, 4));
    public static Block blockClay = new Block(new Vector2(8, 4));
    public static Block reed = new BlockTallGrass(new Vector2(9, 4));
    public static Block music = new Block(new Vector2(10, 4));
    public static Block jukebox = new Block(new Vector2(11, 4));
    public static Block waterlily = new Block(new Vector2(12, 4));
    public static Block mycelium = new Block(new Vector2(13, 4));
    public static Block sapling_birch = new BlockSapling(new Vector2(15, 4));

    public Vector2 tex;

    protected Block() {

    }

    protected Block(Vector2 tex) {
        this.tex = tex;
    }

    public void render(Tesselator t, int x, int y, int z) {
        // ..:: Negative X ::..
        this.renderFace(t, x, y, z, 0);

        // ..:: Positive X ::..
        this.renderFace(t, x, y, z, 1);

        // ..:: Negative Y ::..
        this.renderFace(t, x, y, z, 2);

        // ..:: Positive Y ::..
        this.renderFace(t, x, y, z, 3);

        // ..:: Negative Z ::..
        this.renderFace(t, x, y, z, 4);

        // ..:: Positive Z ::..
        this.renderFace(t, x, y, z, 5);
    }

    protected virtual Vector2 getTexture(int face) {
        return this.tex;
    }

    protected virtual void renderFace(Tesselator t, int x, int y, int z, int face) {
        float x0 = x + 0.0f;
        float y0 = y + 0.0f;
        float z0 = z + 0.0f;

        float x1 = x + 1.0f;
        float y1 = y + 1.0f;
        float z1 = z + 1.0f;

        Vector2 tex = this.getTexture(face);

        // ..:: Negative X ::..
        if(face == 0) {
            t.vertex(x0, y0, z0);
            t.vertex(x0, y1, z0);
            t.vertex(x0, y1, z1);
            t.vertex(x0, y0, z1);

            t.triangle();
            t.tex(tex.X, tex.Y);
        }

        // ..:: Positive X ::..
        if(face == 1) {
            t.vertex(x1, y0, z1);
            t.vertex(x1, y1, z1);
            t.vertex(x1, y1, z0);
            t.vertex(x1, y0, z0);

            t.triangle();
            t.tex(tex.X, tex.Y);
        }

        // ..:: Negative Y ::..
        if(face == 2) {
            t.vertex(x0, y0, z0);
            t.vertex(x0, y0, z1);
            t.vertex(x1, y0, z1);
            t.vertex(x1, y0, z0);

            t.triangle();
            t.tex(tex.X, tex.Y);
        }

        // ..:: Positive Y ::..
        if(face == 3) {
            t.vertex(x0, y1, z1);
            t.vertex(x0, y1, z0);
            t.vertex(x1, y1, z0);
            t.vertex(x1, y1, z1);

            t.triangle();
            t.tex(tex.X, tex.Y);
        }

        // ..:: Negative Z ::..
        if(face == 4) {
            t.vertex(x1, y0, z0);
            t.vertex(x1, y1, z0);
            t.vertex(x0, y1, z0);
            t.vertex(x0, y0, z0);

            t.triangle();
            t.tex(tex.X, tex.Y);
        }

        // ..:: Positive Z ::..
        if(face == 5) {
            t.vertex(x0, y0, z1);
            t.vertex(x0, y1, z1);
            t.vertex(x1, y1, z1);
            t.vertex(x1, y0, z1);

            t.triangle();
            t.tex(tex.X, tex.Y);
        }
    }
}
