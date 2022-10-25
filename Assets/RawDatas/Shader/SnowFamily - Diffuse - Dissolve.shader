//몬스터 사망시 교체될 쉐이더
Shader "Snow Family/ Diffuse - dissolve"
{
	Properties 
	{
		_MainColor ("Diffuse", Color) = (0.5,0.5,0.5,1)
		_MainTex ("Base (RGB)", 2D) = "white" {}

		_DissolvePower ("Dissolve Power", Range(0.6, -0.6)) = 0.6
		_DissolveEmissionColor ("Dissolve Emission Color", Color) = (0.82,0.7,0.58)
		_DissolveTex ("Dissolve Texture", 2D) = "white"{}
		
	}
	
	SubShader 
	{
    Tags {"IgnoreProjector"="True" "RenderType"="TransparentCutout"}	
     Cull Off


            CGPROGRAM

            #pragma surface surf BlinnPhong approxview novertexlights exclude_path:prepass exclude_path:deferred nolightmap noshadow nodirlightmap nometa alphatest:Zero noforwardadd noshadow

		
		
		sampler2D _MainTex;
		fixed4 _MainColor;
		int _UseState;
		sampler2D _DissolveTex;
    	float3 _DissolveEmissionColor;
    	fixed _DissolvePower;
		
		struct Input
		{
			half2 uv_MainTex;
			half2 uv_DissolveTex;
			half3 pos : POSITION0;
			half3 viewDir;
		};
		//
		void surf (Input IN, inout SurfaceOutput o)
		{
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) ; //메인텍스쳐의 변수 선언 "c"
			fixed4 texd = tex2D(_DissolveTex, IN.uv_DissolveTex);

			o.Albedo = (c.rgb * _MainColor * 2) ; //메인 텍스쳐에 메인컬러를 씌우고 콤하기2로 어둠과 밝음이 표현되게 한다.  (콥하기2가 없으면 무조건 어둡게 나옴)

			o.Alpha = _DissolvePower - texd.r;
     
			if ((o.Alpha < 0)&&(o.Alpha > -0.05)){
				o.Alpha = 1;
				o.Albedo = c.rgb + (_DissolveEmissionColor * 1);
				//o.Emission =  _DissolveEmissionColor;
				}
			

		}
            

      	
      	
      	

            ENDCG
        
	} 
	FallBack "Mobile/Particles/Alpha Blended"
}
