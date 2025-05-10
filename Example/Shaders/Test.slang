struct Constants
{
    float4x4 Projection;

    float4x4 View;

    float4x4 Model;
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

struct Block
{
    Constants Constants;

    Texture2D Texture;

    SamplerState Sampler;
};

uniform ParameterBlock<Block> block;

float3 SrgbToLinear(float3 srgb)
{
    return srgb * (srgb * (srgb * 0.305306011 + 0.682171111) + 0.012522878);
}

[shader("vertex")]
VSOutput VSMain(VSInput input)
{
    float4x4 mvp = block.Constants.Projection * block.Constants.View * block.Constants.Model;

    VSOutput output;

    output.Position = mul(mvp, float4(input.Position, 0.0f, 1.0f));
    output.UV = input.UV;
    output.Color = input.Color;
    output.Color.rgb = SrgbToLinear(output.Color.rgb);

    return output;
}

[shader("pixel")]
float4 PSMain(VSOutput input) : SV_TARGET
{
    return input.Color * block.Texture.Sample(block.Sampler, input.UV);
}