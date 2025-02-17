using OpenTK.Graphics.OpenGL4;

namespace Breakout;

// Texture2D is able to store and configure a texture in OpenGL.
// It also hosts utility functions for easy management.
public class Texture2D {
    // holds the ID of the texture object, used for all texture operations to reference to this particular texture
    public int ID;

    // texture image dimensions
    public int Width; // width of loaded image in pixels
    public int Height; // height of loaded image in pixels

    // texture Format
    public PixelInternalFormat Internal_Format;
    public PixelFormat Image_Format;

    // texture configuration
    public int Wrap_S; // wrapping mode on S axis
    public int Wrap_T; // wrapping mode on T axis
    public int Filter_Min; // filtering mode if texture pixels < screen pixels
    public int Filter_Max; // filtering mode if texture pixels > screen pixels

    // constructor (sets default texture modes)
    public Texture2D() {
        this.Width = 0;
        this.Height = 0;
        this.Internal_Format = PixelInternalFormat.Rgb;
        this.Image_Format = PixelFormat.Rgb;
        this.Wrap_S = (int)TextureWrapMode.Repeat;
        this.Wrap_T = (int)TextureWrapMode.Repeat;
        this.Filter_Min = (int)TextureMinFilter.Linear;
        this.Filter_Max = (int)TextureMagFilter.Linear;

        this.ID = GL.GenTexture();
    }

    // generates texture from image data
    public void Generate(int width, int height, byte[] data) {
        this.Width = width;
        this.Height = height;

        // create Texture
        GL.BindTexture(TextureTarget.Texture2D, this.ID);
        GL.TexImage2D(TextureTarget.Texture2D, 0, this.Internal_Format, width, height, 0, this.Image_Format, PixelType.UnsignedByte, data);

        // set Texture wrap and filter modes
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, this.Wrap_S);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, this.Wrap_T);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, this.Filter_Min);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, this.Filter_Max);

        // unbind texture
        GL.BindTexture(TextureTarget.Texture2D, 0);
    }

    // binds the texture as the current active GL_TEXTURE_2D texture object
    public void Bind() {
        GL.BindTexture(TextureTarget.Texture2D, this.ID);
    }
}
