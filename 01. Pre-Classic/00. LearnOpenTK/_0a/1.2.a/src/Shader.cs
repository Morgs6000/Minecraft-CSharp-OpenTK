using OpenTK.Graphics.OpenGL4;

namespace LearnOpenTK.src {
    internal class Shader {
        int shaderProgram;

        public Shader(string vertexPath, string fragmentPath) {
            // ..:: Vertex Shader ::..

            //string vertexPath = "../../../src/Shaders/shader.vert";

            string vertexShaderSource = File.ReadAllText("../../../src/Shaders/" + vertexPath);

            int vertexShader;
            vertexShader = GL.CreateShader(ShaderType.VertexShader);

            GL.ShaderSource(vertexShader, vertexShaderSource);
            GL.CompileShader(vertexShader);

            // ..:: Fragment Shader ::..

            //string fragmentPath = "../../../src/Shaders/shader.frag";

            string fragmentShaderSource = File.ReadAllText("../../../src/Shaders/" + fragmentPath);

            int fragmentShader;
            fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fragmentShader, fragmentShaderSource);
            GL.CompileShader(fragmentShader);

            // ..:: Shader Program ::..

            shaderProgram = GL.CreateProgram();

            GL.AttachShader(shaderProgram, vertexShader);
            GL.AttachShader(shaderProgram, fragmentShader);
            GL.LinkProgram(shaderProgram);

            GL.DeleteShader(vertexShader);
            GL.DeleteShader(fragmentShader);
        }

        public void Use() {
            GL.UseProgram(shaderProgram);
        }
    }
}
