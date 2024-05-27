#version 330 core
//layout(location = 0) in vec3 aPosition;
layout(location = 0) in vec3 aPos;
layout(location = 1) in vec3 aColor;

//out vec4 vertexColor;
out vec3 ourColor;

void main() {
    //gl_Position = vec4(aPosition, 1.0);
    gl_Position = vec4(aPos, 1.0);
    //vertexColor = vec4(0.5, 0.0, 0.0, 1.0);
    ourColor = aColor;
}