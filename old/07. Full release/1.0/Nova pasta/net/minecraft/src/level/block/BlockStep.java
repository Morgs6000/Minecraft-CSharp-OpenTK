// Decompiled by Jad v1.5.8g. Copyright 2001 Pavel Kouznetsov.
// Jad home page: http://www.kpdus.com/jad.html
// Decompiler options: packimports(3) braces deadcode fieldsfirst 

package net.minecraft.src.level.block;

import java.util.Random;

// Referenced classes of package net.minecraft.src:
//            Block, Material, IBlockAccess, ItemStack, 
//            World

public class BlockStep extends Block
{

    public static final String field_22037_a[] = {
        "stone", "sand", "wood", "cobble", "brick", "smoothStoneBrick"
    };
    private boolean blockType;

    public BlockStep(int i, boolean flag)
    {
        super(i, 6, Material.rock);
        blockType = flag;
        if(!flag)
        {
            setBlockBounds(0.0F, 0.0F, 0.0F, 1.0F, 0.5F, 1.0F);
        } else
        {
            opaqueCubeLookup[i] = true;
        }
        setLightOpacity(255);
    }

    public int getBlockTextureFromSideAndMetadata(int i, int j)
    {
        if(j == 0)
        {
            return i > 1 ? 5 : 6;
        }
        if(j == 1)
        {
            if(i == 0)
            {
                return 208;
            }
            return i != 1 ? 192 : 176;
        }
        if(j == 2)
        {
            return 4;
        }
        if(j == 3)
        {
            return 16;
        }
        if(j == 4)
        {
            return Block.brick.blockIndexInTexture;
        }
        if(j == 5)
        {
            return Block.stoneBrick.blockIndexInTexture;
        } else
        {
            return 6;
        }
    }

    public int getBlockTextureFromSide(int i)
    {
        return getBlockTextureFromSideAndMetadata(i, 0);
    }

    public boolean isOpaqueCube()
    {
        return blockType;
    }

    public void onBlockAdded(World world, int i, int j, int k)
    {
    }

    public int idDropped(int i, Random random, int j)
    {
        return Block.stairSingle.blockID;
    }

    public int quantityDropped(Random random)
    {
        return !blockType ? 1 : 2;
    }

    protected int damageDropped(int i)
    {
        return i;
    }

    public boolean renderAsNormalBlock()
    {
        return blockType;
    }

    public boolean shouldSideBeRendered(IBlockAccess iblockaccess, int i, int j, int k, int l)
    {
        if(this != Block.stairSingle)
        {
            super.shouldSideBeRendered(iblockaccess, i, j, k, l);
        }
        if(l == 1)
        {
            return true;
        }
        if(!super.shouldSideBeRendered(iblockaccess, i, j, k, l))
        {
            return false;
        }
        if(l == 0)
        {
            return true;
        } else
        {
            return iblockaccess.getBlockId(i, j, k) != blockID;
        }
    }

    protected ItemStack func_41049_c_(int i)
    {
        return new ItemStack(Block.stairSingle.blockID, 1, i);
    }

}
