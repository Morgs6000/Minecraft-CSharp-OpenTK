//package com.mojang.rubydung.level;

//import java.nio.FloatBuffer;
//import org.lwjgl.BufferUtils;
//import org.lwjgl.opengl.GL11;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace com.Mojang.RubyDung.Level {
    //public class Tesselator {
    public class Tesselator {
    //    private static final int MAX_VERTICES = 100000;
    //    private FloatBuffer vertexBuffer = BufferUtils.createFloatBuffer(300000);
        //private float[] vertexArray;
        private List<Vector3> vertexList = new List<Vector3>();
        private List<int> triangleList = new List<int>();
    //    private FloatBuffer texCoordBuffer = BufferUtils.createFloatBuffer(200000);
        private List<Vector2> texCoordList = new List<Vector2>();
    //    private FloatBuffer colorBuffer = BufferUtils.createFloatBuffer(300000);
    //    private int vertices = 0;
        private int vertices = 0;
    //    private float u;
        private float u;
    //    private float v;
        private float v;
    //    private float r;
    //    private float g;
    //    private float b;
    //    private boolean hasColor = false;
    //    private boolean hasTexture = false;
        private bool hasTexture = false;

    //    public Tesselator() {
    //    }

    //    public void flush() {
        public void flush() {            
    //        this.vertexBuffer.flip();
    //        this.texCoordBuffer.flip();
    //        this.colorBuffer.flip();
    //        GL11.glVertexPointer(3, 0, this.vertexBuffer);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

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
            GL.DrawArrays(PrimitiveType.Triangles, 0, 3);
    //        GL11.glDisableClientState(32884);
    //        if(this.hasTexture) {
    //            GL11.glDisableClientState(32888);
    //        }

    //        if(this.hasColor) {
    //            GL11.glDisableClientState(32886);
    //        }

    //        this.clear();
    //    }
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
        public void tex(float u, float v) {
    //        this.hasTexture = true;
            this.hasTexture = true;
    //        this.u = u;
            this.u = u;
    //        this.v = v;
            this.v = v;
    //    }
        }

    //    public void color(float r, float g, float b) {
    //        this.hasColor = true;
    //        this.r = r;
    //        this.g = g;
    //        this.b = b;
    //    }

    //    public void vertex(float x, float y, float z) {
        public void vertex(float x, float y, float z) {
    //        this.vertexBuffer.put(this.vertices * 3 + 0, x).put(this.vertices * 3 + 1, y).put(this.vertices * 3 + 2, z);
            /*
            int vertexIndex = this.vertices * 3;
            this.vertexArray[vertexIndex] = x;
            this.vertexArray[vertexIndex + 1] = y;
            this.vertexArray[vertexIndex + 2] = z;
            */
            this.vertexList.Add(new Vector3(x, y, z));
    //        if(this.hasTexture) {
            if(this.hasTexture) {
                //            this.texCoordBuffer.put(this.vertices * 2 + 0, this.u).put(this.vertices * 2 + 1, this.v);
                this.texCoordList.Add(new Vector2(this.u, this.v));
    //        }
            }

    //        if(this.hasColor) {
    //            this.colorBuffer.put(this.vertices * 3 + 0, this.r).put(this.vertices * 3 + 1, this.g).put(this.vertices * 3 + 2, this.b);
    //        }

    //        ++this.vertices;
            this.vertices++;
    //        if(this.vertices == 100000) {
    //            this.flush();
    //        }

            // Verificar se a um conjunto completo de 4 vertices para formar um quadrado.
            if(this.vertices % 4 == 0) {
                // Calcula o indice inicial do conjunto de vertices.
                int startIndex = this.vertices - 4;

                // Adiciona os indices aos triangulos para formar um quadrado.
                // Primeiro Triangulo:
                this.triangleList.Add(0 + startIndex);
                this.triangleList.Add(1 + startIndex);
                this.triangleList.Add(2 + startIndex);
                // Segundo Triangulo:
                this.triangleList.Add(0 + startIndex);
                this.triangleList.Add(2 + startIndex);
                this.triangleList.Add(3 + startIndex);
            }
    //    }
        }

        public List<Vector3> GetVertices() {
            return vertexList;
        }

        public List<int> GetTriangles() {
            return triangleList;
        }
    //}
    }
}
