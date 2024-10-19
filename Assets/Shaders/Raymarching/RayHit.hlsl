struct RayHit
{
    float3 Position;
    float Distance;
    float3 Normal;
};

RayHit InitRayHit()
{
    RayHit hit;
    hit.Position = 0.0;
    hit.Distance = 1.#INF;
    hit.Normal = 0.0;
    return hit;
}
