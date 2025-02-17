#version 330 core
out vec4 FragColor;
in vec2 texCoord;

uniform bool wireframeMode;
uniform sampler2D texture0;

void main() {
    if(!wireframeMode) {
        FragColor = texture(texture0, texCoord);
    }
    else {
        FragColor = vec4(0.0f);
    }
}
