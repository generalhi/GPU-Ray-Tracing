float3 DeltaSin(float delta, float3 axis, float speed)
{
    return axis * delta * sin(Time * speed);
}

float3 Move(float3 position, float3 delta)
{
    return position + delta;
}

float3 Rotation(float3 p, float3 axis, float angle)
{
    return lerp(dot(axis, p) * axis, p, cos(angle))
        + cross(axis, p) * sin(angle);
}
