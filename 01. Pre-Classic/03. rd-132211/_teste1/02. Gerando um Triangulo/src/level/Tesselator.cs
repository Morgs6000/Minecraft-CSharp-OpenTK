using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace RubyDung.src.level {
    public class Tesselator {
        private List<Vector3> vertexList = new List<Vector3>();

        private int VAO; // Vertex Array Object
        private int VBO; // Vertex Buffer Object

        private Shader shader;

        public void flush() {
            // ..:: Vertex Array Object ::..
            VAO = GL.GenVertexArray();
            GL.BindVertexArray(VAO);

            // ..:: Vertex Buffer Object ::..
            VBO = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, VBO);
            GL.BufferData(BufferTarget.ArrayBuffer, this.vertexList.Count * Vector3.SizeInBytes, this.vertexList.ToArray(), BufferUsageHint.StreamDraw);

            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 0, 0);
            GL.EnableVertexAttribArray(0);

            // ..::  ::..
            this.shader = new Shader();
        }

        // Essa função deveria ficar aqui?
        public void use() {
            GL.BindVertexArray(VAO);
            GL.DrawArrays(PrimitiveType.Triangles, 0, 3);

            this.shader.use();
        }

        public void vertex(float x, float y, float z) {
            this.vertexList.Add(new Vector3(x, y, z));
        }
    }
}
