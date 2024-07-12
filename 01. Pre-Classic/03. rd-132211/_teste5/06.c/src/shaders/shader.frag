#version 330 core
out vec4 FragColor;

uniform bool isWireframe;

in vec2 TexCoord;
uniform sampler2D texture0;

void main() {
    if(!isWireframe) {
        FragColor = texture(texture0, TexCoord);
        //FragColor = vec4(1.0f);
    }
    else {
        FragColor = vec4(0.0f);
    }
}
