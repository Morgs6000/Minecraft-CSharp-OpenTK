using OpenTK.Mathematics;

namespace RubyDung.src;

public enum Camera_Movement {
    FORWARD,
    BACKWARD,
    LEFT,
    RIGHT,
    UP,
    DOWN
}

public class Camera {
    public float fovy = 60.0f;

    private Vector3 eye = new Vector3(0.0f, 0.0f, 3.0f);
    private Vector3 target = new Vector3(0.0f, 0.0f, -1.0f);
    private Vector3 up = new Vector3(0.0f, 1.0f, 0.0f);
    //private Vector3 right;
    //private Vector3 worldUp;

    private float MovementSpeed = 4.317f;

    private float yaw;
    private float pitch;

    public Camera() {
        this.yaw = -90.0f;
        this.pitch = 0.0f;

        //this.updateCameraVectors();
    }

    public Matrix4 GetViewMatrix() {
        return Matrix4.LookAt(this.eye, this.eye + this.target, this.up);
    }

    public void ProcessKeyboard(Camera_Movement direction, float deltaTime) {
        float velocity = (float)(this.MovementSpeed * deltaTime);

        float x = 0.0f;
        float y = 0.0f;
        float z = 0.0f;

        if(direction == Camera_Movement.FORWARD) {
            z++;
        }
        if(direction == Camera_Movement.BACKWARD) {
            z--;
        }
        if(direction == Camera_Movement.LEFT) {
            x++;
        }
        if(direction == Camera_Movement.RIGHT) {
            x--;
        }

        if(direction == Camera_Movement.UP) {
            y++;
        }
        if(direction == Camera_Movement.DOWN) {
            y--;
        }

        this.eye += x * Vector3.Normalize(Vector3.Cross(-this.target, this.up)) * velocity;
        this.eye += y * this.up * velocity;
        this.eye += z * Vector3.Normalize(new Vector3(this.target.X, 0.0f, this.target.Y)) * velocity;
    }

    public void ProcessMouseMovement(float xoffset, float yoffset) {
        float sensitivity = 0.1f;
        xoffset *= sensitivity;
        yoffset *= sensitivity;

        this.yaw += xoffset;
        this.pitch += yoffset;

        if(this.pitch > 89.0f) {
            this.pitch = 89.0f;
        }
        if(this.pitch < -89.0f) {
            this.pitch = -89.0f;
        }

        this.updateCameraVectors();
    }

    public void updateCameraVectors() {
        Vector3 front;
        front.X = MathF.Cos(MathHelper.DegreesToRadians(this.yaw)) * MathF.Cos(MathHelper.DegreesToRadians(this.pitch));
        front.Y = MathF.Sin(MathHelper.DegreesToRadians(this.pitch));
        front.Z = MathF.Sin(MathHelper.DegreesToRadians(this.yaw)) * MathF.Cos(MathHelper.DegreesToRadians(this.pitch));

        this.target = Vector3.Normalize(front);
    }
}
