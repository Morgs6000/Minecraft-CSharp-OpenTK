using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace RubyDung.src {
    public class DrawChunk {
        private List<Vector3> vertexBuffer = new List<Vector3>();
        private List<int> triangleBuffer = new List<int>();
        private List<Vector2> texCoordBuffer = new List<Vector2>();

        private int VAO; // Vertex Array Object
        private int VBO; // Vertex Buffer Object
        private int TBO; // Texture Buffer Object
        private int EBO; // Element Buffer Object

        private int vertices;

        private int texX;
        private int texY;

        private float col = 16.0f;
        private float row = 16.0f;

        public int width;
        public int height;
        public int depth;
        private byte[] blocks;

        public void loadChunk() {
            this.level(16, 16, 16);
            this.chunk(0, 0, 0, 16, 16 ,16);

            // ..:: Vertex Array Object ::..
            VAO = GL.GenVertexArray();
            GL.BindVertexArray(VAO);

            // ..:: Vertex Array Object ::..
            VBO = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, VBO);
            GL.BufferData(BufferTarget.ArrayBuffer, vertexBuffer.Count * Vector3.SizeInBytes, vertexBuffer.ToArray(), BufferUsageHint.StaticDraw);

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
            GL.BufferData(BufferTarget.ElementArrayBuffer, triangleBuffer.Count * sizeof(int), triangleBuffer.ToArray(), BufferUsageHint.StaticDraw);
        }

        public void bind() {
            GL.BindVertexArray(VAO);
            //GL.DrawArrays(PrimitiveType.Triangles, 0, 3);
            GL.DrawElements(PrimitiveType.Triangles, triangleBuffer.Count, DrawElementsType.UnsignedInt, 0);
        }

        public void level(int w, int h, int d) {
            this.width = w;
            this.height = h;
            this.depth = d;
            this.blocks = new byte[w * h * d];

            for(int x = 0; x < w; x++) {
                for(int y = 0; y < h; y++) {
                    for(int z = 0; z < d; z++) {
                        int i = (y * this.depth + z) * this.width + x;
                        this.blocks[i] = 1;
                    }
                }
            }
        }

        public bool isTile(int x, int y, int z) {
            if(x >= 0 && y >= 0 && z >= 0 && x < this.width && y < this.height && z < this.depth) {
                return this.blocks[(y * this.depth + z) * this.width + x] == 1;
            }
            else {
                return false;
            }
        }

        public bool isSolidTile(int x, int y, int z) {
            return this.isTile(x, y, z);
        }

        public void chunk(int x0, int y0, int z0, int x1, int y1, int z1) {
            for(int x = x0; x < x1; x++) {
                for(int y = y0; y < y1; y++) {
                    for(int z = z0; z < z1; z++) {
                        this.vertex(x, y, z);
                    }
                }
            }
        }

        public void vertex(int x, int y, int z) {
            float x0 = (float)x + 0.0f;
            float y0 = (float)y + 0.0f;
            float z0 = (float)z + 0.0f;

            float x1 = (float)x + 1.0f;
            float y1 = (float)y + 1.0f;
            float z1 = (float)z + 1.0f;

            // ..:: Negative X ::..
            if(!isSolidTile(x - 1, y, z)) {
                vertexBuffer.Add(new Vector3(x0, y0, z0));
                vertexBuffer.Add(new Vector3(x0, y1, z0));
                vertexBuffer.Add(new Vector3(x0, y1, z1));
                vertexBuffer.Add(new Vector3(x0, y0, z1));

                triangle();
                tex();
            }

            // ..:: Positive X ::..
            if(!isSolidTile(x + 1, y, z)) {
                vertexBuffer.Add(new Vector3(x1, y0, z1));
                vertexBuffer.Add(new Vector3(x1, y1, z1));
                vertexBuffer.Add(new Vector3(x1, y1, z0));
                vertexBuffer.Add(new Vector3(x1, y0, z0));

                triangle();
                tex();
            }

            // ..:: Negative Y ::..
            if(!isSolidTile(x, y - 1, z)) {
                vertexBuffer.Add(new Vector3(x0, y0, z0));
                vertexBuffer.Add(new Vector3(x0, y0, z1));
                vertexBuffer.Add(new Vector3(x1, y0, z1));
                vertexBuffer.Add(new Vector3(x1, y0, z0));

                triangle();
                tex();
            }

            // ..:: Positive Y ::..
            if(!isSolidTile(x, y + 1, z)) {
                vertexBuffer.Add(new Vector3(x0, y1, z1));
                vertexBuffer.Add(new Vector3(x0, y1, z0));
                vertexBuffer.Add(new Vector3(x1, y1, z0));
                vertexBuffer.Add(new Vector3(x1, y1, z1));

                triangle();
                tex();
            }

            // ..:: Negative Z ::..
            if(!isSolidTile(x, y, z - 1)) {
                vertexBuffer.Add(new Vector3(x1, y0, z0));
                vertexBuffer.Add(new Vector3(x1, y1, z0));
                vertexBuffer.Add(new Vector3(x0, y1, z0));
                vertexBuffer.Add(new Vector3(x0, y0, z0));

                triangle();
                tex();
            }

            // ..:: Positive Z ::..
            if(!isSolidTile(x, y, z + 1)) {
                vertexBuffer.Add(new Vector3(x0, y0, z1));
                vertexBuffer.Add(new Vector3(x0, y1, z1));
                vertexBuffer.Add(new Vector3(x1, y1, z1));
                vertexBuffer.Add(new Vector3(x1, y0, z1));

                triangle();
                tex();
            }
        }

        public void triangle() {
            // first triangle
            triangleBuffer.Add(0 + vertices);
            triangleBuffer.Add(1 + vertices);
            triangleBuffer.Add(2 + vertices);

            // second triangle
            triangleBuffer.Add(0 + vertices);
            triangleBuffer.Add(2 + vertices);
            triangleBuffer.Add(3 + vertices);

            vertices += 4;
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
