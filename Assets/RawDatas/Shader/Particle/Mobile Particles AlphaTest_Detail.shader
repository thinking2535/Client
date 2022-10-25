Shader "Snow Family/Particle/Alpha Test Detailt" {
Properties {
	_TintColor ("Tint Color", Color) = (0.5,0.5,0.5,0.5)
	_MainTex ("Particle Texture", 2D) = "white" {}
	_Detail ("Detail (RGB)", 2D) = "gray" {}
	_Cutoff ("Alpha cutoff", Range (0,1)) = 0.5
}

Category {
	Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }
	Blend SrcAlpha OneMinusSrcAlpha
	AlphaTest Greater .01
	ColorMask RGB
	Cull off Lighting Off ZWrite Off Fog { Mode Off }
	BindChannels {
		Bind "Color", color
		Bind "Vertex", vertex
		Bind "TexCoord", texcoord
	}
	
	// ---- Dual texture cards
	SubShader {
		Pass {
		AlphaTest Greater [_Cutoff]
			SetTexture [_MainTex] {
				constantColor [_TintColor]
				combine constant * previous DOUBLE
			}
			SetTexture [_MainTex] {
				combine texture * previous DOUBLE
			}
			SetTexture [_Detail] { 
				combine previous * texture
			}
		}
	}
	
	// ---- Single texture cards (does not do color tint)
	//SubShader {
		//Pass {
			//SetTexture [_MainTex] {
				//combine texture * primary
			//}
		//}
	//}
}
}
