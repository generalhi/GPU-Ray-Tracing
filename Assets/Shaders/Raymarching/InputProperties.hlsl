RWTexture2D<float4> Result;

float4x4 World;
float4x4 Projection;

float4 DirectionalLight;

Texture2D<float4> SkyBoxTexture;
SamplerState sampler_SkyBoxTexture;

float Time;
int ReflectionsCount;

StructuredBuffer<Plane> Plains;
StructuredBuffer<Sphere> Spheres;
StructuredBuffer<Cube> Cubes;
