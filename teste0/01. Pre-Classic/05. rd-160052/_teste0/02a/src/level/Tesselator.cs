﻿using OpenTK.Graphics.OpenGL4;

namespace RubyDung.src.level;

public class Tesselator {
    private List<float> vertexBuffer = new List<float>();
    private List<int> indiceBuffer = new List<int>();
    private List<float> texCoordBuffer = new List<float>();

    private int vertices = 0;

    private float u;
    private float v;

    private bool hasTextrue = false;

    private int VertexArrayObject;
    private int VertexBufferObject;
    private int ElementBufferObject;
    private int TextureBufferObject;

    public void Load() {
        VertexArrayObject = GL.GenVertexArray();
        GL.BindVertexArray(VertexArrayObject);

        VertexBufferObject = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);
        GL.BufferData(BufferTarget.ArrayBuffer, vertexBuffer.Count * sizeof(float), vertexBuffer.ToArray(), BufferUsageHint.StaticDraw);

        GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 0, 0);
        GL.EnableVertexAttribArray(0);

        ElementBufferObject = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);
        GL.BufferData(BufferTarget.ElementArrayBuffer, indiceBuffer.Count * sizeof(int), indiceBuffer.ToArray(), BufferUsageHint.StaticDraw);

        if(hasTextrue) {
            TextureBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, TextureBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, texCoordBuffer.Count * sizeof(float), texCoordBuffer.ToArray(), BufferUsageHint.StaticDraw);

            GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 0, 0);
            GL.EnableVertexAttribArray(1);
        }
    }

    public void Render() {
        GL.BindVertexArray(VertexArrayObject);
        GL.DrawElements(PrimitiveType.Triangles, indiceBuffer.Count, DrawElementsType.UnsignedInt, 0);
    }

    private void Clear() {
        this.vertexBuffer.Clear();
        this.indiceBuffer.Clear();
        this.texCoordBuffer.Clear();

        this.vertices = 0;
    }

    public void Init() {
        this.Clear();

        hasTextrue = false;
    }

    public void Vertex(float x, float y, float z) {
        vertexBuffer.Add(x);
        vertexBuffer.Add(y);
        vertexBuffer.Add(z);

        if(hasTextrue) {
            texCoordBuffer.Add(u);
            texCoordBuffer.Add(v);
        }
    }

    public void Indice() {
        indiceBuffer.Add(0 + vertices);
        indiceBuffer.Add(1 + vertices);
        indiceBuffer.Add(2 + vertices);

        indiceBuffer.Add(0 + vertices);
        indiceBuffer.Add(2 + vertices);
        indiceBuffer.Add(3 + vertices);

        vertices += 4;
    }

    public void Tex(float u, float v) {
        hasTextrue = true;

        this.u = u;
        this.v = v;
    }

    public void VertexUV(float x, float y, float z, float u, float v) {
        Vertex(x, y, z);
        Tex(u, v);
    }
}
