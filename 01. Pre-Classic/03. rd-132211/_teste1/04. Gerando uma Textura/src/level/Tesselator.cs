using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace RubyDung.src.level {
    public class Tesselator {
        private List<Vector3> vertexBuffer = new List<Vector3>();
        private List<int> triangleBuffer = new List<int>();
        private List<Vector2> texCoordBuffer = new List<Vector2>();

        private int VAO; // Vertex Array Object
        private int VBO; // Vertex Buffer Object
        private int TBO; // Texture Buffer Object
        private int EBO; // Element Buffer Object

        private Shader shader = new Shader();
        private Texture texture = new Texture();

        public void flush() {
            // ..:: Vertex Array Object ::..
            this.VAO = GL.GenVertexArray();
            GL.BindVertexArray(this.VAO);

            // ..:: Vertex Buffer Object ::..
            this.VBO = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, this.VBO);
            GL.BufferData(BufferTarget.ArrayBuffer, this.vertexBuffer.Count * Vector3.SizeInBytes, this.vertexBuffer.ToArray(), BufferUsageHint.StreamDraw);

            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 0, 0);
            GL.EnableVertexAttribArray(0);

            // ..:: Texture Buffer Object ::..
            TBO = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, TBO);
            GL.BufferData(BufferTarget.ArrayBuffer, this.texCoordBuffer.Count * Vector2.SizeInBytes, this.texCoordBuffer.ToArray(), BufferUsageHint.StreamDraw);

            GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 0, 0);
            GL.EnableVertexAttribArray(1);

            // ..:: Element Buffer Object ::..
            this.EBO = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, this.EBO);
            GL.BufferData(BufferTarget.ElementArrayBuffer, this.triangleBuffer.Count * sizeof(int), this.triangleBuffer.ToArray(), BufferUsageHint.StreamDraw);

            // ..::  ::..
            this.shader.loadShader();

            // ..::  ::..
            this.texture.loadTexture();
        }

        // Essa função deveria ficar aqui?
        public void use() {
            // ..::  ::..
            GL.BindVertexArray(this.VAO);
            //GL.DrawArrays(PrimitiveType.Triangles, 0, 3);
            GL.DrawElements(PrimitiveType.Triangles, this.triangleBuffer.Count, DrawElementsType.UnsignedInt, 0);

            // ..::  ::..
            this.shader.use();

            // ..::  ::..
            this.texture.use();
        }

        public void vertex(float x, float y, float z) {
            this.vertexBuffer.Add(new Vector3(x, y, z));
        }

        public void triangle() {
            // first triangle
            this.triangleBuffer.Add(0);
            this.triangleBuffer.Add(1);
            this.triangleBuffer.Add(2);

            // second triangle
            this.triangleBuffer.Add(0);
            this.triangleBuffer.Add(2);
            this.triangleBuffer.Add(3);
        }

        public void tex(float u0, float u1, float v0, float v1) {
            this.texCoordBuffer.Add(new Vector2(u0, v0)); // bottom left
            this.texCoordBuffer.Add(new Vector2(u0, v1)); // top left 
            this.texCoordBuffer.Add(new Vector2(u1, v1)); // top right
            this.texCoordBuffer.Add(new Vector2(u1, v0)); // bottom right
        }
    }
}
