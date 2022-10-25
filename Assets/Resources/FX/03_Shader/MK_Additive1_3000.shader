﻿// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Piyong/Additive1_3000"
{
	Properties
	{
		_MainTex ("Particle Texture", 2D) = "white" {}
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
		Tags{ "Queue" = "Transparent" }
		Pass
		{
			Fog { Mode Off }
			Lighting Off
			Blend SrcAlpha One
			Cull Off
			ZWrite Off

			Stencil
			{
				Ref[_Stencil]
				Comp[_StencilComp]
				Pass[_StencilOp]
				ReadMask[_StencilReadMask]
				WriteMask[_StencilWriteMask]
			}
			
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"
			
			
			sampler2D _MainTex;
			float4 _MainTex_ST;
			
			float4 _TintColor;
			float _Multiplier;
			float _InitUOffset;
			float _InitVOffset;
			float _InitRotate;
			float _InitScale;
			float _USpeed;
			float _VSpeed;
			float _RotateSpeed;
			float _ScaleSpeed;
			
			struct vertexInput
			{
				float4 vertex : POSITION;
				float4 vertexcolor : COLOR0;
				float2 texcoord0 : TEXCOORD0;
			};

			struct fragmentInput
			{
				float4 position : SV_POSITION;
				float4 vertexcolor : COLOR0;
				float2 texcoord0 : TEXCOORD0;
			};			

			fragmentInput vert(vertexInput i)
			{
				fragmentInput o;
				o.position = UnityObjectToClipPos (i.vertex);
				
				//
				float2 uv = TRANSFORM_TEX(i.texcoord0.xy, _MainTex);
				const float2 center = float2(0.5f, 0.5f);
				const float deg2rad = 0.01745328f;
				
				float2 _uv = uv - center;
				
				float scale = 1.0f / (_InitScale + _ScaleSpeed * _Time.y);
				_uv = _uv * scale;

				float rotate = _InitRotate * deg2rad + _RotateSpeed * deg2rad * _Time.y;
				float _cos = cos(rotate);
				float _sin = sin(rotate);

				uv.x = (_uv.x * _cos) - (_uv.y * _sin);
				uv.y = (_uv.x * _sin) + (_uv.y * _cos);				
				uv = center + uv;
								
				float uoffset = _USpeed * _Time.y + _InitUOffset;
				float voffset = _VSpeed * _Time.y + _InitVOffset;
				uv.x = uv.x + uoffset;
				uv.y = uv.y + voffset;				
				
				o.texcoord0 = uv;

				//
				o.vertexcolor = i.vertexcolor;

				//
				return o;
			}

			fixed4 frag(fragmentInput i) : COLOR
			{
				fixed4 color = tex2D(_MainTex, i.texcoord0);
				color = saturate(color * _TintColor * i.vertexcolor * _Multiplier);
				
				color.a *= saturate(ceil(color.r + color.g + color.b - 0.05f));
				//clip(color.r + color.g + color.b - 0.05f);
				//if((color.r + color.g + color.b - 0.05f) < 0.0f) discard;				
				return color;
			}
			ENDCG
		}
	}
}