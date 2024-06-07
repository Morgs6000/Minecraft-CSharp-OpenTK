using OpenTK.Mathematics;

namespace RubyDung.src.level.block;

public class Block {
    public static Block grass = new BlockGrass(BlockGrass.grassType.grass);
    public static Block stone = new Block().setTexture(1, 0);
    public static Block dirt = new Block().setTexture(2, 0);
    public static Block planks = new Block().setTexture(4, 0);
    public static Block stoneDoubleSlab = new BlockHalfSlab();
    public static Block stoneSlab = new Block().setTexture(6, 0);
    public static Block brick = new Block().setTexture(7, 0);
    public static Block tnt = new BlockTNT();
    public static Block web = new BlockWeb();
    public static Block plantRed = new BlockFlower(BlockFlower.flowerType.rose);
    public static Block plantYellow = new BlockFlower(BlockFlower.flowerType.dandelion);
    public static Block sapling = new BlockSapling(BlockSapling.saplingType.oak);

    public static Block cobblestone = new Block().setTexture(0, 1);
    public static Block bedrock = new Block().setTexture(1, 1);
    public static Block sand = new Block().setTexture(2, 1);
    public static Block gravel = new Block().setTexture(3, 1);
    public static Block wood = new BlockLog(BlockLog.logType.oak);
    public static Block blockSteel = new Block().setTexture(6, 1);
    public static Block blockGold = new Block().setTexture(7, 1);
    public static Block blockDiamond = new Block().setTexture(8, 1);
    public static Block blockEmerald = new Block().setTexture(9, 1);
    public static Block mushroomRed = new BlockFlower(BlockFlower.flowerType.red_mushroom);
    public static Block mushroomBrown = new BlockFlower(BlockFlower.flowerType.brown_mushroom);
    public static Block sapling_jungle = new BlockSapling(BlockSapling.saplingType.jungle);

    public static Block oreGold = new Block().setTexture(0, 2);
    public static Block oreIron = new Block().setTexture(1, 2);
    public static Block oreCoal = new Block().setTexture(2, 2);
    public static Block bookShelf = new BlockBookshelf();
    public static Block cobblestoneMossy = new Block().setTexture(4, 2);
    public static Block obsidian = new Block().setTexture(5, 2);
    public static Block tallGrass = new BlockTallGrass(BlockTallGrass.grassType.tall_grass);
    public static Block beacon = new BlockBeacon();
    public static Block stoneOvenIdle = new BlockFurnace(new Vector2(12, 2));
    public static Block dispenser = new BlockDispenser();

    public static Block sponge = new Block().setTexture(0, 3);
    public static Block glass = new Block().setTexture(1, 3);
    public static Block oreDiamond = new Block().setTexture(2, 3);
    public static Block oreRedstone = new Block().setTexture(3, 3);
    public static Block leaves = new BlockLeaves(new Vector2(4, 3));
    public static Block leaves_opaque = new BlockLeaves(new Vector2(5, 3));
    public static Block stoneBrick = new Block().setTexture(6, 3);
    public static Block deadBush = new BlockTallGrass(BlockTallGrass.grassType.dead_bush);
    public static Block fern = new BlockTallGrass(BlockTallGrass.grassType.fern);
    public static Block workbench = new BlockWorkbench();
    public static Block stoneOvenActive = new BlockFurnace(new Vector2(13, 3));
    public static Block sapling_spruce = new BlockSapling(BlockSapling.saplingType.spruce);

    public static Block cloth = new Block().setTexture(0, 4);
    public static Block mobSpawner = new Block().setTexture(1, 4);
    public static Block blockSnow = new Block().setTexture(2, 4);
    public static Block ice = new Block().setTexture(3, 4);
    public static Block grass_snow = new BlockGrass(BlockGrass.grassType.snow);
    public static Block cactus = new BlockCactus();
    public static Block blockClay = new Block().setTexture(8, 4);
    public static Block reed = new BlockReed();
    public static Block music = new Block().setTexture(10, 4);
    public static Block jukebox = new Block().setTexture(11, 4);
    public static Block waterlily = new Block().setTexture(12, 4);
    public static Block mycelium = new Block().setTexture(13, 4);
    public static Block sapling_birch = new BlockSapling(BlockSapling.saplingType.birch);

    public static Block torchWood = new Block().setTexture(0, 5);
    public static Block stairCompactPlanks = new Block().setTexture(3, 5);
    public static Block trapdoor = new Block().setTexture(4, 5);
    public static Block fenceIron = new Block().setTexture(5, 5);
    public static Block tilledField = new Block().setTexture(6, 5);
    public static Block tilledField_dry = new Block().setTexture(7, 5);
    public static Block crops_wheat_0 = new BlockCrops(BlockCrops.cropType.wheat_0);
    public static Block crops_wheat_1 = new BlockCrops(BlockCrops.cropType.wheat_1);
    public static Block crops_wheat_2 = new BlockCrops(BlockCrops.cropType.wheat_2);
    public static Block crops_wheat_3 = new BlockCrops(BlockCrops.cropType.wheat_3);
    public static Block crops_wheat_4 = new BlockCrops(BlockCrops.cropType.wheat_4);
    public static Block crops_wheat_5 = new BlockCrops(BlockCrops.cropType.wheat_5);
    public static Block crops_wheat_6 = new BlockCrops(BlockCrops.cropType.wheat_6);
    public static Block crops_wheat_7 = new BlockCrops(BlockCrops.cropType.wheat_7);

