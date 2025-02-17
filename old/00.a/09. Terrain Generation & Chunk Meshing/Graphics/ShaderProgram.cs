using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace openTk_Minecraft_Clone_Tutorial_Series.Graphics {
    internal class ShaderProgram {
        public int ID;

        public ShaderProgram(string vertexShaderFilepath, string fragmentShaderFilepath) {
            // create the shader program
            ID = GL.CreateProgram();

            int vertexShader = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(vertexShader, LoadShaderSource(vertexShaderFilepath));
            GL.CompileShader(vertexShader);

            int fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fragmentShader, LoadShaderSource(fragmentShaderFilepath));
            GL.CompileShader(fragmentShader);

            GL.AttachShader(ID, vertexShader);
            GL.AttachShader(ID, fragmentShader);

            GL.LinkProgram(ID);

            // delete the shaders
            GL.DeleteShader(vertexShader);
            GL.DeleteShader(fragmentShader);
        }

        public void Bind() {
            GL.UseProgram(ID);
        }

        public void UnBind() {
            GL.UseProgram(0);
        }

        public void Delete() {
            GL.DeleteShader(ID);
        }

        public static string LoadShaderSource(string filePath) {
            string shaderSource = "";

            try {
                using(StreamReader reader = new StreamReader("../../../Shaders/" + filePath)) {
                    shaderSource = reader.ReadToEnd();
                }
            }
            catch(Exception e) {
                Console.WriteLine("Failed to load shader source file: " + e.Message);
            }

            return shaderSource;
        }
    }
}
