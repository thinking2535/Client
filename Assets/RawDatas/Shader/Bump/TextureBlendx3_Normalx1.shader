Shader "VPaint/Lit/Blend 3 Bumped Texture 1" {
	Properties{
		_Color("Base Color", Color) = (1,1,1,1)
		_SpecularColor("Specular Color", Color) = (1,1,1,1)
		_Shininess("Shininess", Range(0.03, 10)) = 0.078125
				
		_Texture1("Texture 1", 2D) = "white" {}
		_Texture2("Texture 2", 2D) = "white" {}
		_Texture3("Texture 3", 2D) = "white" {}
		[NoScaleOffset] _TextureBump("Texture Bump", 2D) = "bump" {}
		
		_UseState("Use State", Int) = 0
		_MatrixColor("MatrixColor", Color) = (0.65,0.65,0.65,0.65)
		_MatrixPower("MatrixPower", float) = 0
	}
		SubShader{
		Tags{ "RenderType" = "Opaque" }

		LOD 250


		CGPROGRAM
		#pragma surface surf MobileBlinnPhong exclude_path:prepass noforwardadd halfasview //interpolateview
		//#pragma target 3.0

		half4 _Color;
		float4 _SpecularColor;
		half _Shininess;
		
		sampler2D _Texture1;
		sampler2D _Texture2;
		sampler2D _Texture3;
		sampler2D _TextureBump;

		int _UseState;
		fixed4 _MatrixColor;
		float _MatrixPower;

		struct Input {
			half2 uv_Texture1;
			half2 uv_Texture2;
			half2 uv_Texture3;
			float4 color : COLOR;
		};
		
		// Global illumination¿ë ½¦ÀÌµù ¿¬»ê.
		inline void LightingMobileBlinnPhong_GI(SurfaceOutput s, UnityGIInput data, inout UnityGI gi)
		{
			gi = UnityGlobalIllumination(data, 1.0, s.Normal);
		}

		inline fixed4 LightingMobileBlinnPhong(SurfaceOutput s, half3 viewDir, UnityGI gi)
		{				
			float3 ld = float3(0.75, -1.0, 0.75);
			half3 viewspacevert = mul(viewDir, (float3x3)UNITY_MATRIX_IT_MV);

			fixed lambertTerm = max(0, dot(ld,s.Normal));
			half3 refVector = reflect(viewspacevert, s.Normal);
			fixed specularcoeff = pow(max(0.0, dot(refVector, viewspacevert)), s.Specular);
			fixed diffspec = (lambertTerm + specularcoeff) * s.Gloss;

			fixed4 c;
			c.rgb = s.Albedo + _SpecularColor * diffspec;
						
			UNITY_OPAQUE_ALPHA(c.a);

			// °£Á¢±¤(½¦µµ¿ì¸Ê) Ãß°¡.
#ifdef UNITY_LIGHT_FUNCTION_APPLY_INDIRECT
			c.rgb *= gi.indirect.diffuse;
#endif

			return c;			
		}


		void surf(Input IN, inout SurfaceOutput o) {
			float4 color = IN.color;

			half4 t1 = tex2D(_Texture1, IN.uv_Texture1);
			half4 t2 = tex2D(_Texture2, IN.uv_Texture2);
			half4 t3 = tex2D(_Texture3, IN.uv_Texture3);
			half4 tb = tex2D(_TextureBump, IN.uv_Texture2);
						
			half4 cum = t1 * color.r + t2 * color.g + t3 * color.b;
			
			fixed fac = color.r + color.g + color.b;

			if (fac != 0)
			{
				cum /= fac;
			}
			cum = lerp(_Color, cum, fac);

			if (_UseState == 1)
			{
				cum.rgb *= _MatrixColor * _MatrixPower;	//0.65, 0.65, 0.65, 0.65; //¸Åµå¸¯½º ¿¬»ê
			}
						
			float glevel = t2.a * color.g;
			
			o.Albedo = cum.rgb *_Color.rgb;
			o.Gloss = glevel;
			o.Alpha = 1.0;
			o.Specular = _Shininess;
						
			o.Normal = lerp(half3(0, 1, 0), UnpackNormal(tb), fac);							
		}
	ENDCG
	}
	Fallback "Mobile/VertexLit"
}