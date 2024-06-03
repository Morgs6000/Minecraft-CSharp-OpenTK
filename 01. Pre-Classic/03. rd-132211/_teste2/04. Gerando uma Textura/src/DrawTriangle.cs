using OpenTK.Graphics.OpenGL4;

namespace RubyDung.src {
    public class DrawTriangle {
        private float[] vertices = {
            -0.5f, -0.5f, 0.0f, //Bottom-left vertex
             0.0f,  0.5f, 0.0f, //Top vertex
             0.5f, -0.5f, 0.0f  //Bottom-right vertex
        };

        private int VAO; // VertexArrayObject
        private int VBO; // VertexBufferObject

        public void loadTriangle() {
            // ..:: VertexArrayObject ::..
            this.VAO = GL.GenVertexArray();
            GL.BindVertexArray(this.VAO);

            // ..:: VertexArrayObject ::..
            this.VBO = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, this.VBO);
            GL.BufferData(BufferTarget.ArrayBuffer, this.vertices.Length * sizeof(float), this.vertices, BufferUsageHint.StaticDraw);

            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);
        }

        public void bind() {
            GL.BindVertexArray(this.VAO);
            GL.DrawArrays(PrimitiveType.Triangles, 0, 3);
        }
    }
}
