using OpenTK.Graphics.OpenGL4;
using StbImageSharp;

namespace RubyDung.src;

//public class Textures {
public class Texture {
//    private static HashMap<String, Integer> idMap = new HashMap();
//    private static int lastId = -9999999;

    private int handle;

//    public Textures() {
//    }

//    public static int loadTexture(String resourceName, int mode) {
    public void LoadTexture(string resourceName, int mode) {    
//        try {
//            if(idMap.containsKey(resourceName)) {
//                return (Integer)idMap.get(resourceName);
//            }
//            else {
//                IntBuffer ib = BufferUtils.createIntBuffer(1);
//                GL11.glGenTextures(ib);
                handle = GL.GenTexture();
//                int id = ib.get(0);

                GL.ActiveTexture(TextureUnit.Texture0);

//                bind(id);
                Bind();
//                GL11.glTexParameteri(3553, 10241, mode);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, mode);
//                GL11.glTexParameteri(3553, 10240, mode);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, mode);

                StbImage.stbi_set_flip_vertically_on_load(1);

//                BufferedImage img = ImageIO.read(Textures.class.getResourceAsStream(resourceName));
                ImageResult image = ImageResult.FromStream(File.OpenRead(resourceName), ColorComponents.RedGreenBlueAlpha);

                GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, image.Width, image.Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, image.Data);        

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
                GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
//                return id;
//            }
//        } catch (IOException var14) {
//            throw new RuntimeException("!!");
//        }
//    }
    }

//    public static void bind(int id) {
    public void Bind() {
//        if(id != lastId) {
//            GL11.glBindTexture(3553, id);
            GL.BindTexture(TextureTarget.Texture2D, handle);
//            lastId = id;
//        }
//    }
    }
//}
}
