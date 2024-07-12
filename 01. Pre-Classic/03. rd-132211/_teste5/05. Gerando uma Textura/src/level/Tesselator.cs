using OpenTK.Graphics.OpenGL4;
using System;

namespace RubyDung.src.level;

public class Tesselator {
    private float[] vertexBuffer = new float[300000];
    private int[] triangleBuffer = new int[600000];
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
        this.setupVAO();
        this.setupVBO();
        this.setupEBO();
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

    public void vertex(float x, float y) {
        this.vertexBuffer[this.vertices * 2 + 0] = x;
        this.vertexBuffer[this.vertices * 2 + 1] = y;

        if(this.hasTexture) {
            this.texCoordBuffer[this.vertices * 2 + 0] = this.u;
            this.texCoordBuffer[this.vertices * 2 + 1] = this.v;
        }

        this.vertices++;

        if(this.vertices % 4 == 0) {
            int _vertex = this.vertices - 4;

            // primeiro Triângulo
            this.triangleBuffer[_vertex * 6 + 0] = 0 + _vertex;
            this.triangleBuffer[_vertex * 6 + 1] = 1 + _vertex;
            this.triangleBuffer[_vertex * 6 + 2] = 2 + _vertex;

            // segundo Triângulo
            this.triangleBuffer[_vertex * 6 + 3] = 0 + _vertex;
            this.triangleBuffer[_vertex * 6 + 4] = 2 + _vertex;
            this.triangleBuffer[_vertex * 6 + 5] = 3 + _vertex;
        }
    }

    public void tex(float u, float v) {
        this.hasTexture = true;

        this.u = u;
        this.v = v;
    }
}
