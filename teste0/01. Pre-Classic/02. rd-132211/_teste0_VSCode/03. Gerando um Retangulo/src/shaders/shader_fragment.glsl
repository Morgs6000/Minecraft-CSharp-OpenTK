#version 330 core
out vec4 FragColor;

uniform bool wireframe;

void main() {
    if(wireframe) {
        FragColor = vec4(0.0f);
    }
    else {
        FragColor = vec4(1.0f, 0.5f, 0.2f, 1.0f);
    }
}