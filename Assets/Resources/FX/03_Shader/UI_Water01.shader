// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Piyong/UI_Water"
{
	Properties
	{
		[PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
		_Color ("Tint", Color) = (1,1,1,1)
		[MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
		[PerRendererData] _AlphaTex ("External Alpha", 2D) = "white" {}
		_Water("Water", 2D) = "black" {}
		_Water_XY("Water_XY", Vector) = (0,-0.1,0,0)
		[NoScaleOffset][Normal]_DistortionNormalMap("Distortion Normal Map", 2D) = "bump" {}
		_DistortAmount("Distort Amount", Range( 0 , 1)) = 0
		[NoScaleOffset]_BlendFireMask("Blend Fire Mask", 2D) = "white" {}
		[HideInInspector] _texcoord( "", 2D ) = "white" {}

	}

	SubShader
	{
		LOD 0

		Tags { "Queue"="Geometry" "IgnoreProjector"="True" "RenderType"="Transparent" "PreviewType"="Plane" "CanUseSpriteAtlas"="True" }

		Cull Off
		Lighting Off
		ZWrite Off
		Blend One One
		
		
		Pass
		{
		CGPROGRAM
			
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0
			#pragma multi_compile _ PIXELSNAP_ON
			#pragma multi_compile _ ETC1_EXTERNAL_ALPHA
			#include "UnityCG.cginc"
			#include "UnityStandardUtils.cginc"
			#include "UnityShaderVariables.cginc"


			struct appdata_t
			{
				float4 vertex   : POSITION;
				float4 color    : COLOR;
				float2 texcoord : TEXCOORD0;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				
			};

			struct v2f
			{
				float4 vertex   : SV_POSITION;
				fixed4 color    : COLOR;
				float2 texcoord  : TEXCOORD0;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
				
			};
			
			uniform fixed4 _Color;
			uniform float _EnableExternalAlpha;
			uniform sampler2D _MainTex;
			uniform sampler2D _AlphaTex;
			uniform float4 _MainTex_ST;
			uniform sampler2D _Water;
			uniform sampler2D _DistortionNormalMap;
			uniform float4 _Water_ST;
			uniform float _DistortAmount;
			uniform float2 _Water_XY;
			uniform sampler2D _BlendFireMask;

			
			v2f vert( appdata_t IN  )
			{
				v2f OUT;
				UNITY_SETUP_INSTANCE_ID(IN);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(OUT);
				UNITY_TRANSFER_INSTANCE_ID(IN, OUT);
				
				
				IN.vertex.xyz +=  float3(0,0,0) ; 
				OUT.vertex = UnityObjectToClipPos(IN.vertex);
				OUT.texcoord = IN.texcoord;
				OUT.color = IN.color * _Color;
				#ifdef PIXELSNAP_ON
				OUT.vertex = UnityPixelSnap (OUT.vertex);
				#endif

				return OUT;
			}

			fixed4 SampleSpriteTexture (float2 uv)
			{
				fixed4 color = tex2D (_MainTex, uv);

#if ETC1_EXTERNAL_ALPHA
				// get the color from an external texture (usecase: Alpha support for ETC1 on android)
				fixed4 alpha = tex2D (_AlphaTex, uv);
				color.a = lerp (color.a, alpha.r, _EnableExternalAlpha);
#endif //ETC1_EXTERNAL_ALPHA

				return color;
			}
			
			fixed4 frag(v2f IN  ) : SV_Target
			{
				float2 uv_MainTex = IN.texcoord.xy * _MainTex_ST.xy + _MainTex_ST.zw;
				float4 temp_output_192_0_g587 = ( tex2D( _MainTex, uv_MainTex ) * _Color );
				float2 uv_Water = IN.texcoord.xy * _Water_ST.xy + _Water_ST.zw;
				float2 MainUvs222_g587 = uv_Water;
				float4 tex2DNode65_g587 = tex2D( _DistortionNormalMap, MainUvs222_g587 );
				float4 appendResult82_g587 = (float4(0.0 , tex2DNode65_g587.g , 0.0 , tex2DNode65_g587.r));
				float2 temp_output_84_0_g587 = (UnpackScaleNormal( appendResult82_g587, _DistortAmount )).xy;
				float2 panner179_g587 = ( 1.0 * _Time.y * _Water_XY + MainUvs222_g587);
				float2 temp_output_71_0_g587 = ( temp_output_84_0_g587 + panner179_g587 );
				float4 tex2DNode96_g587 = tex2D( _Water, temp_output_71_0_g587 );
				float2 uv_BlendFireMask232_g587 = IN.texcoord.xy;
				
				fixed4 c = ( temp_output_192_0_g587 + ( ( tex2DNode96_g587 * tex2DNode96_g587.a * tex2D( _BlendFireMask, uv_BlendFireMask232_g587 ).g ) * (temp_output_192_0_g587).a ) );
				c.rgb *= c.a;
				return c;
			}
		ENDCG
		}
	}
	CustomEditor "ASEMaterialInspector"
	
	
}
/*ASEBEGIN
Version=18707
1920;0;1920;1139;3614.904;2583.294;3.617521;True;True
Node;AmplifyShaderEditor.CommentaryNode;789;-1827.163,-696.9235;Inherit;False;849.0742;480.2428;Comment;4;795;792;791;790;Base Sprite;1,1,1,1;0;0
Node;AmplifyShaderEditor.TemplateShaderPropertyNode;790;-1799.685,-641.1445;Inherit;True;0;0;_MainTex;Shader;False;0;5;SAMPLER2D;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.CommentaryNode;793;-600.5656,-802.1516;Inherit;False;1023.074;537.6009;Comment;5;807;801;800;797;798;Layer 1 - Fire Distortion;1,1,1,1;0;0
Node;AmplifyShaderEditor.SamplerNode;792;-1545.685,-646.1445;Inherit;True;Property;_TextureSample4;Texture Sample 4;26;0;Create;True;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TemplateShaderPropertyNode;791;-1409.685,-449.1435;Inherit;False;0;0;_Color;Shader;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;795;-1175.091,-641.2786;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.Vector2Node;800;-261.5659,-650.1514;Float;False;Property;_Water_XY;Water_XY;8;0;Create;True;0;0;False;0;False;0,-0.1;0.05,0.05;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.RangedFloatNode;801;-247.5659,-347.1519;Float;False;Property;_DistortAmount;Distort Amount;10;0;Create;True;0;0;False;0;False;0;0.55;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.TexturePropertyNode;797;-593.5656,-686.1514;Float;True;Property;_Water;Water;7;0;Create;True;0;0;False;0;False;None;c3f3360fcc462a24fae7844e60a3b5a3;False;black;Auto;Texture2D;-1;0;2;SAMPLER2D;0;SAMPLERSTATE;1
Node;AmplifyShaderEditor.TexturePropertyNode;798;-579.5657,-477.1515;Float;True;Property;_DistortionNormalMap;Distortion Normal Map;9;2;[NoScaleOffset];[Normal];Create;True;0;0;False;0;False;None;52b354854c8d8754eaed900166fc5b3d;True;bump;Auto;Texture2D;-1;0;2;SAMPLER2D;0;SAMPLERSTATE;1
Node;AmplifyShaderEditor.TexturePropertyNode;796;-314.383,-42.81065;Float;True;Property;_BlendFireMask;Blend Fire Mask;11;1;[NoScaleOffset];Create;True;0;0;False;0;False;None;1525699fd69a3904d99b80871f8c61c7;False;white;Auto;Texture2D;-1;0;2;SAMPLER2D;0;SAMPLERSTATE;1
Node;AmplifyShaderEditor.FunctionNode;807;119.4321,-738.1515;Inherit;False;UI-Sprite Effect Layer;0;;587;789bf62641c5cfe4ab7126850acc22b8;18,74,0,204,0,191,0,225,0,242,0,237,0,249,0,186,0,177,1,182,0,229,1,92,1,98,1,234,0,126,0,129,0,130,0,31,2;18;192;COLOR;1,1,1,1;False;39;COLOR;1,1,1,1;False;37;SAMPLER2D;;False;218;FLOAT2;0,0;False;239;FLOAT2;0,0;False;181;FLOAT2;0,0;False;75;SAMPLER2D;;False;80;FLOAT;1;False;183;FLOAT2;0,0;False;188;SAMPLER2D;;False;33;SAMPLER2D;;False;248;FLOAT2;0,0;False;233;SAMPLER2D;;False;101;SAMPLER2D;;False;57;FLOAT4;0,0,0,0;False;40;FLOAT;0;False;231;FLOAT;1;False;30;FLOAT;1;False;2;COLOR;0;FLOAT2;172
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;788;720,-928;Float;False;True;-1;2;ASEMaterialInspector;0;6;Piyong/UI_Water;0f8ba0101102bb14ebf021ddadce9b49;True;SubShader 0 Pass 0;0;0;SubShader 0 Pass 0;2;True;4;1;False;-1;1;False;-1;0;1;False;-1;0;False;-1;False;False;False;False;False;False;False;False;True;2;False;-1;False;False;False;False;False;True;2;False;-1;False;False;True;5;Queue=Geometry=Queue=0;IgnoreProjector=True;RenderType=Transparent=RenderType;PreviewType=Plane;CanUseSpriteAtlas=True;False;0;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;2;0;;0;0;Standard;0;0;1;True;False;;False;0
WireConnection;792;0;790;0
WireConnection;795;0;792;0
WireConnection;795;1;791;0
WireConnection;807;192;795;0
WireConnection;807;37;797;0
WireConnection;807;181;800;0
WireConnection;807;75;798;0
WireConnection;807;80;801;0
WireConnection;807;233;796;0
WireConnection;788;0;807;0
ASEEND*/
//CHKSM=E82AD5A9E854805DFC46947B6D6AB814A8256AD1