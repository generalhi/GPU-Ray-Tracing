float SmoothMin(float a, float b, float k)
{
    float h = max(k - abs(a - b), 0.0) / k;
    return min(a, b) - h * h * h * h * (1.0 / 6.0);
}

float2 SmoothMin2(float a, float b, float k)
{
    k *= 6.0;
    float h = max(k - abs(a - b), 0.0) / k;
    float m = h * h * h * 0.5;
    float s = m * k * (1.0 / 3.0);
    return a < b
               ? float2(a - s, m)
               : float2(b - s, 1.0 - m);
}
