using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace RubyDung;

public class Shader {
    private int program;

    public Shader(string vertexPath, string fragmentPath) {
        string vertexShaderSource = File.ReadAllText(vertexPath);
        string fragmentShaderSource = File.ReadAllText(fragmentPath);

        int vertexShader = GL.CreateShader(ShaderType.VertexShader);
        GL.ShaderSource(vertexShader, vertexShaderSource);

        int fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
        GL.ShaderSource(fragmentShader, fragmentShaderSource);

        CompileShader(vertexShader);
        CompileShader(fragmentShader);

        program = GL.CreateProgram();

        GL.AttachShader(program, vertexShader);
        GL.AttachShader(program, fragmentShader);

        LinkProgram();

        GL.DetachShader(program, vertexShader);
        GL.DetachShader(program, fragmentShader);
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
        GL.LinkProgram(program);

        GL.GetProgram(program, GetProgramParameterName.LinkStatus, out int success);
        if(success == 0) {
            string infoLog = GL.GetProgramInfoLog(program);
            Console.WriteLine(infoLog);
        }
    }

    public void OnRenderFrame() {
        GL.UseProgram(program);
    }

    public int GetAttribLocation(string name) {
        return GL.GetAttribLocation(program, name);
    }

    public void SetBool(string name, bool value) {
        int location = GL.GetUniformLocation(program, name);
        GL.Uniform1(location, value ? 1 : 0);
    }

    public void SetMatrix4(string name, Matrix4 matrix) {
        int location = GL.GetUniformLocation(program, name);
        GL.UniformMatrix4(location, true, ref matrix);
    }

    public void SetColorRGB(string name, float r, float g, float b) {
        int location = GL.GetUniformLocation(program, name);
        GL.Uniform3(location, r, g, b);
    }

    public void SetColorRGBA(string name, float r, float g, float b, float a) {
        int location = GL.GetUniformLocation(program, name);
        GL.Uniform4(location, r, g, b, a);
    }
}
