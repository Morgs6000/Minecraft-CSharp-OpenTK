using OpenTK.Mathematics;

namespace RubyDung.src.level.block;

public class Block {
    public static Block[] blocksList = new Block[4096];

    public static Block stone = new BlockStone().setID(1).setTexture(1, 0);
    public static Block grass = new BlockGrass().setID(2);
    public static Block dirt = new BlockDirt().setID(3).setTexture(2, 0);
    public static Block cobblestone = new Block().setID(4).setTexture(0, 1);
    public static Block planks = new BlockWood().setID(5);
    public static Block sapling = new BlockSapling().setID(6).setTexture(15, 0);
    public static Block bedrock = new Block().setID(7).setTexture(1, 1);
    public static Block waterMoving = new BlockStationary().setID(8).setTexture(15, 12);
    public static Block waterStill = new BlockStationary().setID(9).setTexture(15, 12);
    public static Block lavaMoving = new BlockStationary().setID(10).setTexture(15, 14);
    public static Block lavaStill = new BlockStationary().setID(11).setTexture(15, 14);
    public static Block sand = new BlockSand().setID(12).setTexture(2, 1);
    public static Block gravel = new BlockGravel().setID(13).setTexture(3, 1);
    public static Block oreGold = new BlockOre().setID(14).setTexture(0, 2);
    public static Block oreIron = new BlockOre().setID(15).setTexture(1, 2);
    public static Block oreCoal = new BlockOre().setID(16).setTexture(2, 2);
    public static Block wood = new BlockLog().setID(17);
    public static Block leaves = new BlockLeaves().setID(18).setTexture(4, 3);
    public static Block sponge = new BlockSponge().setID(19);
    public static Block glass = new BlockGlass().setID(20).setTexture(1, 3);
    public static Block oreLapis = new BlockOre().setID(21).setTexture(0, 10);
    public static Block blockLapis = new Block().setID(22).setTexture(0, 9);
    public static Block dispenser = new BlockDispenser().setID(23);
    public static Block sandStone = new BlockSandStone().setID(24).setTexture(0, 12);
    /**/public static Block sandStone_chiseled = new BlockSandStone().setTexture(5, 14);
    /**/public static Block sandStone_smooth = new BlockSandStone().setTexture(6, 14);
    public static Block music = new BlockNote().setID(25);
    public static Block bed = new BlockBed().setID(26);
    public static Block railPowered = new BlockRail().setID(27).setTexture(3, 10);
    /**/public static Block railPoweredActive = new BlockRail().setTexture(3, 11);
    public static Block railDetector = new BlockDetectorRail().setID(28).setTexture(3, 12);
    public static Block pistonStickyBase = new BlockPistonBase().setID(29).setTexture(10, 6);
    public static Block web = new BlockWeb().setID(30).setTexture(11, 0);
    public static Block tallGrass = new BlockTallGrass().setID(31).setTexture(7, 2);
    /**/public static Block fern = new BlockTallGrass().setTexture(8, 3);
    public static Block deadBush = new BlockDeadBush().setID(32).setTexture(7, 3);
    public static Block pistonBase = new BlockPistonBase().setID(33).setTexture(11, 6);
    public static Block cloth = new BlockCloth().setID(35);
    public static Block plantYellow = new BlockFlower().setID(37).setTexture(13, 0);
    public static Block plantRed = new BlockFlower().setID(38).setTexture(12, 0);
    public static Block mushroomBrown = new BlockFlower().setID(39).setTexture(13, 1);
    public static Block mushroomRed = new BlockFlower().setID(40).setTexture(12, 1);
    public static Block blockGold = new BlockOreStorage().setID(41).setTexture(7, 1);
    public static Block blockSteel = new BlockOreStorage().setID(42).setTexture(6, 1);
    /**/public static Block stoneSlab = new Block().setTexture(6, 0);
    public static Block stoneDoubleSlab = new BlockHalfSlab().setID(43);
    public static Block stoneSingleSlab = new BlockHalfSlab().setID(44);
    public static Block brick = new Block().setID(45).setTexture(7, 0);
    public static Block tnt = new BlockTNT().setID(46).setTexture(8, 0);
    public static Block bookShelf = new BlockBookshelf().setID(47).setTexture(3, 2);
    public static Block cobblestoneMossy = new Block().setID(48).setTexture(4, 2);
    public static Block obsidian = new BlockObsidian().setID(49).setTexture(5, 2);
    public static Block torchWood = new BlockTorch().setID(50).setTexture(0, 5);
    public static Block fire = new BlockFire().setID(51).setTexture(15, 1);
    public static Block mobSpawner = new BlockMobSpawner().setID(52).setTexture(1, 4);
    /* Não é esse a escada da parede */public static Block stairCompactPlanks = new BlockStairs().setID(53);
    public static Block chest = new Block().setID(54);
    /**/public static Block redstoneWire_0 = new BlockRedstoneWire().setTexture(4, 10);
    /**/public static Block redstoneWire_1 = new BlockRedstoneWire().setTexture(5, 10);
    public static Block redstoneWire = new BlockRedstoneWire().setID(55).setTexture(5, 10);
    public static Block oreDiamond = new BlockOre().setID(56).setTexture(2, 3);
    public static Block blockDiamond = new BlockOreStorage().setID(57).setTexture(8, 1);
    public static Block workbench = new BlockWorkbench().setID(58);
    public static Block crops = new BlockCrops().setID(59).setTexture(8, 5);
    public static Block tilledField = new BlockFarmland().setID(60).setTexture(6, 5);
    public static Block stoneOvenIdle = new BlockFurnace(false).setID(61);
    public static Block stoneOvenActive = new BlockFurnace(true).setID(62);
    public static Block doorWood = new BlockDoor().setID(64);
    public static Block rail = new BlockRail().setID(66).setTexture(0, 8);
    public static Block lever = new BlockLever().setID(69).setTexture(0, 6);
    public static Block doorSteel = new BlockDoor().setID(71);
    public static Block oreRedstone = new BlockRedstoneOre().setID(73).setTexture(3, 3);
    public static Block torchRedstoneIdle = new BlockRedstoneTorch().setID(75).setTexture(3, 7);
    public static Block torchRedstoneActive = new BlockRedstoneTorch().setID(76).setTexture(3, 6);
    public static Block ice = new BlockIce().setID(79).setTexture(3, 4);
    public static Block blockSnow = new BlockSnowBlock().setID(80).setTexture(2, 4);
    public static Block cactus = new BlockCactus().setID(81).setTexture(6, 4);
    public static Block blockClay = new BlockClay().setID(82).setTexture(8, 4);
    public static Block reed = new BlockReed().setID(83).setTexture(9, 4);
    public static Block jukebox = new BlockJukeBox().setID(84).setTexture(10, 4);
    public static Block pumpkin = new BlockPumpkin().setID(86).setTexture(7, 7);
    public static Block netherrack = new BlockNetherrack().setID(87).setTexture(7, 6);
    public static Block slowSand = new BlockSoulSand().setID(88).setTexture(8, 6);
    public static Block glowStone = new BlockGlowStone().setID(89).setTexture(9, 6);
    public static Block pumpkinLantern = new BlockPumpkin().setID(91).setTexture(8, 7);
    public static Block cake = new BlockCake().setID(92).setTexture(9, 7);
    public static Block redstoneRepeaterIdle = new BlockRedstoneRepeater().setID(93).setTexture(3, 8);
    public static Block redstoneRepeaterActive = new BlockRedstoneRepeater().setID(94).setTexture(3, 9);
    public static Block trapdoor = new BlockTrapDoor().setID(96);
    public static Block stoneBrick = new BlockStoneBrick().setID(98);
    public static Block mushroomCapBrown = new BlockMushroomCap().setID(99).setTexture(14, 7);
    public static Block mushroomCapRed = new BlockMushroomCap().setID(100).setTexture(13, 7);
    //public static Block mushroomStem = new BlockMushroomCap().setTexture(13, 8);
    public static Block fenceIron = new BlockPane().setID(101).setTexture(5, 5);
    public static Block thinGlass = new BlockPane().setID(102).setTexture(4, 9);
    public static Block melon = new BlockMelon().setID(103);
    public static Block pumpkinStem = new BlockStem().setID(104);
    public static Block melonStem = new BlockStem().setID(105);
    public static Block vine = new BlockVine().setID(106);
    public static Block mycelium = new BlockMycelium().setID(110);
    public static Block waterlily = new BlockLilyPad().setID(111).setTexture(12, 4);
    public static Block netherBrick = new Block().setID(112).setTexture(0, 14);
    public static Block enchantmentTable = new BlockEnchantmentTable().setID(116).setTexture(6, 11);
    public static Block brewingStand = new BlockBrewingStand().setID(117);
    public static Block cauldron = new BlockCauldron().setID(118);
    public static Block endPortalFrame = new BlockEndPortalFrame().setID(120);
    public static Block whiteStone = new Block().setID(121).setTexture(15, 10);
    public static Block dragonEgg = new BlockDragonEgg().setID(122);
    public static Block redstoneLampIdle = new BlockRedstoneLight().setID(123).setTexture(3, 13);
    public static Block redstoneLampActive = new BlockRedstoneLight().setID(124).setTexture(4, 13);
    /**/public static Block cocoaPlant_2 = new BlockCocoa().setTexture(8, 10);
    /**/public static Block cocoaPlant_1 = new BlockCocoa().setTexture(9, 10);
    /**/public static Block cocoaPlant_0 = new BlockCocoa().setTexture(10, 10);
    public static Block oreEmerald = new BlockOre().setID(129).setTexture(11, 10);
    public static Block tripWireSource = new BlockTripWireSource().setID(131).setTexture(12, 10);
    public static Block blockEmerald = new BlockOreStorage().setID(133).setTexture(9, 1);
    public static Block commandBlock = new BlockCommandBlock().setID(137).setTexture(8, 11);
    public static Block beacon = new BlockBeacon().setID(138);
    public static Block flowerPot = new BlockFlowerPot().setID(140).setTexture(10, 11);
    /**/public static Block anvil_0 = new BlockAnvil().setTexture(7, 13);
    /**/public static Block anvil_1 = new BlockAnvil().setTexture(8, 13);
    /**/public static Block anvil_2 = new BlockAnvil().setTexture(8, 13);
    /**/public static Block itemFrame = new Block().setTexture(9, 11);

    protected Vector2 tex;
    protected Vector3 color = new Vector3(1.0f, 1.0f, 1.0f);

    public int blockID;

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

    protected Block setID(int ID) {
        blocksList[ID] = this;
        this.blockID = ID;

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
