using OpenTK.Mathematics;
using openTk_Minecraft_Clone_Tutorial_Series.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace openTk_Minecraft_Clone_Tutorial_Series.World {
    internal class Block {
        public Vector3 position;

        public BlockType type;

        private Dictionary<Faces, FaceData> faces;

        public Dictionary<Faces, List<Vector2>> blockUV = new Dictionary<Faces, List<Vector2>>() {
            {
                Faces.FRONT, new List<Vector2>()
            },
            {
                Faces.BACK, new List<Vector2>()
            },
            {
                Faces.LEFT, new List<Vector2>()
            },
            {
                Faces.RIGHT, new List<Vector2>()
            },
            {
                Faces.TOP, new List<Vector2>()
            },
            {
                Faces.BOTTOM, new List<Vector2>()
            }
        };

        public Dictionary<Faces, List<Vector2>> GetUVsFromCoordinates(Dictionary<Faces, Vector2> coords) {
            
        }

        public Block(Vector3 position, BlockType blockType = BlockType.EMPTY) {
            type = blockType;

            this.position = position;

            if(type == BlockType.DIRT) { 

            }
            else if(blockType != BlockType.EMPTY) {
                blockUV = TextureData.blockTypesUVs[blockType];
            }

            faces = new Dictionary<Faces, FaceData>() {
                {
                    Faces.FRONT, new FaceData {
                        vertices = AddTransformdVertices(FaceDataRaw.rawVertexData[Faces.FRONT]),
                        uv = blockUV[Faces.FRONT]
                    }
                },
                {
                    Faces.BACK, new FaceData {
                        vertices = AddTransformdVertices(FaceDataRaw.rawVertexData[Faces.BACK]),
                        uv = blockUV[Faces.BACK]
                    }
                },
                {
                    Faces.LEFT, new FaceData {
                        vertices = AddTransformdVertices(FaceDataRaw.rawVertexData[Faces.LEFT]),
                        uv = blockUV[Faces.LEFT]
        }
                },
                {
                    Faces.RIGHT, new FaceData {
                        vertices = AddTransformdVertices(FaceDataRaw.rawVertexData[Faces.RIGHT]),
                        uv = blockUV[Faces.RIGHT]
                    }
                },
                {
                    Faces.TOP, new FaceData {
                        vertices = AddTransformdVertices(FaceDataRaw.rawVertexData[Faces.TOP]),
                        uv = blockUV[Faces.TOP]
}
                },
                {
                    Faces.BOTTOM, new FaceData {
                        vertices = AddTransformdVertices(FaceDataRaw.rawVertexData[Faces.BOTTOM]),
                        uv = blockUV[Faces.BOTTOM]
}
                }
            };
        }

        public List<Vector3> AddTransformdVertices(List<Vector3> vertices) {
            List<Vector3> transformedVertices = new List<Vector3>();

            foreach(var vert in vertices) {
                transformedVertices.Add(vert + position);
            }

            return transformedVertices;
        }

        public FaceData GetFace(Faces face) {
            return faces[face];
        }
    }
}
