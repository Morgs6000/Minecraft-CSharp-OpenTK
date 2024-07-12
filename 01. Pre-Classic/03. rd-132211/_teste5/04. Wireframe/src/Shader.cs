using OpenTK.Compute.OpenCL;
using OpenTK.Graphics.OpenGL4;

namespace RubyDung.src;

public class Shader {
    public int shaderProgram;

    private int vertexShader;
    private int fragmentShader;

    public Shader(string vertexPath, string fragmentPath) {
        // sombreador de vértice
        //this.vertex_shader(vertexPath);
        this.vertexShader = this.CompileShader(vertexPath, ShaderType.VertexShader, "VERTEX");

        // sombreador de fragmento
        //this.fragment_shader(fragmentPath);
        this.fragmentShader = this.CompileShader(fragmentPath, ShaderType.FragmentShader, "FRAGMENT");

        // shaders de link
        this.LinkShader();

        this.DeleteShader();
    }

    private int CompileShader(string path, ShaderType type, string typeName) {
        string shaderSource = File.ReadAllText("../../../src/shaders/" + path);

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

        this.CheckCompileErrors(this.shaderProgram, "");
    }

    private void DeleteShader() {
        GL.DeleteShader(this.vertexShader);
        GL.DeleteShader(this.fragmentShader);
    }

    private void CheckCompileErrors(int shader, string type) {
        int success;
        string infoLog;

        if(shader == this.shaderProgram) {
            GL.GetProgram(shader, GetProgramParameterName.LinkStatus, out success);

            if(success == 0) {
                GL.GetProgramInfoLog(shader, out infoLog);

                Console.WriteLine($"ERROR::SHADER::PROGRAM::LINKING_FAILED\n{infoLog}");
            }
        }
        else {
            GL.GetShader(shader, ShaderParameter.CompileStatus, out success);

            if(success == 0) {
                GL.GetShaderInfoLog(shader, out infoLog);

                Console.WriteLine($"ERROR::SHADER::{type}::COMPILATION_FAILED\n{infoLog}");
            }
        }
    }

    public void use() {
        GL.UseProgram(this.shaderProgram);
    }

    public void setBool(string name, bool value) {
        GL.Uniform1(GL.GetUniformLocation(shaderProgram, name), value ? 1 : 0);
    }
}
