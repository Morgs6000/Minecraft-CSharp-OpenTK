//package com.mojang.rubydung.level;

//import java.nio.FloatBuffer;
//import org.lwjgl.BufferUtils;
//import org.lwjgl.opengl.GL11;

using OpenTK.Graphics.OpenGL4;

namespace RubyDung.src.level {
    //public class Tesselator {
    public class Tesselator {
    //    private static final int MAX_VERTICES = 100000;
    //    private FloatBuffer vertexBuffer = BufferUtils.createFloatBuffer(300000);
        private float[] vertexBuffer = new float[300000];
        private int[] triangleBuffer = new int[300000];
        //    private FloatBuffer texCoordBuffer = BufferUtils.createFloatBuffer(200000);
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
            GL.BufferData(BufferTarget.ArrayBuffer, this.vertexBuffer.Length * sizeof(float), this.vertexBuffer, BufferUsageHint.StreamDraw);

            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            // ..:: Element Buffer Object ::..
            EBO = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, EBO);
            GL.BufferData(BufferTarget.ElementArrayBuffer, this.triangleBuffer.Length * sizeof(int), this.triangleBuffer, BufferUsageHint.StreamDraw);

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
            GL.DrawElements(PrimitiveType.Triangles, triangleBuffer.Length, DrawElementsType.UnsignedInt, 0);
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
        public void vertex(float x, float y, float z) {
    //        this.vertexBuffer.put(this.vertices * 3 + 0, x).put(this.vertices * 3 + 1, y).put(this.vertices * 3 + 2, z);
            this.vertexBuffer[vertices * 3] = x;
            this.vertexBuffer[vertices * 3 + 1] = y;
            this.vertexBuffer[vertices * 3 + 2] = z;

    //        if(this.hasTexture) {
    //            this.texCoordBuffer.put(this.vertices * 2 + 0, this.u).put(this.vertices * 2 + 1, this.v);
    //        }

    //        if(this.hasColor) {
    //            this.colorBuffer.put(this.vertices * 3 + 0, this.r).put(this.vertices * 3 + 1, this.g).put(this.vertices * 3 + 2, this.b);
    //        }

    //        ++this.vertices;
            this.vertices++;
    //        if(this.vertices == 100000) {
    //            this.flush();
    //        }

            // Verificar se há um conjunto completo de 4 vértices para formar um quadrado
            if(this.vertices % 4 == 0) {
                // Adiciona os índices dos triângulos para formar um quadrado
                // Primeiro Triangulo
                this.triangleBuffer[0] = 0 + (this.vertices - 4);
                this.triangleBuffer[1] = 1 + (this.vertices - 4);
                this.triangleBuffer[2] = 2 + (this.vertices - 4);

                // Segundo Triangulo
                this.triangleBuffer[3] = 0 + (this.vertices - 4);
                this.triangleBuffer[4] = 2 + (this.vertices - 4);
                this.triangleBuffer[5] = 3 + (this.vertices - 4);
            }

    //    }
        } 
    //}
    }
}
