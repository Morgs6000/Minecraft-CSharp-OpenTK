#version 330 core
out vec4 FragColor;

in vec2 texCoord;

uniform bool wireframe;
uniform bool hasTexture;

uniform sampler2D texture0;

void main() {
    if(wireframe) {
        FragColor = vec4(0.0f);
    }
    else {
        if(hasTexture) {
            FragColor = texture(texture0, texCoord);
        }
        else {
            FragColor = vec4(1.0f);
        }
    }
}
