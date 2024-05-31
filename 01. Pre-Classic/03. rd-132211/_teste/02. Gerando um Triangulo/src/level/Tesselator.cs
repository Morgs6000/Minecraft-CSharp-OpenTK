using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace RubyDung.src.level {
    internal class Tesselator {
        int VAO; // Vertex Array Object
        int VBO; // Vertex Buffer Object

        private List<Vector3> vertexList = new List<Vector3>();

        public void flush() {
            this.vertex();

            // ..:: Vertex Array Object ::..
            VAO = GL.GenVertexArray();
            GL.BindVertexArray(VAO);

            // ..:: Vertex Buffer Object ::..
            VBO = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, VBO);
            GL.BufferData(BufferTarget.ArrayBuffer, this.vertexList.Count * Vector3.SizeInBytes, this.vertexList.ToArray(), BufferUsageHint.StreamDraw);

            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);
        }

        public void use() {
            GL.BindVertexArray(VAO);
            GL.DrawArrays(PrimitiveType.Triangles, 0, 3);
        }

        public void vertex() {
            this.vertexList.Add(new Vector3(-0.5f, -0.5f,  0.0f)); // Bottom-left vertex
            this.vertexList.Add(new Vector3( 0.0f,  0.5f,  0.0f)); // Top vertex
            this.vertexList.Add(new Vector3( 0.5f, -0.5f,  0.0f)); // Bottom-right vertex
        }
    }
}
