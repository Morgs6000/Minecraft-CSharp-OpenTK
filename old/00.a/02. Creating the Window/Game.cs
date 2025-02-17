using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace openTk_Minecraft_Clone_Tutorial_Series {
    internal class Game : GameWindow {
        // CONSTANTS
        private static int SCREENWIDTH;
        private static int SCREENHEIGHT;

        public Game(int width, int height) : base(GameWindowSettings.Default, NativeWindowSettings.Default) {
            // center the window on monitor
            this.CenterWindow(new Vector2i(width, height));

            SCREENWIDTH = width;
            SCREENHEIGHT = height;
        }
    }
}
