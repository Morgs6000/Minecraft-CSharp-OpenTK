using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace ConsoleApp1.src;

public class Shader {
    public int handle;

    public Shader(string vertexPath, string fragmentPath) {
        int success;

        // vertex shader
        string vertexShaderSource = File.ReadAllText($"../../../src/shaders/{vertexPath}");
        int vertexShader = GL.CreateShader(ShaderType.VertexShader);
        GL.ShaderSource(vertexShader, vertexShaderSource);
        GL.CompileShader(vertexShader);

        GL.GetShader(vertexShader, ShaderParameter.CompileStatus, out success);
        if(success == 0) {
            string infoLog = GL.GetShaderInfoLog(vertexShader);
            Console.WriteLine(infoLog);
        }

        // fragment shader
        string fragmentShaderSource = File.ReadAllText($"../../../src/shaders/{fragmentPath}");
        int fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
        GL.ShaderSource(fragmentShader, fragmentShaderSource);
        GL.CompileShader(fragmentShader);

        GL.GetShader(fragmentShader, ShaderParameter.CompileStatus, out success);
        if(success == 0) {
            string infoLog = GL.GetShaderInfoLog(fragmentShader);
            Console.WriteLine(infoLog);
        }

        // link shader
        this.handle = GL.CreateProgram();

        GL.AttachShader(this.handle, vertexShader);
        GL.AttachShader(this.handle, fragmentShader);

        GL.LinkProgram(this.handle);

        GL.GetProgram(this.handle, GetProgramParameterName.LinkStatus, out success);
        if(success == 0) {
            string infoLog = GL.GetProgramInfoLog(this.handle);
            Console.WriteLine(infoLog);
        }

        // delete shader
        GL.DetachShader(this.handle, vertexShader);
        GL.DetachShader(this.handle, fragmentShader);
        GL.DeleteShader(vertexShader);
        GL.DeleteShader(fragmentShader);
    }

    public void Use() {
        GL.UseProgram(this.handle);
    }

    public void SetBool(string name, bool data) {
        GL.Uniform1(GL.GetUniformLocation(this.handle, name), data ? 1 : 0);
    }

    public void SetMatrix4(string name, Matrix4 data) {
        GL.UniformMatrix4(GL.GetUniformLocation(this.handle, name), true, ref data);
    }
}
