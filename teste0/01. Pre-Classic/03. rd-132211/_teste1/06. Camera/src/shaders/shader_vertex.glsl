#version 330 core
layout(location = 0) in vec2 aPos;
layout(location = 1) in vec2 aTexCoord;

out vec2 texCoord;

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;

void main() {
    gl_Position = vec4(aPos, 0.0f, 1.0f);
    gl_Position *= model;
    gl_Position *= view;
    gl_Position *= projection;

    texCoord = aTexCoord;
}
