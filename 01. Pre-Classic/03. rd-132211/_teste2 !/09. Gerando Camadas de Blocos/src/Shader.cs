using OpenTK.Graphics.OpenGL4;

namespace RubyDung.src;

public class Shader {
    public int shaderProgram;

    public void loadShader() {
        int success;
        string infoLog;

        // ..:: Shader Vertex ::..
        string vertexPath = "../../../src/shaders/shader.vert";

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
        string fragmentPath = "../../../src/shaders/shader.frag";

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

    public void use() {
        GL.UseProgram(this.shaderProgram);
    }
}
