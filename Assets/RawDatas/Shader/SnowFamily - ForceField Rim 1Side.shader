Shader "Snow Family/ForceField Rim 1Side" {
Properties {
	_Color ("Main Color", Color) = (1,1,1,1)	
	_Shininess ("Shininess", Range (0.01, 1)) = 0.078125
	_Parallax ("Distortion", Range (0.0, 1.0)) = 0.02
	_MainTex ("Base (RGB) Gloss (A)", 2D) = "white" {}		
	_Movement("Movement",Range(0,5)) = 1	
}
SubShader { 
	Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
	LOD 100

CGPROGRAM
#pragma surface surf BlinnPhong alpha

sampler2D _MainTex;
fixed4 _Color;
float _Parallax;
float _Movement;
half _Shininess;


struct Input {
	float2 uv_MainTex;
};

void surf(Input IN, inout SurfaceOutput o) {
	half h = tex2D (_MainTex, IN.uv_MainTex+_Time[0]*_Movement).w;
	IN.uv_MainTex = h*_Parallax;
	
	fixed4 tex = tex2D(_MainTex, IN.uv_MainTex);	
	o.Albedo = tex.rgb * _Color.rgb;	
	o.Alpha = tex.a * _Color.a;	
	
	o.Gloss = tex.a;	
	o.Specular = _Shininess;
	o.Emission = o.Alpha*_Color;
}
ENDCG
}

}
