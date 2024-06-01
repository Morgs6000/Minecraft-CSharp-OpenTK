//package com.mojang.rubydung;

//import java.awt.image.BufferedImage;
//import java.io.IOException;
//import java.nio.ByteBuffer;
//import java.nio.IntBuffer;
//import java.util.HashMap;
//import javax.imageio.ImageIO;
//import org.lwjgl.BufferUtils;
//import org.lwjgl.opengl.GL11;
//import org.lwjgl.util.glu.GLU;

using OpenTK.Graphics.OpenGL4;
using StbImageSharp;

namespace RubyDung.src {
    //public class Textures {
    public class Textures {
    //    private static HashMap<String, Integer> idMap = new HashMap();
    //    private static int lastId = -9999999;

        public int ID;

    //    public Textures() {            
    //    }

    //    public static int loadTexture(String resourceName, int mode) {
        public void loadTexture() {
    //        try {
    //            if(idMap.containsKey(resourceName)) {
    //                return (Integer)idMap.get(resourceName);
    //            }
    //            else {
    //                IntBuffer ib = BufferUtils.createIntBuffer(1);
    //                GL11.glGenTextures(ib);
                    this.ID = GL.GenTexture();

    //                int id = ib.get(0);
    //                bind(id);
                    bind(this.ID);

    //                GL11.glTexParameteri(3553, 10241, mode);
                    GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
    //                GL11.glTexParameteri(3553, 10240, mode);
                    GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);

                    StbImage.stbi_set_flip_vertically_on_load(1);
    //                BufferedImage img = ImageIO.read(Textures.class.getResourceAsStream(resourceName));
                    ImageResult img = ImageResult.FromStream(File.OpenRead("../../../src/Textures/terrain.png"), ColorComponents.RedGreenBlueAlpha);

                    if(img.Data != null) {
                        GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, img.Width, img.Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, img.Data);
                        GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
                    }
                    else {
                        Console.WriteLine("Failed to load texture");
                    }

    //                int w = img.getWidth();
    //                int h = img.getHeight();
    //                ByteBuffer pixels = BufferUtils.createByteBuffer(w * h * 4);
    //                int[] rawPixels = new int[w * h];
    //                img.getRGB(0, 0, w, h, rawPixels, 0, w);

    //                for(int i = 0; i<rawPixels.length; ++i) {
    //                    int a = rawPixels[i] >> 24 & 255;
    //                    int r = rawPixels[i] >> 16 & 255;
    //                    int g = rawPixels[i] >> 8 & 255;
    //                    int b = rawPixels[i] & 255;
    //                    rawPixels[i] = a << 24 | b << 16 | g << 8 | r;
    //                }

    //                pixels.asIntBuffer().put(rawPixels);
    //                GLU.gluBuild2DMipmaps(3553, 6408, w, h, 6408, 5121, pixels);
    //                return id;
    //            }
    //        } catch (IOException var14) {
    //            throw new RuntimeException("!!");
    //        }
    //    }
        }

    //    public static void bind(int id) {
        public void bind(int id) {
    //        if(id != lastId) {
    //            GL11.glBindTexture(3553, id);
                GL.BindTexture(TextureTarget.Texture2D, id);
    //            lastId = id;
    //        }

    //    }
        }

        public void use() {
            GL.BindTexture(TextureTarget.Texture2D, this.ID);
        }
    //}
    }
}
