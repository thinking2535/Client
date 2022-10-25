// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Snow Family/Particle/GlowAdditiveTwoColor - Detail - Offset" {
	Properties {
	_TintColor ("Tint Color", Color) = (0.5,0.5,0.5,0.5)
	_CoreColor ("Core Color", Color) = (0.5,0.5,0.5,0.5)
	_MainTex ("Particle Texture", 2D) = "white" {}
	_Detail ("Base (RGB) Trans (A)", 2D) = "white" {}
	_TintStrength ("Tint Color Strength", Float) = 1
	_CoreStrength ("Core Color Strength", Float) = 1
	_CutOutLightCore ("CutOut Light Core", Range(0, 1)) = 0.5

	 _ScrollSpd("_ScrollSpd", Float) = 30
}

Category {
	Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }
	Blend SrcAlpha One
	Cull Off 
	Lighting Off 
	ZWrite Off 
	Fog { Color (0,0,0,0) }
	
	SubShader {
		Pass {
		
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			//#pragma multi_compile_particles
		
			#include "UnityCG.cginc"

			sampler2D _MainTex;
			sampler2D _Detail;
			fixed4 _TintColor;
			fixed4 _CoreColor;
			float _CutOutLightCore;
			float _TintStrength;
			float _CoreStrength;

			
			struct appdata_t {
				float4 vertex : POSITION;
				fixed4 color : COLOR;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f {
				float4 vertex : POSITION;
				fixed4 color : COLOR;
				fixed2 uv : TEXCOORD0;
				fixed2 uv2 : TEXCOORD1;
			};
			
			float4 _MainTex_ST;
			  fixed _ScrollSpd;

			v2f vert (appdata_t v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.color = v.color;
   				o.uv.x = v.texcoord.x;
				o.uv.y = v.texcoord.y;
				fixed DetailTexOffset = _Time * _ScrollSpd;
  				o.uv2.x = v.texcoord.x;
   				o.uv2.y = v.texcoord.y + DetailTexOffset;


				//o.texcoord = TRANSFORM_TEX(v.texcoord,_MainTex);
				return o;
			}


			
			fixed4 frag (v2f i) : COLOR
			{
				fixed4 tex = tex2D(_MainTex, i.uv);
				fixed4 detail = tex2D(_Detail, i.uv2);
				fixed4 col = (_TintColor * tex.g * _TintStrength + tex.r * _CoreColor * _CoreStrength  - _CutOutLightCore) * detail; 
				return i.color * clamp(col, 0, 255);
			}
			ENDCG 
		}
	}	
}
}
