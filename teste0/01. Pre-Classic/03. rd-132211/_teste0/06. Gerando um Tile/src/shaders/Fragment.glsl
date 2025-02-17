#version 330 core
out vec4 FragColor;
in vec2 texCoord;

uniform bool isWireframe;
uniform sampler2D texture0;

void main() {
    if(!isWireframe) {
        //FragColor = vec4(1.0f, 0.5f, 0.2f, 1.0f);
        FragColor = texture(texture0, texCoord);
    }
    else {
        FragColor = vec4(0.0f);
    }
}
