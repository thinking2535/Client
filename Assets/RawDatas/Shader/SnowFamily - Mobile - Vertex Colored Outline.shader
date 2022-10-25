// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Snow Family/Mobile/Vertex Colored Outline" {
Properties {
    _Color ("Main Color", Color) = (1,1,1,1) 
    _OutlineColor ("Outline Color", Color) = (1,0,0,1)
    _Outline ("Outline width", Range (.002, 0.03)) = .002
    _MainTex ("Base (RGB)", 2D) = "white" {}
}

CGINCLUDE
#include "UnityCG.cginc"
 
struct appdata {
    float4 vertex : POSITION;
    float3 normal : NORMAL;
};
 
struct v2f {
    float4 pos : POSITION;
    float4 color : COLOR;
};
 
uniform float _Outline;
uniform float4 _OutlineColor;
 
v2f vert(appdata v) {
    
    v2f o;
    
    o.pos = UnityObjectToClipPos(v.vertex);
    
    float3 norm   = mul ((float3x3)UNITY_MATRIX_IT_MV, v.normal);
    float2 offset = TransformViewToProjection(norm.xy);
 
    o.pos.xy += offset * o.pos.z * _Outline;
    o.color = _OutlineColor;
    return o;
}
ENDCG
 
SubShader {

CGPROGRAM
#pragma surface surf Lambert
 
sampler2D _MainTex;
fixed4 _Color;
struct Input {
    float2 uv_MainTex;
};
 
void surf (Input IN, inout SurfaceOutput o) {
    fixed4 tex = tex2D(_MainTex, IN.uv_MainTex);
    fixed4 c = tex * _Color;
    o.Albedo = c.rgb*3;	
    o.Alpha = c.a;
}
ENDCG 

    Pass {
       
        ColorMaterial AmbientAndDiffuse
        Lighting Off	
        Fog { Mode Off }
        
        SetTexture [_MainTex] {
            constantColor [_Color]
            Combine texture * constant DOUBLE
        } 
    }
    
    Pass {
            Name "OUTLINE"
            Tags { "LightMode" = "Always" }
            Cull Front
            //ZWrite On
        //	ColorMask RGB
            //Blend SrcAlpha OneMinusSrcAlpha
        //	Offset -50,-50
            Blend One OneMinusDstColor
 
            CGPROGRAM	
            #pragma vertex vert				
            #pragma fragment frag
            half4 frag(v2f i) :COLOR { return i.color; }
            ENDCG
        }	
}
 
Fallback "Mobile/VertexLit"
}