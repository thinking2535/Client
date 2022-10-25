// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Snow Family/FX/Unlit-Add_Detail_Power" {
Properties {
	_TintColor("Tint Color", Color) = (0.5,0.5,0.5,0.5)
	_CoreColor("Core Color", Color) = (0.5,0.5,0.5,0.5)
	_MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
	_Detail ("Base (RGB) Trans (A)", 2D) = "white" {}
	_TintStrength("Tint Color Strength", Float) = 1
	_CoreStrength("Core Color Strength", Float) = 1
	_CutOutLightCore("CutOut Light Core", Range(0, 1)) = 0.5
}

SubShader {
	Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
	Cull off Lighting Off ZWrite Off Fog { Mode Off }
	LOD 100
	
	ZWrite Off
	Blend SrcAlpha One
	
	Pass {  
		CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_fog
			
			#include "UnityCG.cginc"

			sampler2D _MainTex;
			sampler2D _Detail;
			fixed4 _TintColor;
			fixed4 _CoreColor;
			float _CutOutLightCore;
			half4 _MainTex_ST;
			half4 _Detail_ST;
			float _TintStrength;
			float _CoreStrength;

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
				fixed4 tex = tex2D(_MainTex, i.texcoord0);
				fixed4 detail = tex2D(_Detail, i.texcoord1);
				fixed4 col = (_TintColor * tex.g * _TintStrength + tex.r * _CoreColor * _CoreStrength) * tex.a * detail - _CutOutLightCore ;
				//fixed4 col = ((tex2D(_MainTex, i.texcoord0) * (tex2D(_MainTex, i.texcoord0))) * _TintColor * tex2D(_Detail, i.texcoord1)) * 2;
				//UNITY_APPLY_FOG(i.fogCoord, col);
				return col;
			}
		ENDCG
	}
}

}
