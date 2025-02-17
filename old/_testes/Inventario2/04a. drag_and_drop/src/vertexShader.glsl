#version 330 core
layout(location = 0) in vec3 aPos;

uniform vec2 rectPosition;

void main() {
	vec3 newPos = aPos + vec3(rectPosition, 0.0);
	gl_Position = vec4(newPos, 1.0);
}
