Shader "Mobile/Vertex Colored Glow_Alpha" {
Properties {
    _Color ("Main Color", Color) = (1,1,1,1)
    //_SpecColor ("Spec Color", Color) = (1,1,1,1)
    //_Emission ("Emmisive Color", Color) = (0,0,0,0)
    _Shininess ("Shininess", Range (0.01, 1)) = 0.7
    _MainTex ("Base (RGB), Alpha (A) ", 2D) = "white" {}
    _GlowTex ("Glow (RGB) ", 2D) = "white" {}
}

  
SubShader {
	Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }
	Blend SrcAlpha OneMinusSrcAlpha
	AlphaTest Greater .01
    Pass {
        Material {
            Shininess [_Shininess]
            Specular [_SpecColor]
            Emission [_Emission]    
        }
        ColorMaterial AmbientAndDiffuse
        Lighting Off
		//Cull Off
        Fog { Mode Off }
        SetTexture [_MainTex] {
            Combine texture * primary, texture * primary
        }
        SetTexture [_MainTex] {
            constantColor [_Color]
            Combine previous * constant DOUBLE, previous * constant
        }
        SetTexture [_GlowTex] {
         combine previous + texture, previous 
         }
        
    }
}
 
Fallback " VertexLit", 1
}