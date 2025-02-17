using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using RubyDung.src.level;
using RubyDung.src.phys;

namespace RubyDung.src;

public class Player {
    private Level level;

    public float xo;
    public float yo;
    public float zo;

    public float x;
    public float y;
    public float z;

    public float xd;
    public float yd;
    public float zd;

    private float xRot;          //pitch
    private float yRot = -90.0f; //yaw

    public AABB bb;

    public bool onGround = false;

    private Vector3 eye = new Vector3(0.0f, 0.0f, -3.0f);
    private Vector3 target = new Vector3(0.0f, 0.0f, 1.0f);
    private Vector3 up = new Vector3(0.0f, 1.0f, 0.0f);

    private Vector2 lastPos;

    private bool firstMouse = true;

    private float fov = 60.0f;

    public Player() {
        this.level = level;
        this.ResetPos();
    }

    private void ResetPos() {
        Random random = new Random();

        float x = (float)random.NextDouble() * (float)level.width;
        float y = (float)(level.depth + 10);
        float z = (float)random.NextDouble() * (float)level.height;

        SetPos(x, y, z);
    }

    private void SetPos(float x, float y, float z) {
        this.x = x;
        this.y = y;
        this.z = z;

        float w = 0.3f;
        float h = 0.9f;

        this.bb = new AABB(x - w, y - h, z - w, x + w, y + h, z + w);
    }

    private void Turn(float xo, float yo) {
        xRot = (float)((double)xRot - (double)xo * 0.15f);
        yRot = (float)((double)yRot + (double)yo * 0.15f);

        if(xRot < -89.0f) {
            xRot = -89.0f;
        }
        if(xRot > 89.0f) {
            xRot = 89.0f;
        }
    }

    public void Tick(GameWindow window) {
        xo = x;
        yo = y;
        zo = z;

        float xa = 0.0f;
        float ya = 0.0f;
        float za = 0.0f;

        if(window.KeyboardState.IsKeyDown(Keys.R)) {
            ResetPos();
        }

        if(window.KeyboardState.IsKeyDown(Keys.W)) {
            za++;
        }
        if(window.KeyboardState.IsKeyDown(Keys.S)) {
            za--;
        }
        if(window.KeyboardState.IsKeyDown(Keys.A)) {
            xa--;
        }
        if(window.KeyboardState.IsKeyDown(Keys.D)) {
            xa++;
        }

        if(window.KeyboardState.IsKeyDown(Keys.Space)) {
            ya++;
        }
        if(window.KeyboardState.IsKeyDown(Keys.LeftShift)) {
            ya--;
        }

        MoveRelative(xa, ya, onGround ? 0.02f : 0.005f);
        yd = (float)((double)yd - 0.005f);
        Move(xd, yd, zd);

        xd *= 0.91f;
        yd *= 0.98f;
        zd *= 0.91f;

        if(onGround) {
            xd *= 0.8f;
            zd *= 0.8f;
        }
    }

    public void Move(float xa, float ya, float za) {
        float xaOrg = xa;
        float yaOrg = ya;
        float zaOrg = za;
        List<AABB> aabbs = this.level.GetCubes(this.bb.Expand(xa, ya, za));

        for(int i = 0; i < aabbs.Count; ++i) {
            ya = aabbs[i].ClipYCollide(this.bb, ya);
        }

        this.bb.Move(0.0f, ya, 0.0f);

        for(int i = 0; i < aabbs.Count; ++i) {
            xa = aabbs[i].ClipXCollide(this.bb, xa);
        }

        this.bb.Move(xa, 0.0f, 0.0f);

        for(int i = 0; i < aabbs.Count; ++i) {
            za = aabbs[i].ClipZCollide(this.bb, za);
        }

        this.bb.Move(0.0f, 0.0f, za);
        this.onGround = yaOrg != ya && yaOrg < 0.0f;
        if(xaOrg != xa) {
            this.xd = 0.0f;
        }

        if(yaOrg != ya) {
            this.yd = 0.0f;
        }

        if(zaOrg != za) {
            this.zd = 0.0f;
        }

        this.x = (this.bb.x0 + this.bb.x1) / 2.0f;
        this.y = this.bb.y0 + 1.62f;
        this.z = (this.bb.z0 + this.bb.z1) / 2.0f;
    }

