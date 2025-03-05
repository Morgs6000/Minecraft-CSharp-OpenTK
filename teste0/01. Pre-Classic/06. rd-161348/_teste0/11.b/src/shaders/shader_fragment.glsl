#version 330 core
out vec4 FragColor;

in vec2 texCoord;
in vec3 color;

uniform bool hasTexture;
uniform bool hasColor;

uniform sampler2D texture0;

void main() {
    // Valor padrão para FragColor
    FragColor = vec4(1.0f, 0.5f, 0.2f, 1.0f); // Cor padrão (laranja)

    // Aplica a cor, se existir
    if (hasColor) {
        FragColor = vec4(color, 1.0f);
    }

    // Aplica a textura, se existir
    if (hasTexture) {
        vec4 texColor = texture(texture0, texCoord);

        // Descarta pixels totalmente transparentes
        if (texColor.a < 0.1f) {
            discard;
        }

        // Combina a cor da textura com a cor existente (se houver)
        FragColor = hasColor ? texColor * FragColor : texColor;
    }
}
