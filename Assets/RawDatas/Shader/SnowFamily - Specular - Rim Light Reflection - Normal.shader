//캐릭터 쉐이더

Shader "Snow Family/Specular/Rim Light - Reflection - Normal"
{
        Properties {
		_MainColor ("Diffuse", Color) = (0.5,0.5,0.5,1)
		_ColorDamage ("Damage Color", Color) = (0,0,0,1)
		_SpecPower ("SpecularPower", Float) = 1
		_Shininess("Shininess", Float) = 10
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_SpecularMask ("Specular Mask(R)SpecMask(G)RefMask(B)None", 2D) = "mask" {}	//R채널을 스펙큘러 마스크맵으로 사용 G채널을 리플렉션 마스크맵으로 사용 B채널은 남겨둠
		_Reflection ("Reflection", CUBE) = "ref" {}
		_ReflectionPower("ReflectionPower", Float) = 1
		
		_RimColor ("Rim Color", Color) = (0.8,0.7,0.6,0.6)
        _RimPower ("Rim Power", Float) = 2
        _RimRange ("Rim Range", Float) = 6

        _BumpMap ("Bumpmap", 2D) = "bump" {}


        }
        SubShader {
        Tags { "RenderType" = "Opaque" }
        CGPROGRAM
          #pragma surface surf MobileBlinnPhong approxview exclude_path:prepass exclude_path:deferred noforwardadd

        sampler2D _MainTex;
   		sampler2D _SpecularMask;
   		sampler2D _BumpMap;
		fixed4 _MainColor;
		fixed4 _ColorDamage;
		fixed4 _RimColor;
		fixed _SpecPower;
		fixed _RimPower;
		fixed _RimRange;
		fixed _ReflectionPower;
   		fixed _Shininess;
		samplerCUBE _Reflection;

		struct Input
		{
			half2 uv_MainTex;
			half2 uv_SpecularMask;
			//half2 uv_BumpMap;
			half3 pos : POSITION0;
			half3 viewDir;
			half3 worldRefl;
			//INTERNAL_DATA
		};


    inline fixed4 LightingMobileBlinnPhong (SurfaceOutput s, fixed3 lightDir,  fixed3 viewDir, fixed atten) 
    {
    	fixed3 halfDir = normalize (lightDir + viewDir);
        fixed NdotL = dot (s.Normal, lightDir);
        fixed NdotV = max(0, dot (s.Normal, viewDir));
        fixed3 NdotH = max(0, dot (s.Normal, halfDir));

        fixed diff = NdotH * 0.5 + 0.5 ;

        half rimLight1 = 1 - saturate( NdotV );

        half spec = pow(NdotV, _Shininess);

		half3 specCol = spec * s.Gloss;

        half4 c;
        //c.rgb = s.Albedo * _LightColor0.rgb * (diff * atten) + (_LightColor0.rgb * specCol * _SpecPower);

        c.rgb = 
		(s.Albedo * _LightColor0.rgb * 1.0) * (diff * atten)
		+ ((((_LightColor0.rgb * 3) * (_RimColor * 1) * (s.Albedo * 2)) / 3) * _RimPower * pow(rimLight1, _RimRange))
		+ (_LightColor0.rgb * specCol * _SpecPower);

		c.a = 0;	// s.Alpha;
        return c;
    }



        void surf (Input IN, inout SurfaceOutput o)
		{
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) ; //메인텍스쳐의 변수 선언 "c"
			fixed4 s = tex2D (_SpecularMask, IN.uv_MainTex); //스펙큘러 텍스쳐의 변수 선언 "c"
			fixed4 ref = texCUBE (_Reflection, IN.worldRefl);

			//fixed rim = saturate(dot (normalize(IN.viewDir), o.Normal));

			o.Albedo = (c.rgb * _MainColor * 2) + _ColorDamage.rgb ; //메인 텍스쳐에 메인컬러를 씌우고 콤하기2로 어둠과 밝음이 표현되게 한다.  (콥하기2가 없으면 무조건 어둡게 나옴)

			o.Alpha = c.a;

			
			o.Gloss = s.r; //스펙큘러 마스크맵의 R채널을 마스크로 사용

		//	o.Emission = (s.g * ref.rgb * _ReflectionPower) * _LightColor0.rgb; // 리플렉션 효과
			o.Normal = UnpackNormal (tex2D(_BumpMap, IN.uv_MainTex))  ;

		}
    ENDCG
        }
        Fallback "Diffuse"
    }