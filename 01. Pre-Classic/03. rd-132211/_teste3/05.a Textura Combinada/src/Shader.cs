﻿using OpenTK.Graphics.OpenGL4;

namespace RubyDung.src;

public class Shader {
    // o ID do programa
    public int ID;

    // construtor lê e constrói o shader
    public Shader(string vertexPath, string fragmentPath) {
        // 1. recupera o código-fonte do vértice/fragmento de filePath
        string vShaderCode = File.ReadAllText(vertexPath);
        string fShaderCode = File.ReadAllText(fragmentPath);

        // 2. compilar shaders
        int vertex, fragment;
        int success;
        string infoLog;

        // shader de vértice
        vertex = GL.CreateShader(ShaderType.VertexShader);
        GL.ShaderSource(vertex, vShaderCode);
        GL.CompileShader(vertex);
        // imprime erros de compilação se houver
        GL.GetShader(vertex, ShaderParameter.CompileStatus, out success);
        if(success == 0) {
            GL.GetShaderInfoLog(vertex, out infoLog);
            Console.WriteLine("ERROR::SHADER::VERTEX::COMPILATION_FAILED\n" + infoLog);
        }

        // shader de fragmento
        fragment = GL.CreateShader(ShaderType.FragmentShader);
        GL.ShaderSource(fragment, fShaderCode);
        GL.CompileShader(fragment);
        // imprime erros de compilação se houver
        GL.GetShader(fragment, ShaderParameter.CompileStatus, out success);
        if(success == 0) {
            GL.GetShaderInfoLog(fragment, out infoLog);
            Console.WriteLine("ERROR::SHADER::FRAGMENT::COMPILATION_FAILED\n" + infoLog);
        }

        // programa de sombreamento
        this.ID = GL.CreateProgram();
        GL.AttachShader(this.ID, vertex);
        GL.AttachShader(this.ID, fragment);
        GL.LinkProgram(this.ID);
        // imprime erros de vinculação, se houver
        GL.GetProgram(this.ID, GetProgramParameterName.LinkStatus, out success);
        if(success == 0) {
            GL.GetProgramInfoLog(this.ID, out infoLog);
            Console.WriteLine("ERROR::SHADER::PROGRAM::LINKING_FAILED\n" + infoLog);
        }

        // exclui os shaders, pois eles estão vinculados ao nosso programa agora e não são mais necessários
        GL.DeleteShader(vertex);
        GL.DeleteShader(fragment);
    }

    // usa/ativa o shader
    public void use() {
        GL.UseProgram(this.ID);
    }

    //funções uniformes utilitárias
    public void setBool(string name, bool value) {
        GL.Uniform1(GL.GetUniformLocation(this.ID, name), value ? 1 : 0);
    }

    public void setInt(string name, int value) {
        GL.Uniform1(GL.GetUniformLocation(this.ID, name), value);
    }

    public void setFloat(string name, float value) {
        GL.Uniform1(GL.GetUniformLocation(this.ID, name), value);
    }
}
