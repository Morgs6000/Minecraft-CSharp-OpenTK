using OpenTK.Graphics.OpenGL4;

namespace RubyDung.src;

public class Shader {
    private int shaderProgram;

    public Shader(string vertexShaderFile, string fragmentShaderFile) {
        string vertexShaderSource = File.ReadAllText($"../../../src/{vertexShaderFile}");
        string fragmentShaderSource = File.ReadAllText($"../../../src/{fragmentShaderFile}");

        int success;
        string infoLog;

        // vertex shader
        int vertexShader = GL.CreateShader(ShaderType.VertexShader);
        GL.ShaderSource(vertexShader, vertexShaderSource);
        GL.CompileShader(vertexShader);

        GL.GetShader(vertexShader, ShaderParameter.CompileStatus, out success);
        if(success == 0) {
            GL.GetShaderInfoLog(vertexShader, out infoLog);
            Console.WriteLine($"ERROR::SHADER::VERTEX::COMPILATION_FAILED\n{infoLog}");
        }

        // fragment sahder
        int fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
        GL.ShaderSource(fragmentShader, fragmentShaderSource);
        GL.CompileShader(fragmentShader);

        GL.GetShader(fragmentShader, ShaderParameter.CompileStatus, out success);
        if(success == 0) {
            GL.GetShaderInfoLog(fragmentShader, out infoLog);
            Console.WriteLine($"ERROR::SHADER::FRAGMENT::COMPILATION_FAILED\n{infoLog}");
        }

        // link shaders
        this.shaderProgram = GL.CreateProgram();
        GL.AttachShader(this.shaderProgram, vertexShader);
        GL.AttachShader(this.shaderProgram, fragmentShader);
        GL.LinkProgram(this.shaderProgram);

        GL.GetProgram(this.shaderProgram, GetProgramParameterName.LinkStatus, out success);
        if(success == 0) {
            GL.GetProgramInfoLog(this.shaderProgram, out infoLog);
            Console.WriteLine($"ERROR::SHADER::PROGRAM::LINKING_FAILED\n{infoLog}");
        }

        GL.DeleteShader(vertexShader);
        GL.DeleteShader(fragmentShader);
    }

    public void use() {
        GL.UseProgram(this.shaderProgram);
    }

    public void setBool(string name, bool value) {
        GL.Uniform1(GL.GetUniformLocation(this.shaderProgram, name), value ? 1 : 0);
    }
}
