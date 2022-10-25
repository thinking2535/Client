Shader "Snow Family/Mobile/Vertex Colored Glow - Rim Light"
{
	Properties
	{
		_Color ("Main Color", Color) = (1,1,1,1)
		
		_MainTex ("Texture", 2D) = "white" {}
		_GlowTex ("Glow (RGB)", 2D) = "white" {}
		
		_RimColor ("Rim Color", Color) = (0.9,0.78,0.7,0.6)
        _RimPower ("Rim Power", Float) = 3.5
    }
    
    SubShader
    {
    	Tags
      	{
      		"RenderType" = "Opaque"
      	}
      	   
		CGPROGRAM
		
		#pragma surface surf RimLightAndGlow
		//#pragma surface surf Lambert
		
		struct Input
		{
			float2 uv_MainTex : TEXCOORD0;
			float2 uv_GlowTex : TEXCOORD1;
			float3 pos : POSITION0;
			float3 viewDir;
		};
		
		sampler2D _MainTex;
		sampler2D _GlowTex;
		float4 _Color;
		float4 _RimColor;
		float _RimPower;
		
		#pragma lighting RimLightAndGlow exclude_path:prepass
		//#pragma lighting Lambert
		
		inline half4 LightingRimLightAndGlow(SurfaceOutput s, half3 lightDir, half atten)
		{
			half4 c;
			c.rgb = s.Albedo * (atten * 2.0);
			c.a = 0.0;
			
			return c;
		}
		
		//------------------------------------------------------------------------------
		// 수정 - 2014. 03. 19 조수운
		// : Glow 텍스트가 같이 나오도록 하기 위해서, SurfaceOutput.Albedo에 첫번째와
		// 두번째 텍스처의 색상값을 모두 더한다.
		//  정확하게 맞는 방법인지는 모르겠지만, 일단 외곽선 광원(Rim Light)과 글로우
		// (Glow) 효과가 같이 보이는 것 같긴 하다.
		//------------------------------------------------------------------------------
		void surf(Input IN, inout SurfaceOutput o)
		{
			half4 MainTexture = tex2D(_MainTex, IN.uv_MainTex);
			half4 GlowTexture = tex2D(_GlowTex, IN.uv_GlowTex);
			
			o.Albedo = (MainTexture.rgb + GlowTexture.rgb) * _Color.rgb;
			
			// 2014. 03. 19 조수운
			// : 이 Shader의 Glow 용 텍스처는 투명도 값이 없다. 주 텍스처의 투명도 값을 그냥 사용하면 됨
			o.Alpha = MainTexture.a;
			
			half rim = 1.0 - saturate(dot (normalize(IN.viewDir), o.Normal));
			o.Emission = _RimColor.rgb * pow(rim, _RimPower) * _RimColor.a;
		}
		
		ENDCG
    }
    
    Fallback "Diffuse"
    //Fallback "Toon/Lighted"
}
