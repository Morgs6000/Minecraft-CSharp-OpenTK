#version 330 core
out vec4 FragColor;

in vec2 texCoord;

struct Material {
    sampler2D dirt;
    sampler2D grass_overlay;
};

uniform bool wireframeMode;
uniform Material material;

void main() {
    if(!wireframeMode) {
        //*
        vec4 dirt = texture(material.dirt, texCoord);
        vec4 grass_overlay = texture(material.grass_overlay, texCoord);

        vec3 rgb = mix(dirt.rgb, grass_overlay.rgb, grass_overlay.a);

        FragColor = vec4(rgb, 1.0f);
        //*/

        /*
        vec3 dirt = texture(material.dirt, texCoord).rgb;
        vec3 grass_overlay = texture(material.grass_overlay, texCoord).rgb;

        vec3 result = dirt + grass_overlay;

        FragColor = vec4(result, 1.0f);
        */

        /*
        vec4 dirt = texture(material.dirt, texCoord);
        vec4 grass_overlay = texture(material.grass_overlay, texCoord);

        vec4 result = mix(dirt, grass_overlay, result);

        FragColor = result;
        */
    }
    else {
        FragColor = vec4(0.0f);
    }
}
