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
    float3 Position;
    float3 Move;
    float3 MoveSpeed;
    float Radius;
    float Smooth;
    float3 Specular;
    float3 Albedo;
};

struct Cube
{
    float3 Position;
    float3 Move;
    float3 MoveSpeed;
    float3 Rotation;
    float Size;
    float Smooth;
    float3 Specular;
    float3 Albedo;
};
