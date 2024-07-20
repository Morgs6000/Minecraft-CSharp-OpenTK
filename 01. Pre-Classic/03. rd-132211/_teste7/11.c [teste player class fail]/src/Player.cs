using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace RubyDung.src;

public class Player {
    public void tick(KeyboardState input, float deltaTime, Vector3 eye, Vector3 target, Vector3 up) {
        float xa = 0.0f;
        float ya = 0.0f;
        float za = 0.0f;

        float speed = 4.317f * deltaTime;

        if(input.IsKeyDown(Keys.W)) {
            za++;
        }
        if(input.IsKeyDown(Keys.S)) {
            za--;
        }
        if(input.IsKeyDown(Keys.A)) {
            xa++;
        }
        if(input.IsKeyDown(Keys.D)) {
            xa--;
        }

        if(input.IsKeyDown(Keys.Space)) {
            ya++;
        }
        if(input.IsKeyDown(Keys.LeftShift)) {
            ya--;
        }

        eye += xa * Vector3.Normalize(Vector3.Cross(-target, up)) * speed;
        eye += ya * up * speed;
        eye += za * Vector3.Normalize(new Vector3(target.X, 0.0f, target.Z)) * speed;
    }
}
