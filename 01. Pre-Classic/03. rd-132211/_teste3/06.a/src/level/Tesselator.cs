using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace RubyDung.src.level;

public class Tesselator {
    private List<Vector3> vertexBuffer = new List<Vector3>();
    private List<int> triangleBuffer = new List<int>();
    private List<Vector2> texCoordBuffer = new List<Vector2>();

    private int vertices;

    private int VAO; // Vertex Array Object
    private int VBO; // Vertex Buffer Object
    private int EBO; // Element Buffer Object
    private int TBO; // Texture Buffer Object

    public Tesselator() {
        Tile.tile.render(this);

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
        GL.DrawElements(PrimitiveType.Triangles, triangleBuffer.Count, DrawElementsType.UnsignedInt, 0);
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
        // first triangle
        triangleBuffer.Add(0 + vertices);
        triangleBuffer.Add(1 + vertices);
        triangleBuffer.Add(2 + vertices);

        // second triangle
        triangleBuffer.Add(0 + vertices);
        triangleBuffer.Add(2 + vertices);
        triangleBuffer.Add(3 + vertices);

        vertices += 4;
    }

    public void tex(float u, float v) {
        this.texCoordBuffer.Add(new Vector2(u, v));
    }
}
