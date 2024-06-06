using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using RubyDung.src.level.block;

namespace RubyDung.src.level;

public class Chunk {
    private Tesselator t = new Tesselator();

    public void load() {
        //chunk();

        Block.grass.render(this.t, (0 * 2), 0, (0 * 2));
        Block.stone.render(this.t, (1 * 2), 0, (0 * 2));
        Block.dirt.render(this.t, (2 * 2), 0, (0 * 2));
        Block.planks.render(this.t, (4 * 2), 0, (0 * 2));
        Block.stoneDoubleSlab.render(this.t, (5 * 2), 0, (0 * 2));
        Block.stoneSlab.render(this.t, (6 * 2), 0, (0 * 2));
        Block.brick.render(this.t, (7 * 2), 0, (0 * 2));
        Block.tnt.render(this.t, (8 * 2), 0, (0 * 2));
        Block.web.render(this.t, (11 * 2), 0, (0 * 2));
        Block.plantRed.render(this.t, (12 * 2), 0, (0 * 2));
        Block.plantYellow.render(this.t, (13 * 2), 0, (0 * 2));
        Block.sapling.render(this.t, (15 * 2), 0, (0 * 2));

        Block.cobblestone.render(this.t, (0 * 2), 0, (1 * 2));
        Block.bedrock.render(this.t, (1 * 2), 0, (1 * 2));
        Block.sand.render(this.t, (2 * 2), 0, (1 * 2));
        Block.gravel.render(this.t, (3 * 2), 0, (1 * 2));
        Block.wood.render(this.t, (4 * 2), 0, (1 * 2));
        Block.blockSteel.render(this.t, (6 * 2), 0, (1 * 2));
        Block.blockGold.render(this.t, (7 * 2), 0, (1 * 2));
        Block.blockDiamond.render(this.t, (8 * 2), 0, (1 * 2));
        Block.blockEmerald.render(this.t, (9 * 2), 0, (1 * 2));
        Block.mushroomRed.render(this.t, (12 * 2), 0, (1 * 2));
        Block.mushroomBrown.render(this.t, (13 * 2), 0, (1 * 2));
        Block.sapling_jungle.render(this.t, (14 * 2), 0, (1 * 2));

        Block.oreGold.render(this.t, (0 * 2), 0, (2 * 2));
        Block.oreIron.render(this.t, (1 * 2), 0, (2 * 2));
        Block.oreCoal.render(this.t, (2 * 2), 0, (2 * 2));
        Block.bookShelf.render(this.t, (3 * 2), 0, (2 * 2));
        Block.cobblestoneMossy.render(this.t, (4 * 2), 0, (2 * 2));
        Block.obsidian.render(this.t, (5 * 2), 0, (2 * 2));
        Block.tallGrass.render(this.t, (7 * 2), 0, (2 * 2));
        //Block.beacon.render(this.t, (9 * 2), 0, (2 * 2));
        Block.workbench.render(this.t, (11 * 2), 0, (2 * 2));
        Block.stoneOvenIdle.render(this.t, (12 * 2), 0, (2 * 2));
        Block.dispenser.render(this.t, (14 * 2), 0, (2 * 2));

        Block.sponge.render(this.t, (0 * 2), 0, (3 * 2));
        Block.glass.render(this.t, (1 * 2), 0, (3 * 2));
        Block.oreDiamond.render(this.t, (2 * 2), 0, (3 * 2));
        Block.oreRedstone.render(this.t, (3 * 2), 0, (3 * 2));
        Block.leaves.render(this.t, (4 * 2), 0, (3 * 2));
        Block.leaves_opaque.render(this.t, (5 * 2), 0, (3 * 2));
        Block.stoneBrick.render(this.t, (6 * 2), 0, (3 * 2));
        Block.deadBush.render(this.t, (7 * 2), 0, (3 * 2));
        Block.fern.render(this.t, (8 * 2), 0, (3 * 2));
        Block.stoneOvenActive.render(this.t, (13 * 2), 0, (3 * 2));
        Block.sapling_spruce.render(this.t, (15 * 2), 0, (3 * 2));

        Block.cloth.render(this.t, (0 * 2), 0, (4 * 2));
        Block.mobSpawner.render(this.t, (1 * 2), 0, (4 * 2));
        Block.blockSnow.render(this.t, (2 * 2), 0, (4 * 2));
        Block.ice.render(this.t, (3 * 2), 0, (4 * 2));
        Block.grass_snow.render(this.t, (4 * 2), 0, (4 * 2));
        Block.cactus.render(this.t, (5 * 2), 0, (4 * 2));
        Block.blockClay.render(this.t, (8 * 2), 0, (4 * 2));
        Block.reed.render(this.t, (9 * 2), 0, (4 * 2));
        Block.music.render(this.t, (10 * 2), 0, (4 * 2));
        Block.jukebox.render(this.t, (11 * 2), 0, (4 * 2));
        Block.waterlily.render(this.t, (12 * 2), 0, (4 * 2));
        Block.mycelium.render(this.t, (13 * 2), 0, (4 * 2));
        Block.sapling_birch.render(this.t, (15 * 2), 0, (4 * 2));

        this.t.flush();
    }

    public void render() {
        this.t.render();
    }

    public void chunk() {
        int x0 = 0;
        int y0 = 0;
        int z0 = 0;

        int x1 = 16;
        int y1 = 16;
        int z1 = 16;

        for(int x = x0; x < x1; x++) {
            for(int y = y0; y < y1; y++) {
                for(int z = z0; z < z1; z++) {
                    //vertex(x, y, z);
                }
            }
        }
    }
}
