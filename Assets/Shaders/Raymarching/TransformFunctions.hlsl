float3 DeltaSin(float3 delta, float3 speed)
{
    float x = delta.x * sin(Time * speed.x);
    float y = delta.y * sin(Time * speed.y);
    float z = delta.z * sin(Time * speed.z);

    return float3(x, y, z);
}

float3 Move(float3 position, float3 delta)
{
    return position + delta;
}

float2x2 Rotation2D(float a)
{
    float s = sin(a);
    float c = cos(a);
    return float2x2(c, -s, s, c);
}

float3 Rotation(float3 p, float3 angle)
{
    float3 axisX = float3(1.0, 0.0, 0.0);
    float3 axisY = float3(0.0, 1.0, 0.0);
    float3 axisZ = float3(0.0, 0.0, 1.0);

    p = lerp(dot(axisZ, p) * axisZ, p, cos(angle.z))
        + cross(axisZ, p) * sin(angle.z);

    p = lerp(dot(axisY, p) * axisY, p, cos(angle.y))
        + cross(axisY, p) * sin(angle.y);

    p = lerp(dot(axisX, p) * axisX, p, cos(angle.x))
        + cross(axisX, p) * sin(angle.x);

    return p;
}
