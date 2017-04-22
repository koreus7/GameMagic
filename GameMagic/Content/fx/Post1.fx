#if OPENGL
#define SV_POSITION POSITION
#define VS_SHADERMODEL vs_3_0
#define PS_SHADERMODEL ps_3_0
#else
#define VS_SHADERMODEL vs_4_0_level_9_1
#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

// Our texture sampler
texture Texture;
sampler TextureSampler = sampler_state
{
	Texture = <Texture>;
};

// Uniforms
//float time;
//float2 res;

// This data comes from the sprite batch vertex shader
struct VertexShaderOutput
{
	float4 Position : TEXCOORD0;
	float4 Color : COLOR0;
	float2 TextureCordinate : TEXCOORD0;
};


// Our pixel shader
float4 PixelShaderFunction(VertexShaderOutput input) : COLOR0
{
	float4 color = tex2D(TextureSampler, input.TextureCordinate);
	return color;
}

// Compile our shader
technique Technique1
{
	pass Pass1
	{
		PixelShader = compile ps_2_0 PixelShaderFunction();
	}
}