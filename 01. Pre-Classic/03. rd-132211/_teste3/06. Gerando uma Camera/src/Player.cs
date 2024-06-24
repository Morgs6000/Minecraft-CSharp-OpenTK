using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace RubyDung.src;

public class Player {
    public Vector3 Position;
    public Vector3 Front = new Vector3(0.0f, 0.0f, -1.0f);
    public Vector3 Up = new Vector3(0.0f, 1.0f, 0.0f);
    public Vector3 Right;
    public Vector3 WorldUp;
    // Ângulos de Euler
    public float Yaw;
    public float Pitch;
    // opções de câmera
    public float MovementSpeed = 2.5f;
    public float Zoom = 45.0f;

    //construtor com vetores
    public Player(Vector3 position) {
        Position = position;
        WorldUp = new Vector3(0.0f, 1.0f, 0.0f);
        Yaw = -90.0f;
        Pitch = 0.0f;
        updateCameraVectors();
    }

    // construtor com valores escalares
    public Player(float posX, float posY, float posZ) {
        Position = new Vector3(posX, posY, posZ);
        WorldUp = new Vector3(0.0f, 1.0f, 0.0f);
        Yaw = -90.0f;
        Pitch = 0.0f;
        updateCameraVectors();
    }

    // retorna a matriz de visualização calculada usando ângulos de Euler e a matriz LookAt
    public Matrix4 GetViewMatrix() {
        return Matrix4.LookAt(Position, Position + Front, Up);
    }

    // processa a entrada recebida de qualquer sistema de entrada semelhante a um teclado. Aceita parâmetro de entrada na forma de ENUM definido pela câmera (para abstraí-lo de sistemas de janelas)
    public void ProcessKeyboard(KeyboardState input, float deltaTime) {
        float velocity = MovementSpeed * deltaTime;
        if(input.IsKeyDown(Keys.W)) {
            Position += Front * velocity;
        }
        if(input.IsKeyDown(Keys.S)) {
            Position -= Front * velocity;
        }
        if(input.IsKeyDown(Keys.A)) {
            Position -= Right * velocity;
        }
        if(input.IsKeyDown(Keys.D)) {
            Position += Right * velocity;
        }
    }

    //processa a entrada recebida de um sistema de entrada de mouse. Espera o valor de deslocamento nas direções x e y.
    public void ProcessMouseMovement(float xoffset, float yoffset) {
        float sensitivity = 0.1f; // altere este valor ao seu gosto
        xoffset *= sensitivity;
        yoffset *= sensitivity;

        Yaw += xoffset;
        Pitch += yoffset;

        // certifique-se de que quando o pitch estiver fora dos limites, a tela não seja invertida
        if(Pitch > 89.0f) {
            Pitch = 89.0f;
        }
        if(Pitch < -89.0f) {
            Pitch = -89.0f;
        }

        // atualiza os vetores frontal, direito e superior usando os ângulos de Euler atualizados
        updateCameraVectors();
    }

    // processa a entrada recebida de um evento de roda de rolagem do mouse. Requer apenas entrada no eixo vertical da roda
    public void ProcessMouseScroll(float yoffset) {
        Zoom -= (float)yoffset;
        if(Zoom < 1.0f) {
            Zoom = 1.0f;
        }
        if(Zoom > 45.0f) {
            Zoom = 45.0f;
        }
    }

    // calcula o vetor frontal a partir dos ângulos de Euler da câmera (atualizados)
    private void updateCameraVectors() {
        // calcula o novo vetor Frontal
        Vector3 front;
        front.X = MathF.Cos(MathHelper.DegreesToRadians(Yaw)) * MathF.Cos(MathHelper.DegreesToRadians(Pitch));
        front.Y = MathF.Sin(MathHelper.DegreesToRadians(Pitch));
        front.Z = MathF.Sin(MathHelper.DegreesToRadians(Yaw)) * MathF.Cos(MathHelper.DegreesToRadians(Pitch));
        Front = Vector3.Normalize(front);
        // também recalcula o vetor Direita e Acima
        Right = Vector3.Normalize(Vector3.Cross(Front, WorldUp)); // normaliza os vetores, porque seu comprimento fica mais próximo de 0 quanto mais você olha para cima ou para baixo, o que resulta em um movimento mais lento.
        Up = Vector3.Normalize(Vector3.Cross(Right, Front));
    }
}
