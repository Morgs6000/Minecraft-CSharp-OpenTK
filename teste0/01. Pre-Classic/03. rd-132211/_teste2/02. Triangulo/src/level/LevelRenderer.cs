﻿using System.Numerics;
using System.Reflection.Emit;

namespace RubyDung.src.level;

//public class LevelRenderer implements LevelListener {
public class LevelRenderer {
//    private static final int CHUNK_SIZE = 16;
//    private Level level;
//    private Chunk[] chunks;
    private Chunk chunk;
//    private int xChunks;
//    private int yChunks;
//    private int zChunks;
//    Tesselator t = new Tesselator();

//    public LevelRenderer(Level level) {
    public LevelRenderer() {
//        this.level = level;
//        level.addListener(this);
//        this.xChunks = level.width / 16;
//        this.yChunks = level.depth / 16;
//        this.zChunks = level.height / 16;
//        this.chunks = new Chunk[this.xChunks * this.yChunks * this.zChunks];

//        for(int x = 0; x < this.xChunks; ++x) {
//            for(int y = 0; y < this.yChunks; ++y) {
//                for(int z = 0; z < this.zChunks; ++z) {
//                    int x0 = x * 16;
//                    int y0 = y * 16;
//                    int z0 = z * 16;
//                    int x1 = (x + 1) * 16;
//                    int y1 = (y + 1) * 16;
//                    int z1 = (z + 1) * 16;
//                    if(x1 > level.width) {
//                        x1 = level.width;
//                    }

//                    if(y1 > level.depth) {
//                        y1 = level.depth;
//                    }

//                    if(z1 > level.height) {
//                        z1 = level.height;
//                    }

//                    this.chunks[(x + y * this.xChunks) * this.zChunks + z] = new Chunk(level, x0, y0, z0, x1, y1, z1);
                    chunk = new Chunk();
                    chunk.Rebuild();
//                }
//            }
//        }
//    }
    }

    public void Render() {
        chunk.Render();
    }

//    public void render(Player player, int layer) {
//        Chunk.rebuiltThisFrame = 0;
//        Frustum frustum = Frustum.getFrustum();

//        for(int i = 0; i < this.chunks.length; ++i) {
//            if(frustum.cubeInFrustum(this.chunks[i].aabb)) {
//                this.chunks[i].render(layer);
//            }
//        }

//    }

//    public void pick(Player player) {
//        float r = 3.0F;
//        AABB box = player.bb.grow(r, r, r);
//        int x0 = (int)box.x0;
//        int x1 = (int)(box.x1 + 1.0F);
//        int y0 = (int)box.y0;
//        int y1 = (int)(box.y1 + 1.0F);
//        int z0 = (int)box.z0;
//        int z1 = (int)(box.z1 + 1.0F);
//        GL11.glInitNames();

//        for(int x = x0; x < x1; ++x) {
//            GL11.glPushName(x);

//            for(int y = y0; y < y1; ++y) {
//                GL11.glPushName(y);

//                for(int z = z0; z < z1; ++z) {
//                    GL11.glPushName(z);
//                    if(this.level.isSolidTile(x, y, z)) {
//                        GL11.glPushName(0);

//                        for(int i = 0; i < 6; ++i) {
//                            GL11.glPushName(i);
//                            this.t.init();
//                            Tile.rock.renderFace(this.t, x, y, z, i);
//                            this.t.flush();
//                            GL11.glPopName();
//                        }

//                        GL11.glPopName();
//                    }

//                    GL11.glPopName();
//                }

//                GL11.glPopName();
//            }

//            GL11.glPopName();
//        }

//    }

//    public void renderHit(HitResult h) {
//        GL11.glEnable(3042);
//        GL11.glBlendFunc(770, 1);
//        GL11.glColor4f(1.0F, 1.0F, 1.0F, (float)Math.sin((double)System.currentTimeMillis() / 100.0) * 0.2F + 0.4F);
//        this.t.init();
//        Tile.rock.renderFace(this.t, h.x, h.y, h.z, h.f);
//        this.t.flush();
//        GL11.glDisable(3042);
//    }

//    public void setDirty(int x0, int y0, int z0, int x1, int y1, int z1) {
//        x0 /= 16;
//        x1 /= 16;
//        y0 /= 16;
//        y1 /= 16;
//        z0 /= 16;
//        z1 /= 16;
//        if(x0 < 0) {
//            x0 = 0;
//        }

//        if(y0 < 0) {
//            y0 = 0;
//        }

//        if(z0 < 0) {
//            z0 = 0;
//        }

//        if(x1 >= this.xChunks) {
//            x1 = this.xChunks - 1;
//        }

//        if(y1 >= this.yChunks) {
//            y1 = this.yChunks - 1;
//        }

//        if(z1 >= this.zChunks) {
//            z1 = this.zChunks - 1;
//        }

//        for(int x = x0; x <= x1; ++x) {
//            for(int y = y0; y <= y1; ++y) {
//                for(int z = z0; z <= z1; ++z) {
//                    this.chunks[(x + y * this.xChunks) * this.zChunks + z].setDirty();
//                }
//            }
//        }

//    }

//    public void tileChanged(int x, int y, int z) {
//        this.setDirty(x - 1, y - 1, z - 1, x + 1, y + 1, z + 1);
//    }

//    public void lightColumnChanged(int x, int z, int y0, int y1) {
//        this.setDirty(x - 1, y0 - 1, z - 1, x + 1, y1 + 1, z + 1);
//    }

//    public void allChanged() {
//        this.setDirty(0, 0, 0, this.level.width, this.level.depth, this.level.height);
//    }
//}
}
