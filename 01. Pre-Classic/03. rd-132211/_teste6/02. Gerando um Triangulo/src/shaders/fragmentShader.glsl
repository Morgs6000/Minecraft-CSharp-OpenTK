#version 330 core
out vec4 FragColor;

vec4 convertColorToRGBA(int r, int g, int b, int a) {
	float fr = float(r) / 255;
	float fg = float(g) / 255;
	float fb = float(b) / 255;
	float fa = float(a) / 255;

	return vec4(fr, fg, fb, fa);
}

// Não foi possivel converte para hexadecimal em .glsl
/*
vec4 convertColorHex(string hex, int a) {
	int fr = Convert.ToInt32(hex.Substring(0, 2), 16);
    int fg = Convert.ToInt32(hex.Substring(2, 2), 16);
    int fb = Convert.ToInt32(hex.Substring(4, 2), 16);
	int fa = a / 255;

	return convertColorToRGBA(fr, fg, fb, fa);
}
*/

uniform vec4 color;

void main() {
	//FragColor = vec4(1.0f, 0.5f, 0.2f, 1.0f);
	//FragColor = convertColorToRGBA(255, 127, 51, 255);
	FragColor = color;
}
