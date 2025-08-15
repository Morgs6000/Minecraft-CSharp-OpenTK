float4x4 World;
float4x4 View;
float4x4 Projection;

float3 DiffuseColor;

struct VertexShaderInput {
    float4 Position : POSITION0;
};

struct VertexShaderOutput {
    float4 Position : SV_POSITION;
};

VertexShaderOutput MainVS(in VertexShaderInput input) {
    VertexShaderOutput output = (VertexShaderOutput)0;

    float4 worldPosition = mul(input.Position, World);
    float4 viewPosition = mul(worldPosition, View);
    output.Position = mul(viewPosition, Projection);

    return output;
}

float4 MainPS(VertexShaderOutput input) : COLOR {
    return float4(DiffuseColor, 1.0);
}

technique BasicColorDrawing {
    pass P0 {
        VertexShader = compile VS_SHADERMODEL MainVS();
        PixelShader = compile PS_SHADERMODEL MainPS();
    }
};
