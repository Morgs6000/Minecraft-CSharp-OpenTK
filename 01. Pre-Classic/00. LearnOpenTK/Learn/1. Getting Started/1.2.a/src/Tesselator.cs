using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace LearnOpenTK.src {
    internal class Tesselator {
        /*
        float[] vertices = {
            -0.5f, -0.5f, 0.0f,  // bottom left
            -0.5f,  0.5f, 0.0f,  // top left 
             0.5f,  0.5f, 0.0f,  // top right
             0.5f, -0.5f, 0.0f   // bottom right
        };
        */
        List<Vector3> vertices = new List<Vector3>();

        /*
        int[] indices = {  // note that we start from 0!
            0, 1, 2,   // first triangle
            0, 2, 3    // second triangle
        };
        */
        List<int> indices = new List<int>();

        int VAO; // Vertex Array Object
        int VBO; // Vertex Buffer Object
        int EBO; // Element Buffer Object

        public Tesselator() {
            
        }

        public void Flush() {
            // ..:: Vertex Array Object ::..

            VAO = GL.GenVertexArray();
            GL.BindVertexArray(VAO);

            // ..:: Vertex Buffer Object ::..

            VBO = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, VBO);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Count * Vector3.SizeInBytes, vertices.ToArray(), BufferUsageHint.StaticDraw);

            // ..:: Element Buffer Object ::..

            EBO = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, EBO);
            GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Count * Vector3.SizeInBytes, indices.ToArray(), BufferUsageHint.StaticDraw);

            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);
        }

        public void Use() {
            GL.BindVertexArray(VAO);
        }

        public void Render() {
            GL.DrawElements(PrimitiveType.Triangles, indices.Count, DrawElementsType.UnsignedInt, 0);
        }

        public void vertex(float x, float y, float z) {
            vertices.Add(new Vector3(x, y, z));

            triangle();
        }

        public void triangle() {
            // first triangle
            indices.Add(0);
            indices.Add(1);
            indices.Add(2);

            // second triangle
            indices.Add(0);
            indices.Add(2);
            indices.Add(3);
        }
    }
}
