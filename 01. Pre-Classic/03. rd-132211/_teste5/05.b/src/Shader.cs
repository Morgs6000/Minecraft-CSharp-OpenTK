using OpenTK.Graphics.OpenGL4;

namespace RubyDung.src;

public class Shader {
    public int shaderProgram;

    private int vertexShader;
    private int fragmentShader;

    public Shader(string vertexPath, string fragmentPath) {
        this.vertexShader = this.CompileShader(vertexPath, ShaderType.VertexShader, "VERTEX");
        this.fragmentShader = this.CompileShader(fragmentPath, ShaderType.FragmentShader, "FRAGMENT");

        this.LinkShader();

        this.DeleteShader();
    }

    private int CompileShader(string path, ShaderType type, string typeName) {
        string shaderSource = File.ReadAllText($"../../../src/shaders/{path}");

        int shader = GL.CreateShader(type);

        GL.ShaderSource(shader, shaderSource);
        GL.CompileShader(shader);

        this.CheckCompileErrors(shader, typeName);

        return shader;
    }

    private void LinkShader() {
        this.shaderProgram = GL.CreateProgram();

        GL.AttachShader(this.shaderProgram, this.vertexShader);
        GL.AttachShader(this.shaderProgram, this.fragmentShader);

        GL.LinkProgram(this.shaderProgram);

        this.CheckCompileErrors(this.shaderProgram, "PROGRAM");
    }

    private void CheckCompileErrors(int shader, string type) {
        int success;
        string infoLog;

        if(type != "PROGRAM") {
            GL.GetShader(shader, ShaderParameter.CompileStatus, out success);

            if(success == 0) {
                GL.GetShaderInfoLog(shader, out infoLog);

                Console.WriteLine($"ERROR::SHADER::{type}::COMPILATION_FAILED\n{infoLog}");
            }
        }
        else {
            GL.GetProgram(shader, GetProgramParameterName.LinkStatus, out success);

            if(success == 0) {
                GL.GetProgramInfoLog(shader, out infoLog);

                Console.WriteLine($"ERROR::SHADER::{type}::LINKING_FAILED\n{infoLog}");
            }
        }
    }

    private void DeleteShader() {
        GL.DeleteShader(this.vertexShader);
        GL.DeleteShader(this.fragmentShader);
    }

    public void use() {
        GL.UseProgram(this.shaderProgram);
    }

    public void setBool(string name, bool value) {
        GL.Uniform1(GL.GetUniformLocation(shaderProgram, name), value ? 1 : 0);
    }
}
