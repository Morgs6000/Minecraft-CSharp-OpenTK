using OpenTK.Graphics.OpenGL4;
using StbImageSharp;

namespace RubyDung.src;

public class Texture {
    private int texture;

    public Texture(string textureFile) {
        GL.GenTextures(1, out this.texture);

        GL.BindTexture(TextureTarget.Texture2D, this.texture);

        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat); 
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);

        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);

        //StbImage.stbi_set_flip_vertically_on_load(1);

        ImageResult image = ImageResult.FromStream(File.OpenRead($"../../../src/textures/{textureFile}"), ColorComponents.RedGreenBlueAlpha);

        if(image.Data != null) {
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, image.Width, image.Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, image.Data);

            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
        }
        else {
            Console.WriteLine("Failed to load texture");
        }
    }

    public void bind() {
        GL.BindTexture(TextureTarget.Texture2D, this.texture);
    }
}
