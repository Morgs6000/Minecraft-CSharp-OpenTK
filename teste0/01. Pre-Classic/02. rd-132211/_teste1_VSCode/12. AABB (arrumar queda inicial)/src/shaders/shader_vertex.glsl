#version 330 core
layout(location = 0) in vec3 aPos;
layout(location = 1) in vec2 aTexCoord;
layout(location = 2) in vec3 aColor;

out vec2 texCoord;
out vec3 color;

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;

void main() {
    gl_Position = vec4(aPos, 1.0f);
    gl_Position *= model;
    gl_Position *= view;
    gl_Position *= projection;

    texCoord = aTexCoord;
    color = aColor;
}
