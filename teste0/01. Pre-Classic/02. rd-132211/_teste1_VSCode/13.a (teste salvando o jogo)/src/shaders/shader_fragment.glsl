#version 330 core
out vec4 FragColor;

in vec2 texCoord;
in vec3 color;

uniform bool hasTexture;
uniform bool hasColor;

uniform sampler2D texture0;
uniform vec4 color0;

void main() {
    if(hasTexture && hasColor) {
        FragColor = texture(texture0, texCoord) * vec4(color, 1.0f);
    }
    else if(hasTexture) {
        FragColor = texture(texture0, texCoord);
    }
    else if(hasColor) {
        FragColor = vec4(color, 1.0f);
    }
    else {
        FragColor = color0;
    }  
}
