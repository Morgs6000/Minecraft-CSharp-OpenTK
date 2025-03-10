// Decompiled by Jad v1.5.8g. Copyright 2001 Pavel Kouznetsov.
// Jad home page: http://www.kpdus.com/jad.html
// Decompiler options: packimports(3) braces deadcode fieldsfirst 

package net.minecraft.src.mapgen;

import net.minecraft.src.BiomeGenBase;
import net.minecraft.src.StructureStart;
import net.minecraft.src.StructureVillageStart;

import java.util.*;

// Referenced classes of package net.minecraft.src:
//            MapGenStructure, World, WorldChunkManager, StructureVillageStart, 
//            BiomeGenBase, StructureStart

public class MapGenVillage extends MapGenStructure
{

    public static List villageSpawnBiomes;

    public MapGenVillage()
    {
    }

    protected boolean canSpawnStructureAtCoords(int i, int j)
    {
        byte byte0 = 32;
        byte byte1 = 8;
        int k = i;
        int l = j;
        if(i < 0)
        {
            i -= byte0 - 1;
        }
        if(j < 0)
        {
            j -= byte0 - 1;
        }
        int i1 = i / byte0;
        int j1 = j / byte0;
        Random random = worldObj.setRandomSeed(i1, j1, 0x9e7f70);
        i1 *= byte0;
        j1 *= byte0;
        i1 += random.nextInt(byte0 - byte1);
        j1 += random.nextInt(byte0 - byte1);
        i = k;
        j = l;
        if(i == i1 && j == j1)
        {
            boolean flag = worldObj.getWorldChunkManager().areBiomesViable(i * 16 + 8, j * 16 + 8, 0, villageSpawnBiomes);
            if(flag)
            {
                return true;
            }
        }
        return false;
    }

    protected StructureStart getStructureStart(int i, int j)
    {
        return new StructureVillageStart(worldObj, rand, i, j);
    }

    static 
    {
        villageSpawnBiomes = Arrays.asList(new BiomeGenBase[] {
            BiomeGenBase.plains, BiomeGenBase.desert
        });
    }
}
