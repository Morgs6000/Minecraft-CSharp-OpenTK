#version 330 core
out vec4 FragColor;

in vec2 texCoord;
in vec3 color;

uniform bool wireframe;
uniform bool hasTexture;
uniform bool hasColor;

uniform sampler2D texture0;
uniform vec4 color0;

void main() {
    //FragColor = vec4(1.0f, 0.5f, 0.2f, 1.0f);
    FragColor = color0;
    
    if(hasTexture) {
        FragColor = texture(texture0, texCoord);

        if(hasColor) {
            FragColor *= vec4(color, 1.0f);
        }
    }    
    else if(hasColor) {
        FragColor = vec4(color, 1.0f);
    }    

    if(wireframe) {
        FragColor = vec4(0.0f);
    }
}
