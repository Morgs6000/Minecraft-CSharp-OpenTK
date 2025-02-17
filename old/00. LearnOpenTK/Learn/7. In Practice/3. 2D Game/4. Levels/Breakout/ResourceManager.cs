using OpenTK.Graphics.OpenGL4;
using StbImageSharp;

namespace Breakout;

// A static singleton ResourceManager class that hosts several
// functions to load Textures and Shaders. Each loaded texture
// and/or shader is also stored for future reference by string
// handles. All functions and resources are static and no 
// public constructor is defined.
public class ResourceManager {
    // resource storage
    public static Dictionary<string, Shader> Shaders = new Dictionary<string, Shader>();
    public static Dictionary<string, Texture2D> Textures = new Dictionary<string, Texture2D>();

    // loads (and generates) a shader program from file loading vertex, fragment (and geometry) shader's source code. If gShaderFile is not nullptr, it also loads a geometry shader
    public static Shader LoadShader(string vShaderFile, string fShaderFile, string gShaderFile, string name) {
        Shaders[name] = loadShaderFromFile(vShaderFile, fShaderFile, gShaderFile);

        return Shaders[name];
    }

    // retrieves a stored sader
    public static Shader GetShader(string name) {
        return Shaders[name];
    }

    // loads (and generates) a texture from file
    public static Texture2D LoadTexture(string file, bool alpha, string name) {
        Textures[name] = loadTextureFromFile(file, alpha);

        return Textures[name];
    }

    // retrieves a stored texture
    public static Texture2D GetTexture(string name) {
        return Textures[name];
    }

    // properly de-allocates all loaded resources
    public static void Clear() {
        // (properly) delete all shaders    
        foreach(var shader in Shaders.Values) {
            GL.DeleteProgram(shader.ID);
        }
        // (properly) delete all textures
        foreach(var texture in Textures.Values) {
            GL.DeleteTexture(texture.ID);
        }
    }

    // private constructor, that is we do not want any actual resource manager objects. Its members and functions should be publicly available (static).
    private ResourceManager() {
        
    }

    // loads and generates a shader from file
    private static Shader loadShaderFromFile(string vShaderFile, string fShaderFile, string gShaderFile = null) {
        // 1. retrieve the vertex/fragment source code from filePath
        string vertexCode = "";
        string fragmentCode = "";
        string geometryCode = "";

        try {
            // open files
            vertexCode = File.ReadAllText(vShaderFile);
            fragmentCode = File.ReadAllText(fShaderFile);

            // read file's buffer contents into streams

            // close file handlers

            // convert stream into string

            // if geometry shader path is present, also load a geometry shader
            if(gShaderFile != null) {
                geometryCode = File.ReadAllText(gShaderFile);
            }
        }
        catch (Exception e) {
            Console.WriteLine("ERROR::SHADER: Failed to read shader files");
        }

        string vShaderCode = vertexCode;
        string fShaderCode = fragmentCode;
        string gShaderCode = geometryCode;

        // 2. now create shader object from source code
        Shader shader = new Shader();
        shader.Compile(vShaderCode, fShaderCode, gShaderCode != null ? gShaderCode : null);

        return shader;
    }

    // loads a single texture from file
    private static Texture2D loadTextureFromFile(string file, bool alpha) {
        // create texture object
        Texture2D texture = new Texture2D();

        if(alpha) {
            texture.Internal_Format = PixelInternalFormat.Rgba;
            texture.Image_Format = PixelFormat.Rgba;
        }

        // load image
        //int width;
        //int height;
        //int nrChannels;

        var data = ImageResult.FromStream(File.OpenRead(file), ColorComponents.RedGreenBlueAlpha);

        // now generate texture
        texture.Generate(data.Width, data.Height, data.Data);

        // and finally free image data
        return texture;
    }
}
