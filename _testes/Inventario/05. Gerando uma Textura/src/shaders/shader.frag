#version 330 core
out vec4 FragColor;

uniform bool isWireframe;

in vec2 TexCoord;

// amostrador de textura
uniform sampler2D texture1;

void main() {
	if(isWireframe) {
		FragColor = vec4(0.0f);
	}
	else {
		FragColor = texture(texture1, TexCoord);
	}
}
