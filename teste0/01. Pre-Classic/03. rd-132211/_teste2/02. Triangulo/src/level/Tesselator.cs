using OpenTK.Graphics.OpenGL;

namespace RubyDung.src.level;

//public class Tesselator {
public class Tesselator {
//    private static final int MAX_VERTICES = 100000;
//    private FloatBuffer vertexBuffer = BufferUtils.createFloatBuffer(300000);
    private List<float> vertexBuffer = new List<float>();
//    private FloatBuffer texCoordBuffer = BufferUtils.createFloatBuffer(200000);
//    private FloatBuffer colorBuffer = BufferUtils.createFloatBuffer(300000);
//    private int vertices = 0;
//    private float u;
//    private float v;
//    private float r;
//    private float g;
//    private float b;
//    private boolean hasColor = false;
//    private boolean hasTexture = false;

    private int vertexArrayObject;
    private int vertexBufferObject;

//    public Tesselator() {
//    }

//    public void flush() {
    public void Flush() {
//        this.vertexBuffer.flip();
//        this.texCoordBuffer.flip();
//        this.colorBuffer.flip();
//        GL11.glVertexPointer(3, 0, this.vertexBuffer);
//        if(this.hasTexture) {
//            GL11.glTexCoordPointer(2, 0, this.texCoordBuffer);
//        }

//        if(this.hasColor) {
//            GL11.glColorPointer(3, 0, this.colorBuffer);
//        }

//        GL11.glEnableClientState(32884);
//        if(this.hasTexture) {
//            GL11.glEnableClientState(32888);
//        }

//        if(this.hasColor) {
//            GL11.glEnableClientState(32886);
//        }

//        GL11.glDrawArrays(7, 0, this.vertices);
//        GL11.glDisableClientState(32884);
//        if(this.hasTexture) {
//            GL11.glDisableClientState(32888);
//        }

//        if(this.hasColor) {
//            GL11.glDisableClientState(32886);
//        }

//        this.clear();

        vertexArrayObject = GL.GenVertexArray();
        GL.BindVertexArray(vertexArrayObject);

        vertexBufferObject = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferObject);
        GL.BufferData(BufferTarget.ArrayBuffer, vertexBuffer.Count * sizeof(float), vertexBuffer.ToArray(), BufferUsageHint.StaticDraw);

        GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
        GL.EnableVertexAttribArray(0);
//    }
    }

    public void Render() {
        GL.BindVertexArray(vertexArrayObject);
        GL.DrawArrays(PrimitiveType.Triangles, 0, 3);
    }

//    private void clear() {
//        this.vertices = 0;
//        this.vertexBuffer.clear();
//        this.texCoordBuffer.clear();
//        this.colorBuffer.clear();
//    }

//    public void init() {
//        this.clear();
//        this.hasColor = false;
//        this.hasTexture = false;
//    }

//    public void tex(float u, float v) {
//        this.hasTexture = true;
//        this.u = u;
//        this.v = v;
//    }

//    public void color(float r, float g, float b) {
//        this.hasColor = true;
//        this.r = r;
//        this.g = g;
//        this.b = b;
//    }

//    public void vertex(float x, float y, float z) {
    public void Vertex(float x, float y, float z) {
//        this.vertexBuffer.put(this.vertices * 3 + 0, x).put(this.vertices * 3 + 1, y).put(this.vertices * 3 + 2, z);
        vertexBuffer.Add(x);
        vertexBuffer.Add(y);
        vertexBuffer.Add(z);
//        if(this.hasTexture) {
//            this.texCoordBuffer.put(this.vertices * 2 + 0, this.u).put(this.vertices * 2 + 1, this.v);
//        }

//        if(this.hasColor) {
//            this.colorBuffer.put(this.vertices * 3 + 0, this.r).put(this.vertices * 3 + 1, this.g).put(this.vertices * 3 + 2, this.b);
//        }

//        ++this.vertices;
//        if(this.vertices == 100000) {
//            this.flush();
//        }

//    }
    }
//}
}
