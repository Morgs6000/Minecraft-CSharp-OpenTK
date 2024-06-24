using OpenTK.Mathematics;
using static System.Net.Mime.MediaTypeNames;

namespace RubyDung.src;

public class Player {
    public Vector3 Position;
    public Vector3 Front = new Vector3(0.0f, 0.0f, -1.0f);
    public Vector3 Up = new Vector3(0.0f, 1.0f, 0.0f);

    private float fov = 60.0f;

    public Player(Shader shader, int width, int height) {
        // passa a matriz de projeção para o shader (observe que neste caso ela poderia mudar todos os frames)
        Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(fov), (float)width / (float)height, 0.1f, 100.0f);
        shader.setMat4("projection", projection);

        // calcula a matriz do modelo para cada objeto e passa para o shader antes de desenhar
        Matrix4 model = Matrix4.CreateTranslation(-1.0f, 0.0f, -2.0f);
        shader.setMat4("model", model);
    }
}
