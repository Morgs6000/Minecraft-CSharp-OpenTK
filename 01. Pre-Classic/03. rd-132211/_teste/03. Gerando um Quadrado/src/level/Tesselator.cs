using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace RubyDung.src.level {
    internal class Tesselator {
        int VAO; // Vertex Array Object
        int VBO; // Vertex Buffer Object
        int EBO; // Element Buffer Object

        private List<Vector3> vertexList = new List<Vector3>();
        private List<int> triangleList = new List<int>();

        public void flush() {
            // ..:: Vertex Array Object ::..
            VAO = GL.GenVertexArray();
            GL.BindVertexArray(VAO);

            // ..:: Vertex Buffer Object ::..
            VBO = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, VBO);
            GL.BufferData(BufferTarget.ArrayBuffer, this.vertexList.Count * Vector3.SizeInBytes, this.vertexList.ToArray(), BufferUsageHint.StreamDraw);

            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            // ..:: Element Buffer Object ::..
            EBO = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, EBO);
            GL.BufferData(BufferTarget.ElementArrayBuffer, this.triangleList.Count * Vector3.SizeInBytes, this.triangleList.ToArray(), BufferUsageHint.StreamDraw);
        }

        public void use() {
            GL.BindVertexArray(VAO);
            GL.DrawElements(PrimitiveType.Triangles, this.triangleList.Count, DrawElementsType.UnsignedInt, 0);
        }

        public void vertex(float x, float y, float z) {
            this.vertexList.Add(new Vector3(x, y, z));
        }

        public void triangle() {
            // first triangle
            this.triangleList.Add(0);
            this.triangleList.Add(1);
            this.triangleList.Add(2);

            // second triangle
            this.triangleList.Add(0);
            this.triangleList.Add(2);
            this.triangleList.Add(3);
        }
    }
}