    public void MoveRelative(float xa, float za, float speed) {
        float dist = xa * xa + za * za;

        if(!(dist < 0.01f)) {
            dist = speed / (float)Math.Sqrt(dist);

            xa *= dist;
            za *= dist;

            float sin = (float)Math.Sin(this.yRot * Math.PI / 180.0);
            float cos = (float)Math.Cos(this.yRot * Math.PI / 180.0);

            this.xd += xa * cos - za * sin;
            this.zd += za * cos + xa * sin;
        }
    }

    public void ProcessInput(GameWindow window, FrameEventArgs args) {
        float speed = 4.317f;

        float xa = 0.0f;
        float ya = 0.0f;
        float za = 0.0f;

        if(window.KeyboardState.IsKeyDown(Keys.W)) {
            za++;
        }
        if(window.KeyboardState.IsKeyDown(Keys.S)) {
            za--;
        }
        if(window.KeyboardState.IsKeyDown(Keys.A)) {
            xa--;
        }
        if(window.KeyboardState.IsKeyDown(Keys.D)) {
            xa++;
        }

        if(window.KeyboardState.IsKeyDown(Keys.Space)) {
            ya++;
        }
        if(window.KeyboardState.IsKeyDown(Keys.LeftShift)) {
            ya--;
        }

        eye += x * Vector3.Normalize(Vector3.Cross(target, up)) * speed * (float)args.Time;
        eye += y * up * speed * (float)args.Time;
        eye += z * Vector3.Normalize(new Vector3(target.X, 0.0f, target.Z)) * speed * (float)args.Time;
    }

    public void MouseProcessInput(GameWindow window, FrameEventArgs args) {
        float scrollSensitivity = 2.0f;
        float dragSensitivity = 0.2f;

        // Movimento para frente e para trás com o scroll do mouse
        float scrollDelta = window.MouseState.ScrollDelta.Y;
        eye += target * scrollDelta * scrollSensitivity;

        // Movimento para a esquerda, direita, cima e baixo arrastando o mouse com o botão esquerdo pressionado
        if(window.MouseState.IsButtonDown(MouseButton.Left) || window.MouseState.IsButtonDown(MouseButton.Middle)) {
            float deltaX = window.MouseState.X - lastPos.X;
            float deltaY = window.MouseState.Y - lastPos.Y;

            eye -= Vector3.Normalize(Vector3.Cross(target, up)) * deltaX * dragSensitivity;
            eye += up * deltaY * dragSensitivity;
        }

        // Girar a câmera arrastando o mouse com o botão direito pressionado
        if(window.MouseState.IsButtonDown(MouseButton.Right)) {
            MouseCallback(window);
        }
        else {
            firstMouse = true;
        }

        lastPos = new Vector2(window.MouseState.X, window.MouseState.Y);
    }

    public void MouseCallback(GameWindow window) {
        float sensitivity = 0.2f;

        if(firstMouse) {
            lastPos = new Vector2(window.MouseState.X, window.MouseState.Y);
            firstMouse = false;
        }
        else {
            float deltaX = window.MouseState.X - lastPos.X;
            float deltaY = window.MouseState.Y - lastPos.Y;
            lastPos = new Vector2(window.MouseState.X, window.MouseState.Y);

            yRot += deltaX * sensitivity;
            xRot -= deltaY * sensitivity;

            if(xRot > 89.0f) {
                xRot = 89.0f;
            }
            if(xRot < -89.0f) {
                xRot = -89.0f;
            }
        }

        target.X = (float)Math.Cos(MathHelper.DegreesToRadians(xRot)) * (float)Math.Cos(MathHelper.DegreesToRadians(yRot));
        target.Y = (float)Math.Sin(MathHelper.DegreesToRadians(xRot));
        target.Z = (float)Math.Cos(MathHelper.DegreesToRadians(xRot)) * (float)Math.Sin(MathHelper.DegreesToRadians(yRot));
        target = Vector3.Normalize(target);
    }

    public void Render(Shader shader, float width, float height) {
        Matrix4 view = Matrix4.Identity;
        view *= Matrix4.CreateTranslation(0.0f, 0.0f, -10.0f);
        view *= Matrix4.LookAt(eye, eye + target, up);
        shader.SetMatrix4("view", view);

        Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(fov), width / height, 0.05f, 1000.0f);
        shader.SetMatrix4("projection", projection);
    }
}
