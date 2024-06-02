using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace RubyDung.src.level {
    public class Tesselator {
        private List<Vector3> vertexList = new List<Vector3>();
        private List<int> triangleList = new List<int>();

        private int VAO; // Vertex Array Object
        private int VBO; // Vertex Buffer Object
        private int EBO; // Element Buffer Object

        private Shader shader;

        public void flush() {
            // ..:: Vertex Array Object ::..
            this.VAO = GL.GenVertexArray();
            GL.BindVertexArray(this.VAO);

            // ..:: Vertex Buffer Object ::..
            this.VBO = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, this.VBO);
            GL.BufferData(BufferTarget.ArrayBuffer, this.vertexList.Count * Vector3.SizeInBytes, this.vertexList.ToArray(), BufferUsageHint.StreamDraw);

            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 0, 0);
            GL.EnableVertexAttribArray(0);

            // ..:: Element Buffer Object ::..
            this.EBO = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, this.EBO);
            GL.BufferData(BufferTarget.ElementArrayBuffer, this.triangleList.Count * sizeof(int), this.triangleList.ToArray(), BufferUsageHint.StreamDraw);

            // ..::  ::..
            this.shader = new Shader();
        }

        // Essa função deveria ficar aqui?
        public void use() {
            GL.BindVertexArray(this.VAO);
            //GL.DrawArrays(PrimitiveType.Triangles, 0, 3);
            GL.DrawElements(PrimitiveType.Triangles, this.triangleList.Count, DrawElementsType.UnsignedInt, 0);

            this.shader.use();
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
