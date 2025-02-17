#version 330 core
out vec4 FragColor;

uniform bool isWireframe;

in vec2 TexCoord;
uniform sampler2D texture1;

void main() {
	if(!isWireframe) {
		FragColor = texture(texture1, TexCoord);
	}
	else {
		FragColor = vec4(0.0f);
	}
}
