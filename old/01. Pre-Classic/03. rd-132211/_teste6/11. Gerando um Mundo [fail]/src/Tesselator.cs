using OpenTK.Graphics.OpenGL4;

namespace RubyDung.src;

public class Tesselator {
    // ..:: CREATE TRIANGLE ::..
    private float[] vertices = new float[300000];
    private int[] indices = new int[600000];
    private float[] texCoords = new float[200000];

    private int verticesLength = 0;
    private int indicesLength = 0;
    //private int texCoordsLength = 0;

    private float u;
    private float v;

    private bool hasTexture = false;

    private int VAO; // Vertex Array Object
    private int VBO; // Vertex Buffer Object
    private int EBO; // Element Buffer Object
    private int TBO; // Texture Buffer Object

    public void DrawTriangle() {
        // ..:: VERTEX ARRAY OBJECT ::..
        GL.GenVertexArrays(1, out this.VAO);

        GL.BindVertexArray(this.VAO);

        // ..:: VERTEX BUFFER OBJECT ::..
        GL.GenBuffers(1, out this.VBO);

        GL.BindBuffer(BufferTarget.ArrayBuffer, this.VBO);
        GL.BufferData(BufferTarget.ArrayBuffer, this.vertices.Length * sizeof(float), this.vertices, BufferUsageHint.StaticDraw);

        GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 0, 0);
        GL.EnableVertexAttribArray(0);

        // ..:: ELEMENT BUFFER OBJECT ::..
        GL.GenBuffers(1, out this.EBO);

        GL.BindBuffer(BufferTarget.ElementArrayBuffer, this.EBO);
        GL.BufferData(BufferTarget.ElementArrayBuffer, this.indices.Length * sizeof(int), this.indices, BufferUsageHint.StaticDraw);

        // ..:: TEXTURE BUFFER OBJECT ::..
        GL.GenBuffers(1, out this.TBO);

        GL.BindBuffer(BufferTarget.ArrayBuffer, this.TBO);
        GL.BufferData(BufferTarget.ArrayBuffer, this.texCoords.Length * sizeof(float), this.texCoords, BufferUsageHint.StaticDraw);

        GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 0, 0);
        GL.EnableVertexAttribArray(1);

        GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        GL.BindVertexArray(0);
    }

    public void render() {
        GL.BindVertexArray(this.VAO);
        GL.DrawElements(PrimitiveType.Triangles, this.indices.Length, DrawElementsType.UnsignedInt, 0);
    }

    public void addVertices(float x, float y, float z) {
        this.vertices[this.verticesLength * 3 + 0] = x;
        this.vertices[this.verticesLength * 3 + 1] = y;
        this.vertices[this.verticesLength * 3 + 2] = z;

        if(this.hasTexture) {
            this.texCoords[this.verticesLength * 2 + 0] = this.u;
            this.texCoords[this.verticesLength * 2 + 1] = this.v;
        }

        this.verticesLength++;
    }

    //*
    public void addIndices() {
        // first triangle
        this.indices[this.indicesLength + 0] = 0 + this.verticesLength - 4;
        this.indices[this.indicesLength + 1] = 1 + this.verticesLength - 4;
        this.indices[this.indicesLength + 2] = 2 + this.verticesLength - 4;

        // second triangle
        this.indices[this.indicesLength + 3] = 0 + this.verticesLength - 4;
        this.indices[this.indicesLength + 4] = 2 + this.verticesLength - 4;
        this.indices[this.indicesLength + 5] = 3 + this.verticesLength - 4;

        this.indicesLength += 6;
    }
    //*/

    /*
    public void addTexCoords(float u, float v) {
        this.texCoords[this.texCoordsLength * 2 + 0] = u;
        this.texCoords[this.texCoordsLength * 2 + 1] = v;

        this.texCoordsLength++;
    }
    */
    public void addTexCoords(float u, float v) {
        this.hasTexture = true;

        this.u = u;
        this.v = v;
    }
}
