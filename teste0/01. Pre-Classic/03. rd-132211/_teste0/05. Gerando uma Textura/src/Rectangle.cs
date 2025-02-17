using OpenTK.Graphics.OpenGL4;

namespace RubyDung.src;

public class Rectangle {
    private float[] vertices = {
        -0.5f, -0.5f, 0.0f,  // bottom left
         0.5f, -0.5f, 0.0f,  // bottom right
         0.5f,  0.5f, 0.0f,  // top right
        -0.5f,  0.5f, 0.0f   // top left
    };

    private uint[] indices = {  // note that we start from 0!
        0, 1, 2,   // first triangle
        0, 2, 3    // second triangle
    };

    private float[] textureCoords = {
        0.0f, 0.0f, // bottom left
        1.0f, 0.0f, // bottom right
        1.0f, 1.0f, // top right
        0.0f, 1.0f  // top left
    };

    private int VertexArrayObject;
    private int VertexBufferObject;
    private int ElementBufferObject;
    private int TextureBufferObject;

    public Rectangle(Shader shader) {
        // ..:: VERTEX ARRAY OBJECT ::..
        VertexArrayObject = GL.GenVertexArray();
        GL.BindVertexArray(VertexArrayObject);

        // ..:: VERTEX BUFFER OBJECT ::..
        VertexBufferObject = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);
        GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

        int vertexLocation = shader.GetAttribLocation("aPos");
        GL.VertexAttribPointer(vertexLocation, 3, VertexAttribPointerType.Float, false, 0, 0);
        GL.EnableVertexAttribArray(vertexLocation);

        // ..:: ELEMENT BUFFER OBJECT ::..
        ElementBufferObject = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);
        GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, BufferUsageHint.StaticDraw);

        // ..:: TEXTURE BUFFER OBJECT ::..
        TextureBufferObject = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ArrayBuffer, TextureBufferObject);
        GL.BufferData(BufferTarget.ArrayBuffer, textureCoords.Length * sizeof(float), textureCoords, BufferUsageHint.StaticDraw);

        int texCoordLocation = shader.GetAttribLocation("aTex");
        GL.VertexAttribPointer(texCoordLocation, 2, VertexAttribPointerType.Float, false, 0, 0);
        GL.EnableVertexAttribArray(texCoordLocation);
    }

    public void Use() {
        GL.BindVertexArray(VertexArrayObject);
        //GL.DrawArrays(PrimitiveType.Triangles, 0, 3);
        GL.DrawElements(PrimitiveType.Triangles, indices.Length, DrawElementsType.UnsignedInt, 0);
    }
}
