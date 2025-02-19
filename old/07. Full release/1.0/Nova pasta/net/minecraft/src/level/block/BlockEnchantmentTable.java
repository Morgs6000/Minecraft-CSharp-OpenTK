// Decompiled by Jad v1.5.8g. Copyright 2001 Pavel Kouznetsov.
// Jad home page: http://www.kpdus.com/jad.html
// Decompiler options: packimports(3) braces deadcode fieldsfirst 

package net.minecraft.src.level.block;

import java.util.Random;

// Referenced classes of package net.minecraft.src:
//            BlockContainer, Material, World, Block, 
//            TileEntityEnchantmentTable, EntityPlayer, TileEntity

public class BlockEnchantmentTable extends BlockContainer
{

    protected BlockEnchantmentTable(int i)
    {
        super(i, 166, Material.rock);
        setBlockBounds(0.0F, 0.0F, 0.0F, 1.0F, 0.75F, 1.0F);
        setLightOpacity(0);
    }

    public boolean renderAsNormalBlock()
    {
        return false;
    }

    public void randomDisplayTick(World world, int i, int j, int k, Random random)
    {
        super.randomDisplayTick(world, i, j, k, random);
        for(int l = i - 2; l <= i + 2; l++)
        {
            for(int i1 = k - 2; i1 <= k + 2; i1++)
            {
                if(l > i - 2 && l < i + 2 && i1 == k - 1)
                {
                    i1 = k + 2;
                }
                if(random.nextInt(16) != 0)
                {
                    continue;
                }
                for(int j1 = j; j1 <= j + 1; j1++)
                {
                    if(world.getBlockId(l, j1, i1) != Block.bookShelf.blockID)
                    {
                        continue;
                    }
                    if(!world.isAirBlock((l - i) / 2 + i, j1, (i1 - k) / 2 + k))
                    {
                        break;
                    }
                    world.spawnParticle("enchantmenttable", (double)i + 0.5D, (double)j + 2D, (double)k + 0.5D, (double)((float)(l - i) + random.nextFloat()) - 0.5D, (float)(j1 - j) - random.nextFloat() - 1.0F, (double)((float)(i1 - k) + random.nextFloat()) - 0.5D);
                }

            }

        }

    }

    public boolean isOpaqueCube()
    {
        return false;
    }

    public int getBlockTextureFromSideAndMetadata(int i, int j)
    {
        return getBlockTextureFromSide(i);
    }

    public int getBlockTextureFromSide(int i)
    {
        if(i == 0)
        {
            return blockIndexInTexture + 17;
        }
        if(i == 1)
        {
            return blockIndexInTexture;
        } else
        {
            return blockIndexInTexture + 16;
        }
    }

    public TileEntity getBlockEntity()
    {
        return new TileEntityEnchantmentTable();
    }

    public boolean blockActivated(World world, int i, int j, int k, EntityPlayer entityplayer)
    {
        if(world.multiplayerWorld)
        {
            return true;
        } else
        {
            entityplayer.func_40181_c(i, j, k);
            return true;
        }
    }
}
