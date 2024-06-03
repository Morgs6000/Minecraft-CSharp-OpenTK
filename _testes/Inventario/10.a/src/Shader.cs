using OpenTK.Graphics.OpenGL4;

namespace RubyDung.src;

public class Shader {
    public int shaderProgram;

    public void load(string vertexPath, string fragmentPath) {
        int success;
        string infoLog;

        // ..:: Shader Vertex ::..
        string vertexShaderSource = File.ReadAllText("../../../src/shaders/" + vertexPath);

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
        string fragmentShaderSource = File.ReadAllText("../../../src/shaders/" + fragmentPath);

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
        this.shaderProgram = GL.CreateProgram();

        GL.AttachShader(this.shaderProgram, vertexShader);
        GL.AttachShader(this.shaderProgram, fragmentShader);
        GL.LinkProgram(this.shaderProgram);

        GL.GetProgram(this.shaderProgram, GetProgramParameterName.LinkStatus, out success);

        if(success == 0) {
            infoLog = GL.GetProgramInfoLog(this.shaderProgram);
            Console.WriteLine("ERROR::SHADER::PROGRAM::LINKING_FAILED\n" + infoLog);
        }

        GL.DeleteShader(vertexShader);
        GL.DeleteShader(fragmentShader);
    }

    public void render() {
        GL.UseProgram(this.shaderProgram);
    }
}
