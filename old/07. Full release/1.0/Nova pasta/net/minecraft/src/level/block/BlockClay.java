// Decompiled by Jad v1.5.8g. Copyright 2001 Pavel Kouznetsov.
// Jad home page: http://www.kpdus.com/jad.html
// Decompiler options: packimports(3) braces deadcode fieldsfirst 

package net.minecraft.src.level.block;

import java.util.Random;

// Referenced classes of package net.minecraft.src:
//            Block, Material, Item

public class BlockClay extends Block
{

    public BlockClay(int i, int j)
    {
        super(i, j, Material.clay);
    }

    public int idDropped(int i, Random random, int j)
    {
        return Item.clay.shiftedIndex;
    }

    public int quantityDropped(Random random)
    {
        return 4;
    }
}
