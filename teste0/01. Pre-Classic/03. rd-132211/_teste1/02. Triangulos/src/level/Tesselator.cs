using OpenTK.Graphics.OpenGL4;

namespace RubyDung;

public class Tesselator {
    private float[] vertexBuffer = {
        -0.5f, -0.5f, // inferior esquerdo => indice 0
         0.5f, -0.5f, // inferior direito  => indice 1
         0.5f,  0.5f, // superior direito  => indice 2
        -0.5f,  0.5f  // superior direito  => indice 3
    };

    private int[] indiceBuffer = {
        0, 1, 2, // primeiro triangulo
        0, 2, 3  // segundo triangulo
    };

    private int vertexArrayObject;
    private int vertexBufferObject;
    private int elementBufferObject;

    public void OnLoad() {
        /* ..:: Vertex Array Object ::.. */
        vertexArrayObject = GL.GenVertexArray();
        GL.BindVertexArray(vertexArrayObject);

        /* ..:: Vertex Buffer Object ::.. */
        vertexBufferObject = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferObject);
        GL.BufferData(BufferTarget.ArrayBuffer, vertexBuffer.Length * sizeof(float), vertexBuffer, BufferUsageHint.StaticDraw);

        GL.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, 0, 0);
        GL.EnableVertexAttribArray(0);

        /* ..:: Element Buffer Object ::.. */
        elementBufferObject = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ElementArrayBuffer, elementBufferObject);
        GL.BufferData(BufferTarget.ElementArrayBuffer, indiceBuffer.Length * sizeof(int), indiceBuffer, BufferUsageHint.StaticDraw);
    }

    public void OnRenderFrame() {
        GL.BindVertexArray(vertexArrayObject);
        GL.DrawElements(PrimitiveType.Triangles, indiceBuffer.Length, DrawElementsType.UnsignedInt, 0);
    }
}