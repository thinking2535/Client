Shader "Snow Family/Mobile/Vertex Colored" {
Properties {
    _Color ("Main Color", Color) = (1,1,1,1)  
    _MainTex ("Base (RGB)", 2D) = "white" {}
}
 
SubShader {
    Pass {     
        ColorMaterial AmbientAndDiffuse
        Lighting Off		
        Fog { Mode Off }
       
        SetTexture [_MainTex] {
            constantColor [_Color]
            Combine texture * constant DOUBLE          
         } 
    }
}
 
Fallback "Mobile/VertexLit"
}