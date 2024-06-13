using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace Breakout;

// General purpose shader object. Compiles from file, generates
// compile/link-time error messages and hosts several utility 
// functions for easy management.
public class Shader {
    // state
    public int ID;

    // constructor
    public Shader() {

    }

    // sets the current shader as active
    public Shader Use() {
        GL.UseProgram(this.ID);
        return this;
    }

    // compiles the shader from given source code
    public void Compile(string vertexSource, string fragmentSource, string geometrySource = null) { // note: geometry source code is optional
        int sVertex;
        int sFragment;
        int gShader;

        // vertex Shader
        sVertex = GL.CreateShader(ShaderType.VertexShader);
        GL.ShaderSource(sVertex, vertexSource);
        GL.CompileShader(sVertex);
        checkCompileErrors(sVertex, "VERTEX");

        // fragment Shader
        sFragment = GL.CreateShader(ShaderType.FragmentShader);
        GL.ShaderSource(sFragment, fragmentSource);
        GL.CompileShader(sFragment);
        checkCompileErrors(sFragment, "FRAGMENT");

        // if geometry shader source code is given, also compile geometry shader
        //if(geometrySource != null) {
        //    gShader = GL.CreateShader(ShaderType.GeometryShader);
        //    GL.ShaderSource(gShader, geometrySource);
        //    GL.CompileShader(gShader);
        //    checkCompileErrors(gShader, "GEOMETRY");
        //}

        // shader program
        this.ID = GL.CreateProgram();
        GL.AttachShader(this.ID, sVertex);
        GL.AttachShader(this.ID, sFragment);

        //if(geometrySource != null) {
        //    GL.AttachShader(this.ID, gShader);
        //}

        GL.LinkProgram(this.ID);
        checkCompileErrors(this.ID, "PROGRAM");

        // delete the shaders as they're linked into our program now and no longer necessary
        GL.DeleteShader(sVertex);
        GL.DeleteShader(sFragment);

        //if(geometrySource != null) {
        //    GL.DeleteShader(gShader);
        //}
        if(geometrySource != null) {
            gShader = GL.CreateShader(ShaderType.GeometryShader);
            GL.ShaderSource(gShader, geometrySource);
            GL.CompileShader(gShader);
            checkCompileErrors(gShader, "GEOMETRY");

            GL.AttachShader(this.ID, gShader);

            GL.DeleteShader(gShader);
        }
    }

        // utility functions
        public void SetFloat(string name, float value, bool useShader = false) {
        if(useShader) {
            this.Use();
        }

        GL.Uniform1(GL.GetUniformLocation(this.ID, name), value);
    }

    public void SetInteger(string name, int value, bool useShader = false) {
        if(useShader) {
            this.Use();
        }

        GL.Uniform1(GL.GetUniformLocation(this.ID, name), value);
    }

    public void SetVector2f(string name, float x, float y, bool useShader = false) {
        if(useShader) {
            this.Use();
        }

        GL.Uniform2(GL.GetUniformLocation(this.ID, name), x, y);
    }

    public void SetVector2f(string name, Vector2 value, bool useShader = false) {
        if(useShader) {
            this.Use();
        }

        GL.Uniform2(GL.GetUniformLocation(this.ID, name), value.X, value.Y);
    }

    public void SetVector3f(string name, float x, float y, float z, bool useShader = false) {
        if(useShader) {
            this.Use();
        }

        GL.Uniform3(GL.GetUniformLocation(this.ID, name), x, y, z);
    }

    public void SetVector3f(string name, Vector3 value, bool useShader = false) {
        if(useShader) {
            this.Use();
        }

        GL.Uniform3(GL.GetUniformLocation(this.ID, name), value.X, value.Y, value.Z);
    }

    public void SetVector4f(string name, float x, float y, float z, float w, bool useShader = false) {
        if(useShader) {
            this.Use();
        }

        GL.Uniform4(GL.GetUniformLocation(this.ID, name), x, y, z, w);
    }

    public void SetVector4f(string name, Vector4 value, bool useShader = false) {
        if(useShader) {
            this.Use();
        }

        GL.Uniform4(GL.GetUniformLocation(this.ID, name), value.X, value.Y, value.Z, value.W);
    }

    public void SetMatrix4(string name, Matrix4 matrix, bool useShader = false) {
        if(useShader) {
            this.Use();
        }

        GL.UniformMatrix4(GL.GetUniformLocation(this.ID, name), false, ref matrix);
    }

    // checks if compilation or linking failed and if so, print the error logs
    private void checkCompileErrors(int obj, string type) {
        int success;
        string infoLog;

        if(type != "PROGRAM") {
            GL.GetShader(obj, ShaderParameter.CompileStatus, out success);

            if(success == 0) {
                infoLog = GL.GetShaderInfoLog(obj);
                Console.WriteLine("| ERROR::SHADER: Compile-time error: Type: " + type + "\n" + infoLog + "\n -- --------------------------------------------------- -- ");
            }
        }
        else {
            GL.GetProgram(obj, GetProgramParameterName.LinkStatus, out success);

            if(success == 0) {
                infoLog = GL.GetProgramInfoLog(obj);
                Console.WriteLine("| ERROR::Shader: Link-time error: Type: " + type + "\n" + infoLog + "\n -- --------------------------------------------------- -- ");
            }
        }
    }
}
