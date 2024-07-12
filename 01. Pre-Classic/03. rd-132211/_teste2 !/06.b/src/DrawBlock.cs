using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace RubyDung.src {
    public class DrawBlock {
        private float[] vertexBuffer = new float[300000];
        //private List<Vector3> vertexBuffer = new List<Vector3>();
        private int[] triangleBuffer = new int[300000];
        //private List<int> triangleBuffer = new List<int>();
        private List<Vector2> texCoordBuffer = new List<Vector2>();

        private int vertices;
        private int vertices2;

        private int texX;
        private int texY;

        private float col = 16.0f;
        private float row = 16.0f;

        private int VAO; // Vertex Array Object
        private int VBO; // Vertex Buffer Object
        private int TBO; // Texture Buffer Object
        private int EBO; // Element Buffer Object

        public void loadBlock() {
            block();

            // ..:: Vertex Array Object ::..
            VAO = GL.GenVertexArray();
            GL.BindVertexArray(VAO);

            // ..:: Vertex Array Object ::..
            VBO = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, VBO);
            GL.BufferData(BufferTarget.ArrayBuffer, vertexBuffer.Length * sizeof(float), vertexBuffer, BufferUsageHint.StaticDraw);
            //GL.BufferData(BufferTarget.ArrayBuffer, vertexBuffer.Count * Vector3.SizeInBytes, vertexBuffer.ToArray(), BufferUsageHint.StaticDraw);

            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            // ..:: Texture Array Object ::..
            TBO = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, TBO);
            GL.BufferData(BufferTarget.ArrayBuffer, texCoordBuffer.Count * Vector2.SizeInBytes, texCoordBuffer.ToArray(), BufferUsageHint.StaticDraw);

            GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 2 * sizeof(float), 0);
            GL.EnableVertexAttribArray(1);

            // ..:: Element Buffer Object ::..
            EBO = GL.GenBuffer();

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, EBO);
            GL.BufferData(BufferTarget.ElementArrayBuffer, triangleBuffer.Length * sizeof(int), triangleBuffer, BufferUsageHint.StaticDraw);
            //GL.BufferData(BufferTarget.ElementArrayBuffer, triangleBuffer.Count * sizeof(int), triangleBuffer.ToArray(), BufferUsageHint.StaticDraw);
        }

        public void bind() {
            GL.BindVertexArray(VAO);
            //GL.DrawArrays(PrimitiveType.Triangles, 0, 3);
            GL.DrawElements(PrimitiveType.Triangles, triangleBuffer.Length, DrawElementsType.UnsignedInt, 0);
            //GL.DrawElements(PrimitiveType.Triangles, triangleBuffer.Count, DrawElementsType.UnsignedInt, 0);
        }

        public void block() {
            float x0 = 0.0f;
            float y0 = 0.0f;
            float z0 = 0.0f;

            float x1 = 1.0f;
            float y1 = 1.0f;
            float z1 = 1.0f;

            // ..:: Negative X ::..
            vertex(x0, y0, z0);
            vertex(x0, y1, z0);
            vertex(x0, y1, z1);
            vertex(x0, y0, z1);

            triangle();
            tex();

            // ..:: Positive X ::..
            vertex(x1, y0, z1);
            vertex(x1, y1, z1);
            vertex(x1, y1, z0);
            vertex(x1, y0, z0);

            triangle();
            tex();

            // ..:: Negative Y ::..
            vertex(x0, y0, z0);
            vertex(x0, y0, z1);
            vertex(x1, y0, z1);
            vertex(x1, y0, z0);

            triangle();
            tex();

            // ..:: Positive Y ::..
            vertex(x0, y1, z1);
            vertex(x0, y1, z0);
            vertex(x1, y1, z0);
            vertex(x1, y1, z1);

            triangle();
            tex();

            // ..:: Negative Z ::..
            vertex(x1, y0, z0);
            vertex(x1, y1, z0);
            vertex(x0, y1, z0);
            vertex(x0, y0, z0);

            triangle();
            tex();

            // ..:: Positive Z ::..
            vertex(x0, y0, z1);
            vertex(x0, y1, z1);
            vertex(x1, y1, z1);
            vertex(x1, y0, z1);

            triangle();
            tex();
        }

        public void vertex(float x, float y, float z) {
            //vertexBuffer.Add(new Vector3(x, y, z));

            int index = vertices * 3;

            vertexBuffer[index + 0] = x;
            vertexBuffer[index + 1] = y;
            vertexBuffer[index + 2] = z;

            vertices++;

            //Console.WriteLine(vertices);
        }

        public void triangle() {
            /*
            // first triangle
            triangleBuffer.Add(0 + vertices2);
            triangleBuffer.Add(1 + vertices2);
            triangleBuffer.Add(2 + vertices2);

            // second triangle
            triangleBuffer.Add(0 + vertices2);
            triangleBuffer.Add(2 + vertices2);
            triangleBuffer.Add(3 + vertices2);
            */

            int index = vertices2 * 6;

            // first triangle
            triangleBuffer[index + 0] = 0 + vertices2;
            triangleBuffer[index + 1] = 1 + vertices2;
            triangleBuffer[index + 2] = 2 + vertices2;

            // second triangle
            triangleBuffer[index + 3] = 0 + vertices2;
            triangleBuffer[index + 4] = 2 + vertices2;
            triangleBuffer[index + 5] = 3 + vertices2;

            vertices2 += 4;
        }

        public void tex() {
            float u0 = texX / col;
            float u1 = u0 + 1.0f / col;
            float v0 = (row - 1.0f - texY) / row;
            float v1 = v0 + 1.0f / row;

            texCoordBuffer.Add(new Vector2(u0, v0));
            texCoordBuffer.Add(new Vector2(u0, v1));
            texCoordBuffer.Add(new Vector2(u1, v1));
            texCoordBuffer.Add(new Vector2(u1, v0));
        }
    }
}
