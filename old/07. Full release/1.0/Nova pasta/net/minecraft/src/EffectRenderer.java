// Decompiled by Jad v1.5.8g. Copyright 2001 Pavel Kouznetsov.
// Jad home page: http://www.kpdus.com/jad.html
// Decompiler options: packimports(3) braces deadcode fieldsfirst 

package net.minecraft.src;

import java.util.*;

import net.minecraft.src.renderer.Tessellator;
import org.lwjgl.opengl.GL11;

// Referenced classes of package net.minecraft.src:
//            EntityFX, ActiveRenderInfo, Entity, RenderEngine, 
//            Tessellator, MathHelper, Block, EntityDiggingFX, 
//            World

public class EffectRenderer
{

    protected World worldObj;
    private List fxLayers[];
    private RenderEngine renderer;
    private Random rand;

    public EffectRenderer(World world, RenderEngine renderengine)
    {
        fxLayers = new List[4];
        rand = new Random();
        if(world != null)
        {
            worldObj = world;
        }
        renderer = renderengine;
        for(int i = 0; i < 4; i++)
        {
            fxLayers[i] = new ArrayList();
        }

    }

    public void addEffect(EntityFX entityfx)
    {
        int i = entityfx.getFXLayer();
        if(fxLayers[i].size() >= 4000)
        {
            fxLayers[i].remove(0);
        }
        fxLayers[i].add(entityfx);
    }

    public void updateEffects()
    {
        for(int i = 0; i < 4; i++)
        {
            for(int j = 0; j < fxLayers[i].size(); j++)
            {
                EntityFX entityfx = (EntityFX)fxLayers[i].get(j);
                entityfx.onUpdate();
                if(entityfx.isDead)
                {
                    fxLayers[i].remove(j--);
                }
            }

        }

    }

    public void renderParticles(Entity entity, float f)
    {
        float f1 = ActiveRenderInfo.field_41070_d;
        float f2 = ActiveRenderInfo.field_41068_f;
        float f3 = ActiveRenderInfo.field_41069_g;
        float f4 = ActiveRenderInfo.field_41078_h;
        float f5 = ActiveRenderInfo.field_41071_e;
        EntityFX.interpPosX = entity.lastTickPosX + (entity.posX - entity.lastTickPosX) * (double)f;
        EntityFX.interpPosY = entity.lastTickPosY + (entity.posY - entity.lastTickPosY) * (double)f;
        EntityFX.interpPosZ = entity.lastTickPosZ + (entity.posZ - entity.lastTickPosZ) * (double)f;
        for(int i = 0; i < 3; i++)
        {
            if(fxLayers[i].size() == 0)
            {
                continue;
            }
            int j = 0;
            if(i == 0)
            {
                j = renderer.getTexture("/particles.png");
            }
            if(i == 1)
            {
                j = renderer.getTexture("/terrain.png");
            }
            if(i == 2)
            {
                j = renderer.getTexture("/gui/items.png");
            }
            GL11.glBindTexture(3553 /*GL_TEXTURE_2D*/, j);
            Tessellator tessellator = Tessellator.instance;
            GL11.glColor4f(1.0F, 1.0F, 1.0F, 1.0F);
            tessellator.startDrawingQuads();
            for(int k = 0; k < fxLayers[i].size(); k++)
            {
                EntityFX entityfx = (EntityFX)fxLayers[i].get(k);
                tessellator.setBrightness(entityfx.getEntityBrightnessForRender(f));
                entityfx.renderParticle(tessellator, f, f1, f5, f2, f3, f4);
            }

            tessellator.draw();
        }

    }

    public void func_1187_b(Entity entity, float f)
    {
        float f1 = MathHelper.cos((entity.rotationYaw * 3.141593F) / 180F);
        float f2 = MathHelper.sin((entity.rotationYaw * 3.141593F) / 180F);
        float f3 = -f2 * MathHelper.sin((entity.rotationPitch * 3.141593F) / 180F);
        float f4 = f1 * MathHelper.sin((entity.rotationPitch * 3.141593F) / 180F);
        float f5 = MathHelper.cos((entity.rotationPitch * 3.141593F) / 180F);
        byte byte0 = 3;
        if(fxLayers[byte0].size() == 0)
        {
            return;
        }
        Tessellator tessellator = Tessellator.instance;
        for(int i = 0; i < fxLayers[byte0].size(); i++)
        {
            EntityFX entityfx = (EntityFX)fxLayers[byte0].get(i);
            tessellator.setBrightness(entityfx.getEntityBrightnessForRender(f));
            entityfx.renderParticle(tessellator, f, f1, f5, f2, f3, f4);
        }

    }

    public void clearEffects(World world)
    {
        worldObj = world;
        for(int i = 0; i < 4; i++)
        {
            fxLayers[i].clear();
        }

    }

    public void addBlockDestroyEffects(int i, int j, int k, int l, int i1)
    {
        if(l == 0)
        {
            return;
        }
        Block block = Block.blocksList[l];
        int j1 = 4;
        for(int k1 = 0; k1 < j1; k1++)
        {
            for(int l1 = 0; l1 < j1; l1++)
            {
                for(int i2 = 0; i2 < j1; i2++)
                {
                    double d = (double)i + ((double)k1 + 0.5D) / (double)j1;
                    double d1 = (double)j + ((double)l1 + 0.5D) / (double)j1;
                    double d2 = (double)k + ((double)i2 + 0.5D) / (double)j1;
                    int j2 = rand.nextInt(6);
                    addEffect((new EntityDiggingFX(worldObj, d, d1, d2, d - (double)i - 0.5D, d1 - (double)j - 0.5D, d2 - (double)k - 0.5D, block, j2, i1)).func_4041_a(i, j, k));
                }

            }

        }

    }

    public void addBlockHitEffects(int i, int j, int k, int l)
    {
        int i1 = worldObj.getBlockId(i, j, k);
        if(i1 == 0)
        {
            return;
        }
        Block block = Block.blocksList[i1];
        float f = 0.1F;
        double d = (double)i + rand.nextDouble() * (block.maxX - block.minX - (double)(f * 2.0F)) + (double)f + block.minX;
        double d1 = (double)j + rand.nextDouble() * (block.maxY - block.minY - (double)(f * 2.0F)) + (double)f + block.minY;
        double d2 = (double)k + rand.nextDouble() * (block.maxZ - block.minZ - (double)(f * 2.0F)) + (double)f + block.minZ;
        if(l == 0)
        {
            d1 = ((double)j + block.minY) - (double)f;
        }
        if(l == 1)
        {
            d1 = (double)j + block.maxY + (double)f;
        }
        if(l == 2)
        {
            d2 = ((double)k + block.minZ) - (double)f;
        }
        if(l == 3)
        {
            d2 = (double)k + block.maxZ + (double)f;
        }
        if(l == 4)
        {
            d = ((double)i + block.minX) - (double)f;
        }
        if(l == 5)
        {
            d = (double)i + block.maxX + (double)f;
        }
        addEffect((new EntityDiggingFX(worldObj, d, d1, d2, 0.0D, 0.0D, 0.0D, block, l, worldObj.getBlockMetadata(i, j, k))).func_4041_a(i, j, k).multiplyVelocity(0.2F).func_405_d(0.6F));
    }

    public String getStatistics()
    {
        return (new StringBuilder()).append("").append(fxLayers[0].size() + fxLayers[1].size() + fxLayers[2].size()).toString();
    }
}