    public static Block lever = new Block().setTexture(0, 6);
    public static Block doorWood = new Block().setTexture(1, 6);
    public static Block doorSteel = new Block().setTexture(2, 6);
    public static Block torchRedstoneActive = new Block().setTexture(3, 6);
    public static Block stoneBrickMossy = new Block().setTexture(4, 6);
    public static Block stoneBrickCracked = new Block().setTexture(5, 6);
    public static Block netherrack = new Block().setTexture(7, 6);
    public static Block slowSand = new Block().setTexture(8, 6);
    public static Block glowStone = new Block().setTexture(9, 6);
    public static Block pistonSticky = new Block().setTexture(10, 6);
    public static Block piston = new Block().setTexture(11, 6);
    public static Block stem = new Block().setTexture(15, 6);

    public static Block rail_curved = new Block().setTexture(0, 7);
    public static Block cloth_black = new Block().setTexture(1, 7);
    public static Block cloth_gray = new Block().setTexture(2, 7);
    public static Block torchRedstoneIdle = new Block().setTexture(3, 7);
    public static Block wood_spruce = new BlockLog(BlockLog.logType.spruce);
    public static Block wood_birch = new BlockLog(BlockLog.logType.birch);
    public static Block pumpkin = new Block().setTexture(7, 7);
    public static Block pumpkinLantern = new Block().setTexture(8, 7);
    public static Block cake = new Block().setTexture(9, 7);
    public static Block mushroomCapRed = new Block().setTexture(13, 7);
    public static Block mushroomCapBrown = new Block().setTexture(14, 7);
    public static Block stem_0 = new Block().setTexture(15, 7);

    public static Block rail = new Block().setTexture(0, 8);
    public static Block cloth_red = new Block().setTexture(1, 8);
    public static Block cloth_pink = new Block().setTexture(2, 8);
    public static Block redstoneRepeaterIdle = new Block().setTexture(3, 8);
    public static Block leaves_spruce = new Block().setTexture(4, 8);
    public static Block leaves_spruce_opaque = new Block().setTexture(5, 8);
    public static Block melon = new Block().setTexture(8, 8);
    public static Block mushroomStem= new Block().setTexture(13, 8);
    public static Block vine = new Block().setTexture(15, 8);

    public static Block blockLapis = new Block().setTexture(0, 9);
    public static Block cloth_green = new Block().setTexture(1, 9);
    public static Block cloth_lime = new Block().setTexture(2, 9);
    public static Block redstoneRepeaterActive = new Block().setTexture(3, 9);
    public static Block thinGlass = new Block().setTexture(4, 9);
    public static Block bed = new Block().setTexture(7, 8);
    public static Block wood_jungle = new BlockLog(BlockLog.logType.jungle);
    public static Block cauldron = new Block().setTexture(10, 9);
    public static Block brewingStand = new Block().setTexture(13, 9);
    public static Block endPortalFrame = new Block().setTexture(15, 9);

    public static Block oreLapis = new Block().setTexture(0, 10);
    public static Block cloth_brown = new Block().setTexture(1, 10);
    public static Block cloth_yellow = new Block().setTexture(2, 10);
    public static Block railPowered = new Block().setTexture(3, 10);
    public static Block redstoneWire = new Block().setTexture(4, 10);
    public static Block redstoneWire_0 = new Block().setTexture(5, 10);
    public static Block dragonEgg = new Block().setTexture(7, 10);
    public static Block cocoaPlant_2 = new Block().setTexture(8, 10);
    public static Block cocoaPlant_1 = new Block().setTexture(9, 10);
    public static Block cocoaPlant_0 = new Block().setTexture(10, 10);
    public static Block oreEmerald = new Block().setTexture(11, 10);
    public static Block tripWireSource = new Block().setTexture(12, 10);
    public static Block whiteStone = new Block().setTexture(15, 10);

    public static Block cloth_blue = new Block().setTexture(1, 11);
    public static Block cloth_light_blue = new Block().setTexture(2, 11);
    public static Block railPoweredActive = new Block().setTexture(3, 11);
    public static Block enchantmentTable = new Block().setTexture(6, 11);
    public static Block commandBlock = new Block().setTexture(8, 11);
    public static Block itemFrame = new Block().setTexture(9, 11);
    public static Block flowerPot = new Block().setTexture(10, 11);

