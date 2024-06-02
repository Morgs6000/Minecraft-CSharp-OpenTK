using OpenTK.Graphics.OpenGL4;
using StbImageSharp;

namespace RubyDung.src {
    public class Texture {
        public int ID;

        public Texture() {
        
        }

        public void loadTexture() {
            this.ID = GL.GenTexture();

            GL.BindTexture(TextureTarget.Texture2D, this.ID);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);

            string resourceName = "../../../src/textures/terrain.png";

            StbImage.stbi_set_flip_vertically_on_load(1);
            ImageResult img = ImageResult.FromStream(File.OpenRead(resourceName), ColorComponents.RedGreenBlueAlpha);

            if(img.Data != null) {
                GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, img.Width, img.Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, img.Data);
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
