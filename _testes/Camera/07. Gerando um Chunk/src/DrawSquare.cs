using OpenTK.Graphics.OpenGL4;

namespace RubyDung.src {
    public class DrawSquare {
        private float[] vertices = {
            -0.5f, -0.5f, 0.0f,  // bottom left
            -0.5f,  0.5f, 0.0f,  // top left 
             0.5f,  0.5f, 0.0f,  // top right
             0.5f, -0.5f, 0.0f   // bottom right
        };

        private int[] indices = {  // note that we start from 0!
            0, 1, 2,   // first triangle
            0, 2, 3    // second triangle
        };

        private int VAO; // Vertex Array Object
        private int VBO; // Vertex Buffer Object
        private int EBO; // Element Buffer Object

        public void loadSquare() {
            // ..:: Vertex Array Object ::..
            this.VAO = GL.GenVertexArray();
            GL.BindVertexArray(this.VAO);

            // ..:: Vertex Array Object ::..
            this.VBO = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, this.VBO);
            GL.BufferData(BufferTarget.ArrayBuffer, this.vertices.Length * sizeof(float), this.vertices, BufferUsageHint.StaticDraw);

            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            // ..:: Element Buffer Object ::..
            this.EBO = GL.GenBuffer();

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, this.EBO);
            GL.BufferData(BufferTarget.ElementArrayBuffer, this.indices.Length * sizeof(int), this.indices, BufferUsageHint.StaticDraw);

        }

        public void bind() {
            GL.BindVertexArray(this.VAO);
            //GL.DrawArrays(PrimitiveType.Triangles, 0, 3);
            GL.DrawElements(PrimitiveType.Triangles, this.indices.Length, DrawElementsType.UnsignedInt, 0);
        }
    }
}
