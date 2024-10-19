float SdPlane(float3 position, float3 normal, float k)
{
    return dot(position, normal) + k;
}

float SdSphere(float3 p, float r)
{
    return length(p) - r;
}

float SdBox(float3 p, float3 b)
{
    float3 q = abs(p) - b;
    return length(max(q, 0.0)) + min(max(q.x, max(q.y, q.z)), 0.0);
}
