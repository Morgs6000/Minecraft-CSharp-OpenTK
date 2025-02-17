﻿using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using RubyDung.src.level;

namespace RubyDung.src;

public class Window : GameWindow {
    private Shader shader;
    private Texture texture;
    private LevelRenderer levelRenderer;

    private bool wireframeMode = false;

    public Window(GameWindowSettings gws, NativeWindowSettings nws) : base(gws, nws) {
        CenterWindow();
    }

    protected override void OnUpdateFrame(FrameEventArgs args) {
        base.OnUpdateFrame(args);

        if(KeyboardState.IsKeyDown(Keys.Escape)) {
            Close();
        }

        if(KeyboardState.IsKeyDown(Keys.F3) && KeyboardState.IsKeyPressed(Keys.W)) {
            wireframeMode = !wireframeMode;

            shader.GetBool("wireframeMode", wireframeMode);

            GL.PolygonMode(TriangleFace.FrontAndBack, wireframeMode ? PolygonMode.Line : PolygonMode.Fill);

            Console.WriteLine($"O modo Wireframe {(wireframeMode ? "está ligado." : "está desligado.")}");
        }
    }

    protected override void OnLoad() {
        base.OnLoad();

        GL.ClearColor(0.5f, 0.8f, 1.0f, 0.0f);

        shader = new Shader("../../../src/shaders/Vertex.glsl", "../../../src/shaders/Fragment.glsl");
        texture = new Texture("../../../src/textures/terrain.png");

        levelRenderer = new LevelRenderer();

        GL.Enable(EnableCap.DepthTest);
        GL.Enable(EnableCap.CullFace);
    }

    protected override void OnRenderFrame(FrameEventArgs args) {
        base.OnRenderFrame(args);

        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

        shader.Render();
        texture.Render();

        levelRenderer.Render();

        Matrix4 view = Matrix4.Identity;
        view *= Matrix4.CreateRotationY(MathHelper.DegreesToRadians((float)GLFW.GetTime() * 100));
        view *= Matrix4.CreateRotationX(MathHelper.DegreesToRadians(30.0f));
        view *= Matrix4.CreateTranslation(0.0f, 0.0f, -3.0f);
        shader.SetMatrix4("view", view);

        Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45.0f), ClientSize.X / ClientSize.Y, 0.05f, 1000.0f);
        shader.SetMatrix4("projection", projection);

        SwapBuffers();
    }

    protected override void OnFramebufferResize(FramebufferResizeEventArgs e) {
        base.OnFramebufferResize(e);

        GL.Viewport(0, 0, e.Width, e.Height);
    }
}
