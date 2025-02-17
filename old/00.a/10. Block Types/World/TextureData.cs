using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace openTk_Minecraft_Clone_Tutorial_Series.World {
    internal static class TextureData {
        public static Dictionary<BlockType, Dictionary<Faces, Vector2>> blockTypeUVCoord = new Dictionary<BlockType, Dictionary<Faces, Vector2>>() {
            {
                BlockType.DIRT, new Dictionary<Faces, Vector2>() {
                    {
                        Faces.FRONT, new Vector2(2, 15)
                    },
                    {
                        Faces.LEFT, new Vector2(2, 15)
                    },
                    {
                        Faces.RIGHT, new Vector2(2, 15)
                    },
                    {
                        Faces.BACK, new Vector2(2, 15)
                    },
                    {
                        Faces.TOP, new Vector2(2, 15)
                    },
                    {
                        Faces.BACK, new Vector2(2, 15)
                    }
                }
            }
        };

        public static readonly Dictionary<BlockType, Dictionary<Faces, List<Vector2>>> blockTypesUVs = new Dictionary<BlockType, Dictionary<Faces, List<Vector2>>>() {
            {
                BlockType.DIRT, new Dictionary<Faces, List<Vector2>>() {
                    {
                        Faces.FRONT, new List<Vector2>() {
                            new Vector2(2.0f / 16.0f, 15.0f / 16.0f),
                            new Vector2(3.0f / 16.0f, 15.0f / 16.0f),
                            new Vector2(3.0f / 16.0f, 1.0f),
                            new Vector2(2.0f / 16.0f, 1.0f)
                        }
                    },
                    {
                        Faces.BACK, new List<Vector2>() {
                            new Vector2(2.0f / 16.0f, 15.0f / 16.0f),
                            new Vector2(3.0f / 16.0f, 15.0f / 16.0f),
                            new Vector2(3.0f / 16.0f, 1.0f),
                            new Vector2(2.0f / 16.0f, 1.0f)
                        }
                    },
                    {
                        Faces.LEFT, new List<Vector2>() {
                            new Vector2(2.0f / 16.0f, 15.0f / 16.0f),
                            new Vector2(3.0f / 16.0f, 15.0f / 16.0f),
                            new Vector2(3.0f / 16.0f, 1.0f),
                            new Vector2(2.0f / 16.0f, 1.0f)
                        }
                    },
                    {
                        Faces.RIGHT, new List<Vector2>() {
                            new Vector2(2.0f / 16.0f, 15.0f / 16.0f),
                            new Vector2(3.0f / 16.0f, 15.0f / 16.0f),
                            new Vector2(3.0f / 16.0f, 1.0f),
                            new Vector2(2.0f / 16.0f, 1.0f)
                        }
                    },
                    {
                        Faces.TOP, new List<Vector2>() {
                            new Vector2(2.0f / 16.0f, 15.0f / 16.0f),
                            new Vector2(3.0f / 16.0f, 15.0f / 16.0f),
                            new Vector2(3.0f / 16.0f, 1.0f),
                            new Vector2(2.0f / 16.0f, 1.0f)
                        }
                    },
                    {
                        Faces.BOTTOM, new List<Vector2>() {
                            new Vector2(2.0f / 16.0f, 15.0f / 16.0f),
                            new Vector2(3.0f / 16.0f, 15.0f / 16.0f),
                            new Vector2(3.0f / 16.0f, 1.0f),
                            new Vector2(2.0f / 16.0f, 1.0f)
                        }
                    }
                }
            },
            {
                BlockType.GRASS, new Dictionary<Faces, List<Vector2>>() {
                    {
                        Faces.FRONT, new List<Vector2>() {
                            new Vector2(4.0f / 16.0f, 1.0f),
                            new Vector2(3.0f / 16.0f, 1.0f),
                            new Vector2(3.0f / 16.0f, 15.0f / 16.0f),
                            new Vector2(4.0f / 16.0f, 15.0f / 16.0f)
                        }
                    },
                    {
                        Faces.BACK, new List<Vector2>() {
                            new Vector2(4.0f / 16.0f, 1.0f),
                            new Vector2(3.0f / 16.0f, 1.0f),
                            new Vector2(3.0f / 16.0f, 15.0f / 16.0f),
                            new Vector2(4.0f / 16.0f, 15.0f / 16.0f)
                        }
                    },
                    {
                        Faces.LEFT, new List<Vector2>() {
                            new Vector2(4.0f / 16.0f, 1.0f),
                            new Vector2(3.0f / 16.0f, 1.0f),
                            new Vector2(3.0f / 16.0f, 15.0f / 16.0f),
                            new Vector2(4.0f / 16.0f, 15.0f / 16.0f)
                        }
                    },
                    {
                        Faces.RIGHT, new List<Vector2>() {
                            new Vector2(4.0f / 16.0f, 1.0f),
                            new Vector2(3.0f / 16.0f, 1.0f),
                            new Vector2(3.0f / 16.0f, 15.0f / 16.0f),
                            new Vector2(4.0f / 16.0f, 15.0f / 16.0f)
                        }
                    },
                    {
                        Faces.TOP, new List<Vector2>() {
                            new Vector2(8.0f / 16.0f, 14.0f / 16.0f),
                            new Vector2(7.0f / 16.0f, 14.0f / 16.0f),
                            new Vector2(7.0f / 16.0f, 13.0f / 16.0f),
                            new Vector2(8.0f / 16.0f, 13.0f / 16.0f)
                        }
                    },
                    {
                        Faces.BOTTOM, new List<Vector2>() {
                            new Vector2(2.0f / 16.0f, 15.0f / 16.0f),
                            new Vector2(3.0f / 16.0f, 15.0f / 16.0f),
                            new Vector2(3.0f / 16.0f, 1.0f),
                            new Vector2(2.0f / 16.0f, 1.0f)
                        }
                    }
                }
            }
        };
    }
}
