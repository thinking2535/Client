// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Snow Family/FX/Unlit-MultX2" {
Properties {
	_MainTex ("Particle Texture", 2D) = "white" {}
	_TintColor ("Tint Color", Color) = (1,1,1,1)
	//_InvFade ("Soft Particles Factor", Range(0.01,3.0)) = 1.0
}

Category {
	Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }
	Blend DstColor SrcColor
	ColorMask RGB
	Cull Off Lighting Off ZWrite Off
	
	SubShader {
		Pass {
		
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			//#pragma multi_compile_particles
			#pragma multi_compile_fog

			#include "UnityCG.cginc"

			sampler2D _MainTex;
			fixed4 _TintColor;
			
			struct appdata_t {
				float4 vertex : POSITION;
				fixed4 color : COLOR;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f {
				float4 vertex : SV_POSITION;
				fixed4 color : COLOR;
				float2 texcoord : TEXCOORD0;
				UNITY_FOG_COORDS(1)
				
			};
			
			float4 _MainTex_ST;

			v2f vert (appdata_t v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				
				o.color = v.color;
				o.texcoord = TRANSFORM_TEX(v.texcoord,_MainTex);
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}

			//sampler2D_float _CameraDepthTexture;
			//float _InvFade;
			
			fixed4 frag (v2f i) : SV_Target
			{
				
				
				fixed4 col;
				fixed4 tex = tex2D(_MainTex, i.texcoord);
				col.rgb = tex.rgb * i.color.rgb * 2 * 0.5;
				col.a = i.color.a * tex.a * _TintColor.a;
				col = lerp(fixed4(0.5f,0.5f,0.5f,0.5f), col, col.a);
				UNITY_APPLY_FOG_COLOR(i.fogCoord, col, fixed4(0.5,0.5,0.5,0.5)); // fixed4(0.5,0.5,0.5,0.5) 요게 포그 컬러와 값을 맞춰준다. 없으면 색이 곱해져서 나온다
				return col;
			}
			ENDCG 
		}
	}
}
}
