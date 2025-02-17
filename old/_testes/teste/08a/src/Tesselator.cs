using OpenTK.Graphics.OpenGL4;

namespace ConsoleApp1.src;

public class Tesselator {
    private List<float> vertexBuffer = new List<float>();
    private List<int> indiceBuffer = new List<int>();
    private List<float> texCoordBuffer = new List<float>();

    private int vertices = 0;

    private float u;
    private float v;

    private bool hasTexture = false;

    private int VAO; // Vertex Array Object
    private int VBO; // Vertex Buffer Object
    private int EBO; // Element Buffer Object
    private int TBO; // Texture Buffer Object

    public void flush() {
        // Vertex Array Object
        GL.GenVertexArrays(1, out this.VAO);

        GL.BindVertexArray(this.VAO);

        // Vertex Buffer Object
        GL.GenBuffers(1, out this.VBO);

        GL.BindBuffer(BufferTarget.ArrayBuffer, this.VBO);
        GL.BufferData(BufferTarget.ArrayBuffer, this.vertexBuffer.Count * sizeof(float), this.vertexBuffer.ToArray(), BufferUsageHint.StaticDraw);

        GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 0, 0);
        GL.EnableVertexAttribArray(0);

        // Element Buffer Object
        GL.GenBuffers(1, out this.EBO);

        GL.BindBuffer(BufferTarget.ElementArrayBuffer, this.EBO);
        GL.BufferData(BufferTarget.ElementArrayBuffer, this.indiceBuffer.Count * sizeof(int), this.indiceBuffer.ToArray(), BufferUsageHint.StaticDraw);

        // Texture Buffer Object
        GL.GenBuffers(1, out this.TBO);

        GL.BindBuffer(BufferTarget.ArrayBuffer, this.TBO);
        GL.BufferData(BufferTarget.ArrayBuffer, this.texCoordBuffer.Count * sizeof(float), this.texCoordBuffer.ToArray(), BufferUsageHint.StaticDraw);

        GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 0, 0);
        GL.EnableVertexAttribArray(1);

        // Clear
        GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

        GL.BindVertexArray(0);
    }

    private void clear() {
        this.vertexBuffer.Clear();
        this.indiceBuffer.Clear();
        this.texCoordBuffer.Clear();

        this.vertices = 0;
    }

    public void init() {
        this.clear();

        this.hasTexture = false;
    }

    public void render() {
        GL.BindVertexArray(this.VAO);
        GL.DrawElements(PrimitiveType.Triangles, this.indiceBuffer.Count, DrawElementsType.UnsignedInt, 0);
    }

    public void vertex(float x, float y, float z) {
        this.vertexBuffer.Add(x);
        this.vertexBuffer.Add(y);
        this.vertexBuffer.Add(z);

        if(this.hasTexture) {
            this.texCoordBuffer.Add(this.u);
            this.texCoordBuffer.Add(this.v);
        }

        this.vertices++;

        if(this.vertices % 4 == 0) {
            int indices = this.vertices - 4;

            this.indiceBuffer.Add(0 + indices);
            this.indiceBuffer.Add(1 + indices);
            this.indiceBuffer.Add(2 + indices);

            this.indiceBuffer.Add(0 + indices);
            this.indiceBuffer.Add(2 + indices);
            this.indiceBuffer.Add(3 + indices);
        }
    }

    public void tex(float u, float v) {
        this.hasTexture = true;

        this.u = u;
        this.v = v;
    }
}
