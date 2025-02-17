using OpenTK.Graphics.OpenGL4;

namespace RubyDung.src;

public class Shader {
    private int handle;

    public Shader(string vertexPath, string fragmentPath) {
        string vertexShaderSource = File.ReadAllText(vertexPath);
        string fragmentShaderSource = File.ReadAllText(fragmentPath);

        var vertexShader = GL.CreateShader(ShaderType.VertexShader);
        GL.ShaderSource(vertexShader, vertexShaderSource);

        var fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
        GL.ShaderSource(fragmentShader, fragmentShaderSource);

        CompileShader(vertexShader);
        CompileShader(fragmentShader);

        handle = GL.CreateProgram();

        GL.AttachShader(handle, vertexShader);
        GL.AttachShader(handle, fragmentShader);

        LinkProgram(handle);

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

    private void LinkProgram(int program) {
        GL.LinkProgram(program);

        GL.GetProgram(program, GetProgramParameterName.LinkStatus, out int success);
        if(success == 0) {
            string infoLog = GL.GetProgramInfoLog(program);
            Console.WriteLine(infoLog);
        }
    }

    public void Render() {
        GL.UseProgram(handle);
    }
}
