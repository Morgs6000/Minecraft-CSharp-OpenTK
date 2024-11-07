﻿using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using RubyDung.src.level;

namespace RubyDung.src;

public class Program : GameWindow {
    private int width;
    private int height;

    public Program(GameWindowSettings gws, NativeWindowSettings nws) : base(gws, nws) {
        this.width = this.ClientSize.X;
        this.height = this.ClientSize.Y;
        
        this.CenterWindow();
    }

    protected override void OnFramebufferResize(FramebufferResizeEventArgs e) {
        this.width = e.Width;
        this.height = e.Height;

        this.framebuffer_size_callback(e.Width, e.Height);
    }

    // ..:: Shader ::..
    Shader shader;

    // ..:: Triangle ::..
    Tesselator t = new Tesselator();

    // ..:: Texture ::..
    Texture texture;

    protected override void OnLoad() {
        this.shader = new Shader("vertexShader.glsl", "fragmentShader.glsl");

        Tile.tile.render(this.t);
        this.t.flush();

        this.texture = new Texture("terrain.png");
    }

    protected override void OnRenderFrame(FrameEventArgs args) {
        this.processInput();

        GL.ClearColor(0.5f, 0.8f, 1.0f, 0.0F);
        GL.Clear(ClearBufferMask.ColorBufferBit);

        this.texture.bind();
        this.shader.use();
        this.t.render();

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

            //GL.Uniform1(GL.GetUniformLocation(this.shaderProgram, "isWireframe"), this.isWireframe ? 1 : 0);
            this.shader.setBool("isWireframe", isWireframe);

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
