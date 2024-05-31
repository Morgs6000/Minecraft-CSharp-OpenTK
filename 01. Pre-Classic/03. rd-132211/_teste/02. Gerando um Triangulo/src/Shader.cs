using OpenTK.Graphics.OpenGL4;

namespace RubyDung.src {
    internal class Shader {
        public int ID;

        public Shader(string vertexPath, string fragmentPath) {
            int success;
            string infoLog;

            // ..:: Vertex Shader ::..

            string vertexCode = File.ReadAllText("../../../src/Shaders/" + vertexPath);

            int vertex;
            vertex = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(vertex, vertexCode);
            GL.CompileShader(vertex);

            GL.GetShader(vertex, ShaderParameter.CompileStatus, out success);

            if(success == 0) {
                infoLog = GL.GetShaderInfoLog(vertex);
                Console.WriteLine("ERROR::SHADER::VERTEX::COMPILATION_FAILED\n" + infoLog);
            }

            // ..:: Fragment Shader ::..

            string fragmentCode = File.ReadAllText("../../../src/Shaders/" + fragmentPath);

            int fragment;
            fragment = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fragment, fragmentCode);
            GL.CompileShader(fragment);

            GL.GetShader(fragment, ShaderParameter.CompileStatus, out success);

            if(success == 0) {
                infoLog = GL.GetShaderInfoLog(fragment);
                Console.WriteLine("ERROR::SHADER::FRAGMENT::COMPILATION_FAILED\n" + infoLog);
            }

            // ..:: Shader Program ::..

            this.ID = GL.CreateProgram();

            GL.AttachShader(this.ID, vertex);
            GL.AttachShader(this.ID, fragment);
            GL.LinkProgram(this.ID);

            GL.GetProgram(this.ID, GetProgramParameterName.LinkStatus, out success);

            if(success == 0) {
                infoLog = GL.GetProgramInfoLog(this.ID);
                Console.WriteLine("ERROR::SHADER::PROGRAM::LINKING_FAILED\n" + infoLog);
            }

            GL.DeleteShader(vertex);
            GL.DeleteShader(fragment);
        }

        public void use() {
            GL.UseProgram(this.ID);
        }
    }
}
