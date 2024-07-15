#version 330 core
out vec4 FragColor;

uniform vec4 color;
uniform bool isWireframe;

void main() {
	if(!isWireframe) {
		FragColor = color;
	}
	else {
		FragColor = vec4(0.0f);
	}
}
