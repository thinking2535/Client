﻿//Wasd Studio S.A. de C.V. 2014
Shader "Snow Family/Specular/Rim Light"
{
	Properties 
	{
		_MainColor ("Diffuse", Color) = (0.5,0.5,0.5,1)
		_ColorDamage ("Damage Color", Color) = (0,0,0,1)
		//_SpecColor ("Specular", Color) = (1,1,1,1)
		_Shininess("Shininess", Float) = 0.02
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_SpecularMask ("Specular Mask", 2D) = "mask" {}
		
		_RimColor ("Rim Color", Color) = (0.8,0.7,0.6,0.6)
        _RimPower ("Rim Power", Float) = 4
	}
	
	SubShader 
	{
		Tags { "RenderType"="Opaque" }				
            CGPROGRAM
            //#pragma surface surf ToonRamp
            //#pragma lighting ToonRamp exclude_path:prepass
            #pragma surface surf BlinnPhong noforwardadd novertexlights approxview exclude_path:prepass
		
		sampler2D _MainTex;
   		sampler2D _SpecularMask;
		fixed4 _MainColor;
		fixed4 _ColorDamage;
		fixed4 _RimColor;
		fixed _RimPower;
   		fixed _Shininess;


		
		

		
		struct Input
		{
			half2 uv_MainTex;
			half2 uv_SpecularMask;
			half3 pos : POSITION0;
			half3 viewDir;
		};
		//
		void surf (Input IN, inout SurfaceOutput o)
		{
			//half4 c = tex2D(_MainTex, IN.uv_MainTex);
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex); //메인 텍스쳐에 메인컬러를 씌우고 콤하기2로 어둠과 밝음이 표현되게 한다.  (콥하기2가 없으면 무조건 어둡게 나옴)
			fixed4 s = tex2D (_SpecularMask, IN.uv_SpecularMask); //스펙큘러 텍스쳐의 변수 선언 "c"
			
			_SpecColor = s; //스펙큘러맵의 색을 스펙큘러 색으로 사용하겠다.
			
			o.Albedo = (c.rgb * _MainColor * 2) + _ColorDamage.rgb;
			o.Alpha = c.a;
			
			o.Specular = _Shininess;
          	//o.Gloss = 5.0;
          	o.Gloss = s.rgb;
			//Rim Light
			fixed rim = 1.0f - saturate( dot(normalize(IN.viewDir), o.Normal) );
			//o.Emission = (_RimColor.rgb * pow(rim, _RimPower)) * _RimColor.a;
			o.Emission = (_RimColor.rgb * pow(rim, _RimPower)) * _RimColor.a ;
		}
            

      	
      	
      	

            ENDCG
        
	} 
	FallBack "Diffuse"
}