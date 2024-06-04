#version 330 core
layout (location = 0) in vec3 aPos;
layout (location = 1) in vec2 aTexCoord;

layout (location = 3) in vec3 aPosItem;

out vec2 TexCoord;

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;

void main() {
    vec4 pos1 = projection * view * model * vec4(aPos, 1.0);
    vec4 pos2 = vec4(aPosItem, 1.0);

    gl_Position = mix(pos1, pos2, 0.5);
    TexCoord = vec2(aTexCoord.x, aTexCoord.y);
}
