//package com.mojang.rubydung.level;

//import java.nio.FloatBuffer;
//import org.lwjgl.BufferUtils;
//import org.lwjgl.opengl.GL11;

using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace RubyDung.src.level {
    //public class Tesselator {
    public class Tesselator {
    //    private static final int MAX_VERTICES = 100000;
    //    private FloatBuffer vertexBuffer = BufferUtils.createFloatBuffer(300000);
        private List<Vector3> vertexBuffer = new List<Vector3>();
        private List<int> triangleBuffer = new List<int>();
    //    private FloatBuffer texCoordBuffer = BufferUtils.createFloatBuffer(200000);
        private List<Vector2> texCoordBuffer = new List<Vector2>();
    //    private FloatBuffer colorBuffer = BufferUtils.createFloatBuffer(300000);
    //    private int vertices = 0;
        private int vertices = 0;
    //    private float u;
    //    private float v;
    //    private float r;
    //    private float g;
    //    private float b;
    //    private boolean hasColor = false;
    //    private boolean hasTexture = false;

        int VAO; // Vertex Array Object
        int VBO; // Vertex Buffer Object
        int TBO; // Texture Buffer Object
        int EBO; // Element Buffer Object

    //    public Tesselator() {
    //    }

    //    public void flush() {
        public void flush() {
            // ..:: Vertex Array Object ::..
            VAO = GL.GenVertexArray();
            GL.BindVertexArray(VAO);

            // ..:: Vertex Buffer Object ::..
            VBO = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, VBO);
            GL.BufferData(BufferTarget.ArrayBuffer, this.vertexBuffer.Count * Vector3.SizeInBytes, this.vertexBuffer.ToArray(), BufferUsageHint.StreamDraw);

            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            // ..:: Texture Buffer Object ::..

            TBO = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, TBO);
            GL.BufferData(BufferTarget.ArrayBuffer, this.texCoordBuffer.Count * Vector2.SizeInBytes, this.texCoordBuffer.ToArray(), BufferUsageHint.StreamDraw);

            GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 2 * sizeof(float), 0);
            GL.EnableVertexAttribArray(1);

            // ..:: Element Buffer Object ::..
            EBO = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, EBO);
            GL.BufferData(BufferTarget.ElementArrayBuffer, this.triangleBuffer.Count * sizeof(int), this.triangleBuffer.ToArray(), BufferUsageHint.StreamDraw);

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
            //    }
        }

        public void use() {            
            GL.BindVertexArray(VAO);
            //GL.DrawArrays(PrimitiveType.Triangles, 0, 3);
            GL.DrawElements(PrimitiveType.Triangles, this.triangleBuffer.Count, DrawElementsType.UnsignedInt, 0);
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
            //        this.u = u;
            //        this.v = v;

            this.texCoordBuffer.Add(new Vector2(u, v));
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
            this.vertexBuffer.Add(new Vector3(x, y, z));

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

        public void triangle() {
            // Primeiro Triangulo
            this.triangleBuffer.Add(0 + this.vertices);
            this.triangleBuffer.Add(1 + this.vertices);
            this.triangleBuffer.Add(2 + this.vertices);

            // Segundo Triangulo
            this.triangleBuffer.Add(0 + this.vertices);
            this.triangleBuffer.Add(2 + this.vertices);
            this.triangleBuffer.Add(3 + this.vertices);

            this.vertices += 4;
        }
    //}
    }
}
