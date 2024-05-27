﻿//package com.mojang.rubydung.level;

namespace com.Mojang.RubyDung.Level {
    //public class Tile {
    public class Tile {
        //    public static Tile rock = new Tile(0);
        public static Tile rock = new Tile();
    //    public static Tile grass = new Tile(1);
    //    private int tex = 0;
        private int tex = 0;

    //    private Tile(int tex) {
        private Tile() {
    //        this.tex = tex;
    //    }
        }

    //    public void render(Tesselator t, Level level, int layer, int x, int y, int z) {
        public void render(Tesselator t, int x, int y, int z) {
    //        float u0 = (float)this.tex / 16.0F;
            float u0 = (float)this.tex / 16.0f;
    //        float u1 = u0 + 0.0624375F;
            float u1 = u0 + (1.0f / 16.0f);
    //        float v0 = 0.0F;
            float v0 = 0.0f;
    //        float v1 = v0 + 0.0624375F;
            float v1 = v0 + (1.0f / 16.0f);
    //        float c1 = 1.0F;
    //        float c2 = 0.8F;
    //        float c3 = 0.6F;
    //        float x0 = (float)x + 0.0F;
            //float x0 = (float)x + 0.0f;
            float x0 = (float)x - 0.5f;
    //        float x1 = (float)x + 1.0F;
            //float x1 = (float)x + 1.0f;
            float x1 = (float)x + 0.5f;
    //        float y0 = (float)y + 0.0F;
            //float y0 = (float)y + 0.0f;
            float y0 = (float)y - 0.5f;
    //        float y1 = (float)y + 1.0F;
            //float y1 = (float)y + 1.0f;
            float y1 = (float)y + 0.5f;
    //        float z0 = (float)z + 0.0F;
           // float z0 = (float)z + 0.0f;
            float z0 = (float)z - 0.5f;
    //        float z1 = (float)z + 1.0F;
            //float z1 = (float)z + 1.0f;
            float z1 = (float)z + 0.5f;
    //        float br;
    //        if(!level.isSolidTile(x, y - 1, z)) {
    //            br = level.getBrightness(x, y - 1, z) * c1;
    //            if(br == c1 ^ layer == 1) {
    //                t.color(br, br, br);
    //                t.tex(u0, v1);
    //                t.vertex(x0, y0, z1);
    //                t.tex(u0, v0);
    //                t.vertex(x0, y0, z0);
    //                t.tex(u1, v0);
    //                t.vertex(x1, y0, z0);
    //                t.tex(u1, v1);
    //                t.vertex(x1, y0, z1);
    //            }
    //        }

    //        if(!level.isSolidTile(x, y + 1, z)) {
    //            br = level.getBrightness(x, y, z) * c1;
    //            if(br == c1 ^ layer == 1) {
    //                t.color(br, br, br);
    //                t.tex(u1, v1);
    //                t.vertex(x1, y1, z1);
    //                t.tex(u1, v0);
    //                t.vertex(x1, y1, z0);
    //                t.tex(u0, v0);
    //                t.vertex(x0, y1, z0);
    //                t.tex(u0, v1);
    //                t.vertex(x0, y1, z1);
    //            }
    //        }

    //        if(!level.isSolidTile(x, y, z - 1)) {
    //            br = level.getBrightness(x, y, z - 1) * c2;
    //            if(br == c2 ^ layer == 1) {
    //                t.color(br, br, br);
    //                t.tex(u1, v0);
                    t.tex(u0, v0);
    //                t.vertex(x0, y1, z0);
                    t.vertex(x0, y0, z0);
    //                t.tex(u0, v0);
                    t.tex(u0, v1);
    //                t.vertex(x1, y1, z0);
                    t.vertex(x0, y1, z0);
    //                t.tex(u0, v1);
                    t.tex(u1, v1);
    //                t.vertex(x1, y0, z0);
                    t.vertex(x1, y1, z0);
    //                t.tex(u1, v1);
                    t.tex(u1, v0);
    //                t.vertex(x0, y0, z0);
                    t.vertex(x1, y0, z0);
    //            }
    //        }

    //        if(!level.isSolidTile(x, y, z + 1)) {
    //            br = level.getBrightness(x, y, z + 1) * c2;
    //            if(br == c2 ^ layer == 1) {
    //                t.color(br, br, br);
    //                t.tex(u0, v0);
    //                t.vertex(x0, y1, z1);
    //                t.tex(u0, v1);
    //                t.vertex(x0, y0, z1);
    //                t.tex(u1, v1);
    //                t.vertex(x1, y0, z1);
    //                t.tex(u1, v0);
    //                t.vertex(x1, y1, z1);
    //            }
    //        }

    //        if(!level.isSolidTile(x - 1, y, z)) {
    //            br = level.getBrightness(x - 1, y, z) * c3;
    //            if(br == c3 ^ layer == 1) {
    //                t.color(br, br, br);
    //                t.tex(u1, v0);
    //                t.vertex(x0, y1, z1);
    //                t.tex(u0, v0);
    //                t.vertex(x0, y1, z0);
    //                t.tex(u0, v1);
    //                t.vertex(x0, y0, z0);
    //                t.tex(u1, v1);
    //                t.vertex(x0, y0, z1);
    //            }
    //        }

    //        if(!level.isSolidTile(x + 1, y, z)) {
    //            br = level.getBrightness(x + 1, y, z) * c3;
    //            if(br == c3 ^ layer == 1) {
    //                t.color(br, br, br);
    //                t.tex(u0, v1);
    //                t.vertex(x1, y0, z1);
    //                t.tex(u1, v1);
    //                t.vertex(x1, y0, z0);
    //                t.tex(u1, v0);
    //                t.vertex(x1, y1, z0);
    //                t.tex(u0, v0);
    //                t.vertex(x1, y1, z1);
    //            }
    //        }

    //    }
        }

    //    public void renderFace(Tesselator t, int x, int y, int z, int face) {
    //        float x0 = (float)x + 0.0F;
    //        float x1 = (float)x + 1.0F;
    //        float y0 = (float)y + 0.0F;
    //        float y1 = (float)y + 1.0F;
    //        float z0 = (float)z + 0.0F;
    //        float z1 = (float)z + 1.0F;
    //        if(face == 0) {
    //            t.vertex(x0, y0, z1);
    //            t.vertex(x0, y0, z0);
    //            t.vertex(x1, y0, z0);
    //            t.vertex(x1, y0, z1);
    //        }

    //        if(face == 1) {
    //            t.vertex(x1, y1, z1);
    //            t.vertex(x1, y1, z0);
    //            t.vertex(x0, y1, z0);
    //            t.vertex(x0, y1, z1);
    //        }

    //        if(face == 2) {
    //            t.vertex(x0, y1, z0);
    //            t.vertex(x1, y1, z0);
    //            t.vertex(x1, y0, z0);
    //            t.vertex(x0, y0, z0);
    //        }

    //        if(face == 3) {
    //            t.vertex(x0, y1, z1);
    //            t.vertex(x0, y0, z1);
    //            t.vertex(x1, y0, z1);
    //            t.vertex(x1, y1, z1);
    //        }

    //        if(face == 4) {
    //            t.vertex(x0, y1, z1);
    //            t.vertex(x0, y1, z0);
    //            t.vertex(x0, y0, z0);
    //            t.vertex(x0, y0, z1);
    //        }

    //        if(face == 5) {
    //            t.vertex(x1, y0, z1);
    //            t.vertex(x1, y0, z0);
    //            t.vertex(x1, y1, z0);
    //            t.vertex(x1, y1, z1);
    //        }

    //    }
    //}
    }
}
