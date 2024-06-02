//package com.mojang.rubydung;

//import com.mojang.rubydung.level.Level;
//import com.mojang.rubydung.phys.AABB;
//import java.util.List;
//import org.lwjgl.input.Keyboard;

using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace RubyDung.src {
    //public class Player {
    public class Player {
    //    private Level level;
    //    public float xo;
    //    public float yo;
    //    public float zo;
    //    public float x;
    //    public float y;
    //    public float z;
    //    public float xd;
    //    public float yd;
    //    public float zd;
    //    public float yRot;
        public float xRot = -90.0f;
    //    public float xRot;
        public float yRot;
    //    public AABB bb;
    //    public boolean onGround = false;

        Vector3 cameraPos = new Vector3(0.0f, 0.0f, 0.3f);
        Vector3 cameraFront = new Vector3(0.0f, 0.0f, -1.0f);
        Vector3 cameraUp = new Vector3(0.0f, 1.0f, 0.0f);

        float deltaTime = 0.0f;
        float lastFrame = 0.0f;

        float lastX = 400.0f;
        float lastY = 300.0f;

        bool firstMouse = true;

    //    public Player(Level level) {
    //        this.level = level;
    //        this.resetPos();
    //    }

    //    private void resetPos() {
    //        float x = (float)Math.random() * (float)this.level.width;
    //        float y = (float)(this.level.depth + 10);
    //        float z = (float)Math.random() * (float)this.level.height;
    //        this.setPos(x, y, z);
    //    }

    //    private void setPos(float x, float y, float z) {
    //        this.x = x;
    //        this.y = y;
    //        this.z = z;
    //        float w = 0.3F;
    //        float h = 0.9F;
    //        this.bb = new AABB(x - w, y - h, z - w, x + w, y + h, z + w);
    //    }

        public void use(Shaders shaders, int width, int height) {
            // ..:: Model ::..
            Matrix4 model = Matrix4.Identity;
            model = Matrix4.CreateTranslation(0.0f, 0.0f, -2.0f);

            int modelLoc = GL.GetUniformLocation(shaders.ID, "model");
            GL.UniformMatrix4(modelLoc, false, ref model);

            // ..:: View ::..
            Matrix4 view = Matrix4.Identity;
            view = Matrix4.LookAt(this.cameraPos, this.cameraPos + this.cameraFront, this.cameraUp);

            int viewLoc = GL.GetUniformLocation(shaders.ID, "view");
            GL.UniformMatrix4(viewLoc, false, ref view);

            // ..:: Projection ::..
            Matrix4 projection;
            projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45.0f), width / height, 0.1f, 100.0f);

            int projectionLoc = GL.GetUniformLocation(shaders.ID, "projection");
            GL.UniformMatrix4(projectionLoc, false, ref projection);
        }

        public void time() {
            float currentFrame = (float)GLFW.GetTime();
            deltaTime = currentFrame - lastFrame;
            lastFrame = currentFrame;
        }

        public void zBuffer() {
            GL.Enable(EnableCap.DepthTest);

            GL.Enable(EnableCap.CullFace);
            GL.CullFace(CullFaceMode.Front);
        }

    //    public void turn(float xo, float yo) {
        public void turn(float xo, float yo) {
            if(this.firstMouse) {
                this.lastX = xo;
                this.lastY = yo;
                this.firstMouse = false;
            }

            float xoffset = xo - this.lastX;
            float yoffset = yo - this.lastY;
            this.lastX = xo;
            this.lastY = yo;

            float sensitivity = 0.1f;
            xoffset *= sensitivity;
            yoffset *= sensitivity;

    //        this.yRot = (float)((double)this.yRot + (double)xo * 0.15);
            this.xRot += xoffset;
    //        this.xRot = (float)((double)this.xRot - (double)yo * 0.15);
            this.yRot -= yoffset;

    //        if(this.xRot < -90.0F) {
            if(this.yRot < -90.0f) {
    //            this.xRot = -90.0F;
                this.yRot = -90.0f;
    //        }
            }
    //        if(this.xRot > 90.0F) {
            if(this.yRot > 90.0f) {
    //            this.xRot = 90.0F;
                this.yRot = 90.0f;
    //        }
            }

            Vector3 direction;
            direction.X = (float)Math.Cos(MathHelper.DegreesToRadians(this.xRot)) * (float)Math.Cos(MathHelper.DegreesToRadians(this.yRot));
            direction.Y = (float)Math.Sin(MathHelper.DegreesToRadians(this.yRot));
            direction.Z = (float)Math.Sin(MathHelper.DegreesToRadians(this.xRot)) * (float)Math.Cos(MathHelper.DegreesToRadians(this.yRot));
            this.cameraFront = Vector3.Normalize(direction);
    //    }
        }

    //    public void tick() {
        public void tick(KeyboardState input) {
    //        this.xo = this.x;
    //        this.yo = this.y;
    //        this.zo = this.z;
    //        float xa = 0.0F;
            float xa = 0.0f;
    //        float ya = 0.0F;
            float ya = 0.0f;
            float za = 0.0f;

            float cameraSpeed = 4.317f * this.deltaTime;

    //        if(Keyboard.isKeyDown(19)) {
    //            this.resetPos();
    //        }

    //        if(Keyboard.isKeyDown(200) || Keyboard.isKeyDown(17)) {
            if(input.IsKeyDown(Keys.W)) {
    //            --ya;
                this.cameraPos += Vector3.Normalize(new Vector3(this.cameraFront.X, 0, this.cameraFront.Z)) * cameraSpeed;
    //        }
            }

    //        if(Keyboard.isKeyDown(208) || Keyboard.isKeyDown(31)) {
            if(input.IsKeyDown(Keys.S)) {
    //            ++ya;
                this.cameraPos -= Vector3.Normalize(new Vector3(this.cameraFront.X, 0, this.cameraFront.Z)) * cameraSpeed;
    //        }
            }

    //        if(Keyboard.isKeyDown(203) || Keyboard.isKeyDown(30)) {
            if(input.IsKeyDown(Keys.A)) {
    //            --xa;
                this.cameraPos -= Vector3.Normalize(Vector3.Cross(this.cameraFront, this.cameraUp)) * cameraSpeed;
    //        }
            }

    //        if(Keyboard.isKeyDown(205) || Keyboard.isKeyDown(32)) {
            if(input.IsKeyDown(Keys.D)) {
    //            ++xa;
                this.cameraPos += Vector3.Normalize(Vector3.Cross(this.cameraFront, this.cameraUp)) * cameraSpeed;
    //        }
            }

    //        if((Keyboard.isKeyDown(57) || Keyboard.isKeyDown(219)) && this.onGround) {
            if(input.IsKeyDown(Keys.Space)) {
    //            this.yd = 0.12F;
                this.cameraPos += this.cameraUp * cameraSpeed;
    //        }
            }
            if(input.IsKeyDown(Keys.LeftShift)) {
                this.cameraPos -= this.cameraUp * cameraSpeed;
            }

    //        this.moveRelative(xa, ya, this.onGround ? 0.02F : 0.005F);
    //        this.yd = (float)((double)this.yd - 0.005);
    //        this.move(this.xd, this.yd, this.zd);
    //        this.xd *= 0.91F;
    //        this.yd *= 0.98F;
    //        this.zd *= 0.91F;
    //        if(this.onGround) {
    //            this.xd *= 0.8F;
    //            this.zd *= 0.8F;
    //        }

    //    }
        }

    //    public void move(float xa, float ya, float za) {
    //        float xaOrg = xa;
    //        float yaOrg = ya;
    //        float zaOrg = za;
    //        List<AABB> aABBs = this.level.getCubes(this.bb.expand(xa, ya, za));

    //        int i;
    //        for(i = 0; i < aABBs.size(); ++i) {
    //            ya = ((AABB)aABBs.get(i)).clipYCollide(this.bb, ya);
    //        }

    //        this.bb.move(0.0F, ya, 0.0F);

    //        for(i = 0; i < aABBs.size(); ++i) {
    //            xa = ((AABB)aABBs.get(i)).clipXCollide(this.bb, xa);
    //        }

    //        this.bb.move(xa, 0.0F, 0.0F);

    //        for(i = 0; i < aABBs.size(); ++i) {
    //            za = ((AABB)aABBs.get(i)).clipZCollide(this.bb, za);
    //        }

    //        this.bb.move(0.0F, 0.0F, za);
    //        this.onGround = yaOrg != ya && yaOrg < 0.0F;
    //        if(xaOrg != xa) {
    //            this.xd = 0.0F;
    //        }

    //        if(yaOrg != ya) {
    //            this.yd = 0.0F;
    //        }

    //        if(zaOrg != za) {
    //            this.zd = 0.0F;
    //        }

    //        this.x = (this.bb.x0 + this.bb.x1) / 2.0F;
    //        this.y = this.bb.y0 + 1.62F;
    //        this.z = (this.bb.z0 + this.bb.z1) / 2.0F;
    //    }

    //    public void moveRelative(float xa, float za, float speed) {
    //        float dist = xa * xa + za * za;
    //        if(!(dist < 0.01F)) {
    //            dist = speed / (float)Math.sqrt((double)dist);
    //            xa *= dist;
    //            za *= dist;
    //            float sin = (float)Math.sin((double)this.yRot * Math.PI / 180.0);
    //            float cos = (float)Math.cos((double)this.yRot * Math.PI / 180.0);
    //            this.xd += xa * cos - za * sin;
    //            this.zd += za * cos + xa * sin;
    //        }
    //    }
    //}
    }
}
