// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Unlit alpha-blended shader.
// - no lighting
// - no lightmap support
// - no per-material color

Shader "Snow Family/FX/Unlit-Alpha_Detail" {
Properties {
	_TintColor ("Tint Color", Color) = (1,1,1,1)
	_MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
	_Detail ("Base (RGB) Trans (A)", 2D) = "white" {}
	_UseState ("Use State", Int) = 0
	_MatrixColor ("MatrixColor", Color) = (0.65,0.65,0.65,0.65)
	_MatrixPower ("MatrixPower", float) = 0
}

SubShader {
	Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
	Cull off Lighting Off ZWrite On Fog { Mode Off }
	LOD 100
	
	ZWrite Off
	Blend SrcAlpha OneMinusSrcAlpha
	
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

			int _UseState;
			fixed4 _MatrixColor;
			float _MatrixPower;
			
			v2f vert (appdata_t v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.texcoord0 = TRANSFORM_TEX(v.texcoord0, _MainTex);
				o.texcoord1 = TRANSFORM_TEX(v.texcoord0, _Detail);
				UNITY_TRANSFER_FOG(o,o.vertex);

				return o;
			}
			
			fixed4 frag (v2f i) : COLOR
			{
				fixed4 col;
				
				//------------------------------------------------------------------------------
				// 수정 - 2017. 02. 27 I.J.
				// : Branching (if 분기) 1개 제거 
				//------------------------------------------------------------------------------
				col = tex2D(_MainTex, i.texcoord0) * _TintColor * tex2D(_Detail, i.texcoord1) * 2;
				
				if (_UseState == 1)
					col.rgb = col.rgb * _MatrixColor.rgb * _MatrixPower;

				/*
				if(_UseState == 0)
				{
					col = tex2D(_MainTex, i.texcoord0) * _TintColor * tex2D(_Detail, i.texcoord1) * 2;
				}
				if(_UseState == 1)
				{
					//col = (tex2D(_MainTex, i.texcoord0) * _TintColor * tex2D(_Detail, i.texcoord1)  * 2 * 0.3) + (_MatrixColor * 0.84);

					col = tex2D(_MainTex, i.texcoord0) * _TintColor * tex2D(_Detail, i.texcoord1) * 2;

					col.rgb = col.rgb * _MatrixColor.rgb * _MatrixPower;
				}
				*/

				UNITY_APPLY_FOG(i.fogCoord, col);
				return col;
			}
		ENDCG
	}
}

}
