using OpenTK.Graphics.OpenGL4;
using StbImageSharp;

namespace RubyDung.src;

internal class Texture {
    public int texture;

    public Texture(string texturePath) {
        GL.GenTextures(1, out this.texture);
        GL.BindTexture(TextureTarget.Texture2D, this.texture);

        this.textureWrapping();
        this.textureFiltering();

        StbImage.stbi_set_flip_vertically_on_load(1);

        ImageResult image = ImageResult.FromStream(File.OpenRead($"../../../src/textures/{texturePath}"), ColorComponents.RedGreenBlueAlpha);

        if(image.Data != null) {
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, image.Width, image.Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, image.Data);
            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
        }
        else {
            Console.WriteLine("Failed to load texture");
        }
    }

    private void textureWrapping() {
        int Wrap_S = (int)TextureWrapMode.Repeat;
        int Wrap_T = (int)TextureWrapMode.Repeat;

        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, Wrap_S);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, Wrap_T);
    }

    private void textureFiltering() {
        int Filter_Min = (int)TextureMinFilter.Nearest;
        int Filter_Max = (int)TextureMagFilter.Nearest;

        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, Filter_Min);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, Filter_Max);
    }

    public void bind() {
        GL.BindTexture(TextureTarget.Texture2D, this.texture);
    }
}
