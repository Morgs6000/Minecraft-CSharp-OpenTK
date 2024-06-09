using OpenTK.Mathematics;

namespace RubyDung.src.level.block;

public class Block {
    public static Block grass = new BlockGrass();
    public static Block stone = new BlockStone().setTexture(1, 0);
    public static Block dirt = new BlockDirt().setTexture(2, 0);
    public static Block planks = new BlockWood();
    public static Block stoneDoubleSlab = new BlockHalfSlab();
    public static Block stoneSlab = new Block().setTexture(6, 0);
    public static Block brick = new Block().setTexture(7, 0);
    public static Block tnt = new BlockTNT().setTexture(8, 0);
    public static Block web = new BlockWeb().setTexture(11, 0);
    public static Block plantRed = new BlockFlower().setTexture(12, 0);
    public static Block plantYellow = new BlockFlower().setTexture(13, 0);
    public static Block sapling = new BlockSapling().setTexture(15, 0);

    public static Block cobblestone = new Block().setTexture(0, 1);
    public static Block bedrock = new Block().setTexture(1, 1);
    public static Block sand = new BlockSand().setTexture(2, 1);
    public static Block gravel = new BlockGravel().setTexture(3, 1);
    public static Block wood = new BlockLog();
    public static Block blockSteel = new BlockOreStorage().setTexture(6, 1);
    public static Block blockGold = new BlockOreStorage().setTexture(7, 1);
    public static Block blockDiamond = new BlockOreStorage().setTexture(8, 1);
    public static Block blockEmerald = new BlockOreStorage().setTexture(9, 1);
    public static Block mushroomRed = new BlockFlower().setTexture(12, 1);
    public static Block mushroomBrown = new BlockFlower().setTexture(13, 1);
    public static Block fire = new BlockFire().setTexture(15, 1);

    public static Block oreGold = new BlockOre().setTexture(0, 2);
    public static Block oreIron = new BlockOre().setTexture(1, 2);
    public static Block oreCoal = new BlockOre().setTexture(2, 2);
    public static Block bookShelf = new BlockBookshelf().setTexture(3, 2);
    public static Block cobblestoneMossy = new Block().setTexture(4, 2);
    public static Block obsidian = new BlockObsidian().setTexture(5, 2);
    public static Block tallGrass = new BlockTallGrass().setTexture(7, 2);
    public static Block beacon = new BlockBeacon();
    public static Block stoneOvenIdle = new BlockFurnace(false);
    public static Block dispenser = new BlockDispenser();

    public static Block sponge = new BlockSponge();
    public static Block glass = new BlockGlass().setTexture(1, 3);
    public static Block oreDiamond = new BlockOre().setTexture(2, 3);
    public static Block oreRedstone = new BlockRedstoneOre().setTexture(3, 3);
    public static Block leaves = new BlockLeaves().setTexture(4, 3);
    public static Block stoneBrick = new BlockStoneBrick();
    public static Block deadBush = new BlockDeadBush().setTexture(7, 3);
    public static Block fern = new BlockTallGrass().setTexture(8, 3);
    public static Block workbench = new BlockWorkbench();
    public static Block stoneOvenActive = new BlockFurnace(true);

    public static Block cloth = new BlockCloth();
    public static Block mobSpawner = new BlockMobSpawner().setTexture(1, 4);
    public static Block blockSnow = new BlockSnowBlock().setTexture(2, 4);
    public static Block ice = new BlockIce().setTexture(3, 4);
    public static Block cactus = new BlockCactus().setTexture(6, 4);
    public static Block blockClay = new BlockClay().setTexture(8, 4);
    public static Block reed = new BlockReed().setTexture(9, 4);
    public static Block music = new BlockNote();
    public static Block jukebox = new BlockJukeBox().setTexture(10, 4);
    public static Block waterlily = new BlockLilyPad().setTexture(12, 4);
    public static Block mycelium = new BlockMycelium();

    public static Block torchWood = new BlockTorch().setTexture(0, 5);
    public static Block stairCompactPlanks = new BlockStairs();
    public static Block trapdoor = new BlockTrapDoor();
    public static Block fenceIron = new BlockPane().setTexture(5, 5);
    public static Block tilledField = new BlockFarmland().setTexture(6, 5);
    public static Block crops = new BlockCrops().setTexture(8, 5);

