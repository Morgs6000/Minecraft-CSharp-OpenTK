using OpenTK.Graphics.OpenGL4;
using System;

namespace RubyDung.src.level;

public class Tesselator {
    private float[] vertexBuffer = new float[300000];
    private int[] triangleBuffer = new int[600000];

    private int vertices = 0;

    private int VAO; // Vertex Array Object
    private int VBO; // Vertex Buffer Object
    private int EBO; // Element Buffer Object


    public void flush() {
        setupVAO();
        setupVBO();
        setupEBO();

        clearBind();
    }

    private void setupVAO() {
        GL.GenVertexArrays(1, out VAO);

        GL.BindVertexArray(VAO);
    }

    private void setupVBO() {
        GL.GenBuffers(1, out VBO);

        GL.BindBuffer(BufferTarget.ArrayBuffer, VBO);
        GL.BufferData(BufferTarget.ArrayBuffer, vertexBuffer.Length * sizeof(float), vertexBuffer, BufferUsageHint.StaticDraw);

        GL.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, 0, 0);
        GL.EnableVertexAttribArray(0);
    }

    private void setupEBO() {
        GL.GenBuffers(1, out EBO);

        GL.BindBuffer(BufferTarget.ElementArrayBuffer, EBO);
        GL.BufferData(BufferTarget.ElementArrayBuffer, triangleBuffer.Length * sizeof(int), triangleBuffer, BufferUsageHint.StaticDraw);
    }

    private void clearBind() {
        GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        GL.BindVertexArray(0);
    }

    public void bind() {
        GL.BindVertexArray(VAO);
        GL.DrawElements(PrimitiveType.Triangles, 6, DrawElementsType.UnsignedInt, 0);
    }

    public void vertex(float x, float y) {
        vertexBuffer[vertices * 2 + 0] = x;
        vertexBuffer[vertices * 2 + 1] = y;

        vertices++;

        if(vertices % 4 == 0) {
            int _vertex = vertices - 4;

            // primeiro Triângulo
            triangleBuffer[_vertex * 6 + 0] = 0 + _vertex;
            triangleBuffer[_vertex * 6 + 1] = 1 + _vertex;
            triangleBuffer[_vertex * 6 + 2] = 2 + _vertex;

            // segundo Triângulo
            triangleBuffer[_vertex * 6 + 3] = 0 + _vertex;
            triangleBuffer[_vertex * 6 + 4] = 2 + _vertex;
            triangleBuffer[_vertex * 6 + 5] = 3 + _vertex;
        }
    }
}
