using OpenTK.Graphics.OpenGL4;

namespace RubyDung.src;

public class Tesselator {
    private float[] vertexBuffer = new float[300000];
    private int[] triangleBuffer = new int[600000];
    private float[] texCoordBuffer = new float[200000];

    private int vertices = 0;

    private int VAO; // Vertex Array Object
    private int VBO; // Vertex Buffer Object
    private int EBO; // Element Buffer Object
    private int TBO; // Texture Buffer Object

    public Tesselator() {
        this.render();

        // ..:: VAO ::..
        this.setupVAO();

        // ..:: VBO ::..
        this.setupVBO();

        // ..:: EBO ::..
        this.setupEBO();

        // ..:: TBO ::..
        this.setupTBO();

        this.clearBind();
    }

    private void setupVAO() {
        GL.GenVertexArrays(1, out this.VAO);

        GL.BindVertexArray(this.VAO);
    }

    private void setupVBO() {
        GL.GenBuffers(1, out this.VBO);

        GL.BindBuffer(BufferTarget.ArrayBuffer, this.VBO);
        GL.BufferData(BufferTarget.ArrayBuffer, this.vertexBuffer.Length * sizeof(float), this.vertexBuffer, BufferUsageHint.StaticDraw);

        // atributo de posição
        GL.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, 0, 0);
        GL.EnableVertexAttribArray(0);
    }

    private void setupEBO() {
        GL.GenBuffers(1, out this.EBO);

        GL.BindBuffer(BufferTarget.ElementArrayBuffer, this.EBO);
        GL.BufferData(BufferTarget.ElementArrayBuffer, this.triangleBuffer.Length * sizeof(int), this.triangleBuffer, BufferUsageHint.StaticDraw);
    }

    private void setupTBO() {
        GL.GenBuffers(1, out this.TBO);

        GL.BindBuffer(BufferTarget.ArrayBuffer, this.TBO);
        GL.BufferData(BufferTarget.ArrayBuffer, this.texCoordBuffer.Length * sizeof(float), this.texCoordBuffer, BufferUsageHint.StaticDraw);

        // atributo de coordenação de textura
        GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 0, 0);
        GL.EnableVertexAttribArray(1);
    }

    private void clearBind() {
        GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

        GL.BindVertexArray(0);
    }

    public void bind() {
        GL.BindVertexArray(this.VAO);

        GL.DrawElements(PrimitiveType.Triangles, 6, DrawElementsType.UnsignedInt, 0);
    }

    private void render() {
        float x0 = -0.5f;
        float y0 = -0.5f;

        float x1 = 0.5f;
        float y1 = 0.5f;

        float u0 = 0.0f;
        float v0 = 0.0f;

        float u1 = 1.0f;
        float v1 = 1.0f;

        /*
        this.vertex(x0, y0);
        this.vertex(x1, y0);
        this.vertex(x1, y1);
        this.vertex(x0, y1);

        this.triangle();

        this.tex(u0, v0);
        this.tex(u1, v0);
        this.tex(u1, v1);
        this.tex(u0, v1);
        */

        this.tex(u0, v0);
        this.vertex(x0, y0);
        this.tex(u1, v0);
        this.vertex(x1, y0);
        this.tex(u1, v1);
        this.vertex(x1, y1);
        this.tex(u0, v1);
        this.vertex(x0, y1);

        this.triangle();
    }

    private void vertex(float x, float y) {
        this.vertexBuffer[this.vertices * 2 + 0] = x;
        this.vertexBuffer[this.vertices * 2 + 1] = y;

        this.vertices++;
    }

    private void triangle() {
        this.triangleBuffer[0] = 0;
        this.triangleBuffer[1] = 1;
        this.triangleBuffer[2] = 2;

        this.triangleBuffer[3] = 0;
        this.triangleBuffer[4] = 2;
        this.triangleBuffer[5] = 3;
    }

    private void tex(float u, float v) {
        this.texCoordBuffer[this.vertices * 2 + 0] = u;
        this.texCoordBuffer[this.vertices * 2 + 1] = v;
    }
}
