#version 330 core
out vec4 FragColor;

in vec2 texCoord;

uniform bool wireframe;
uniform sampler2D texture0;

void main() {
    if(!wireframe) {
        FragColor = texture(texture0, texCoord);
    }
    else {
        FragColor = vec4(0.0f);
    }
}
