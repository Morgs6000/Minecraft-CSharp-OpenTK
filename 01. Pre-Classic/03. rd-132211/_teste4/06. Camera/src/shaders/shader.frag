#version 330 core
out vec4 FragColor;

uniform bool isWireframe;

in vec2 TexCoord;

uniform sampler2D texture1;

uniform vec4 color;

void main() {
	if(isWireframe) {
		FragColor = vec4(0.0f);
	}
	else {
		FragColor = texture(texture1, TexCoord);
		//FragColor = color;
	}
}
