using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace RubyDung.src.level;

public class Tesselator {
    //private float[] vertexBuffer = new float[300000];
    private List<float> vertexBuffer = new List<float>();
    //private int[] indiceBuffer = new int[600000];
    private List<int> indiceBuffer = new List<int>();
    //private float[] texCoordBuffer = new float[200000];
    private List<float> texCoordBuffer = new List<float>();

    private int vertices = 0;
    private int indices = 0;
    private int texCoords = 0;

    private int VAO; // Vertex Array Object
    private int VBO; // Vertex Buffer Object
    private int EBO; // Element Buffer Object
    private int TBO; // Texture Buffer Object

    public void flush() {
        this.setVAO();
        this.setVBO();
        this.setEBO();
        this.setTBO();

        this.clearBind();

        //GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);
    }

    private void setVAO() {
        GL.GenVertexArrays(1, out this.VAO);

        GL.BindVertexArray(this.VAO);
    }

    private void setVBO() {
        GL.GenBuffers(1, out this.VBO);

        GL.BindBuffer(BufferTarget.ArrayBuffer, this.VBO);
        GL.BufferData(BufferTarget.ArrayBuffer, this.vertexBuffer.Count * sizeof(float), this.vertexBuffer.ToArray(), BufferUsageHint.StaticDraw);

        GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 0, 0);
        GL.EnableVertexAttribArray(0);
    }

    private void setEBO() {
        GL.GenBuffers(1, out this.EBO);

        GL.BindBuffer(BufferTarget.ElementArrayBuffer, this.EBO);
        GL.BufferData(BufferTarget.ElementArrayBuffer, this.indiceBuffer.Count * sizeof(int), this.indiceBuffer.ToArray(), BufferUsageHint.StaticDraw);
    }

    private void setTBO() {
        GL.GenBuffers(1, out this.TBO);

        GL.BindBuffer(BufferTarget.ArrayBuffer, this.TBO);
        GL.BufferData(BufferTarget.ArrayBuffer, this.texCoordBuffer.Count * sizeof(float), this.texCoordBuffer.ToArray(), BufferUsageHint.StaticDraw);

        GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 0, 0);
        GL.EnableVertexAttribArray(1);
    }

    private void clearBind() {
        // Clear VBO
        GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

        // Clear VAO
        GL.BindVertexArray(0);
    }

    public void bind() {
        GL.BindVertexArray(this.VAO);

        //GL.DrawArrays(PrimitiveType.Triangles, 0, 3);
        //GL.DrawArrays(PrimitiveType.Triangles, 0, 6);
        GL.DrawElements(PrimitiveType.Triangles, this.indiceBuffer.Count, DrawElementsType.UnsignedInt, 0);
    }

    public void vertex(float x, float y, float z) {
        //this.vertexBuffer[this.vertices * 3 + 0] = x;
        this.vertexBuffer.Add(x);
        //this.vertexBuffer[this.vertices * 3 + 1] = y;
        this.vertexBuffer.Add(y);
        //this.vertexBuffer[this.vertices * 3 + 2] = z;
        this.vertexBuffer.Add(z);

        this.vertices++;
    }

    public void indice(int i) {
        //this.indiceBuffer[this.indices * 1 + 0] = i + this.vertices - 4;
        this.indiceBuffer.Add(i + this.vertices - 4);

        this.indices++;
    }

    public void tex(float u, float v) {
        //this.texCoordBuffer[this.texCoords * 2 + 0] = u;
        this.texCoordBuffer.Add(u);
        //this.texCoordBuffer[this.texCoords * 2 + 1] = v;
        this.texCoordBuffer.Add(v);

        this.texCoords++;
    }
}
