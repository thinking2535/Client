//Wasd Studio S.A. de C.V. 2014
Shader "Snow Family/Specular"
{
	Properties 
	{
		_MainColor ("Diffuse", Color) = (0.5,0.5,0.5,1)
		_SpecColor ("Specular", Color) = (1,1,1,1)
		_Shininess("Shininess", Range(0.1,10)) = 0.1
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_SpecularMask ("Specular Mask", 2D) = "mask" {}

	}
	
	SubShader 
	{
		Tags { "RenderType"="Opaque" }				
            CGPROGRAM
            #pragma surface surf BlinnPhong
      		struct Input 
      		{
          		float2 uv_MainTex;
          		float2 uv_SpecularMask;
      		};
      		
      		fixed4 _MainColor;
      		
      		fixed _Shininess;
      		sampler2D _MainTex;
      		sampler2D _SpecularMask;

      		
      		void surf (Input IN, inout SurfaceOutput o) 
      		{
          		half4 c = tex2D (_MainTex, IN.uv_MainTex) * _MainColor * 2;    
      			o.Albedo = c.rgb;
          		o.Alpha = c.a;
          		o.Specular = _Shininess ;
          		o.Gloss = tex2D (_SpecularMask, IN.uv_SpecularMask).rgb * 3;
	
      		}
            ENDCG
        
	} 
	FallBack "Diffuse"
}
