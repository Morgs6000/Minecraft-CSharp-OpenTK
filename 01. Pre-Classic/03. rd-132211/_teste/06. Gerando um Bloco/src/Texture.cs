using OpenTK.Graphics.OpenGL4;
using StbImageSharp;

namespace RubyDung.src {
    internal class Texture {
        public int ID;

        public Texture() {
            this.ID = GL.GenTexture();

            GL.BindTexture(TextureTarget.Texture2D, this.ID);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);

            string texturePath = "../../../src/Textures/terrain2.png";

            StbImage.stbi_set_flip_vertically_on_load(1);
            ImageResult image = ImageResult.FromStream(File.OpenRead(texturePath), ColorComponents.RedGreenBlueAlpha);

            if(image.Data != null) {
                GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, image.Width, image.Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, image.Data);
                GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
            }
            else {
                Console.WriteLine("Failed to load texture");
            }
        }

        public void use() {
            GL.BindTexture(TextureTarget.Texture2D, this.ID);
        }
    }
}
