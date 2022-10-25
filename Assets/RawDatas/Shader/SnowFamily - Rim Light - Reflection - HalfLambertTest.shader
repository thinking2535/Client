	Shader "Snow Family/Specular/Rim Light - Reflection - HalfLambertTest"
    {
        Properties {
		_MainColor ("Diffuse", Color) = (0.5,0.5,0.5,1)
		_ColorDamage ("Damage Color", Color) = (0,0,0,1)
		_SpecPower ("SpecularPower", Float) = 1
		_Shininess("Shininess", Float) = 0.02
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_SpecularMask ("Specular Mask", 2D) = "mask" {}
		_Reflection ("Reflection", CUBE) = "" { TexGen CubeReflect }
		
		_RimColor ("Rim Color", Color) = (0.8,0.7,0.6,0.6)
        _RimPower ("Rim Power", Float) = 4
        }
        SubShader {
        Tags { "RenderType" = "Opaque" }
        CGPROGRAM
          #pragma surface surf WrapLambert approxview exclude_path:prepass exclude_path:deferred noforwardadd

          		sampler2D _MainTex;
   		sampler2D _SpecularMask;
		fixed4 _MainColor;
		fixed4 _ColorDamage;
		fixed4 _RimColor;
		fixed _SpecPower;
		fixed _RimPower;
   		fixed _Shininess;
		samplerCUBE _Reflection;

				struct Input
		{
			half2 uv_MainTex;
			half2 uv_SpecularMask;
			half3 pos : POSITION0;
			half3 viewDir;
			half3 worldRefl;
			//INTERNAL_DATA
		};


     fixed4 LightingWrapLambert (SurfaceOutput s, fixed3 lightDir,  fixed3 viewDir, fixed atten) 
    {
    	fixed3 halfDir = normalize (lightDir + viewDir);
        fixed NdotL = dot (s.Normal, lightDir);


        fixed diff = NdotL * 0.5 + 0.5;


        fixed3 NdotH = max(0, dot (s.Normal, halfDir));

        half spec = pow(NdotH, _Shininess);

		half3 specCol = spec * s.Gloss;

		//half3 speccolor = tex2D (_SpecularMask, fixed2(diff)).rgb;

        half4 c;
        c.rgb = s.Albedo * _LightColor0.rgb * (diff * atten) + (_LightColor0.rgb * specCol * _SpecPower);
        c.a = s.Alpha;
        return c;
    }



		

    
   // sampler2D _MainTex;
        void surf (Input IN, inout SurfaceOutput o)
		{
			//half4 c = tex2D(_MainTex, IN.uv_MainTex);
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) ; //메인텍스쳐의 변수 선언 "c"
			fixed4 s = tex2D (_SpecularMask, IN.uv_SpecularMask); //스펙큘러 텍스쳐의 변수 선언 "c"
			fixed4 ref = texCUBE (_Reflection, IN.worldRefl);
			
			//_SpecColor = ref * s; //스펙큘러맵의 색을 스펙큘러 색으로 사용하겠다.

			fixed rim = saturate(dot (normalize(IN.viewDir), o.Normal));

			//fixed dotPower = 1.0 + rim;	//max(0.0, (rim - 0.3));

			//_SpecColor = (ref * 1.25) * s * dotPower; //스펙큘러맵의 색을 스펙큘러 색으로 사용하겠다.
		
			//o.Albedo = (c.rgb * _MainColor * 2 * dotPower) + _ColorDamage.rgb ; //메인 텍스쳐에 메인컬러를 씌우고 콤하기2로 어둠과 밝음이 표현되게 한다.  (콥하기2가 없으면 무조건 어둡게 나옴)
			o.Albedo = (c.rgb * _MainColor * 2) + _ColorDamage.rgb ; //메인 텍스쳐에 메인컬러를 씌우고 콤하기2로 어둠과 밝음이 표현되게 한다.  (콥하기2가 없으면 무조건 어둡게 나옴)
			o.Alpha = c.a;
			
			o.Gloss = s.rgb * ref.rgb;
			//o.Specular = _Shininess ;

			//Rim Light
			//fixed rim = 1.0f - saturate( dot(normalize(IN.viewDir), o.Normal) );
			o.Emission = ((_RimColor.rgb * 1) * pow((1.0 - rim), _RimPower)) * _RimColor.a ;


		}
    ENDCG
        }
        Fallback "Diffuse"
    }