// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Piyong/Distortion_Blend(Intro)"
{
	Properties
	{
		_Tex01("Tex01", 2D) = "white" {}
		_MaskTest("MaskTest", 2D) = "white" {}
		_Base_Color("Base_Color", Color) = (1,1,1,0)
		_Main_Powor("Main_Powor", Float) = 1
		_Tex01_Panner("Tex01_Panner", Vector) = (0,0,0,0)
		_Mask_Panner("Mask_Panner", Vector) = (0,0,0,0)
		_Distortion01("Distortion01", 2D) = "white" {}
		_Distortion01_Power("Distortion01_Power", Float) = 1
		_Distortion01_UVSpeed("Distortion01_UVSpeed", Vector) = (0,0,0,0)
		_Distortion02_Mask("Distortion02_Mask", 2D) = "white" {}
		_Alpha_Mask01("Alpha_Mask01", 2D) = "white" {}
		_Alpha_Power("Alpha_Power", Float) = 1.23
		_Distortion02_power("Distortion02_power", Float) = 0.5
		_Distortion02_UVSpeed("Distortion02_UVSpeed", Vector) = (0,0,0,0)
		_Alpha_Mask01_UV("Alpha_Mask01_UV", Vector) = (0,0,0,0)
		_Alpha_Mask01_Ratator("Alpha_Mask01_Ratator", Float) = 0
		[HideInInspector] _tex4coord2( "", 2D ) = "white" {}
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Transparent" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Off
		ZWrite Off
		ZTest LEqual
		Blend SrcAlpha OneMinusSrcAlpha , SrcAlpha OneMinusSrcAlpha
		
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf NoLighting keepalpha noshadow noambient novertexlights nolightmap  nodynlightmap nodirlightmap nofog nometa noforwardadd 
		#undef TRANSFORM_TEX
		#define TRANSFORM_TEX(tex,name) float4(tex.xy * name##_ST.xy + name##_ST.zw, tex.z, tex.w)
		struct Input
		{
			float2 uv_texcoord;
			float4 uv2_tex4coord2;
			float4 vertexColor : COLOR;
		};

		uniform sampler2D _Tex01;
		uniform float4 _Tex01_ST;
		uniform float2 _Tex01_Panner;
		uniform sampler2D _Distortion01;
		uniform float2 _Distortion01_UVSpeed;
		uniform float4 _Distortion01_ST;
		uniform float _Distortion01_Power;
		uniform sampler2D _Distortion02_Mask;
		uniform float2 _Distortion02_UVSpeed;
		uniform float4 _Distortion02_Mask_ST;
		uniform float _Distortion02_power;
		uniform float _Main_Powor;
		uniform float4 _Base_Color;
		uniform sampler2D _MaskTest;
		uniform float4 _MaskTest_ST;
		uniform float2 _Mask_Panner;
		uniform float _Alpha_Power;
		uniform sampler2D _Alpha_Mask01;
		uniform float4 _Alpha_Mask01_ST;
		uniform float _Alpha_Mask01_Ratator;
		uniform float2 _Alpha_Mask01_UV;

		fixed4 LightingNoLighting(SurfaceOutput s, fixed3 lightDir, fixed atten)
		{
			fixed4 c;
			c.rgb = s.Albedo;
			c.a = s.Alpha;
			return c;
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			float2 uv0_Tex01 = i.uv_texcoord * _Tex01_ST.xy + _Tex01_ST.zw;
			float2 temp_cast_1 = (_Tex01_Panner.x).xx;
			float2 panner58 = ( _Time.y * temp_cast_1 + float2( 0,0 ));
			float2 temp_cast_3 = (_Tex01_Panner.y).xx;
			float2 panner63 = ( _Time.y * temp_cast_3 + float2( 0,0 ));
			float2 appendResult69 = (float2(( (panner58).xy * 1.0 ).x , ( (panner63).xy * 1.0 ).x));
			float2 uv0_Distortion01 = i.uv_texcoord * _Distortion01_ST.xy + _Distortion01_ST.zw;
			float2 panner124 = ( 1.0 * _Time.y * _Distortion01_UVSpeed + uv0_Distortion01);
			float2 uv0_Distortion02_Mask = i.uv_texcoord * _Distortion02_Mask_ST.xy + _Distortion02_Mask_ST.zw;
			float2 panner41 = ( 1.0 * _Time.y * _Distortion02_UVSpeed + uv0_Distortion02_Mask);
			float4 appendResult126 = (float4(i.uv2_tex4coord2.x , i.uv2_tex4coord2.y , 0.0 , 0.0));
			float4 temp_output_14_0 = ( ( tex2D( _Distortion01, (panner124).xy ) * _Distortion01_Power ) * ( tex2D( _Distortion02_Mask, panner41 ) * _Distortion02_power ) * appendResult126 );
			float4 temp_output_45_0 = ( tex2D( _Tex01, ( float4( uv0_Tex01, 0.0 , 0.0 ) + float4( appendResult69, 0.0 , 0.0 ) + temp_output_14_0 ).rg ) * _Main_Powor * _Base_Color * i.vertexColor );
			o.Albedo = temp_output_45_0.rgb;
			o.Emission = temp_output_45_0.rgb;
			float2 uv0_MaskTest = i.uv_texcoord * _MaskTest_ST.xy + _MaskTest_ST.zw;
			float2 temp_cast_11 = (_Mask_Panner.x).xx;
			float2 panner173 = ( _Time.y * temp_cast_11 + float2( 0,0 ));
			float2 temp_cast_13 = (_Mask_Panner.y).xx;
			float2 panner174 = ( _Time.y * temp_cast_13 + float2( 0,0 ));
			float2 appendResult182 = (float2(( (panner173).xy * 1.0 ).x , ( (panner174).xy * 1.0 ).x));
			float2 uv0_Alpha_Mask01 = i.uv_texcoord * _Alpha_Mask01_ST.xy + _Alpha_Mask01_ST.zw;
			float cos165 = cos( _Alpha_Mask01_Ratator );
			float sin165 = sin( _Alpha_Mask01_Ratator );
			float2 rotator165 = mul( uv0_Alpha_Mask01 - float2( 0.5,0.5 ) , float2x2( cos165 , -sin165 , sin165 , cos165 )) + float2( 0.5,0.5 );
			float2 _Vector0 = float2(1,1);
			float2 temp_cast_19 = (_Vector0.x).xx;
			float2 panner157 = ( _Alpha_Mask01_UV.x * temp_cast_19 + float2( 0,0 ));
			float2 temp_cast_21 = (_Vector0.y).xx;
			float2 panner158 = ( _Alpha_Mask01_UV.y * temp_cast_21 + float2( 0,0 ));
			float3 appendResult164 = (float3((panner157).xy.x , (panner158).xy.x , 0.0));
			o.Alpha = ( ( tex2D( _MaskTest, ( float4( uv0_MaskTest, 0.0 , 0.0 ) + float4( appendResult182, 0.0 , 0.0 ) + temp_output_14_0 ).rg ) * _Alpha_Power ) * tex2D( _Alpha_Mask01, ( float3( rotator165 ,  0.0 ) + appendResult164 ).xy ) * i.vertexColor.a ).r;
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=16600
1930;8;1906;1124;2717.175;1269.515;1;True;True
Node;AmplifyShaderEditor.TexturePropertyNode;34;-2759.319,-245.7483;Float;True;Property;_Distortion01;Distortion01;7;0;Create;True;0;0;False;0;None;b3e490ef12462bc449219cd0443e58db;False;white;Auto;Texture2D;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.Vector2Node;123;-2432.842,-53.69399;Float;False;Property;_Distortion01_UVSpeed;Distortion01_UVSpeed;9;0;Create;True;0;0;False;0;0,0;-0.48,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.TextureCoordinatesNode;122;-2423.272,-170.8674;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TexturePropertyNode;39;-2747.55,248.7281;Float;True;Property;_Distortion02_Mask;Distortion02_Mask;10;0;Create;True;0;0;False;0;None;b3e490ef12462bc449219cd0443e58db;False;white;Auto;Texture2D;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;40;-2536.019,214.3668;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector2Node;77;-2519.478,360.308;Float;False;Property;_Distortion02_UVSpeed;Distortion02_UVSpeed;14;0;Create;True;0;0;False;0;0,0;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.Vector2Node;172;-3596.49,1309.198;Float;False;Property;_Mask_Panner;Mask_Panner;6;0;Create;True;0;0;False;0;0,0;-2.45,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.PannerNode;124;-2120.188,-163.0602;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleTimeNode;171;-3208.757,1307.669;Float;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;55;-1801.934,-734.3166;Float;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;174;-3066.867,1547.197;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.Vector2Node;56;-2028.506,-787.2651;Float;False;Property;_Tex01_Panner;Tex01_Panner;5;0;Create;True;0;0;False;0;0,0;-2.45,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.ComponentMaskNode;125;-1867.374,-168.9522;Float;False;True;True;True;True;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PannerNode;173;-2904.467,1190.696;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PannerNode;41;-2246.344,217.7034;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;25;-2054.81,111.6876;Float;True;Property;_TM_Gradi21;TM_Gradi21;7;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;177;-2668.082,1314.04;Float;True;Constant;_Float0;Float 0;4;0;Create;True;0;0;False;0;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;36;-1530.271,5.094133;Float;False;Property;_Distortion01_Power;Distortion01_Power;8;0;Create;True;0;0;False;0;1;1.62;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;169;-1900.777,2409.081;Float;False;Property;_Alpha_Mask01_UV;Alpha_Mask01_UV;15;0;Create;True;0;0;False;0;0,0;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.TextureCoordinatesNode;127;-2112.646,420.9083;Float;False;1;-1;4;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;5;-1600.965,-190.8325;Float;True;Property;_TM_Magama_Noise_di;TM_Magama_Noise_di;3;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector2Node;168;-1881.212,2261.591;Float;False;Constant;_Vector0;Vector 0;4;0;Create;True;0;0;False;0;1,1;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.ComponentMaskNode;176;-2710.754,1536.818;Float;False;True;True;True;True;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PannerNode;58;-1647.456,-928.4662;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;178;-2681.446,1648.788;Float;True;Constant;_Float3;Float 3;6;0;Create;True;0;0;False;0;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;63;-1631.856,-576.9658;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.ComponentMaskNode;175;-2721.557,1184.714;Float;False;True;True;True;True;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;30;-1987.451,319.2898;Float;False;Property;_Distortion02_power;Distortion02_power;13;0;Create;True;0;0;False;0;0.5;0.5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;179;-2467.155,1296.915;Float;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;65;-1411.688,-503.9005;Float;True;Constant;_Float2;Float 2;6;0;Create;True;0;0;False;0;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TexturePropertyNode;149;-2147.025,1686.063;Float;True;Property;_Alpha_Mask01;Alpha_Mask01;11;0;Create;True;0;0;False;0;e56b320f1afc1ff42bace4981ea36bd8;e56b320f1afc1ff42bace4981ea36bd8;False;white;Auto;Texture2D;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.PannerNode;157;-1584.391,2240.727;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;59;-1411.071,-805.1224;Float;True;Constant;_Float1;Float 1;4;0;Create;True;0;0;False;0;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;158;-1587.275,2371.198;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;29;-1698.392,200.3618;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;181;-2475.458,1554.016;Float;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;35;-1234.906,-115.2109;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.DynamicAppendNode;126;-1380.77,402.0063;Float;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.TexturePropertyNode;180;-2656.469,800.6498;Float;True;Property;_MaskTest;MaskTest;1;0;Create;True;0;0;False;0;None;7b55893f2a859a649b30b93027fe7024;False;white;Auto;Texture2D;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.ComponentMaskNode;60;-1464.546,-934.4482;Float;False;True;True;True;True;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.ComponentMaskNode;66;-1453.743,-582.3445;Float;False;True;True;True;True;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;61;-1210.145,-822.2474;Float;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TexturePropertyNode;68;-1529.642,-1165.322;Float;True;Property;_Tex01;Tex01;0;0;Create;True;0;0;False;0;None;7b55893f2a859a649b30b93027fe7024;False;white;Auto;Texture2D;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.ComponentMaskNode;161;-1404.131,2238.282;Float;False;True;True;True;True;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;167;-1458.376,2151.02;Float;False;Property;_Alpha_Mask01_Ratator;Alpha_Mask01_Ratator;16;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;150;-1449.153,1884.978;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;183;-2157.913,1085.814;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;14;-1064.99,71.4361;Float;False;3;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT4;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;64;-1218.447,-565.1464;Float;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.DynamicAppendNode;182;-2104.311,1228.968;Float;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.Vector2Node;163;-1412.457,2019.866;Float;False;Constant;_Vector1;Vector 1;4;0;Create;True;0;0;False;0;0.5,0.5;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.ComponentMaskNode;160;-1402.556,2382.915;Float;False;True;True;True;True;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RotatorNode;165;-853.5721,2087.941;Float;False;3;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;2;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;67;-1192.675,-976.5219;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;184;-1510.391,1255.389;Float;False;3;3;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.DynamicAppendNode;69;-1032.476,-767.4529;Float;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.DynamicAppendNode;164;-1064.716,2338.67;Float;False;FLOAT3;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleAddOpNode;166;-629.3314,2214.256;Float;False;2;2;0;FLOAT2;0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleAddOpNode;53;-794.2535,-970.2258;Float;False;3;3;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;185;-953.603,741.13;Float;True;Property;_TextureSample0;Texture Sample 0;2;0;Create;True;0;0;False;0;47afbd361055fbb49bcece2a6a1ea365;47afbd361055fbb49bcece2a6a1ea365;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;131;-840.3523,933.0024;Float;False;Property;_Alpha_Power;Alpha_Power;12;0;Create;True;0;0;False;0;1.23;1.23;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;47;-102.7117,-221.3676;Float;False;Property;_Main_Powor;Main_Powor;4;0;Create;True;0;0;False;0;1;1.75;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;148;-671.7618,1745.163;Float;True;Property;_BaseTex;BaseTex;2;0;Create;True;0;0;False;0;445e99383b6e0c64a9cc0f2ed204c96a;445e99383b6e0c64a9cc0f2ed204c96a;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;76;-152.5831,-85.28558;Float;False;Property;_Base_Color;Base_Color;3;0;Create;True;0;0;False;0;1,1,1,0;1,1,1,1;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.VertexColorNode;186;-85.35889,248.2917;Float;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;1;-496.4229,-1090.988;Float;True;Property;_Main_T;Main_T;2;0;Create;True;0;0;False;0;47afbd361055fbb49bcece2a6a1ea365;47afbd361055fbb49bcece2a6a1ea365;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;132;-383.5499,725.6173;Float;True;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;45;311.4699,-215.6027;Float;True;4;4;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;134;574.3486,487.4945;Float;False;3;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;1476.376,-173.2622;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;Piyong/Distortion_Blend(Intro);False;False;False;False;True;True;True;True;True;True;True;True;False;False;True;False;False;False;False;False;False;Off;2;False;-1;3;False;-1;False;1;False;-1;1;False;-1;False;3;Custom;1;True;False;0;True;Opaque;;Overlay;All;False;True;False;False;True;False;False;False;False;False;False;False;False;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;False;2;5;False;-1;10;False;-1;2;5;False;-1;10;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Absolute;0;;2;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;122;2;34;0
WireConnection;40;2;39;0
WireConnection;124;0;122;0
WireConnection;124;2;123;0
WireConnection;174;2;172;2
WireConnection;174;1;171;0
WireConnection;125;0;124;0
WireConnection;173;2;172;1
WireConnection;173;1;171;0
WireConnection;41;0;40;0
WireConnection;41;2;77;0
WireConnection;25;0;39;0
WireConnection;25;1;41;0
WireConnection;5;0;34;0
WireConnection;5;1;125;0
WireConnection;176;0;174;0
WireConnection;58;2;56;1
WireConnection;58;1;55;0
WireConnection;63;2;56;2
WireConnection;63;1;55;0
WireConnection;175;0;173;0
WireConnection;179;0;175;0
WireConnection;179;1;177;0
WireConnection;157;2;168;1
WireConnection;157;1;169;1
WireConnection;158;2;168;2
WireConnection;158;1;169;2
WireConnection;29;0;25;0
WireConnection;29;1;30;0
WireConnection;181;0;176;0
WireConnection;181;1;178;0
WireConnection;35;0;5;0
WireConnection;35;1;36;0
WireConnection;126;0;127;1
WireConnection;126;1;127;2
WireConnection;60;0;58;0
WireConnection;66;0;63;0
WireConnection;61;0;60;0
WireConnection;61;1;59;0
WireConnection;161;0;157;0
WireConnection;150;2;149;0
WireConnection;183;2;180;0
WireConnection;14;0;35;0
WireConnection;14;1;29;0
WireConnection;14;2;126;0
WireConnection;64;0;66;0
WireConnection;64;1;65;0
WireConnection;182;0;179;0
WireConnection;182;1;181;0
WireConnection;160;0;158;0
WireConnection;165;0;150;0
WireConnection;165;1;163;0
WireConnection;165;2;167;0
WireConnection;67;2;68;0
WireConnection;184;0;183;0
WireConnection;184;1;182;0
WireConnection;184;2;14;0
WireConnection;69;0;61;0
WireConnection;69;1;64;0
WireConnection;164;0;161;0
WireConnection;164;1;160;0
WireConnection;166;0;165;0
WireConnection;166;1;164;0
WireConnection;53;0;67;0
WireConnection;53;1;69;0
WireConnection;53;2;14;0
WireConnection;185;0;180;0
WireConnection;185;1;184;0
WireConnection;148;0;149;0
WireConnection;148;1;166;0
WireConnection;1;0;68;0
WireConnection;1;1;53;0
WireConnection;132;0;185;0
WireConnection;132;1;131;0
WireConnection;45;0;1;0
WireConnection;45;1;47;0
WireConnection;45;2;76;0
WireConnection;45;3;186;0
WireConnection;134;0;132;0
WireConnection;134;1;148;0
WireConnection;134;2;186;4
WireConnection;0;0;45;0
WireConnection;0;2;45;0
WireConnection;0;9;134;0
ASEEND*/
//CHKSM=B01D80E3533E4B50E4B0AD1DBB42D9513FB024AF