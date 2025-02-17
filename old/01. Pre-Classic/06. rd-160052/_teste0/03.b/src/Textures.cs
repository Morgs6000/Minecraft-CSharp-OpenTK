using OpenTK.Graphics.OpenGL4;
using System.Drawing;
using System.Drawing.Imaging;

namespace RubyDung.src;

public class Textures {
    private static Dictionary<string, int> idMap = new Dictionary<string, int>();

    public Textures() {
    
    }

    public static int loadTexture(string resourceName, TextureMinFilter minFilter, TextureMagFilter magFilter) {
        if(idMap.ContainsKey(resourceName)) {
            return idMap[resourceName];
        }
        else {
            int id = GL.GenTexture();
            idMap[resourceName] = id;
            Console.WriteLine(resourceName + " -> " + id);

            GL.BindTexture(TextureTarget.Texture2D, id);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)minFilter);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)magFilter);

            Bitmap img = new Bitmap(new FileStream($"../../../src/textures/{resourceName}", FileMode.Open));

            //img.RotateFlip(RotateFlipType.RotateNoneFlipY);

            int w = img.Width;
            int h = img.Height;

            var data = img.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, w, h, 0, OpenTK.Graphics.OpenGL4.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);

            img.UnlockBits(data);

            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
            return id;
        }
    }
}
