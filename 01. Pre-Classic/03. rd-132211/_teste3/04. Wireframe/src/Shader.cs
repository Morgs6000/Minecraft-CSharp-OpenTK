using OpenTK.Graphics.OpenGL4;

namespace RubyDung.src;

public class Shader {
    // o ID do programa
    public int shaderProgram;

    public Shader() {
        // construir e compilar nosso programa shader
        // ------------------------------------
        // shader de vértice
        int vertexShader = GL.CreateShader(ShaderType.VertexShader);
        GL.ShaderSource(vertexShader, File.ReadAllText("../../../src/shaders/shaderVert.glsl"));
        GL.CompileShader(vertexShader);
        // verifica erros de compilação do shader
        int success;
        string infoLog;
        GL.GetShader(vertexShader, ShaderParameter.CompileStatus, out success);
        if(success == 0) {
            GL.GetShaderInfoLog(vertexShader, out infoLog);
            Console.WriteLine("ERROR::SHADER::VERTEX::COMPILATION_FAILED\n" + infoLog);
        }
        // shader de fragmento
        int fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
        GL.ShaderSource(fragmentShader, File.ReadAllText("../../../src/shaders/shaderFrag.glsl"));
        GL.CompileShader(fragmentShader);
        // verifica erros de compilação do shader
        GL.GetShader(fragmentShader, ShaderParameter.CompileStatus, out success);
        if(success == 0) {
            GL.GetShaderInfoLog(fragmentShader, out infoLog);
            Console.WriteLine("ERROR::SHADER::FRAGMENT::COMPILATION_FAILED\n" + infoLog);
        }
        // vincula shaders
        this.shaderProgram = GL.CreateProgram();
        GL.AttachShader(this.shaderProgram, vertexShader);
        GL.AttachShader(this.shaderProgram, fragmentShader);
        GL.LinkProgram(this.shaderProgram);
        // verifica se há erros de vinculação
        GL.GetProgram(this.shaderProgram, GetProgramParameterName.LinkStatus, out success);
        if(success == 0) {
            GL.GetProgramInfoLog(this.shaderProgram, out infoLog);
            Console.WriteLine("ERROR::SHADER::PROGRAM::LINKING_FAILED\n" + infoLog);
        }
        GL.DeleteShader(vertexShader);
        GL.DeleteShader(fragmentShader);
    }

    // ativar o sombreador
    // ------------------------------------------------------------------------
    public void use() {
        GL.UseProgram(this.shaderProgram);
    }
}
