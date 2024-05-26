using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace com {
    internal class FileName : GameWindow {
        public FileName() 
            : base(GameWindowSettings.Default, new NativeWindowSettings() {
                ClientSize = (1024, 768),
                Title = "Game"
            }) {
        }

        protected override void OnUpdateFrame(FrameEventArgs args) {
            base.OnUpdateFrame(args);
        }
    }
}
