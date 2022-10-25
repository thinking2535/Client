Shader "Snow Family/Diffuse bumped" {
	Properties{
		_Color("Base Color", Color) = (1,1,1,1)
		_SpecularColor("Specular Color", Color) = (1,1,1,1)
		_Shininess("Shininess", Range(0.03, 1)) = 0.078125

		_MainTex("Base (RGB) Gloss (A)", 2D) = "white" {}
		[NoScaleOffset] _BumpMap("Normalmap", 2D) = "bump" {}
		
	}
	
	SubShader{
		Tags{ "RenderType" = "Opaque" }

		LOD 150

		CGPROGRAM
		#pragma surface surf MobileBlinnPhongBumped exclude_path:prepass nolightmap noforwardadd interpolateview //halfasview //novertexlights  
		
		half4 _Color;
		float4 _SpecularColor;
		half _Shininess;

		sampler2D _MainTex;
		sampler2D _BumpMap;
						
		struct Input {
			float2 uv_MainTex;
		};

		inline fixed4 LightingMobileBlinnPhongBumped(SurfaceOutput s, fixed3 lightDir, fixed3 halfDir, fixed atten)
		{
			fixed3 ld = fixed3(-1.0,-1.0,-1.0);
			fixed diff = max(0, dot(s.Normal, ld));
			fixed nh = max(0, dot(s.Normal, halfDir));
			fixed spec = pow(nh, s.Specular * 128) * s.Gloss;

			fixed4 c;
			c.rgb = (s.Albedo * _LightColor0.rgb * diff + _SpecularColor.rgb * spec) * (atten * 2);
			UNITY_OPAQUE_ALPHA(c.a);
			return c;
		}		

		void surf(Input IN, inout SurfaceOutput o) {
			
			half4 td = tex2D(_MainTex, IN.uv_MainTex);
			half4 tb = tex2D(_BumpMap, IN.uv_MainTex);
						
			o.Albedo = td.rgb *_Color.rgb;
			o.Gloss = td.a;
			o.Alpha = td.a;			
			o.Specular = _Shininess;
			o.Normal = UnpackNormal(normalize(tb));			
		}
	ENDCG
	}
	Fallback "Mobile/DiffuseBumped"
}