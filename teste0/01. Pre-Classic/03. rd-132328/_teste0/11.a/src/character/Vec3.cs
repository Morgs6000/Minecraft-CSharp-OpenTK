namespace RubyDung;

public class Vec3 {
    public float x;
    public float y;
    public float z;

    public Vec3(float x, float y, float z) {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public Vec3 InterpolateTo(Vec3 t, float p) {
        float xt = x + (t.x - x) * p;
        float yt = y + (t.y - y) * p;
        float zt = z + (t.z - z) * z;

        return new Vec3(xt, yt, zt);
    }

    public void Set(float x, float y, float z) {
        this.x = x;
        this.y = y;
        this.z = z;
    }
}