    public static Block sandStone = new Block().setTexture(0, 12);
    public static Block cloth_purple = new Block().setTexture(1, 12);
    public static Block cloth_magenta = new Block().setTexture(2, 12);
    public static Block railDetector = new Block().setTexture(3, 12);
    public static Block leaves_jungle = new Block().setTexture(4, 12);
    public static Block leaves_jungle_opaque = new Block().setTexture(5, 12);
    public static Block planks_spruce = new Block().setTexture(6, 12);
    public static Block planks_jungle = new Block().setTexture(7, 12);
    public static Block crops_0 = new BlockCrops(BlockCrops.cropType.crops_0);
    public static Block crops_1 = new BlockCrops(BlockCrops.cropType.crops_1);
    public static Block crops_2 = new BlockCrops(BlockCrops.cropType.crops_2);
    public static Block crops_carrot = new BlockCrops(BlockCrops.cropType.carrot_3);
    public static Block crops_potato = new BlockCrops(BlockCrops.cropType.potato_3);
    public static Block waterStill = new Block().setTexture(15, 12);

    public static Block cloth_cyan = new Block().setTexture(1, 13);
    public static Block cloth_orange = new Block().setTexture(2, 13);
    public static Block redstoneLampIdle = new Block().setTexture(3, 13);
    public static Block redstoneLampActive = new Block().setTexture(4, 13);
    public static Block stoneBrickChiseled = new Block().setTexture(5, 13);
    public static Block planks_birch = new Block().setTexture(6, 13);
    public static Block anvil_1 = new Block().setTexture(8, 13);

    public static Block netherBrick = new Block().setTexture(0, 14);
    public static Block cloth_light_gray = new Block().setTexture(1, 14);
    public static Block nether_wart_0 = new BlockCrops(BlockCrops.cropType.nether_wart_0);
    public static Block nether_wart_1 = new BlockCrops(BlockCrops.cropType.nether_wart_1);
    public static Block nether_wart_2 = new BlockCrops(BlockCrops.cropType.nether_wart_2);
    public static Block sandStone_chiseled = new Block().setTexture(5, 14);
    public static Block sandStone_smooth = new Block().setTexture(6, 14);
    public static Block anvil_0 = new Block().setTexture(7, 13);
    public static Block anvil_2 = new Block().setTexture(8, 13);
    public static Block lavaStill = new Block().setTexture(15, 14);

    public Vector2 tex;
    public Vector3 color = new Vector3(1.0f, 1.0f, 1.0f);

    protected Block() {

    }

    protected Block(Vector2 tex) {
        this.tex = tex;
    }

    protected Block setTexture(int texX, int texY) {
        this.tex.X = texX;
        this.tex.Y = texY;

        return this;
    }

    protected Block setColor(int r, int g, int b) {
        this.color.X = r;
        this.color.Y = g;
        this.color.Z = b;

        return this;
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

    protected virtual Vector3 getColor(int face) {
        return this.color;
    }

    protected virtual void renderFace(Tesselator t, int x, int y, int z, int face) {
        float x0 = x + 0.0f;
        float y0 = y + 0.0f;
        float z0 = z + 0.0f;

        float x1 = x + 1.0f;
        float y1 = y + 1.0f;
        float z1 = z + 1.0f;

        Vector2 tex = this.getTexture(face);
        Vector3 color = this.getColor(face);

        // ..:: Negative X ::..
        if(face == 0) {
            t.vertex(x0, y0, z0);
            t.vertex(x0, y1, z0);
            t.vertex(x0, y1, z1);
            t.vertex(x0, y0, z1);

            t.triangle();
            t.tex(tex.X, tex.Y);
            t.color(color.X, color.Y, color.Z);
        }

        // ..:: Positive X ::..
        if(face == 1) {
            t.vertex(x1, y0, z1);
            t.vertex(x1, y1, z1);
            t.vertex(x1, y1, z0);
            t.vertex(x1, y0, z0);

            t.triangle();
            t.tex(tex.X, tex.Y);
            t.color(color.X, color.Y, color.Z);
        }

        // ..:: Negative Y ::..
        if(face == 2) {
            t.vertex(x0, y0, z0);
            t.vertex(x0, y0, z1);
            t.vertex(x1, y0, z1);
            t.vertex(x1, y0, z0);

            t.triangle();
            t.tex(tex.X, tex.Y);
            t.color(color.X, color.Y, color.Z);
        }

        // ..:: Positive Y ::..
        if(face == 3) {
            t.vertex(x0, y1, z1);
            t.vertex(x0, y1, z0);
            t.vertex(x1, y1, z0);
            t.vertex(x1, y1, z1);

            t.triangle();
            t.tex(tex.X, tex.Y);
            t.color(color.X, color.Y, color.Z);
        }

        // ..:: Negative Z ::..
        if(face == 4) {
            t.vertex(x1, y0, z0);
            t.vertex(x1, y1, z0);
            t.vertex(x0, y1, z0);
            t.vertex(x0, y0, z0);

            t.triangle();
            t.tex(tex.X, tex.Y);
            t.color(color.X, color.Y, color.Z);
        }

        // ..:: Positive Z ::..
        if(face == 5) {
            t.vertex(x0, y0, z1);
            t.vertex(x0, y1, z1);
            t.vertex(x1, y1, z1);
            t.vertex(x1, y0, z1);

            t.triangle();
            t.tex(tex.X, tex.Y);
            t.color(color.X, color.Y, color.Z);
        }
    }
}
