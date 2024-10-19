struct Ray
{
    float3 Position;
    float3 Direction;
    float3 Energy;
};

Ray InitRay(float3 position, float3 direction)
{
    Ray r;
    r.Position = position;
    r.Direction = direction;
    r.Energy = 1.0;
    return r;
}

Ray InitCameraRay(float2 uv)
{
    float3 position = mul(World, float4(0.0, 0.0, 0.0, 1.0)).xyz;
    float3 direction = mul(Projection, float4(uv, 0.0, 1.0)).xyz;

    direction = mul(World, float4(direction, 0.0)).xyz;
    direction = normalize(direction);

    return InitRay(position, direction);
}
