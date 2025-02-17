using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace RubyDung.src;

public class Shader {
    private int Handle;

    public Shader(string vertexPath, string fragmentPath) {
        string VertexShaderSource = File.ReadAllText(vertexPath);
        string FragmentShaderSource = File.ReadAllText(fragmentPath);

        var VertexShader = GL.CreateShader(ShaderType.VertexShader);
        GL.ShaderSource(VertexShader, VertexShaderSource);

        var FragmentShader = GL.CreateShader(ShaderType.FragmentShader);
        GL.ShaderSource(FragmentShader, FragmentShaderSource);

        CompileShader(VertexShader);

        CompileShader(FragmentShader);

        Handle = GL.CreateProgram();

        GL.AttachShader(Handle, VertexShader);
        GL.AttachShader(Handle, FragmentShader);

        LinkProgram(Handle);

        GL.DetachShader(Handle, VertexShader);
        GL.DetachShader(Handle, FragmentShader);
        GL.DeleteShader(VertexShader);
        GL.DeleteShader(FragmentShader);
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
        GL.UseProgram(Handle);
    }

    public void GetBool(string name, bool value) {
        var location = GL.GetUniformLocation(Handle, name);
        GL.Uniform1(location, value ? 1 : 0);
    }

    public int GetAttribLocation(string attribName) {
        return GL.GetAttribLocation(Handle, attribName);
    }

    public void SetMatrix4(string name, Matrix4 matrix) {
        int location = GL.GetUniformLocation(Handle, name);
        GL.UniformMatrix4(location, true, ref matrix);
    }
}
