#version 330 core
out vec4 FragColor;

in vec3 ourColor;
in vec2 TexCoord;

// amostrador de textura
uniform sampler2D texture1;
uniform sampler2D texture2;

void main() {
	vec4 tex1 = texture(texture1, TexCoord);
	vec4 tex2 = texture(texture2, TexCoord);

    FragColor = tex1 * tex2;
}
