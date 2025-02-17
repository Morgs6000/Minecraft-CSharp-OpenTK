using OpenTK.Graphics.OpenGL4;
using StbImageSharp;

namespace RubyDung.src;

public class TextureAtlas {
    private int atlasWidth;
    private int atlasHeight;
    private byte[] atlasData;

    public TextureAtlas(int width, int height) {
        atlasWidth = width;
        atlasHeight = height;
        atlasData = new byte[width * height * 4]; // 4 bytes por pixel (RGBA)
    }

    public void AddTexture(string filePath, int x, int y) {
        StbImage.stbi_set_flip_vertically_on_load(1);

        // Caminho relativo para o arquivo de textura
        string fullPath = $"../../../src/textures/blocks/{filePath}.png";

        if(!File.Exists(fullPath)) {
            Console.WriteLine($"Arquivo não encontrado: {fullPath}");
            return; // Se o arquivo não existir, não faça nada
        }

        using(var stream = File.OpenRead(fullPath)) {
            var image = ImageResult.FromStream(stream, ColorComponents.RedGreenBlueAlpha);

            for(int row = 0; row < image.Height; row++) {
                for(int col = 0; col < image.Width; col++) {
                    int srcIndex = (row * image.Width + col) * 4;
                    int destIndex = ((atlasHeight - y - image.Height + row) * atlasWidth + (x + col)) * 4;

                    atlasData[destIndex] = image.Data[srcIndex];
                    atlasData[destIndex + 1] = image.Data[srcIndex + 1];
                    atlasData[destIndex + 2] = image.Data[srcIndex + 2];
                    atlasData[destIndex + 3] = image.Data[srcIndex + 3];
                }
            }
        }
    }

    public int CreateTexture() {
        int texture;
        GL.GenTextures(1, out texture);
        GL.BindTexture(TextureTarget.Texture2D, texture);
        GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, atlasWidth, atlasHeight, 0, PixelFormat.Rgba, PixelType.UnsignedByte, atlasData);

        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);

        return texture;
    }
}
