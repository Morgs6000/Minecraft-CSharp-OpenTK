﻿using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace RubyDung.src;

public class Program : GameWindow {
    private int width;
    private int height;

    private string vertexShaderSource = File.ReadAllText("../../../src/vertexShader.glsl");
    private string fragmentShaderSource = File.ReadAllText("../../../src/fragmentShader.glsl");

    public Program(GameWindowSettings gws, NativeWindowSettings nws) : base(gws, nws) {
        this.width = this.ClientSize.X;
        this.height = this.ClientSize.Y;
        
        this.CenterWindow();
    }

    protected override void OnFramebufferResize(FramebufferResizeEventArgs e) {
        this.width = e.Width;
        this.height = e.Height;

        this.framebuffer_size_callback(this.width, this.height);
    }

    // Variáveis para as cores
    private Vector3 defaultColor = new Vector3(1.0f, 1.0f, 1.0f); // Branco
    private Vector3 hoverColor = new Vector3(1.0f, 0.0f, 0.0f);   // Vermelho
    private Vector3 currentColor;

    // ..:: Shader ::..
    private int shaderProgram;

    private void Shader() {
        int success;
        string infoLog;

        // vertex shader
        int vertexShader = GL.CreateShader(ShaderType.VertexShader);
        GL.ShaderSource(vertexShader, this.vertexShaderSource);
        GL.CompileShader(vertexShader);

        GL.GetShader(vertexShader, ShaderParameter.CompileStatus, out success);
        if(success == 0) {
            GL.GetShaderInfoLog(vertexShader, out infoLog);
            Console.WriteLine($"ERROR::SHADER::VERTEX::COMPILATION_FAILED\n{infoLog}");
        }

        // fragment sahder
        int fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
        GL.ShaderSource(fragmentShader, this.fragmentShaderSource);
        GL.CompileShader(fragmentShader);

        GL.GetShader(fragmentShader, ShaderParameter.CompileStatus, out success);
        if(success == 0) {
            GL.GetShaderInfoLog(fragmentShader, out infoLog);
            Console.WriteLine($"ERROR::SHADER::FRAGMENT::COMPILATION_FAILED\n{infoLog}");
        }

        // link shaders
        this.shaderProgram = GL.CreateProgram();
        GL.AttachShader(this.shaderProgram, vertexShader);
        GL.AttachShader(this.shaderProgram, fragmentShader);
        GL.LinkProgram(this.shaderProgram);

        GL.GetProgram(this.shaderProgram, GetProgramParameterName.LinkStatus, out success);
        if(success == 0) {
            GL.GetProgramInfoLog(this.shaderProgram, out infoLog);
            Console.WriteLine($"ERROR::SHADER::PROGRAM::LINKING_FAILED\n{infoLog}");
        }

        GL.DeleteShader(vertexShader);
        GL.DeleteShader(fragmentShader);
    }

    // ..:: Triangle ::..
    //float[] vertices = {
    //    -0.5f, -0.5f,  // bottom left  // 0
    //     0.5f, -0.5f,  // bottom right // 1
    //     0.5f,  0.5f,  // top right    // 2

    //    -0.5f, -0.5f,  // bottom left  // 0
    //     0.5f,  0.5f,  // top right    // 2
    //    -0.5f,  0.5f   // top left     // 3
    //};
    private float[] vertices = {
        -0.5f, -0.5f,  // bottom left  // 0
         0.5f, -0.5f,  // bottom right // 1
         0.5f,  0.5f,  // top right    // 2
        -0.5f,  0.5f   // top left     // 3
    };

    private int[] indices = {
        0, 1, 2, // first Triangle
        0, 2, 3  // second Triangle
    };

    private int VAO; // Vertex Array Object
    private int VBO; // Vertex Buffer Object
    private int EBO; // Element Buffer Object

    private void Triangle() {
        // Vertex Array Object
        GL.GenVertexArrays(1, out this.VAO);

        GL.BindVertexArray(this.VAO);

        // Vertex Buffer Object
        GL.GenBuffers(1, out this.VBO);

        GL.BindBuffer(BufferTarget.ArrayBuffer, this.VBO);
        GL.BufferData(BufferTarget.ArrayBuffer, this.vertices.Length * sizeof(float), this.vertices, BufferUsageHint.StaticDraw);

        GL.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, 0, 0);
        GL.EnableVertexAttribArray(0);

        // Element Buffer Object
        GL.GenBuffers(1, out this.EBO);

