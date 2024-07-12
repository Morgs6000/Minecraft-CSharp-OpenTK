using OpenTK.Compute.OpenCL;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System.Reflection;

namespace RubyDung.src;

public class Shader {
    public int program;

    private int shader_vertex;
    private int shader_fragment;

    public Shader(string vertexPath, string fragmentPath) {
        // sombreador de vértice
        this.shader_vertex = this.CompileShader(vertexPath, ShaderType.VertexShader, "VERTEX");

        // sombreador de fragmento
        this.shader_fragment = this.CompileShader(fragmentPath, ShaderType.FragmentShader, "FRAGMENT");

        // shaders de link
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
        this.program = GL.CreateProgram();

        GL.AttachShader(this.program, this.shader_vertex);
        GL.AttachShader(this.program, this.shader_fragment);

        GL.LinkProgram(this.program);

        this.CheckCompileErrors(this.program, "PROGRAM");
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
        GL.DeleteShader(this.shader_vertex);
        GL.DeleteShader(this.shader_fragment);
    }

    public void use() {
        GL.UseProgram(this.program);
    }

    public void setBool(string name, bool value) {
        GL.Uniform1(GL.GetUniformLocation(this.program, name), value ? 1 : 0);
    }

    public void setMatrix4(string name, Matrix4 matrix) {
        GL.UniformMatrix4(GL.GetUniformLocation(this.program, name), false, ref matrix);
    }
}
