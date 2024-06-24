using OpenTK.Graphics.OpenGL4;

namespace RubyDung.src;

public class Shader {
    public int ID;

    public void load() {
        int success;
        string infoLog;

        // ..:: Shader Vertex ::..
        string vertexPath = "../../../src/shaders/shaderVert.glsl";

        string vertexShaderSource = File.ReadAllText(vertexPath);

        int vertexShader;
        vertexShader = GL.CreateShader(ShaderType.VertexShader);

        GL.ShaderSource(vertexShader, vertexShaderSource);
        GL.CompileShader(vertexShader);

        GL.GetShader(vertexShader, ShaderParameter.CompileStatus, out success);

        if(success == 0) {
            infoLog = GL.GetShaderInfoLog(vertexShader);
            Console.WriteLine("ERROR::SHADER::VERTEX::COMPILATION_FAILED\n" + infoLog);
        }

        // ..:: Shader Fragment ::..
        string fragmentPath = "../../../src/shaders/shaderFrag.glsl";

        string fragmentShaderSource = File.ReadAllText(fragmentPath);

        int fragmentShader;
        fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
        GL.ShaderSource(fragmentShader, fragmentShaderSource);
        GL.CompileShader(fragmentShader);

        GL.GetShader(fragmentShader, ShaderParameter.CompileStatus, out success);

        if(success == 0) {
            infoLog = GL.GetShaderInfoLog(fragmentShader);
            Console.WriteLine("ERROR::SHADER::FRAGMENT::COMPILATION_FAILED\n" + infoLog);
        }

        // ..:: Shader Program ::..
        this.ID = GL.CreateProgram();

        GL.AttachShader(this.ID, vertexShader);
        GL.AttachShader(this.ID, fragmentShader);
        GL.LinkProgram(this.ID);

        GL.GetProgram(this.ID, GetProgramParameterName.LinkStatus, out success);

        if(success == 0) {
            infoLog = GL.GetProgramInfoLog(this.ID);
            Console.WriteLine("ERROR::SHADER::PROGRAM::LINKING_FAILED\n" + infoLog);
        }

        GL.DeleteShader(vertexShader);
        GL.DeleteShader(fragmentShader);
    }

    public void render() {
        GL.UseProgram(this.ID);
    }
}
