using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace openTk_Minecraft_Clone_Tutorial_Series.World {
    internal class Block {
        public Vector3 position;

        private Dictionary<Faces, FaceData> faces;

        List<Vector2> dirtUV = new List<Vector2>() {
            new Vector2(0.0f, 1.0f),
            new Vector2(1.0f, 1.0f),
            new Vector2(1.0f, 0.0f),
            new Vector2(0.0f, 0.0f)
        };

        public Block(Vector3 position) {
            this.position = position;

            faces = new Dictionary<Faces, FaceData>() {
                {
                    Faces.FRONT, new FaceData {
                        vertices = AddTransformdVertices(FaceDataRaw.rawVertexData[Faces.FRONT]),
                        uv = dirtUV
                    }
                },
                {
                    Faces.BACK, new FaceData {
                        vertices = AddTransformdVertices(FaceDataRaw.rawVertexData[Faces.BACK]),
                        uv = dirtUV
                    }
                },
                {
                    Faces.LEFT, new FaceData {
                        vertices = AddTransformdVertices(FaceDataRaw.rawVertexData[Faces.LEFT]),
                        uv = dirtUV
                    }
                },
                {
                    Faces.RIGHT, new FaceData {
                        vertices = AddTransformdVertices(FaceDataRaw.rawVertexData[Faces.RIGHT]),
                        uv = dirtUV
                    }
                },
                {
                    Faces.TOP, new FaceData {
                        vertices = AddTransformdVertices(FaceDataRaw.rawVertexData[Faces.TOP]),
                        uv = dirtUV
                    }
                },
                {
                    Faces.BOTTOM, new FaceData {
                        vertices = AddTransformdVertices(FaceDataRaw.rawVertexData[Faces.BOTTOM]),
                        uv = dirtUV
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
