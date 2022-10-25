// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Unlit alpha-blended shader.
// - no lighting
// - no lightmap support
// - no per-material color

Shader "Snow Family/FX/Unlit-Multi_Detail_Cutout" {
Properties {
	_TintColor ("Tint Color", Color) = (1,1,1,1)
	_MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
	_Detail ("Base (RGB) Trans (A)", 2D) = "white" {}
	_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
}

SubShader {
	Tags {"Queue"="Transparent+1" "IgnoreProjector"="True" "RenderType"="TransparentCutout"}
	Blend DstColor SrcColor
	//Blend SrcAlpha One
	//AlphaTest Greater 0.5
	ColorMask RGB
	Cull off Lighting Off ZWrite Off
	LOD 150
	
	Pass {  
		CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_fog
			
			#include "UnityCG.cginc"

			struct appdata_t {
				half4 vertex : POSITION;
				half2 texcoord0 : TEXCOORD0;
				half2 texcoord1 : TEXCOORD2;
			};

			struct v2f {
				half4 vertex : SV_POSITION;
				half2 texcoord0 : TEXCOORD0;
				half2 texcoord1 : TEXCOORD2;
				UNITY_FOG_COORDS(1)
			};

			sampler2D _MainTex;
			sampler2D _Detail;
			half4 _MainTex_ST;
			half4 _Detail_ST;
			fixed4 _TintColor;
			fixed _Cutoff;
			
			v2f vert (appdata_t v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.texcoord0 = TRANSFORM_TEX(v.texcoord0, _MainTex);
				o.texcoord1 = TRANSFORM_TEX(v.texcoord1, _Detail);
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : COLOR
			{
				fixed4 col = (tex2D(_MainTex, i.texcoord0) * _TintColor * tex2D(_Detail, i.texcoord1)) * 2;
				clip(col.a - _Cutoff);
				UNITY_APPLY_FOG(i.fogCoord, col);
				return col;
			}
		ENDCG
	}
}

}