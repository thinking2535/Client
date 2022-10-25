// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

//ㅇㅇㅇ
Shader "Snow Family/FX/Unlit-AlphaBlend_Detail_Power - Distortion" {
Properties 
{
_TintColor("Tint Color", Color) = (0.5,0.5,0.5,0.5)
_TintPower("Tint Power", Float) = 1.0
 _MainTex("_MainTex", 2D) = "black" {}
 _OffsetTex("_Offset", 2D) = "black" {}
 _Distortion("_Distortion", Float ) = 0.1
 //_TurbulenceSpd("_TurbulenceSpd", Float) = -50  
 //_ScrollSpd("_ScrollSpd", Float) = 30
}
 
SubShader 
{
 Tags
 {
  "Queue"="Transparent"
  "IgnoreProjector"="True"
  "RenderType"="Transparent"
 }
  
 Cull Off
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
   
  fixed4 _MainTex_ST;
  fixed4 _OffsetTex_ST;
  fixed _TintPower;
  fixed _Distortion;
  //fixed _TurbulenceSpd;
  //fixed _ScrollSpd;
  sampler2D _MainTex;
  sampler2D _OffsetTex;
  fixed4 _TintColor;
   
   
			v2f vert (appdata_t v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.texcoord0 = TRANSFORM_TEX(v.texcoord0, _MainTex);
				o.texcoord1 = TRANSFORM_TEX(v.texcoord1, _OffsetTex);
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
   

   
  fixed4 frag(v2f i) : COLOR 

  {
   fixed4 offsetTexColor = tex2D(_OffsetTex, i.texcoord1);
   fixed lum = Luminance(offsetTexColor);
   fixed2 texCord = ( lum, lum );
    
   fixed4 mainTexColor = tex2D( _MainTex, i.texcoord0 + texCord * _Distortion ) * _TintColor * _TintPower;;

   UNITY_APPLY_FOG(i.fogCoord, mainTexColor);
   return mainTexColor;
  }
   
  ENDCG
 }
}
}