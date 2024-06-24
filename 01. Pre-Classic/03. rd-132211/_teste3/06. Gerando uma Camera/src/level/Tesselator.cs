using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace RubyDung.src.level;

public class Tesselator {
    private List<Vector3> vertexBuffer = new List<Vector3>();
    private List<int> triangleBuffer = new List<int>();
    private List<Vector2> texCoordBuffer = new List<Vector2>();

    private int VAO; // Vertex Array Object
    private int VBO; // Vertex Buffer Object
    private int EBO; // Element Buffer Object
    private int TBO; // Texture Buffer Object

    public Tesselator() {
        float x0 = -0.5f;
        float y0 = -0.5f;
        float z0 = -0.5f;

        float x1 = 0.5f;
        float y1 = 0.5f;
        float z1 = 0.5f;

        float u0 = 0.0f / 16.0f;
        float u1 = u0 + (1.0f / 16.0f);
        float v0 = ((16.0f - 1.0f) - 0.0f) / 16.0f;
        float v1 = v0 + (1.0f / 16.0f);

        this.vertex(x0, y0, z1);
        this.vertex(x0, y1, z1);
        this.vertex(x1, y1, z1);
        this.vertex(x1, y0, z1);

        this.triangle();

        this.tex(u0, v0);
        this.tex(u0, v1);
        this.tex(u1, v1);
        this.tex(u1, v0);

        GL.GenVertexArrays(1, out this.VAO);
        GL.GenBuffers(1, out this.VBO);
        GL.GenBuffers(1, out this.EBO);
        GL.GenBuffers(1, out this.TBO);

        GL.BindVertexArray(this.VAO);

        GL.BindBuffer(BufferTarget.ArrayBuffer, this.VBO);
        GL.BufferData(BufferTarget.ArrayBuffer, vertexBuffer.Count * Vector3.SizeInBytes, vertexBuffer.ToArray(), BufferUsageHint.StaticDraw);

        GL.BindBuffer(BufferTarget.ElementArrayBuffer, this.EBO);
        GL.BufferData(BufferTarget.ElementArrayBuffer, triangleBuffer.Count * sizeof(int), triangleBuffer.ToArray(), BufferUsageHint.StaticDraw);

        // atributo de posição
        GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 0, 0);
        GL.EnableVertexAttribArray(0);

        GL.BindBuffer(BufferTarget.ArrayBuffer, this.TBO);
        GL.BufferData(BufferTarget.ArrayBuffer, texCoordBuffer.Count * Vector2.SizeInBytes, texCoordBuffer.ToArray(), BufferUsageHint.StaticDraw);

        // atributo de coordenação de textura
        GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 0, 0);
        GL.EnableVertexAttribArray(1);
    }

    public void use() {
        GL.BindVertexArray(this.VAO);
        GL.DrawElements(PrimitiveType.Triangles, 6, DrawElementsType.UnsignedInt, 0);
    }

    public void wireframe_mode(KeyboardState input, Shader shader) {
        if(input.IsKeyDown(Keys.PageUp)) {
            GL.Uniform1(GL.GetUniformLocation(shader.ID, "isWireframe"), 1);
            GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);
        }
        if(input.IsKeyDown(Keys.PageDown)) {
            GL.Uniform1(GL.GetUniformLocation(shader.ID, "isWireframe"), 0);
            GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);
        }
    }

    public void vertex(float x, float y, float z) {
        this.vertexBuffer.Add(new Vector3(x, y, z));
    }

    public void triangle() {
        this.triangleBuffer.Add(0);
        this.triangleBuffer.Add(1);
        this.triangleBuffer.Add(2);

        this.triangleBuffer.Add(0);
        this.triangleBuffer.Add(2);
        this.triangleBuffer.Add(3);
    }

    public void tex(float u, float v) {
        this.texCoordBuffer.Add(new Vector2(u, v));
    }
}
