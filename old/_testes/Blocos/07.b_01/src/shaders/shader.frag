#version 330 core
out vec4 FragColor;

in vec2 TexCoord;
in vec3 ourColor;

uniform sampler2D ourTexture;

void main() {
    vec4 texColor = texture(ourTexture, TexCoord);

    if(texColor.a < 0.1)
        discard;

    FragColor = texColor * vec4(ourColor, 1.0);
}
