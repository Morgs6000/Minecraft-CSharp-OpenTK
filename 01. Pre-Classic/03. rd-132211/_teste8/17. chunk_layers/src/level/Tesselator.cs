using OpenTK.Graphics.OpenGL4;

namespace RubyDung.src.level;

public class Tesselator {
    private float[] vertexBuffer = new float[300000];
    private int[] indiceBuffer = new int[600000];
    private float[] texCoordBuffer = new float[200000];

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
        GL.GenVertexArrays(1, out VAO);

        GL.BindVertexArray(VAO);

        // Vertex Buffer Object
        GL.GenBuffers(1, out VBO);

        GL.BindBuffer(BufferTarget.ArrayBuffer, VBO);
        GL.BufferData(BufferTarget.ArrayBuffer, vertexBuffer.Length * sizeof(float), vertexBuffer, BufferUsageHint.StaticDraw);

        GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 0, 0);
        GL.EnableVertexAttribArray(0);

        // Element Buffer Object
        GL.GenBuffers(1, out EBO);

        GL.BindBuffer(BufferTarget.ElementArrayBuffer, EBO);
        GL.BufferData(BufferTarget.ElementArrayBuffer, indiceBuffer.Length * sizeof(int), indiceBuffer, BufferUsageHint.StaticDraw);

        // Texture Buffer Object
        GL.GenBuffers(1, out TBO);

        GL.BindBuffer(BufferTarget.ArrayBuffer, TBO);
        GL.BufferData(BufferTarget.ArrayBuffer, texCoordBuffer.Length * sizeof(float), texCoordBuffer, BufferUsageHint.StaticDraw);

        GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 0, 0);
        GL.EnableVertexAttribArray(1);

        // Clear
        GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

        GL.BindVertexArray(0);
    }

    public void render() {
        GL.BindVertexArray(VAO);
        GL.DrawElements(PrimitiveType.Triangles, this.indiceBuffer.Length, DrawElementsType.UnsignedInt, 0);
    }

    public void vertex(float x, float y, float z) {
        this.vertexBuffer[this.vertices * 3 + 0] = x;
        this.vertexBuffer[this.vertices * 3 + 1] = y;
        this.vertexBuffer[this.vertices * 3 + 2] = z;

        if(this.hasTexture) {
            this.texCoordBuffer[this.vertices * 2 + 0] = this.u;
            this.texCoordBuffer[this.vertices * 2 + 1] = this.v;
        }

        this.vertices++;

        if(this.vertices % 4 == 0) {
            int indices = this.vertices - 4;

            this.indiceBuffer[indices * 6 + 0] = 0 + indices;
            this.indiceBuffer[indices * 6 + 1] = 1 + indices;
            this.indiceBuffer[indices * 6 + 2] = 2 + indices;

            this.indiceBuffer[indices * 6 + 3] = 0 + indices;
            this.indiceBuffer[indices * 6 + 4] = 2 + indices;
            this.indiceBuffer[indices * 6 + 5] = 3 + indices;
        }
    }

    public void tex(float u, float v) {
        this.hasTexture = true;

        this.u = u;
        this.v = v;
    }
}
