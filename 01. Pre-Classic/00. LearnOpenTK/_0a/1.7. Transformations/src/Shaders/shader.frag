#version 330 core
out vec4 FragColor;
  
in vec3 ourColor;
in vec2 TexCoord;

uniform sampler2D texture0;
uniform sampler2D texture1;

void main() {
    vec4 tex0 = texture(texture0, TexCoord);
    vec4 tex1 = texture(texture1, TexCoord);
    FragColor = mix(tex0, tex1, 0.2);
}
