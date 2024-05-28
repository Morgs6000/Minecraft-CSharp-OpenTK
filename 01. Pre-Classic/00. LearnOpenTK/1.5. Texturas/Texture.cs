using System;
using System.IO;
using System.Reflection.Metadata;
using OpenTK.Graphics.OpenGL4;
using StbImageSharp;

namespace LearnOpenTK {
    internal class Texture {
        int Handle;

        public static Texture LoadFromFile(string path) {
            int Handle = GL.GenTexture();

            StbImage.stbi_set_flip_vertically_on_load(1);

            ImageResult image = ImageResult.FromStream(File.OpenRead("../../../Resources/" + path), ColorComponents.RedGreenBlueAlpha);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, image.Width, image.Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, image.Data);

            return new Texture(Handle);
        }

        public Texture(int Handle) {
            this.Handle = Handle;
        }

        public void Use(TextureUnit unit) {
            GL.ActiveTexture(unit);
            GL.BindTexture(TextureTarget.Texture2D, this.Handle);
        }
    }
}
