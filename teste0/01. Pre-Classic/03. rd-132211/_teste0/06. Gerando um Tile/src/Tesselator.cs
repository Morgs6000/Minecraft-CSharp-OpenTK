using OpenTK.Graphics.OpenGL4;

namespace RubyDung.src;

public class Tesselator {
    private List<float> vertexBuffer = new List<float>();
    private List<uint> indiceBuffer = new List<uint>();
    private List<float> texCoordBuffer = new List<float>();

    private int VertexArrayObject;
    private int VertexBufferObject;
    private int ElementBufferObject;
    private int TextureBufferObject;

    public void flush(Shader shader) {
        // ..:: VERTEX ARRAY OBJECT ::..
        VertexArrayObject = GL.GenVertexArray();
        GL.BindVertexArray(VertexArrayObject);

        // ..:: VERTEX BUFFER OBJECT ::..
        VertexBufferObject = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);
        GL.BufferData(BufferTarget.ArrayBuffer, vertexBuffer.Count * sizeof(float), vertexBuffer.ToArray(), BufferUsageHint.StaticDraw);

        int vertexLocation = shader.GetAttribLocation("aPos");
        GL.VertexAttribPointer(vertexLocation, 3, VertexAttribPointerType.Float, false, 0, 0);
        GL.EnableVertexAttribArray(vertexLocation);

        // ..:: ELEMENT BUFFER OBJECT ::..
        ElementBufferObject = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);
        GL.BufferData(BufferTarget.ElementArrayBuffer, indiceBuffer.Count * sizeof(uint), indiceBuffer.ToArray(), BufferUsageHint.StaticDraw);

        // ..:: TEXTURE BUFFER OBJECT ::..
        TextureBufferObject = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ArrayBuffer, TextureBufferObject);
        GL.BufferData(BufferTarget.ArrayBuffer, texCoordBuffer.Count * sizeof(float), texCoordBuffer.ToArray(), BufferUsageHint.StaticDraw);

        int texCoordLocation = shader.GetAttribLocation("aTex");
        GL.VertexAttribPointer(texCoordLocation, 2, VertexAttribPointerType.Float, false, 0, 0);
        GL.EnableVertexAttribArray(texCoordLocation);
    }

    public void Use() {
        GL.BindVertexArray(VertexArrayObject);
        //GL.DrawArrays(PrimitiveType.Triangles, 0, 3);
        GL.DrawElements(PrimitiveType.Triangles, indiceBuffer.Count, DrawElementsType.UnsignedInt, 0);
    }

    public void vertex(float x, float y, float z) {
        vertexBuffer.Add(x);
        vertexBuffer.Add(y);
        vertexBuffer.Add(z);
    }

    public void indice() {
        indiceBuffer.Add(0);
        indiceBuffer.Add(1);
        indiceBuffer.Add(2);

        indiceBuffer.Add(0);
        indiceBuffer.Add(2);
        indiceBuffer.Add(3);
    }

    public void tex(float u, float v) {
        texCoordBuffer.Add(u);
        texCoordBuffer.Add(v);
    }
}
