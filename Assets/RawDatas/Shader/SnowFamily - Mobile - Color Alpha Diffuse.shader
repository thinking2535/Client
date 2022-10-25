// Simplified Diffuse shader. Differences from regular Diffuse one:
// - no Main Color
// - fully supports only 1 directional light. Other lights can affect it, but it will be per-vertex/SH.

Shader "Snow Family/Color Alpha Diffuse" {
Properties {
	_Color ("Main Color", Color) = (1,1,1,1)
	//_RGBColor ("RGB Color", Color) = (1,1,1,1)
	_MainTex ("Base (RGB)", 2D) = "white" {}
}

SubShader {
	Tags {"Queue"="Transparent" "RenderType"="Transparent"}
	Alphatest Greater 0.5
	Blend SrcAlpha OneMinusSrcAlpha 
  Pass {
            ColorMask 0
        }
CGPROGRAM
#pragma surface surf Lambert alpha:fade

sampler2D _MainTex;
fixed4 _Color;

struct Input {
	float2 uv_MainTex;
};

void surf (Input IN, inout SurfaceOutput o) {
	fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
	o.Albedo = c.rgb * _Color.rgb;
	o.Alpha = c.a * _Color.a;
}
ENDCG
}

Fallback "Mobile/VertexLit"
}
