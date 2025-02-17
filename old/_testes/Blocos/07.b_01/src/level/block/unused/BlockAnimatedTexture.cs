using OpenTK.Mathematics;
using System.Timers;

namespace RubyDung.src.level.block.unused;

public class BlockAnimatedTexture : Block {
    private List<Vector2> textures = new List<Vector2>();
    private int currentTextureIndex = 0;
    private System.Timers.Timer animationTimer;

    public BlockAnimatedTexture() {
        // Adiciona as texturas à lista
        textures.Add(new Vector2(13, 14));
        textures.Add(new Vector2(14, 14));
        textures.Add(new Vector2(15, 14));
        textures.Add(new Vector2(14, 15));
        textures.Add(new Vector2(15, 15));

        // Configura o temporizador para alternar texturas a cada 1 segundo (1000 milissegundos)
        animationTimer = new System.Timers.Timer(1000);
        animationTimer.Elapsed += OnTimedEvent;
        animationTimer.AutoReset = true;
        animationTimer.Enabled = true;

        // Define a primeira textura
        SetCurrentTexture();
    }

    private void OnTimedEvent(object source, ElapsedEventArgs e) {
        // Alterna para a próxima textura
        currentTextureIndex = (currentTextureIndex + 1) % textures.Count;
        SetCurrentTexture();
        Console.WriteLine($"Current Texture: {textures[currentTextureIndex].X}, {textures[currentTextureIndex].Y}");
    }

    private void SetCurrentTexture() {
        // Obtém as coordenadas da textura atual
        int x = (int)textures[currentTextureIndex].X;
        int y = (int)textures[currentTextureIndex].Y;

        // Chama o método setTexture da classe base (Block)
        setTexture(x, y);
    }
}
