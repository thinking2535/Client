// Simplified Bumped Specular shader. Differences from regular Bumped Specular one:
// - no Main Color nor Specular Color
// - specular lighting directions are approximated per vertex
// - writes zero to alpha channel
// - Normalmap uses Tiling/Offset of the Base texture
// - no Deferred Lighting support
// - no Lightmap support
// - fully supports only 1 directional light. Other lights can affect it, but it will be per-vertex/SH.

Shader "Snow Family/Bumped Specular Shadow Direction" {
Properties {
	_LightPosition("Light position", Vector) = (0.75, 1.0, 0.75, 0.0)
	_Color("Base Color", Color) = (1,1,1,1)
	_SpecularColor("Specular Color", Color) = (1,1,1,1)
	_Shininess ("Shininess", Range (0.03, 1)) = 0.078125
	_MainTex ("Base (RGB) Gloss (A)", 2D) = "white" {}
	[NoScaleOffset] _BumpMap ("Normalmap", 2D) = "bump" {}
}
SubShader { 
	Tags { "RenderType"="Opaque" }
	LOD 250
	
CGPROGRAM
#pragma surface surf MobileBlinnPhong exclude_path:prepass noforwardadd interpolateview halfasview//nolightmap

half4 _LightPosition;
half4 _Color;
sampler2D _MainTex;
sampler2D _BumpMap;
float4 _SpecularColor;
half _Shininess;

// Global illumination¿ë ½¦ÀÌµù ¿¬»ê.
inline void LightingMobileBlinnPhong_GI(SurfaceOutput s, UnityGIInput data, inout UnityGI gi)
{
	gi = UnityGlobalIllumination(data, 1.0, s.Normal);
}

inline fixed4 LightingMobileBlinnPhong (SurfaceOutput s, half3 viewDir, UnityGI gi)
{
	half3 nlp = normalize(_LightPosition.xyz);
			
	fixed diff = max (0, dot (nlp, s.Normal));
	fixed nh = max (0, dot (nlp, s.Normal));
	fixed spec = pow (nh, s.Specular*128) * s.Gloss;
	fixed4 c;
	c.rgb = s.Albedo + (s.Albedo * _LightColor0.rgb * diff + _SpecularColor * spec);
	UNITY_OPAQUE_ALPHA(c.a);
		
	// °£Á¢±¤(½¦µµ¿ì¸Ê) Ãß°¡.
#ifdef UNITY_LIGHT_FUNCTION_APPLY_INDIRECT
	c.rgb *= gi.indirect.diffuse;
#endif

	return c;
}


struct Input {
	float2 uv_MainTex;
};

void surf (Input IN, inout SurfaceOutput o) {
	fixed4 tex = tex2D(_MainTex, IN.uv_MainTex);
	o.Albedo = tex.rgb * _Color.rgb;
	o.Gloss = tex.a;
	o.Alpha = tex.a;
	o.Specular = _Shininess;
	o.Normal = UnpackNormal (tex2D(_BumpMap, IN.uv_MainTex));
}
ENDCG
}

Fallback "Mobile/DiffuseBumpedShadow"
}
