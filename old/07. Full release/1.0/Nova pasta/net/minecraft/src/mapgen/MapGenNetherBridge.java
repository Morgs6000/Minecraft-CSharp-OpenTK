// Decompiled by Jad v1.5.8g. Copyright 2001 Pavel Kouznetsov.
// Jad home page: http://www.kpdus.com/jad.html
// Decompiler options: packimports(3) braces deadcode fieldsfirst 

package net.minecraft.src.mapgen;

import net.minecraft.src.SpawnListEntry;
import net.minecraft.src.StructureNetherBridgeStart;
import net.minecraft.src.StructureStart;

import java.util.*;

// Referenced classes of package net.minecraft.src:
//            MapGenStructure, SpawnListEntry, EntityBlaze, EntityPigZombie, 
//            EntityMagmaCube, World, StructureNetherBridgeStart, StructureStart

public class MapGenNetherBridge extends MapGenStructure
{

    private List field_40486_a;

    public MapGenNetherBridge()
    {
        field_40486_a = new ArrayList();
        field_40486_a.add(new SpawnListEntry(net.minecraft.src.EntityBlaze.class, 10, 2, 3));
        field_40486_a.add(new SpawnListEntry(net.minecraft.src.EntityPigZombie.class, 10, 4, 4));
        field_40486_a.add(new SpawnListEntry(net.minecraft.src.EntityMagmaCube.class, 3, 4, 4));
    }

    public List func_40485_b()
    {
        return field_40486_a;
    }

    protected boolean canSpawnStructureAtCoords(int i, int j)
    {
        int k = i >> 4;
        int l = j >> 4;
        rand.setSeed((long)(k ^ l << 4) ^ worldObj.getWorldSeed());
        rand.nextInt();
        if(rand.nextInt(3) != 0)
        {
            return false;
        }
        if(i != (k << 4) + 4 + rand.nextInt(8))
        {
            return false;
        }
        return j == (l << 4) + 4 + rand.nextInt(8);
    }

    protected StructureStart getStructureStart(int i, int j)
    {
        return new StructureNetherBridgeStart(worldObj, rand, i, j);
    }
}