        GL.BindBuffer(BufferTarget.ElementArrayBuffer, this.EBO);
        GL.BufferData(BufferTarget.ElementArrayBuffer, this.indices.Length * sizeof(int), this.indices, BufferUsageHint.StaticDraw);

        // Clear
        GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

        GL.BindVertexArray(0);

        // Wireframe
        //GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);
    }

    protected override void OnLoad() {
        this.Shader();
        this.Triangle();

        this.currentColor = this.defaultColor;
    }

    private bool isDrag = false;
    private Vector2 rectPosition = Vector2.Zero; // Posição inicial do retângulo
    private Vector2 dragOffset = Vector2.Zero;   // Deslocamento entre o mouse e o centro do retângulo

    protected override void OnUpdateFrame(FrameEventArgs args) {
        var mouseX = this.MouseState.Position.X / this.width * 2 - 1;
        var mouseY = this.MouseState.Position.Y / this.height * -2 + 1;
        Vector2 mousePos = new Vector2(mouseX, mouseY);

        bool isInside = mouseX >= this.rectPosition.X - 0.5f && mouseX <= this.rectPosition.X + 0.5f &&
                        mouseY >= this.rectPosition.Y - 0.5f && mouseY <= this.rectPosition.Y + 0.5f;

        this.currentColor = isInside ? this.hoverColor : this.defaultColor;

        // Verifica clique dentro do retângulo
        if(!this.isDrag) {
            // Início do arrasto
            if(isInside && this.MouseState.IsButtonPressed(MouseButton.Left)) {
                Console.WriteLine("Começo do arrasto!");
                this.isDrag = true;
                this.dragOffset = mousePos - this.rectPosition; // Calcula o deslocamento inicial
            }
        }
        else {
            // Durante o arrasto
            if(this.MouseState.IsButtonDown(MouseButton.Left)) {
                Console.WriteLine("Arrastando!");
                this.rectPosition = mousePos - this.dragOffset; // Atualiza a posição do retângulo com o mouse
            }

            // Fim do arrasto
            if(this.MouseState.IsButtonReleased(MouseButton.Left)) {
                Console.WriteLine("Fim do arrasto!");
                this.isDrag = false;
            }
        }

        // Atualiza a posição do retângulo no shader
        GL.UseProgram(this.shaderProgram);

        int colorLocation = GL.GetUniformLocation(this.shaderProgram, "color");
        GL.Uniform3(colorLocation, this.currentColor);

        int positionLocation = GL.GetUniformLocation(this.shaderProgram, "rectPosition");
        GL.Uniform2(positionLocation, this.rectPosition);
    }

    /*
    public void OnBeginDrag() {
        Console.WriteLine("Começo do arrasto!");
    }

    public void OnDrag() {
        Console.WriteLine("Arrastando!");
    }

    public void OnEndDrag() {
        Console.WriteLine("Fim do arrasto!");
    }
    */

    protected override void OnRenderFrame(FrameEventArgs args) {
        this.processInput();

        GL.ClearColor(0.5f, 0.8f, 1.0f, 0.0F);
        GL.Clear(ClearBufferMask.ColorBufferBit);

        GL.UseProgram(this.shaderProgram);
        GL.BindVertexArray(this.VAO);

        //GL.DrawArrays(PrimitiveType.Triangles, 0, 3);
        //GL.DrawArrays(PrimitiveType.Triangles, 0, 6);
        GL.DrawElements(PrimitiveType.Triangles, 6, DrawElementsType.UnsignedInt, 0);

        this.SwapBuffers();
    }

    private bool isWireframe = false;

    private void processInput() {
        // CLose Window
        if(this.KeyboardState.IsKeyDown(Keys.Escape)) {
            this.Close();
        }

        // Wireframe
        if(this.KeyboardState.IsKeyDown(Keys.F3) && this.KeyboardState.IsKeyPressed(Keys.W)) {
            this.isWireframe = !this.isWireframe;

            GL.Uniform1(GL.GetUniformLocation(this.shaderProgram, "isWireframe"), this.isWireframe ? 1 : 0);

            GL.PolygonMode(MaterialFace.FrontAndBack, this.isWireframe ? PolygonMode.Line : PolygonMode.Fill);
        }
    }

    private void framebuffer_size_callback(int width, int height) {
        GL.Viewport(0, 0, width, height);
    }

    private static void Main(string[] args) {
        GameWindowSettings gws = GameWindowSettings.Default;

        NativeWindowSettings nws = NativeWindowSettings.Default;
        nws.ClientSize = (1024, 768);
        nws.Title = "Game";

        new Program(gws, nws).Run();
    }
}
