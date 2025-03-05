using OpenTK.Mathematics;
using OpenTK.Windowing.Desktop;

namespace RubyDung;

public class Entity {
    private Level level;

    // Variaveis da Camera
    public Vector3 position   = Vector3.Zero;

    public Vector3 horizontal = Vector3.UnitX;
    public Vector3 vertical   = Vector3.UnitY;
    public Vector3 direction  = Vector3.UnitZ;

    public Entity(Level level) {
        this.level = level;
        ResetPos();
    }

    public virtual void OnLoad(GameWindow window) {

    }

    public virtual void OnUpdateFrame(GameWindow window) {

    }

    public void ResetPos() {
        Random random = new Random();

        float x = (float)random.NextDouble() * (float)level.width;
        float y = (float)(level.height + 10);
        float z = (float)random.NextDouble() * (float)level.depth;

        SetPos(x, y, z);
    }

    protected void SetPos(float x, float y, float z) {
        position.X = x;
        position.Y = y;
        position.Z = z;
    }
}