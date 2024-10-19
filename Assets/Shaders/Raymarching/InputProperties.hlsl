RWTexture2D<float4> Result;

float4x4 World;
float4x4 Projection;

Texture2D<float4> SkyBoxTexture;
SamplerState sampler_SkyBoxTexture;

float Time;
int ReflectionsCount;
