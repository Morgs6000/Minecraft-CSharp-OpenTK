using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace RubyDung {
    internal class FileName : GameWindow {
        public FileName()
            : base(GameWindowSettings.Default, NativeWindowSettings.Default) {
        }

        protected override void OnFramebufferResize(FramebufferResizeEventArgs e) {
            base.OnFramebufferResize(e);
        }

        protected override void OnUpdateFrame(FrameEventArgs args) {
            base.OnUpdateFrame(args);
        }

        protected override void OnLoad() {
            base.OnLoad();
        }

        protected override void OnRenderFrame(FrameEventArgs args) {
            base.OnRenderFrame(args);
        }
    }
}