    public static Block lever = new BlockLever().setTexture(0, 6);
    public static Block doorWood = new BlockDoor();
    public static Block doorSteel = new BlockDoor();
    public static Block torchRedstoneActive = new BlockRedstoneTorch().setTexture(3, 6);
    public static Block netherrack = new BlockNetherrack().setTexture(7, 6);
    public static Block slowSand = new BlockSoulSand().setTexture(8, 6);
    public static Block glowStone = new BlockGlowStone().setTexture(9, 6);
    public static Block pistonStickyBase = new BlockPistonBase().setTexture(10, 6);
    public static Block pistonBase = new BlockPistonBase().setTexture(11, 6);
    public static Block pumpkinStem = new BlockStem();
    public static Block melonStem = new BlockStem();

    public static Block torchRedstoneIdle = new BlockRedstoneTorch().setTexture(3, 7);
    public static Block pumpkin = new BlockPumpkin().setTexture(7, 7);
    public static Block pumpkinLantern = new BlockPumpkin().setTexture(8, 7);
    public static Block cake = new BlockCake().setTexture(9, 7);
    public static Block mushroomCapRed = new BlockMushroomCap().setTexture(13, 7);
    public static Block mushroomCapBrown = new BlockMushroomCap().setTexture(14, 7);

    public static Block rail = new BlockRail().setTexture(0, 8);
    public static Block redstoneRepeaterIdle = new BlockRedstoneRepeater().setTexture(3, 8);
    public static Block melon = new BlockMelon();
    //public static Block mushroomStem = new BlockMushroomCap().setTexture(13, 8);
    public static Block vine = new BlockVine();

    public static Block blockLapis = new Block().setTexture(0, 9);
    public static Block redstoneRepeaterActive = new BlockRedstoneRepeater().setTexture(3, 9);
    public static Block thinGlass = new BlockPane().setTexture(4, 9);
    public static Block bed = new BlockBed();
    public static Block cauldron = new BlockCauldron();
    public static Block brewingStand = new BlockBrewingStand();
    public static Block endPortalFrame = new BlockEndPortalFrame();

    public static Block oreLapis = new BlockOre().setTexture(0, 10);
    public static Block railPowered = new BlockRail().setTexture(3, 10);
    public static Block redstoneWire_0 = new BlockRedstoneWire().setTexture(4, 10);
    public static Block redstoneWire_1 = new BlockRedstoneWire().setTexture(5, 10);
    public static Block dragonEgg = new BlockDragonEgg();
    public static Block cocoaPlant_2 = new BlockCocoa().setTexture(8, 10);
    public static Block cocoaPlant_1 = new BlockCocoa().setTexture(9, 10);
    public static Block cocoaPlant_0 = new BlockCocoa().setTexture(10, 10);
    public static Block oreEmerald = new BlockOre().setTexture(11, 10);
    public static Block tripWireSource = new BlockTripWireSource().setTexture(12, 10);
    public static Block whiteStone = new Block().setTexture(15, 10);

    public static Block railPoweredActive = new BlockRail().setTexture(3, 11);
    public static Block enchantmentTable = new BlockEnchantmentTable().setTexture(6, 11);
    public static Block commandBlock = new BlockCommandBlock().setTexture(8, 11);
    public static Block itemFrame = new Block().setTexture(9, 11);
    public static Block flowerPot = new BlockFlowerPot().setTexture(10, 11);

    public static Block sandStone = new BlockSandStone().setTexture(0, 12);
    public static Block railDetector = new BlockDetectorRail().setTexture(3, 12);
    public static Block waterStill = new BlockStationary().setTexture(15, 12);

    public static Block redstoneLampIdle = new BlockRedstoneLight().setTexture(3, 13);
    public static Block redstoneLampActive = new BlockRedstoneLight().setTexture(4, 13);
    public static Block anvil_1 = new BlockAnvil().setTexture(8, 13);

    public static Block netherBrick = new Block().setTexture(0, 14);
    public static Block sandStone_chiseled = new BlockSandStone().setTexture(5, 14);
    public static Block sandStone_smooth = new BlockSandStone().setTexture(6, 14);
    public static Block anvil_0 = new BlockAnvil().setTexture(7, 13);
    public static Block anvil_2 = new BlockAnvil().setTexture(8, 13);
    public static Block lavaStill = new BlockStationary().setTexture(15, 14);

    protected Vector2 tex;
    protected Vector3 color = new Vector3(1.0f, 1.0f, 1.0f);

