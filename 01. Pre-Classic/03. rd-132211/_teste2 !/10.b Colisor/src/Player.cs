﻿using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
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

    public AABB bb;

    public bool onGround = false;

    Vector3 cameraPos = new Vector3(0.0f, 0.0f, 0.3f);
    Vector3 cameraFront = new Vector3(0.0f, 0.0f, -1.0f);
    Vector3 cameraUp = new Vector3(0.0f, 1.0f, 0.0f);
    Vector3 cameraRight;

    float fov = 60.0f;

    float deltaTime = 0.0f;
    float lastFrame = 0.0f;

    bool firstMouse;

    float lastX = 400.0f;
    float lastY = 300.0f;

    float yRot = -90.0f;
    float xRot;

    public Player(Level level) {
        this.level = level;
        this.setPos(cameraPos.X, cameraPos.Y, cameraPos.Z);
        //this.resetPos();
    }

    private void resetPos() {
        Random random = new Random();

        float x = (float)random.NextDouble() * this.level.width;
        float y = (float)(this.level.height + 10);
        float z = (float)random.NextDouble() * this.level.depth;

        this.setPos(x, y, z);
    }

    private void setPos(float x, float y, float z) {
        cameraPos.X = x;
        cameraPos.Y = y;
        cameraPos.Z = z;

        float w = 0.3f;
        float h = 0.9f;

        this.bb = new AABB(x - w, y - h, z - w, x + w, y + h, z + w);
    }

    public void render(Shader shader, int width, int height) {
        // ..:: Model ::..
        Matrix4 model = Matrix4.Identity;
        model = Matrix4.CreateTranslation(0.0f, 0.0f, -256.0f - 16.0f);

        int modelLoc = GL.GetUniformLocation(shader.shaderProgram, "model");
        GL.UniformMatrix4(modelLoc, false, ref model);

        // ..:: View ::..
        Matrix4 view = Matrix4.Identity;
        view = Matrix4.LookAt(this.cameraPos, this.cameraPos + this.cameraFront, this.cameraUp);

        int viewLoc = GL.GetUniformLocation(shader.shaderProgram, "view");
        GL.UniformMatrix4(viewLoc, false, ref view);

        // ..:: Projection ::..
        Matrix4 projection;
        projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(this.fov), (float)width / (float)height, 0.1f, 100.0f);

        int projectionLoc = GL.GetUniformLocation(shader.shaderProgram, "projection");
        GL.UniformMatrix4(projectionLoc, false, ref projection);

        // ..:: Time ::..
        this.time();
    }

    public void time() {
        float currentFrame = (float)GLFW.GetTime();
        deltaTime = currentFrame - lastFrame;
        lastFrame = currentFrame;
    }

    public void tick(KeyboardState input) {
        float cameraSpeed = 4.317f * deltaTime;

        this.xo = this.x;
        this.yo = this.y;
        this.zo = this.z;

        float xa = 0.0f;
        float ya = 0.0f;
        float za = 0.0f;

        if(input.IsKeyDown(Keys.R)) {
            this.resetPos();
        }

        if(input.IsKeyDown(Keys.W)) {
            //cameraPos += cameraSpeed * cameraFront;
            //cameraPos += Vector3.Normalize(new Vector3(cameraFront.X, 0, cameraFront.Z)) * cameraSpeed;
            za++;
        }
        if(input.IsKeyDown(Keys.S)) {
            //cameraPos -= cameraSpeed * cameraFront;
            //cameraPos -= Vector3.Normalize(new Vector3(cameraFront.X, 0, cameraFront.Z)) * cameraSpeed;
            za--;
        }
        if(input.IsKeyDown(Keys.A)) {
            //cameraPos -= Vector3.Normalize(Vector3.Cross(cameraFront, cameraUp)) * cameraSpeed;
            xa--;
        }
        if(input.IsKeyDown(Keys.D)) {
            //cameraPos += Vector3.Normalize(Vector3.Cross(cameraFront, cameraUp)) * cameraSpeed;
            xa++;
        }

        if(input.IsKeyDown(Keys.Space)) {
            //cameraPos += cameraUp * cameraSpeed;
            ya++;
        }
        if(input.IsKeyDown(Keys.LeftShift)) {
            //cameraPos -= cameraUp * cameraSpeed;
            ya--;
        }

        //if(input.IsKeyDown(Keys.Space) && this.onGround) {
        //    this.yd = 0.12f;
        //}

        // ----------

        cameraRight = Vector3.Normalize(Vector3.Cross(cameraFront, cameraUp));
        cameraFront = Vector3.Normalize(new Vector3(cameraFront.X, 0, cameraFront.Z));

        cameraPos += xa * cameraRight * cameraSpeed;
        cameraPos += ya * cameraUp * cameraSpeed;
        cameraPos += za * cameraFront * cameraSpeed;

        // ----------

        this.moveReleative(xa, za, this.onGround ? 0.02f : 0.005f);

        //this.yd = (float)((double)this.yd - 0.005f);

        this.move(this.xd, this.yd, this.zd);

        //this.xd *= 0.91f;
        //this.yd *= 0.98f;
        //this.zd *= 0.91f;

        if(this.onGround) {
            this.xd *= 0.8f;
            this.zd *= 0.8f;
        }
    }

    public void mouse_callback(float xpos, float ypos) {
        if(firstMouse) {
            lastX = xpos;
            lastY = ypos;
            firstMouse = false;
        }

        float xoffset = xpos - lastX;
        float yoffset = ypos - lastY;
        lastX = xpos;
        lastY = ypos;

        float sensitivity = 0.1f;
        xoffset *= sensitivity;
        yoffset *= sensitivity;

        yRot += xoffset;
        xRot -= yoffset;

        if(xRot > 89.0f) {
            xRot = 89.0f;
        }
        if(xRot < -89.0f) {
            xRot = -89.0f;
        }

        Vector3 direction;
        direction.X = (float)Math.Cos(MathHelper.DegreesToRadians(yRot)) * (float)Math.Cos(MathHelper.DegreesToRadians(xRot));
        direction.Y = (float)Math.Sin(MathHelper.DegreesToRadians(xRot));
        direction.Z = (float)Math.Sin(MathHelper.DegreesToRadians(yRot)) * (float)Math.Cos(MathHelper.DegreesToRadians(xRot));
        cameraFront = Vector3.Normalize(direction);
    }

    public void zBuffer() {
        GL.Enable(EnableCap.DepthTest);

        GL.Enable(EnableCap.CullFace);
        GL.CullFace(CullFaceMode.Front);
    }

    public void move(float xa, float ya, float za) {
        float xaOrg = xa;
        float yaOrg = ya;   
        float zaOrg = za;

        List<AABB> aABBs = this.level.getCubes(this.bb.expand(xa, ya, za));

        /*
        for(int j = 0; j < aABBs.Count; j++) {
            Console.WriteLine(
                aABBs[j].x0 + " " +
                aABBs[j].y0 + " " +
                aABBs[j].z0 + " " +

                aABBs[j].x1 + " " +
                aABBs[j].y1 + " " +
                aABBs[j].z1
            );
        }
        */

        //Console.WriteLine(xa + " " + ya + " " + za);
        Console.WriteLine((int)xa + " " + (int)ya + " " + (int)za);

        int i;

        for(i = 0; i < aABBs.Count; i++) {
            ya = aABBs[i].clipYCollide(this.bb, ya);
        }

        this.bb.move(0.0f, ya, 0.0f);

        for(i = 0; i < aABBs.Count; i++) {
            xa = aABBs[i].clipXCollide(this.bb, xa);
        }

        this.bb.move(xa, 0.0f, 0.0f);

        for(i = 0; i < aABBs.Count; i++) {
            za = aABBs[i].clipZCollide(this.bb, za);
        }

        //Console.WriteLine(xa + " " + ya + " " + za);

        this.bb.move(0.0f, 0.0f, za);
        this.onGround = yaOrg != ya && yaOrg < 0.0f;
        //Console.WriteLine(this.onGround);

        if(xaOrg != xa) {
            this.xd = 0.0f;
        }

        if(yaOrg != ya) {
            this.yd = 0.0f;
        }

        if(zaOrg  != za) {
            this.zd = 0.0f;
        }

        this.x = (this.bb.x0 + this.bb.x1) / 2.0f;
        this.y = this.bb.y0 + 1.62f;
        this.z = (this.bb.z0 + this.bb.z1) / 2.0f;
    }

    public void moveReleative(float xa, float za, float speed) {
        float dist = xa * xa + za * za;

        if(!(dist < 0.01f)) {
            dist = speed / (float)Math.Sqrt((double)dist);

            xa *= dist;
            za *= dist;

            float sin = (float)Math.Sin((double)this.yRot * Math.PI / 180.0f);
            float cos = (float)Math.Cos((double)this.yRot * Math.PI / 180.0f);

            this.xd += xa * cos - za * sin;
            this.zd += za * cos - xa * sin;
        }
    }
}
