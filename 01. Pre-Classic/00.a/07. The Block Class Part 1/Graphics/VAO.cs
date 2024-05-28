using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace openTk_Minecraft_Clone_Tutorial_Series.Graphics {
    internal class VAO {
        public int ID;

        public VAO() {
            ID = GL.GenVertexArray();
            GL.BindVertexArray(ID);
        }

        public void LinkToVAO(int location, int size, VBO vbo) {
            Bind();
            vbo.Bind();
            GL.VertexAttribPointer(location, size, VertexAttribPointerType.Float, false, 0, 0);
            GL.EnableVertexAttribArray(location);
            UnBind();
        }

        public void Bind() {
            GL.BindVertexArray(ID);
        }

        public void UnBind() {
            GL.BindVertexArray(0);
        }

        public void Delete() {
            GL.DeleteVertexArray(ID);
        }
    }
}