    public Block() {

    }

    protected Block setTexture(int texX, int texY) {
        this.tex.X = texX;
        this.tex.Y = texY;

        return this;
    }

    protected Block setColor(float r, float g, float b) {
        this.color.X = r;
        this.color.Y = g;
        this.color.Z = b;

        return this;
    }

    protected string type;

    public Block setType(string type) {
        this.type = type;

        return this;
    }

    public void render(Tesselator t, int x, int y, int z) {
        this.renderFace(t, x, y, z, "x0");
        this.renderFace(t, x, y, z, "x1");
        this.renderFace(t, x, y, z, "y0");
        this.renderFace(t, x, y, z, "ya0");
        this.renderFace(t, x, y, z, "y1");
        this.renderFace(t, x, y, z, "ya1");
        this.renderFace(t, x, y, z, "z0");
        this.renderFace(t, x, y, z, "z1");
    }

    protected virtual Vector2 getTexture(string face) {
        return this.tex;
    }

    protected virtual Vector3 getColor(string face) {
        return this.color;
    }

    public virtual void renderFace(Tesselator t, int x, int y, int z, string face) {
        float x0 = x + 0.0f;
        float y0 = y + 0.0f;
        float z0 = z + 0.0f;

        float x1 = x + 1.0f;
        float y1 = y + 1.0f;
        float z1 = z + 1.0f;

        Vector2 tex = this.getTexture(face);
        Vector3 color = this.getColor(face);

        float col = 16.0f;
        float row = 16.0f;

        float u0 = tex.X / col;
        float u1 = u0 + 1.0f / col;
        float v0 = (row - 1.0f - tex.Y) / row;
        float v1 = v0 + 1.0f / row;

        // ..:: Negative X ::..
        if(face == "x0") {
            t.vertex(x0, y0, z0);
            t.vertex(x0, y1, z0);
            t.vertex(x0, y1, z1);
            t.vertex(x0, y0, z1);

            t.triangle();

            t.tex(u0, v0);
            t.tex(u0, v1);
            t.tex(u1, v1);
            t.tex(u1, v0);

            t.color(color.X, color.Y, color.Z);
        }

        // ..:: Positive X ::..
        if(face == "x1") {
            t.vertex(x1, y0, z1);
            t.vertex(x1, y1, z1);
            t.vertex(x1, y1, z0);
            t.vertex(x1, y0, z0);

            t.triangle();

            t.tex(u0, v0);
            t.tex(u0, v1);
            t.tex(u1, v1);
            t.tex(u1, v0);

            t.color(color.X, color.Y, color.Z);
        }

        // ..:: Negative Y ::..
        if(face == "y0") {
            t.vertex(x0, y0, z0);
            t.vertex(x0, y0, z1);
            t.vertex(x1, y0, z1);
            t.vertex(x1, y0, z0);

            t.triangle();

            t.tex(u0, v0);
            t.tex(u0, v1);
            t.tex(u1, v1);
            t.tex(u1, v0);

            t.color(color.X, color.Y, color.Z);
        }

        // ..:: Positive Y ::..
        if(face == "y1") {
            t.vertex(x0, y1, z1);
            t.vertex(x0, y1, z0);
            t.vertex(x1, y1, z0);
            t.vertex(x1, y1, z1);

            t.triangle();

            t.tex(u0, v0);
            t.tex(u0, v1);
            t.tex(u1, v1);
            t.tex(u1, v0);

            t.color(color.X, color.Y, color.Z);
        }

        // ..:: Negative Z ::..
        if(face == "z0") {
            t.vertex(x1, y0, z0);
            t.vertex(x1, y1, z0);
            t.vertex(x0, y1, z0);
            t.vertex(x0, y0, z0);

            t.triangle();

            t.tex(u0, v0);
            t.tex(u0, v1);
            t.tex(u1, v1);
            t.tex(u1, v0);

            t.color(color.X, color.Y, color.Z);
        }

        // ..:: Positive Z ::..
        if(face == "z1") {
            t.vertex(x0, y0, z1);
            t.vertex(x0, y1, z1);
            t.vertex(x1, y1, z1);
            t.vertex(x1, y0, z1);

            t.triangle();

            t.tex(u0, v0);
            t.tex(u0, v1);
            t.tex(u1, v1);
            t.tex(u1, v0);

            t.color(color.X, color.Y, color.Z);
        }
    }
}
