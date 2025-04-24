#if OPENGL
	#define SV_POSITION POSITION
	#define VS_SHADERMODEL vs_3_0
	#define PS_SHADERMODEL ps_3_0
#else
	#define VS_SHADERMODEL vs_4_0_level_9_1
	#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

Texture2D SpriteTexture;

sampler2D SpriteTextureSampler = sampler_state
{
	Texture = <SpriteTexture>;
};

struct VertexShaderOutput
{
	float4 Position : SV_POSITION;
	float4 Color : COLOR0;
	float2 TextureCoordinates : TEXCOORD0;
};

float4 MainPS(VertexShaderOutput input) : COLOR
{
	return tex2D(SpriteTextureSampler,input.TextureCoordinates) * input.Color;
}
float4 PS(float2 vTex0 : TEXCOORD0 ) : COLOR0
{	
	float distfromcenter=distance(float2(0.5f, 0.5f), vTex0);	
	float4 rColor = lerp(float4(0,0,0,1),float4(1,1,1,1), saturate(distfromcenter));	
	return rColor;
}

technique SpriteDrawing
{
	pass P0
	{
		PixelShader = compile PS_SHADERMODEL PS();
	}
	
};