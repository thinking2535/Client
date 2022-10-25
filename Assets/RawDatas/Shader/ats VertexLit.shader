// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'

Shader "Hi/TerrainEngine/Details/Vertexlit" {
Properties {
	_Color ("Main Color r:ampl g:speed b:time", Color) = (1,1,1,1)
	_MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
	_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
	_DiffuseColor ("Diffuse Color", Color) = (1,1,1,1)
}

SubShader {
	Tags {"IgnoreProjector"="True" "RenderType"="TreeLeaf" }
	LOD 200
	Cull Off	
CGPROGRAM
#pragma surface surf MyFoliage alphatest:_Cutoff vertex:vert exclude_path:prepass noforwardadd halfasview //addshadow nolightmap

sampler2D _MainTex;
sampler2D _BumpTex;
float4 _Color;
fixed4 _DiffuseColor;

// Global illumination侩 溅捞爹 楷魂.
inline void LightingMyFoliage_GI(SurfaceOutput s, UnityGIInput data, inout UnityGI gi)
{
	gi = UnityGlobalIllumination(data, 1.0, s.Normal);
}

// forward rendering
//inline half4 LightingMyFoliage (SurfaceOutput s, half3 lightDir, half3 viewDir, half atten)
inline fixed4 LightingMyFoliage(SurfaceOutput s, half3 viewDir, UnityGI gi)
{
	fixed4 c;
	c.rgb = s.Albedo;
	//c.rgb *= _DiffuseColor.rgb; // 디퓨즈 칼라 조정
	c.a = s.Alpha;

	// 라이트맵 합성
#ifdef UNITY_LIGHT_FUNCTION_APPLY_INDIRECT
	c.rgb *= gi.indirect.diffuse;
#endif	

	return c;
}

// deferred
// pre-pass lighting doesn't provide us with the light direction --> no wrap arround diffuse...
/*
inline half4 LightingMyFoliage_PrePass (SurfaceOutput s, half4 light) {
	fixed4 c;
////////////////////////
	light.rgb = max(light.rgb, _VertexLitBaselight * 3);
////////////////////////
	c.rgb = s.Albedo * light.rgb;
	c.a = s.Alpha;
	return c;
}
*/

struct Input {
	float2 uv_MainTex;
	float4 color : COLOR;
};

void FastSinCos (float4 val, out float4 s, out float4 c) {
	val = val * 6.408849 - 3.1415927;
	// powers for taylor series
	float4 r5 = val * val;
	float4 r6 = r5 * r5;
	float4 r7 = r6 * r5;
	float4 r8 = r6 * r5;
	float4 r1 = r5 * val;
	float4 r2 = r1 * r5;
	float4 r3 = r2 * r5;
	//Vectors for taylor's series expansion of sin and cos
	float4 sin7 = {1, -0.16161616, 0.0083333, -0.00019841};
	float4 cos8  = {-0.5, 0.041666666, -0.0013888889, 0.000024801587};
	// sin
	s =  val + r1 * sin7.y + r2 * sin7.z + r3 * sin7.w;
	// cos
	c = 1 + r5 * cos8.x + r6 * cos8.y + r7 * cos8.z + r8 * cos8.w;
}


void vert (inout appdata_full v) {
	
////////// start bending
	
	// _Color = color passed from single meshes: main color
	// v.color = color passed by terrain engine: healthy / dry
	//
	// red = WaveMove or Displacement
	// blue = Time
	// green = Windspeed
	// alpha = adjustment Factor
	
	float factor = (1 - _Color.r -  v.color.r) * 0.5;
		
	const float _WindSpeed  = (_Color.g  +  v.color.g );		
		
	const float4 _waveXSize = float4(0.012, 0.02, 0.06, 0.024) * 4;
	const float4 _waveZSize = float4 (0.006, .02, 0.02, 0.05) * 4;
	const float4 waveSpeed = float4 (0.3, .5, .4, 1.2) * 4;

	float4 _waveXmove = float4(0.012, 0.02, -0.06, 0.048) * 10 * factor;
	float4 _waveZmove = float4(0.006, .02, -0.02, 0.1) * 10 * factor;
    
	float4 waves;
	waves = v.vertex.x * _waveXSize;
	waves += v.vertex.z * _waveZSize;

	waves += _Time.x * (1 - _Color.b * 2 - v.color.b ) * waveSpeed *_WindSpeed;

	float4 s, c;
	waves = frac (waves);
	FastSinCos (waves, s,c);

	float waveAmount = v.texcoord.y * (v.color.a + _Color.a);
	s *= waveAmount;

	// Faster winds move the grass more than slow winds 
	s *= normalize (waveSpeed);

	//s = s * s;
	float fade = dot (s, 1.3);
	//s = s * s;
	float3 waveMove = float3 (0,0,0);
	waveMove.x = dot (s, _waveXmove);
	waveMove.z = dot (s, _waveZmove);
	v.vertex.xz -= mul ((float3x3)unity_WorldToObject, waveMove).xz;
	
////////// end bending
}

void surf (Input IN, inout SurfaceOutput o) {
	half4 c = tex2D(_MainTex, float2(IN.uv_MainTex));
	o.Albedo = c.rgb;
	o.Alpha = c.a;
	
}
ENDCG
}
//Fallback "Transparent/Cutout/VertexLit"
}