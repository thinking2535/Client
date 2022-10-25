// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Piyong/Blend2_3000"
{
	Properties
	{
		_MainTex ("Particle Texture", 2D) = "white" {}
		_SubTex ("Sub (RGB)", 2D) = "white" {}
		_TintColor ("Tint Color", Color) = (1.0, 1.0, 1.0, 1.0)
		_Multiplier ("Color Multiplier", Float) = 1.0
		_InitUOffset ("Initial U Offset", Float) = 0.0		
		_InitVOffset ("Initial V Offset", Float) = 0.0		
		_InitRotate ("Initial Rotate", Float) = 0.0
		_InitScale ("Initial Scale", Float) = 1.0		
		_USpeed ("U Speed", Float) = 0.0
		_VSpeed ("V Speed", Float) = 0.0
		_RotateSpeed ("Rotate Speed", Float) = 0.0
		_ScaleSpeed ("Scale Speed", Float) = 0.0
		_InitUOffset2 ("Initial U Offset2", Float) = 0.0		
		_InitVOffset2 ("Initial V Offset2", Float) = 0.0		
		_InitRotate2 ("Initial Rotate2", Float) = 0.0
		_InitScale2 ("Initial Scale2", Float) = 1.0		
		_USpeed2 ("U Speed2", Float) = 0.0
		_VSpeed2 ("V Speed2", Float) = 0.0
		_RotateSpeed2 ("Rotate Speed2", Float) = 0.0
		_ScaleSpeed2 ("Scale Speed2", Float) = 0.0
		_ShowSecondOnly ("Show Second Texture Only", Float) = 0
		//ui shader에서 property 없다고 워닝 뛰어서 추가함.
		_Stencil("Dummy1", Float) = 0
		_StencilOp("Dummy2", Float) = 0
		_StencilComp("Dummy3", Float) = 0
		_StencilWriteMask("Dummy4", Float) = 255
		_StencilReadMask("Dummy5", Float) = 255
		_ColorMask("Dummy6", Float) = 15
	}
		
	SubShader
	{
		Tags {"Queue" = "Transparent" }
		
		Pass
		{
			Fog { Mode Off }
			Lighting Off
			Blend SrcAlpha OneMinusSrcAlpha
			Cull Off
			ZWrite Off
			
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag	
			#pragma fragmentoption ARB_precision_hint_fastest		
			#include "UnityCG.cginc"
			
			sampler2D _MainTex;
			half4 _MainTex_ST;
			sampler2D _SubTex;
			half4 _SubTex_ST;
			
			half4 _TintColor;
			half _Multiplier;
			half _InitUOffset;
			half _InitVOffset;
			half _InitRotate;
			half _InitScale;
			half _USpeed;
			half _VSpeed;
			half _RotateSpeed;
			half _ScaleSpeed;
			half _InitUOffset2;
			half _InitVOffset2;
			half _InitRotate2;
			half _InitScale2;
			half _USpeed2;
			half _VSpeed2;
			half _RotateSpeed2;
			half _ScaleSpeed2;
			int _ShowSecondOnly;
			
			struct vertexInput
			{
				half4 vertex : POSITION;
				half4 vertexcolor : COLOR0;
				half2 texcoord0 : TEXCOORD0;
			};

			struct fragmentInput
			{
				half4 position : SV_POSITION;
				half4 vertexcolor : COLOR0;
				half2 texcoord0 : TEXCOORD0;
				half2 texcoord1 : TEXCOORD1;
			};
			

			fragmentInput vert(vertexInput i)
			{
				fragmentInput o;
				o.position = UnityObjectToClipPos (i.vertex);
				
				//
				half2 uv = TRANSFORM_TEX(i.texcoord0.xy, _MainTex);				
				half2 center = TRANSFORM_TEX(half2(0.5f, 0.5f), _MainTex);
				const half deg2rad = 0.01745328f;
				
				half2 _uv = uv - center;
				
				half scale = 1.0f / (_InitScale + _ScaleSpeed * _Time.y);
				_uv = _uv * scale;

				half rotate = _InitRotate * deg2rad + _RotateSpeed * deg2rad * _Time.y;
				half _cos = cos(rotate);
				half _sin = sin(rotate);

				uv.x = (_uv.x * _cos) - (_uv.y * _sin);
				uv.y = (_uv.x * _sin) + (_uv.y * _cos);				
				uv = center + uv;
								
				half uoffset = _USpeed * _Time.y + _InitUOffset;
				half voffset = _VSpeed * _Time.y + _InitVOffset;
				uv.x = uv.x + uoffset;
				uv.y = uv.y + voffset;				
				
				o.texcoord0 = uv;

				//
				half2 uv2 = TRANSFORM_TEX(i.texcoord0.xy, _SubTex);				
				half2 center2 = TRANSFORM_TEX(half2(0.5f, 0.5f), _SubTex);
				
				_uv = uv2 - center2;
				
				scale = 1.0f / (_InitScale2 + _ScaleSpeed2 * _Time.y);
				_uv = _uv * scale;
				
				rotate = _InitRotate2 * deg2rad + _RotateSpeed2 * deg2rad * _Time.y;
				_cos = cos(rotate);
				_sin = sin(rotate);

				uv2.x = (_uv.x * _cos) - (_uv.y * _sin);
				uv2.y = (_uv.x * _sin) + (_uv.y * _cos);				
				uv2 = center2 + uv2;
								
				uoffset = _USpeed2 * _Time.y + _InitUOffset2;
				voffset = _VSpeed2 * _Time.y + _InitVOffset2;
				uv2.x = uv2.x + uoffset;
				uv2.y = uv2.y + voffset;				
				
				//
				o.texcoord1 = uv2;
				
				//
				o.vertexcolor = i.vertexcolor;

				//
				return o;
			}

			fixed4 frag(fragmentInput i) : COLOR
			{
			#if defined(SHADER_API_D3D9)		// window(editor)에서만 동작하도록..
				fixed4 mainTexColor;
				if(_ShowSecondOnly == 1) 
					mainTexColor = fixed4(1.0f, 1.0f, 1.0f, 1.0f);
				else
					mainTexColor = tex2D(_MainTex, i.texcoord0);
			#else
				fixed4 mainTexColor = tex2D(_MainTex, i.texcoord0);
			#endif
				fixed4 subTexColor = tex2D(_SubTex, i.texcoord1);
				
				fixed4 color = mainTexColor * subTexColor * _TintColor * i.vertexcolor;
				color = saturate(color * _Multiplier);
			
				return color;
			}
			ENDCG
		}
	}
}