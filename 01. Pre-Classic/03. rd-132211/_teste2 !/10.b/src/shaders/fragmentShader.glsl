#version 330 core
out vec4 FragColor;

in vec2 TexCoord;

uniform sampler2D texture0;
uniform bool isWireframe;

void main() {
    if(!isWireframe) {
		FragColor = texture(texture0, TexCoord);
	}
	else {
		FragColor = vec4(0.0f);
	}
}
