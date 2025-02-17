#version 330 core
out vec4 FragColor;

uniform bool isWireframe;
uniform vec4 setColor;

void main() {
	if(!isWireframe) {
		//FragColor = vec4(1.0f, 0.5f, 0.2f, 1.0f);
		FragColor = setColor;
	}
	else {
		FragColor = vec4(0.0f);
	}
}
