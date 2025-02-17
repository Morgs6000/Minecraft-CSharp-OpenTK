using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace RubyDung.src;

public class Shader {
    private int vertexShader;
    private int fragmentShader;

    private int shaderProgram;

    public Shader(string vertexFile, string fragmentFile) {
        this.vertexShader = this.shader(ShaderType.VertexShader, vertexFile, "VERTEX");
        this.fragmentShader = this.shader(ShaderType.FragmentShader, fragmentFile, "FRAGMENT");

        this.program();
    }

    private int shader(ShaderType type, string file, string TYPE) {
        int shader = GL.CreateShader(type);

        GL.ShaderSource(shader, this.source(file));
        GL.CompileShader(shader);

        this.checkErros(shader, TYPE);

        return shader;
    }

    private void program() {
        this.shaderProgram = GL.CreateProgram();

        GL.AttachShader(this.shaderProgram, this.vertexShader);
        GL.AttachShader(this.shaderProgram, this.fragmentShader);

        GL.LinkProgram(this.shaderProgram);

        this.checkErros(this.shaderProgram, "PROGRAM");
    }

    public void use() {
        GL.UseProgram(this.shaderProgram);
    }

    private string source(string file) {
        return File.ReadAllText($"../../../src/shaders/{file}");
    }

    private void checkErros(int shader, string TYPE) {
        int success;
        string infoLog;

        if(TYPE != "PROGRAM") {
            GL.GetShader(shader, ShaderParameter.CompileStatus, out success);

            if(success == 0) {
                GL.GetShaderInfoLog(shader, out infoLog);

                Console.WriteLine($"ERROR::SHADER::{TYPE}::COMPILATION_FAILED\n{infoLog}");
            }
        }
        else {
            GL.GetProgram(shader, GetProgramParameterName.LinkStatus, out success);

            if(success == 0) {
                GL.GetShaderInfoLog(shader, out infoLog);

                Console.WriteLine($"ERROR::SHADER::{TYPE}::LINKING_FAILED\n{infoLog}");
            }
        }
    }

    public void setColor(string name, float red, float green, float blue, float alpha) {
        GL.Uniform4(GL.GetUniformLocation(this.shaderProgram, name), red, green, blue, alpha);
    }

    public void setColor(string name, Color4 color) {
        GL.Uniform4(GL.GetUniformLocation(this.shaderProgram, name), color);
    }
}
