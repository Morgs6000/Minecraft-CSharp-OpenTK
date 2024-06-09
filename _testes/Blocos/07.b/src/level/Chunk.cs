﻿using RubyDung.src.level.block;

namespace RubyDung.src.level;

public class Chunk {
    private Tesselator t = new Tesselator();

    public void load() {
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
        Block.fire.render(this.t, (15 * 2), 0, (1 * 2));

        Block.oreGold.render(this.t, (0 * 2), 0, (2 * 2));
        Block.oreIron.render(this.t, (1 * 2), 0, (2 * 2));
        Block.oreCoal.render(this.t, (2 * 2), 0, (2 * 2));
        Block.bookShelf.render(this.t, (3 * 2), 0, (2 * 2));
        Block.cobblestoneMossy.render(this.t, (4 * 2), 0, (2 * 2));
        Block.obsidian.render(this.t, (5 * 2), 0, (2 * 2));
        Block.tallGrass.render(this.t, (7 * 2), 0, (2 * 2));
        Block.beacon.render(this.t, (9 * 2), 0, (2 * 2));
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
        Block.workbench.render(this.t, (11 * 2), 0, (3 * 2));
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

        Block.torchWood.render(this.t, (0 * 2), 0, (5 * 2));
        Block.stairCompactPlanks.render(this.t, (3 * 2), 0, (5 * 2));
        Block.trapdoor.render(this.t, (4 * 2), 0, (5 * 2));
        Block.fenceIron.render(this.t, (5 * 2), 0, (5 * 2));
        Block.tilledField.render(this.t, (6 * 2), 0, (5 * 2));
        Block.tilledField_dry.render(this.t, (7 * 2), 0, (5 * 2));
        Block.crops_wheat_0.render(this.t, (8 * 2), 0, (5 * 2));
        Block.crops_wheat_1.render(this.t, (9 * 2), 0, (5 * 2));
        Block.crops_wheat_2.render(this.t, (10 * 2), 0, (5 * 2));
        Block.crops_wheat_3.render(this.t, (11 * 2), 0, (5 * 2));
        Block.crops_wheat_4.render(this.t, (12 * 2), 0, (5 * 2));
        Block.crops_wheat_5.render(this.t, (13 * 2), 0, (5 * 2));
        Block.crops_wheat_6.render(this.t, (14 * 2), 0, (5 * 2));
        Block.crops_wheat_7.render(this.t, (15 * 2), 0, (5 * 2));

        Block.lever.render(this.t, (0 * 2), 0, (6 * 2));
        Block.doorWood.render(this.t, (1 * 2), 0, (6 * 2));
        Block.doorSteel.render(this.t, (2 * 2), 0, (6 * 2));
        Block.torchRedstoneActive.render(this.t, (3 * 2), 0, (6 * 2));
        Block.stoneBrickMossy.render(this.t, (4 * 2), 0, (6 * 2));
        Block.stoneBrickCracked.render(this.t, (5 * 2), 0, (6 * 2));
        Block.netherrack.render(this.t, (7 * 2), 0, (6 * 2));
        Block.slowSand.render(this.t, (8 * 2), 0, (6 * 2));
        Block.glowStone.render(this.t, (9 * 2), 0, (6 * 2));
        Block.pistonSticky.render(this.t, (10 * 2), 0, (6 * 2));
        Block.piston.render(this.t, (11 * 2), 0, (6 * 2));
        Block.stem.render(this.t, (15 * 2), 0, (6 * 2));

        Block.rail_curved.render(this.t, (0 * 2), 0, (7 * 2));
        Block.cloth_black.render(this.t, (1 * 2), 0, (7 * 2));
        Block.cloth_gray.render(this.t, (2 * 2), 0, (7 * 2));
        Block.torchRedstoneIdle.render(this.t, (3 * 2), 0, (7 * 2));
        Block.wood_spruce.render(this.t, (4 * 2), 0, (7 * 2));
        Block.wood_birch.render(this.t, (5 * 2), 0, (7 * 2));
        Block.pumpkin.render(this.t, (7 * 2), 0, (7 * 2));
        Block.pumpkinLantern.render(this.t, (8 * 2), 0, (7 * 2));
        Block.cake.render(this.t, (9 * 2), 0, (7 * 2));
        Block.mushroomCapRed.render(this.t, (13 * 2), 0, (7 * 2));
        Block.mushroomCapBrown.render(this.t, (14 * 2), 0, (7 * 2));
        Block.stem_0.render(this.t, (15 * 2), 0, (7 * 2));

        Block.rail.render(this.t, (0 * 2), 0, (8 * 2));
        Block.cloth_red.render(this.t, (1 * 2), 0, (8 * 2));
        Block.cloth_pink.render(this.t, (2 * 2), 0, (8 * 2));
        Block.redstoneRepeaterIdle.render(this.t, (3 * 2), 0, (8 * 2));
        Block.leaves_spruce.render(this.t, (4 * 2), 0, (8 * 2));
        Block.leaves_spruce_opaque.render(this.t, (5 * 2), 0, (8 * 2));
        Block.melon.render(this.t, (8 * 2), 0, (8 * 2));
        Block.mushroomStem.render(this.t, (13 * 2), 0, (8 * 2));
        Block.vine.render(this.t, (15 * 2), 0, (8 * 2));

        Block.blockLapis.render(this.t, (0 * 2), 0, (9 * 2));
        Block.cloth_green.render(this.t, (1 * 2), 0, (9 * 2));
        Block.cloth_lime.render(this.t, (2 * 2), 0, (9 * 2));
        Block.redstoneRepeaterActive.render(this.t, (3 * 2), 0, (9 * 2));
        Block.thinGlass.render(this.t, (4 * 2), 0, (9 * 2));
        Block.bed.render(this.t, (6 * 2), 0, (9 * 2));
        Block.wood_jungle.render(this.t, (9 * 2), 0, (9 * 2));
        Block.cauldron.render(this.t, (10 * 2), 0, (9 * 2));
        Block.brewingStand.render(this.t, (13 * 2), 0, (9 * 2));
        Block.endPortalFrame.render(this.t, (15 * 2), 0, (9 * 2));

        Block.oreLapis.render(this.t, (0 * 2), 0, (10 * 2));
        Block.cloth_brown.render(this.t, (1 * 2), 0, (10 * 2));
        Block.cloth_yellow.render(this.t, (2 * 2), 0, (10 * 2));
        Block.railPowered.render(this.t, (3 * 2), 0, (10 * 2));
        Block.redstoneWire.render(this.t, (4 * 2), 0, (10 * 2));
        Block.redstoneWire_0.render(this.t, (5 * 2), 0, (10 * 2));
        Block.dragonEgg.render(this.t, (7 * 2), 0, (10 * 2));
        Block.cocoaPlant_2.render(this.t, (8 * 2), 0, (10 * 2));
        Block.cocoaPlant_1.render(this.t, (9 * 2), 0, (10 * 2));
        Block.cocoaPlant_0.render(this.t, (10 * 2), 0, (10 * 2));
        Block.oreEmerald.render(this.t, (11 * 2), 0, (10 * 2));
        Block.tripWireSource.render(this.t, (12 * 2), 0, (10 * 2));
        Block.whiteStone.render(this.t, (15 * 2), 0, (10 * 2));

        Block.cloth_blue.render(this.t, (1 * 2), 0, (11 * 2));
        Block.cloth_light_blue.render(this.t, (2 * 2), 0, (11 * 2));
        Block.railPoweredActive.render(this.t, (3 * 2), 0, (11 * 2));
        Block.enchantmentTable.render(this.t, (6 * 2), 0, (11 * 2));
        Block.commandBlock.render(this.t, (8 * 2), 0, (11 * 2));
        Block.itemFrame.render(this.t, (9 * 2), 0, (11 * 2));
        Block.flowerPot.render(this.t, (10 * 2), 0, (11 * 2));

        Block.sandStone.render(this.t, (0 * 2), 0, (12 * 2));
        Block.cloth_purple.render(this.t, (1 * 2), 0, (12 * 2));
        Block.cloth_magenta.render(this.t, (2 * 2), 0, (12 * 2));
        Block.railDetector.render(this.t, (3 * 2), 0, (12 * 2));
        Block.leaves_jungle.render(this.t, (4 * 2), 0, (12 * 2));
        Block.leaves_jungle_opaque.render(this.t, (5 * 2), 0, (12 * 2));
        Block.planks_spruce.render(this.t, (6 * 2), 0, (12 * 2));
        Block.planks_jungle.render(this.t, (7 * 2), 0, (12 * 2));
        Block.crops_0.render(this.t, (8 * 2), 0, (12 * 2));
        Block.crops_1.render(this.t, (9 * 2), 0, (12 * 2));
        Block.crops_2.render(this.t, (10 * 2), 0, (12 * 2));
        Block.crops_carrot.render(this.t, (11 * 2), 0, (12 * 2));
        Block.crops_potato.render(this.t, (12 * 2), 0, (12 * 2));
        Block.waterStill.render(this.t, (15 * 2), 0, (12 * 2));

        Block.cloth_cyan.render(this.t, (1 * 2), 0, (13 * 2));
        Block.cloth_orange.render(this.t, (2 * 2), 0, (13 * 2));
        Block.redstoneLampIdle.render(this.t, (3 * 2), 0, (13 * 2));
        Block.redstoneLampActive.render(this.t, (4 * 2), 0, (13 * 2));
        Block.stoneBrickChiseled.render(this.t, (5 * 2), 0, (13 * 2));
        Block.planks_birch.render(this.t, (6 * 2), 0, (13 * 2));
        Block.anvil_1.render(this.t, (8 * 2), 0, (13 * 2));

        Block.netherBrick.render(this.t, (0 * 2), 0, (14 * 2));
        Block.cloth_light_gray.render(this.t, (1 * 2), 0, (14 * 2));
        Block.nether_wart_0.render(this.t, (2 * 2), 0, (14 * 2));
        Block.nether_wart_1.render(this.t, (3 * 2), 0, (14 * 2));
        Block.nether_wart_2.render(this.t, (4 * 2), 0, (14 * 2));
        Block.sandStone_chiseled.render(this.t, (5 * 2), 0, (14 * 2));
        Block.sandStone_smooth.render(this.t, (6 * 2), 0, (14 * 2));
        Block.anvil_0.render(this.t, (7 * 2), 0, (14 * 2));
        Block.anvil_2.render(this.t, (8 * 2), 0, (14 * 2));
        Block.lavaStill.render(this.t, (15 * 2), 0, (14 * 2));

        this.t.flush();
    }

    public void render() {
        this.t.render();
    }
}