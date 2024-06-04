using OpenTK.Graphics.OpenGL4;

namespace LearnOpenTK.src {
    internal class Shader {
        public int ID;

        public Shader(string vertexPath, string fragmentPath) {
            string vertexCode = File.ReadAllText("../../../src/Shaders/" + vertexPath);
            string fragmentCode = File.ReadAllText("../../../src/Shaders/" + fragmentPath);
            /*
            StreamReader vShaderFile;
            StreamReader fShaderFile;

            try {
                vShaderFile = new StreamReader(vertexPath);
                fShaderFile = new StreamReader(fragmentPath);
                StringWriter vShaderStrem = new StringWriter();
                StringWriter fShaderStrem = new StringWriter();

                vShaderStrem.Write(vShaderFile.ReadToEnd());
                fShaderStrem.Write(fShaderFile.ReadToEnd());

                vShaderFile.Close();
                fShaderFile.Close();

                vertexCode = vShaderStrem.ToString();
                fragmentCode = fShaderStrem.ToString();
            }
            catch(Exception e) {
                Console.WriteLine("ERROR::SHADER::FILE_NOT_SUCCESFULLY_READ");
            }
            */

            //string vShaderCode = vertexCode;
            //string fShaderCode = fragmentCode;

            int vertex;
            int fragment;
            int success;
            string infoLog;

            // vertex Shader
            vertex = GL.CreateShader(ShaderType.VertexShader);
            //GL.ShaderSource(vertex, vShaderCode);
            GL.ShaderSource(vertex, vertexCode);
            GL.CompileShader(vertex);

            GL.GetShader(vertex, ShaderParameter.CompileStatus, out success);
            if(success == 0) {
                infoLog = GL.GetShaderInfoLog(vertex);
                Console.WriteLine("ERROR::SHADER::VERTEX::COMPILATION_FAILED\n" + infoLog);
            }

            // fragment Shader
            fragment = GL.CreateShader(ShaderType.FragmentShader);
            //GL.ShaderSource(fragment, fShaderCode);
            GL.ShaderSource(fragment, fragmentCode);
            GL.CompileShader(fragment);

            GL.GetShader(fragment, ShaderParameter.CompileStatus, out success);
            if(success == 0) {
                infoLog = GL.GetShaderInfoLog(fragment);
                Console.WriteLine("ERROR::SHADER::VERTEX::COMPILATION_FAILED\n" + infoLog);
            }

            // shader Program
            ID = GL.CreateProgram();
            GL.AttachShader(ID, vertex);
            GL.AttachShader(ID, fragment);
            GL.LinkProgram(ID);

            GL.GetProgram(ID, GetProgramParameterName.LinkStatus, out success);
            if(success == 0) {
                infoLog = GL.GetProgramInfoLog(ID);
                Console.WriteLine("ERROR::SHADER::PROGRAM::LINKING_FAILED\n" + infoLog);
            }

            GL.DeleteShader(vertex);
            GL.DeleteShader(fragment);
        }

        public void use() {
            GL.UseProgram(ID);
        }

        public void setBool(string name, bool value) {
            GL.Uniform1(GL.GetUniformLocation(ID, name), value ? 1 : 0);
        }

        public void setInt(string name, int value) {
            GL.Uniform1(GL.GetUniformLocation(ID, name), value);
        }

        public void setFloat(string name, float value) {
            GL.Uniform1(GL.GetUniformLocation(ID, name), value);
        }
    }
}
