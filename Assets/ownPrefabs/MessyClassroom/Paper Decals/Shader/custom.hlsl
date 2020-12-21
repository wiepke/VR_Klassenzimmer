void ramp_float(float2 UV, float2 Normal, float2 Center, out float Value)
{
	Value = clamp(dot(Center - UV, Normal), -1, 1);
}