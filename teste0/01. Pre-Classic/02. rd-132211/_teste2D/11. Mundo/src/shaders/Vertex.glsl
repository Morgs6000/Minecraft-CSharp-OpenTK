#version 330 core
layout(location = 0) in vec3 aPos;
layout(location = 1) in vec2 aTex;

out vec2 texCoord;

uniform mat4 view;
uniform mat4 projection;

void main() {
    gl_Position = vec4(aPos, 1.0f);
    gl_Position *= view;
    gl_Position *= projection;
    texCoord = aTex;
}
