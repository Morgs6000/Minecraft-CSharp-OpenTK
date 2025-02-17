using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace openTk_Minecraft_Clone_Tutorial_Series.Graphics {
    internal class IBO {
        public int ID;

        public IBO(List<uint> data) {
            ID = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ID);
            GL.BufferData(BufferTarget.ElementArrayBuffer, data.Count * sizeof(uint), data.ToArray(), BufferUsageHint.StreamDraw);
        }

        public void Bind() {
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ID);
        }

        public void UnBind() {
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
        }

        public void Delete() {
            GL.DeleteBuffer(ID);
        }
    }
}
