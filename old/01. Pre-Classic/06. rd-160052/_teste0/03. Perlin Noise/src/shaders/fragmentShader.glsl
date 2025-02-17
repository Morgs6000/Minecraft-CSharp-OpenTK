#version 330 core
out vec4 FragColor;

uniform vec4 color;

in vec2 TexCoord;
uniform sampler2D texture0;

uniform bool isWireframe;

void main() {
	if(!isWireframe) {
		//FragColor = color;
		FragColor = texture(texture0, TexCoord);
	}
	else {
		FragColor = vec4(0.0f);
	}
}
