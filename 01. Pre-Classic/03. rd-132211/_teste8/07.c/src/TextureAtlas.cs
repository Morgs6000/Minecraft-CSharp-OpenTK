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
    private List<(int x, int y, int width, int height)> freeRectangles;

    public TextureAtlas(int width, int height) {
        Width = width;
        Height = height;
        atlasData = new byte[width * height * 4]; // 4 bytes por pixel (RGBA)
        freeRectangles = new List<(int, int, int, int)> { (0, 0, width, height) };
    }

    private (int x, int y)? FindFreeRect(int textureWidth, int textureHeight) {
        for(int i = 0; i < freeRectangles.Count; i++) {
            var rect = freeRectangles[i];
            if(rect.width >= textureWidth && rect.height >= textureHeight) {
                return (rect.x, rect.y);
            }
        }
        return null;
    }

    public bool AddTexture(byte[] imageData, int imageWidth, int imageHeight) {
        StbImage.stbi_set_flip_vertically_on_load(1);

        var position = FindFreeRect(imageWidth, imageHeight);
        if(position == null) {
            return false; // Sem espaço suficiente
        }

        var (x, y) = position.Value;

        // Adiciona a textura no atlas
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

        // Atualiza a lista de retângulos livres
        var rect = freeRectangles.Find(r => r.x == x && r.y == y);
        freeRectangles.Remove(rect);

        if(rect.width > imageWidth) {
            freeRectangles.Add((x + imageWidth, y, rect.width - imageWidth, imageHeight));
        }
        if(rect.height > imageHeight) {
            freeRectangles.Add((x, y + imageHeight, rect.width, rect.height - imageHeight));
        }

        return true;
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
