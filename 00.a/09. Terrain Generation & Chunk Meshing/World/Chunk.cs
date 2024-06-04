using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using openTk_Minecraft_Clone_Tutorial_Series.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace openTk_Minecraft_Clone_Tutorial_Series.World {
    internal class Chunk {
        private List<Vector3> chunkVerts;
        private List<Vector2> chuvkUVS;
        private List<uint> chunkIndices;

        const int SIZE = 16;
        const int HEIGHT = 32;

        public Vector3 position;

        private uint indexCount;

        VAO chunkVAO;
        VBO chunkVertexVBO;
        VBO chunkUVVBO;
        IBO chunkIBO;

        Texture texture;

        Block[,,] chunkBlocks = new Block[SIZE, HEIGHT, SIZE];

        public Chunk(Vector3 position) {
            this.position = position;

            chunkVerts = new List<Vector3>();
            chuvkUVS = new List<Vector2>();
            chunkIndices = new List<uint>();

            float[,] heightmap = GenChunk();

            GenBlocks(heightmap);
            GenFaces(heightmap);
            BuildChunk();
        }

        public float[,] GenChunk() { // generate the data
            float[,] heightmap = new float[SIZE, SIZE];

            SimplexNoise.Noise.Seed = 123456;

            for(int x = 0; x < SIZE; x++) {
                for(int z = 0; z < SIZE; z++) {
                    heightmap[x, z] = SimplexNoise.Noise.CalcPixel2D(x, z, 0.01f);
                }
            }

            return heightmap;
        }

        public void GenBlocks(float[,] heightmap) { // generate the appropriate block faces given the data
            for(int x = 0; x < SIZE; x++) {
                for(int z = 0; z < SIZE; z++) {
                    int columnHeight = (int)(heightmap[x, z] / 10);

                    for(int y = 0; y < HEIGHT; y++) {
                        if(y < columnHeight) {
                            chunkBlocks[x, y, z] = new Block(new Vector3(x, y, z), BlockType.DIRT);
                        }
                        else {
                            chunkBlocks[x, y, z] = new Block(new Vector3(x, y, z), BlockType.EMPTY);
                        }
                    }
                }
            }
        }

        public void GenFaces(float[,] heightmap) {
            for(int x = 0; x < SIZE; x++) {
                for(int z = 0; z < SIZE; z++) {
                    int columnHeight = (int)(heightmap[x, z] / 10);

                    for(int y = 0; y < columnHeight; y++) {
                        // left faces
                        // qualification: block to left is empty, is not farthest left in chunk
                        int numFaces = 0;

                        if(x > 0) {
                            if(chunkBlocks[x - 1, y, z].type == BlockType.EMPTY) {
                                IntegrateFace(chunkBlocks[x, y, z], Faces.LEFT);
                                numFaces++;
                            }
                        }
                        else {
                            IntegrateFace(chunkBlocks[x, y, z], Faces.LEFT);
                            numFaces++;
                        }
                        // right faces
                        // qualification: block to right is empty, is farthest right in chunk
                        if(x < SIZE - 1) {
                            if(chunkBlocks[x + 1, y, z].type == BlockType.EMPTY) {
                                IntegrateFace(chunkBlocks[x, y, z], Faces.RIGHT);
                                numFaces++;
                            }
                        }
                        else {
                            IntegrateFace(chunkBlocks[x, y, z], Faces.RIGHT);
                            numFaces++;
                        }
                        // top faces
                        // qualification: block above is empty, is farthest up in chunk
                        if(y < columnHeight - 1) {
                            if(chunkBlocks[x , y + 1, z].type == BlockType.EMPTY) {
                                IntegrateFace(chunkBlocks[x, y, z], Faces.TOP);
                                numFaces++;
                            }
                        }
                        else {
                            IntegrateFace(chunkBlocks[x, y, z], Faces.TOP);
                            numFaces++;
                        }
                        // bottom faces
                        // qualification: block below is empty, is farthest down in chunk
                        if(y > 0) {
                            if(chunkBlocks[x, y - 1, z].type == BlockType.EMPTY) {
                                IntegrateFace(chunkBlocks[x, y, z], Faces.BOTTOM);
                                numFaces++;
                            }
                        }
                        else {
                            IntegrateFace(chunkBlocks[x, y, z], Faces.BOTTOM);
                            numFaces++;
                        }
                        // front faces
                        if(z < SIZE - 1) {
                            if(chunkBlocks[x, y, z + 1].type == BlockType.EMPTY) {
                                IntegrateFace(chunkBlocks[x, y, z], Faces.FRONT);
                                numFaces++;
                            }
                        }
                        else {
                            IntegrateFace(chunkBlocks[x, y, z], Faces.FRONT);
                            numFaces++;
                        }
                        // back faces
                        if(z > 0) {
                            if(chunkBlocks[x, y, z - 1].type == BlockType.EMPTY) {
                                IntegrateFace(chunkBlocks[x, y, z], Faces.BACK);
                                numFaces++;
                            }
                        }
                        else {
                            IntegrateFace(chunkBlocks[x, y, z], Faces.BACK);
                            numFaces++;
                        }

                        AddIndices(numFaces);
                    }
                }
            }
        }

        public void IntegrateFace(Block block, Faces face) {
            var faceData = block.GetFace(face);

            chunkVerts.AddRange(faceData.vertices);
            chuvkUVS.AddRange(faceData.uv);
        }

        public void AddIndices(int amtFaces) {
            for(int i = 0; i < amtFaces; i++) {
                chunkIndices.Add(0 + indexCount);
                chunkIndices.Add(1 + indexCount);
                chunkIndices.Add(2 + indexCount);
                chunkIndices.Add(2 + indexCount);
                chunkIndices.Add(3 + indexCount);
                chunkIndices.Add(0 + indexCount);

                indexCount += 4;
            }
        }

        public void BuildChunk() { // take data and process it for rendering
            chunkVAO = new VAO();
            chunkVAO.Bind();

            chunkVertexVBO = new VBO(chunkVerts);
            chunkVertexVBO.Bind();
            chunkVAO.LinkToVAO(0, 3, chunkVertexVBO);

            chunkUVVBO = new VBO(chuvkUVS);
            chunkUVVBO.Bind();
            chunkVAO.LinkToVAO(1, 2, chunkUVVBO);

            chunkIBO = new IBO(chunkIndices);

            texture = new Texture("dirt.png");
        }

        public void Render(ShaderProgram program) { // drawing the chunk
            program.Bind();
            chunkVAO.Bind();
            chunkIBO.Bind();
            texture.Bind();
            GL.DrawElements(PrimitiveType.Triangles, chunkIndices.Count, DrawElementsType.UnsignedInt, 0);
        }

        public void Delete() {
            chunkVAO.Delete();
            chunkVertexVBO.Delete();
            chunkUVVBO.Delete();
            chunkIBO.Delete();
            texture.Delete();
        }
    }
}
