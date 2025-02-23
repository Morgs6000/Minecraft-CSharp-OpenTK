using OpenTK.Graphics.OpenGL4;

namespace RubyDung.src;

public class Shader {
    private int handle;

    public Shader(string vertexPath, string fragmentPath) {
        // Carregar o código-fonte dos arquivos de shader.
        string vertexShaderSource = File.ReadAllText(vertexPath);
        string fragmentShaderSource = File.ReadAllText(fragmentPath);

        // Gerar os shaders e vincular o código-fonte aos sombreadores.
        int vertexShader = GL.CreateShader(ShaderType.VertexShader);
        GL.ShaderSource(vertexShader, vertexShaderSource);

        int fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
        GL.ShaderSource(fragmentShader, fragmentShaderSource);

        // Compilar os shaders e verificar se há erros.
        CompileShader(vertexShader);
        CompileShader(fragmentShader);

        // Vincular os shaders ao programa.
        handle = GL.CreateProgram();

        GL.AttachShader(handle, vertexShader);
        GL.AttachShader(handle, fragmentShader);

        LinkProgram();

        GL.DetachShader(handle, vertexShader);
        GL.DetachShader(handle, fragmentShader);
        GL.DeleteShader(vertexShader);
        GL.DeleteShader(fragmentShader);
    }

    private void CompileShader(int shader) {
        GL.CompileShader(shader);

        GL.GetShader(shader, ShaderParameter.CompileStatus, out int success);
        if(success == 0) {
            string infoLog = GL.GetShaderInfoLog(shader);
            Console.WriteLine(infoLog);
        }
    }

    private void LinkProgram() {
        GL.LinkProgram(handle);

        GL.GetProgram(handle, GetProgramParameterName.LinkStatus, out int sucess);
        if(sucess == 0) {
            string infoLog = GL.GetProgramInfoLog(handle);
            Console.WriteLine(infoLog);
        }
    }

    public void Render() {
        GL.UseProgram(handle);
    }

    public void SetBool(string name, bool value) {
        int location = GL.GetUniformLocation(handle, name);
        GL.Uniform1(location, value ? 1 : 0);
    }
}
