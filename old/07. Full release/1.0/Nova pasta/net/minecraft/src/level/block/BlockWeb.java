// Decompiled by Jad v1.5.8g. Copyright 2001 Pavel Kouznetsov.
// Jad home page: http://www.kpdus.com/jad.html
// Decompiler options: packimports(3) braces deadcode fieldsfirst 

package net.minecraft.src.level.block;

import java.util.Random;

// Referenced classes of package net.minecraft.src:
//            Block, Material, Entity, Item, 
//            World, AxisAlignedBB

public class BlockWeb extends Block
{

    public BlockWeb(int i, int j)
    {
        super(i, j, Material.web);
    }

    public void onEntityCollidedWithBlock(World world, int i, int j, int k, Entity entity)
    {
        entity.setInWeb();
    }

    public boolean isOpaqueCube()
    {
        return false;
    }

    public AxisAlignedBB getCollisionBoundingBoxFromPool(World world, int i, int j, int k)
    {
        return null;
    }

    public int getRenderType()
    {
        return 1;
    }

    public boolean renderAsNormalBlock()
    {
        return false;
    }

    public int idDropped(int i, Random random, int j)
    {
        return Item.silk.shiftedIndex;
    }
}
