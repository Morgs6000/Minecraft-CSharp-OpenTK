using OpenTK.Mathematics;

namespace RubyDung.src;

public class ColorConverter {
    public static Vector3 HexToVector3(string hex) {
        // Verifica se o comprimento da string hex é válido
        if(hex.Length != 6) {
            throw new ArgumentException("A string hexadecimal deve ter 6 dígitos.");
        }

        // Extrai os componentes RGB da string hexadecimal
        int r = Convert.ToInt32(hex.Substring(0, 2), 16);
        int g = Convert.ToInt32(hex.Substring(2, 2), 16);
        int b = Convert.ToInt32(hex.Substring(4, 2), 16);

        // Converte os componentes RGB para o intervalo de 0 a 1
        float rf = (float)r / 255.0f;
        float gf = (float)g / 255.0f;
        float bf = (float)b / 255.0f;

        // Retorna o vetor Vector3 correspondente
        return new Vector3(rf, gf, bf);
    }
}
