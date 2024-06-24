#version 330 core
out vec4 FragColor;

in vec3 ourColor;
in vec2 TexCoord;

// amostrador de textura
uniform sampler2D texture1;
uniform sampler2D texture2;

uniform bool isWireframe;

void main() {
	if(isWireframe) {
		FragColor = vec4(0.0f);
	}
	else {
		vec4 color1 = texture(texture1, TexCoord);
		vec4 color2 = texture(texture2, TexCoord);

		FragColor = mix(color1, color2, 0.2); // Combina as duas texturas (50% de cada)
	}
}
