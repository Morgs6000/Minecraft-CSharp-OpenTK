using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;
using static System.Net.Mime.MediaTypeNames;

namespace RubyDung.src;

public class Player {
    public Vector3 Position;
    public Vector3 Front = new Vector3(0.0f, 0.0f, -1.0f);
    public Vector3 Up = new Vector3(0.0f, 1.0f, 0.0f);
    public Vector3 Right;

    private float fov = 60.0f;

    // opções de câmera
    public float MovementSpeed = 2.5f;

    public Player(Shader shader, int width, int height) {
        this.Position = new Vector3(0.0f, 0.0f, 3.0f); // Posição inicial do jogador

        // passa a matriz de projeção para o shader (observe que neste caso ela poderia mudar todos os frames)
        Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(fov), (float)width / (float)height, 0.1f, 100.0f);
        shader.setMat4("projection", projection);
    }

    public void UpdateModelMatrix(Shader shader) {
        // calcula a matriz do modelo para cada objeto e passa para o shader antes de desenhar
        Matrix4 model = Matrix4.CreateTranslation(new Vector3(Position.X, Position.Y, Position.Z - 2.0f));
        shader.setMat4("model", model);
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
}
