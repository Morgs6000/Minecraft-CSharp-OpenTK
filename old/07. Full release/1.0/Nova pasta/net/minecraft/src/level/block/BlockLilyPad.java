// Decompiled by Jad v1.5.8g. Copyright 2001 Pavel Kouznetsov.
// Jad home page: http://www.kpdus.com/jad.html
// Decompiler options: packimports(3) braces deadcode fieldsfirst 

package net.minecraft.src.level.block;


// Referenced classes of package net.minecraft.src:
//            BlockFlower, AxisAlignedBB, Block, World, 
//            Material, IBlockAccess

public class BlockLilyPad extends BlockFlower
{

    protected BlockLilyPad(int i, int j)
    {
        super(i, j);
        float f = 0.5F;
        float f1 = 0.015625F;
        setBlockBounds(0.5F - f, 0.0F, 0.5F - f, 0.5F + f, f1, 0.5F + f);
    }

    public int getRenderType()
    {
        return 23;
    }

    public AxisAlignedBB getCollisionBoundingBoxFromPool(World world, int i, int j, int k)
    {
        return AxisAlignedBB.getBoundingBoxFromPool((double)i + minX, (double)j + minY, (double)k + minZ, (double)i + maxX, (double)j + maxY, (double)k + maxZ);
    }

    public int getBlockColor()
    {
        return 0x208030;
    }

    public int getRenderColor(int i)
    {
        return 0x208030;
    }

    public boolean canPlaceBlockAt(World world, int i, int j, int k)
    {
        return super.canPlaceBlockAt(world, i, j, k);
    }

    public int colorMultiplier(IBlockAccess iblockaccess, int i, int j, int k)
    {
        return 0x208030;
    }

    protected boolean canThisPlantGrowOnThisBlockID(int i)
    {
        return i == Block.waterStill.blockID;
    }

    public boolean canBlockStay(World world, int i, int j, int k)
    {
        if(j < 0 || j >= world.field_35472_c)
        {
            return false;
        } else
        {
            return world.getBlockMaterial(i, j - 1, k) == Material.water && world.getBlockMetadata(i, j - 1, k) == 0;
        }
    }
}
