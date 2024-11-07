using OpenTK.Graphics.OpenGL4;
using StbImageSharp;

namespace RubyDung.src;

public class TextureAtlas {
    public int Width {
        get;
    }
    public int Height {
        get;
    }
    private byte[] atlasData;
    private Dictionary<string, (int x, int y)> texturePositions;

    public TextureAtlas(int width, int height) {
        Width = width;
        Height = height;
        atlasData = new byte[width * height * 4]; // 4 bytes por pixel (RGBA)
        texturePositions = new Dictionary<string, (int x, int y)>();
    }

    public void AddTexture(string name, byte[] imageData, int imageWidth, int imageHeight, int x, int y) {
        StbImage.stbi_set_flip_vertically_on_load(1);

        texturePositions[name] = (x, y);

        for(int row = 0; row < imageHeight; row++) {
            for(int col = 0; col < imageWidth; col++) {
                int srcIndex = (row * imageWidth + col) * 4;
                int destIndex = ((Height - y - imageHeight + row) * Width + (x + col)) * 4;

                atlasData[destIndex] = imageData[srcIndex];
                atlasData[destIndex + 1] = imageData[srcIndex + 1];
                atlasData[destIndex + 2] = imageData[srcIndex + 2];
                atlasData[destIndex + 3] = imageData[srcIndex + 3];
            }
        }
    }

    public (float u0, float v0, float u1, float v1) GetTextureCoordinates(string name) {
        if(texturePositions.TryGetValue(name, out var pos)) {
            float u0 = (float)pos.x / Width;
            float v0 = (float)(Height - pos.y - 16) / Height; // Ajuste para a inversão vertical
            float u1 = u0 + (16.0f / Width);
            float v1 = v0 + (16.0f / Height);

            return (u0, v0, u1, v1);
        }
        else {
            throw new Exception($"Textura '{name}' não encontrada no atlas.");
        }
    }

    public int CreateTexture() {
        int texture;
        GL.GenTextures(1, out texture);
        GL.BindTexture(TextureTarget.Texture2D, texture);
        GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, Width, Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, atlasData);

        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);

        return texture;
    }
}
