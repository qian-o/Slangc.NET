struct Constants
{
    float4x4 Projection;
};

struct VSInput
{
    float2 Position : POSITION0;

    float2 UV : TEXCOORD0;

    float4 Color : COLOR0;
};

struct VSOutput
{
    float4 Position : SV_POSITION;

    float2 UV : TEXCOORD0;

    float4 Color : COLOR0;
};

uniform Constants constants;
uniform Texture2D texture0;
uniform SamplerState sampler0;

float3 SrgbToLinear(float3 srgb)
{
    return srgb * (srgb * (srgb * 0.305306011 + 0.682171111) + 0.012522878);
}

[shader("vertex")]
VSOutput VSMain(VSInput input)
{
    VSOutput output;

    output.Position = mul(float4(input.Position, 0.0, 1.0), constants.Projection);
    output.UV = input.UV;
    output.Color = input.Color;
    output.Color.rgb = SrgbToLinear(output.Color.rgb);

    return output;
}

[shader("pixel")]
float4 PSMain(VSOutput input) : SV_TARGET
{
    return input.Color;
}