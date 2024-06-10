using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace RubyDung.src {
    public class DrawTexture {
        private List<Vector3> vertices = new List<Vector3>() {
            new Vector3(-0.5f, -0.5f, 0.0f),  // bottom left
            new Vector3(-0.5f,  0.5f, 0.0f),  // top left 
            new Vector3( 0.5f,  0.5f, 0.0f),  // top right
            new Vector3( 0.5f, -0.5f, 0.0f)   // bottom right
        };

        private List<int> indices = new List<int>() {  // note that we start from 0!
            0, 1, 2,   // first triangle
            0, 2, 3    // second triangle
        };

        private List<Vector2> texCoords = new List<Vector2>();

        private int texX;
        private int texY;

        private float col = 16.0f;
        private float row = 16.0f;

        private int VAO; // Vertex Array Object
        private int VBO; // Vertex Buffer Object
        private int TBO; // Texture Buffer Object
        private int EBO; // Element Buffer Object

        public void loadSquare() {
            this.tex();

            // ..:: Vertex Array Object ::..
            this.VAO = GL.GenVertexArray();
            GL.BindVertexArray(this.VAO);

            // ..:: Vertex Array Object ::..
            this.VBO = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, this.VBO);
            GL.BufferData(BufferTarget.ArrayBuffer, this.vertices.Count * Vector3.SizeInBytes, this.vertices.ToArray(), BufferUsageHint.StaticDraw);

            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            // ..:: Texture Array Object ::..
            this.TBO = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, this.TBO);
            GL.BufferData(BufferTarget.ArrayBuffer, this.texCoords.Count * Vector2.SizeInBytes, this.texCoords.ToArray(), BufferUsageHint.StaticDraw);

            GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 2 * sizeof(float), 0);
            GL.EnableVertexAttribArray(1);

            // ..:: Element Buffer Object ::..
            this.EBO = GL.GenBuffer();

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, this.EBO);
            GL.BufferData(BufferTarget.ElementArrayBuffer, this.indices.Count * sizeof(int), this.indices.ToArray(), BufferUsageHint.StaticDraw);
        }

        public void bind() {
            GL.BindVertexArray(this.VAO);
            //GL.DrawArrays(PrimitiveType.Triangles, 0, 3);
            GL.DrawElements(PrimitiveType.Triangles, this.indices.Count, DrawElementsType.UnsignedInt, 0);
        }

        public void tex() {
            float u0 = (float)this.texX / this.col;
            float u1 = u0 + (1.0f / this.col);
            float v0 = ((this.row - 1.0f) - (float)this.texY) / this.row;
            float v1 = v0 + (1.0f / this.row);

            this.texCoords.Add(new Vector2(u0, v0));
            this.texCoords.Add(new Vector2(u0, v1));
            this.texCoords.Add(new Vector2(u1, v1));
            this.texCoords.Add(new Vector2(u1, v0));
        }
    }
}
