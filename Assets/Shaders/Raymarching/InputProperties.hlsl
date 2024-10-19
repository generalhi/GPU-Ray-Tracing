RWTexture2D<float4> Result;

float4x4 World;
float4x4 Projection;

float4 DirectionalLight;

Texture2D<float4> SkyBoxTexture;
SamplerState sampler_SkyBoxTexture;

float Time;
int ReflectionsCount;

struct Plane
{
    float3 Normal;
    float K;
    float Smooth;
    float3 Specular;
    float3 Albedo;
};

struct Sphere
{
    float Radius;
    float Smooth;
    float3 Specular;
    float3 Albedo;
};

StructuredBuffer<Plane> Plains;
StructuredBuffer<Sphere> Spheres;
