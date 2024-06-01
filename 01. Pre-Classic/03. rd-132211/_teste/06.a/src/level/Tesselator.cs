using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace RubyDung.src.level {
    public class Tesselator {
        int VAO; // Vertex Array Object
        int VBO; // Vertex Buffer Object
        int TBO; // Texture Buffer Object
        int EBO; // Element Buffer Object

        private List<Vector3> vertexList = new List<Vector3>();
        private List<int> triangleList = new List<int>();
        private List<Vector2> texCoordList = new List<Vector2>();

        private int vertices = 0;

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

            // ..:: Texture Buffer Object ::..

            TBO = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, TBO);
            GL.BufferData(BufferTarget.ArrayBuffer, this.texCoordList.Count * Vector2.SizeInBytes, this.texCoordList.ToArray(), BufferUsageHint.StreamDraw);

            GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 2 * sizeof(float), 0);
            GL.EnableVertexAttribArray(1);

            // ..:: Element Buffer Object ::..
            EBO = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, EBO);
            GL.BufferData(BufferTarget.ElementArrayBuffer, this.triangleList.Count * sizeof(int), this.triangleList.ToArray(), BufferUsageHint.StreamDraw);
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
            this.triangleList.Add(0 + vertices);
            this.triangleList.Add(1 + vertices);
            this.triangleList.Add(2 + vertices);

            // second triangle
            this.triangleList.Add(0 + vertices);
            this.triangleList.Add(2 + vertices);
            this.triangleList.Add(3 + vertices);

            vertices += 4;
        }

        /*
        public void tex(float u, float v) {
            this.texCoordList.Add(new Vector2(u, v));
        }
        */
        public void tex(float u0, float u1, float v0, float v1) {
            this.texCoordList.Add(new Vector2(u0, v0));
            this.texCoordList.Add(new Vector2(u0, v1));
            this.texCoordList.Add(new Vector2(u1, v1));
            this.texCoordList.Add(new Vector2(u1, v0));
        }
    }
}
