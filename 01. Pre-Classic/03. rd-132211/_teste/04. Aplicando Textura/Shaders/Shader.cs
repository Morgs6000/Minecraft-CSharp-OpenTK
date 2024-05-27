using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL4;

namespace com.Shaders {
    internal class Shader {
        int Handle;

        public Shader(string vertexPath, string fragmentPath) {
            //public Shader() {
            string path = "../../../Shaders/";

            //*
            string VertexShaderSource = File.ReadAllText(path + vertexPath);
            string FragmentShaderSource = File.ReadAllText(path + fragmentPath);
            //*/

            /*
            string VertexShaderSource = @"
                #version 330 core
                layout(location = 0) in vec3 aPosition;

                void main() {
                    gl_Position = vec4(aPosition, 1.0);
                }
            ";
            string FragmentShaderSource = @"
                #version 330 core
                out vec4 FragColor;

                void main() {
                    FragColor = vec4(1.0f, 0.5f, 0.2f, 1.0f);
                }
            ";
            //*/

            var VertexShader = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(VertexShader, VertexShaderSource);

            var FragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(FragmentShader, FragmentShaderSource);

            GL.CompileShader(VertexShader);

            GL.GetShader(VertexShader, ShaderParameter.CompileStatus, out int successVert);
            if(successVert == 0) {
                string infoLog = GL.GetShaderInfoLog(VertexShader);
                Console.WriteLine($"Erro de compilação no Vertex Shader:\n{infoLog}");
            }

            GL.CompileShader(FragmentShader);

            GL.GetShader(FragmentShader, ShaderParameter.CompileStatus, out int successFrag);
            if(successFrag == 0) {
                string infoLog = GL.GetShaderInfoLog(FragmentShader);
                Console.WriteLine($"Erro de compilação no Fragment Shader:\n{infoLog}");
            }

            Handle = GL.CreateProgram();

            GL.AttachShader(Handle, VertexShader);
            GL.AttachShader(Handle, FragmentShader);

            GL.LinkProgram(Handle);

            GL.GetProgram(Handle, GetProgramParameterName.LinkStatus, out int success);
            if(success == 0) {
                string infoLog = GL.GetProgramInfoLog(Handle);
                Console.WriteLine($"Erro de linkagem do programa:\n{infoLog}");
            }

            //*
            GL.DetachShader(Handle, VertexShader);
            GL.DetachShader(Handle, FragmentShader);
            GL.DeleteShader(FragmentShader);
            GL.DeleteShader(VertexShader);
            //*/
        }

        public void Use() {
            GL.UseProgram(Handle);
        }

        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing) {
            if(!disposedValue) {
                GL.DeleteProgram(Handle);

                disposedValue = true;
            }
        }

        ~Shader() {
            if(disposedValue == false) {
                Console.WriteLine("Vazamento de recursos da GPU! Você se esqueceu de chamar Dispose()?");
            }
        }

        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
