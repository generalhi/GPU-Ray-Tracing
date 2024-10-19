float SdPlane(float3 position, in Plane plane)
{
    return dot(position, plane.Normal) + plane.K;
}

float SdSphere(float3 p, float s)
{
    return length(p) - s;
}

float SdBox(float3 p, float3 b)
{
    float3 q = abs(p) - b;
    return length(max(q, 0.0)) + min(max(q.x, max(q.y, q.z)), 0.0);
}
